import os
import warnings
import numpy as np
from astropy.io import fits
from PyQSOFit.Components import LineDef, Section
import xml.etree.ElementTree as Et
from typing import Tuple

warnings.filterwarnings("ignore")


class ComponentsXmlReader:
    def __init__(self, config_path):
        self.xmldata = Et.parse(config_path).getroot()
        self._config = None
        self.sections = []
        self.vdict = {"Free": "", "Fixed": ".", "Negative": "-", "Positive": "+"}

    @property
    def config(self):
        if self._config is None:
            self._config = self.Create_ConfigObj()
        return self._config

    @staticmethod
    def xmlstr_to_float(xml) -> float:
        return xml.text if xml.get('type') == 's' else float(xml.text)

    def skew_dict(self, xml) -> Tuple:
        if xml.get('mode') == "Free":
            if self.xmlstr_to_float(xml) > 0:
                return -1 * self.xmlstr_to_float(xml), self.xmlstr_to_float(xml)
            else:
                return -10, 10
        elif xml.get('mode') == "Fixed":
            return (self.xmlstr_to_float(xml), )

    def Create_ConfigObj(self) -> np.array:
        for xsection in self.xmldata.findall('section'):
            self.sections.append(self.Build_Section(xsection))
        return np.concatenate([i.lines for i in self.sections])

    def Build_Section(self, xml) -> Section:
        xargs = {i.tag: self.xmlstr_to_float(i) for i in xml if i.get('type')}
        xobj = Section(**xargs)
        xlines = [self.Build_Line(xline) for xline in xml.findall('line')]
        xobj.add_lines(xlines)
        return xobj

    def Build_Line(self, xml) -> LineDef:
        xbaseargs = {i.tag: self.xmlstr_to_float(i) for i in xml if i.get('info')}
        xextraargs = {}
        if xml.find("default") is not None:
            if xml.find("default").text == "BEL":
                xextraargs["default_bel"] = True
            elif xml.find("default").text == "NEL":
                xextraargs["default_nel"] = True
        elif xml.find("profile_link") is not None:
            xextraargs["profile_link"] = f'{xml.find("profile_link").text}*1'
        else:
            xextraargs["fwhm"] = (self.xmlstr_to_float(xml.find("fwhm1")),
                                  self.xmlstr_to_float(xml.find("fwhm2")))
            xextraargs["voffset"] = self.xmlstr_to_float(xml.find("voffset"))
            xextraargs["vmode"] = self.vdict[xml.find("voffset").get("mode")]
            xextraargs["skew"] = self.skew_dict(xml.find("skew"))

        if xml.find("flux_link") is not None:
            xextraargs["flux_link"] = f'{xml.find("flux_link").text}*{xbaseargs["scale"]}'

        return LineDef(**xbaseargs, **xextraargs)


class ContiWindowTxtReader:
    def __init__(self, config_path):
        self.conti_data = np.genfromtxt(config_path, delimiter=",", skip_header=1)

    @property
    def config(self):
        tmp = [(i[0], i[1]) for i in self.conti_data]
        return np.rec.array(tmp, formats = 'float32, float32', names = 'min, max')


def conti_param_arrgen(FeScaleVal=0, FeScaleMin=0, FeScaleMax=1000,
                       FeFWHMVal=3000, FeFWHMMin=1200, FeFWHMMax=5000,
                       FeOffVal=0, FeOffMin=-0.01, FeOffMax=0.01,
                       PLScaleVal=0.001, PLScaleMin=0, PLScaleMax=100,
                       PLSlopeVal=0, PLSlopeMin=-5, PLSlopeMax=3):

    conti_priors = np.rec.array([
        ('Fe_uv_norm', FeScaleVal, FeScaleMin, FeScaleMax, 1),  # Normalization of the MgII Fe template [flux]
        ('Fe_uv_FWHM', FeFWHMVal, FeFWHMMin, FeFWHMMax, 1),  # FWHM of the MgII Fe template [AA]
        ('Fe_uv_shift', FeOffVal, FeOffMin, FeOffMax, 1),  # Wavelength shift of the MgII Fe template [lnlambda]
        ('Fe_op_norm', FeScaleVal, FeScaleMin, FeScaleMax, 1),  # Normalization of the Hbeta/Halpha Fe template [flux]
        ('Fe_op_FWHM', FeFWHMVal, FeFWHMMin, FeFWHMMax, 1),  # FWHM of the Hbeta/Halpha Fe template [AA]
        ('Fe_op_shift', FeOffVal, FeOffMin, FeOffMax, 1),  # Wavelength shift of the Hbeta/Halpha Fe template [lnlambda]
        ('PL_norm', PLScaleVal, PLScaleMin, PLScaleMax, 1),
        # Normalization of the power-law (PL) continuum f_lambda = (lambda/3000)^-alpha
        ('PL_slope', PLSlopeVal, PLSlopeMin, PLSlopeMax, 1),  # Slope of the power-law (PL) continuum
        ('Balmer_norm', 0.0, 0.0, 1000, 1),
        # Normalization of the Balmer continuum at < 3646 AA [flux] (Dietrich et al. 2002)
        ('Balmer_Te', 15000, 10000, 50000, 1),  # Te of the Balmer continuum at < 3646 AA [K?]
        ('Balmer_Tau', 0.5, 0.1, 2.0, 1),  # Tau of the Balmer continuum at < 3646 AA
        ('conti_a_0', 0.0, None, None, 1),  # 1st coefficient of the polynomial continuum
        ('conti_a_1', 0.0, None, None, 1),  # 2nd coefficient of the polynomial continuum
        ('conti_a_2', 0.0, None, None, 1),  # 3rd coefficient of the polynomial continuum
        # Note: The min/max bounds on the conti_a_0 coefficients are ignored by the code,
        # so they can be determined automatically for numerical stability.
    ],

        formats='a20,  float32, float32, float32, int32',
        names='parname, initial,   min,     max,     vary')

    return conti_priors

# create a header
hdr0 = fits.Header()
hdr0['Author'] = 'Mol the Hrafn'
primary_hdu = fits.PrimaryHDU(header=hdr0)

hdr3 = fits.Header()
hdr3['ini'] = 'Initial guess of line scale [flux]'
hdr3['min'] = 'FWHM of the MgII Fe template'
hdr3['max'] = 'Wavelength shift of the MgII Fe template'

hdr3['vary'] = 'Whether or not to vary the parameter (set to 0 to fix the continuum parameter to initial values)'


# path definitions to save the component definitions file
def write_file(config_path, conti_path, contiparams):
    save_path = config_path[:-3]+"py"
    newdata = ComponentsXmlReader(config_path).config
    hdu1 = fits.BinTableHDU(data=newdata, header=Section.hdu_generate(), name='line_priors')
    conti_windows = ContiWindowTxtReader(conti_path).config
    hdu2 = fits.BinTableHDU(data=conti_windows, name='conti_windows')

    conti_priors = conti_param_arrgen(**contiparams)
    hdu3 = fits.BinTableHDU(data=conti_priors, header=hdr3, name='conti_priors')
    hdu_list = fits.HDUList([primary_hdu, hdu1, hdu2, hdu3])
    hdu_list.writeto(save_path, overwrite=True)
    return save_path
