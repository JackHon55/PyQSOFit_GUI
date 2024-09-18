import numpy as np
import xml.etree.ElementTree as Et
from PyQSOFit.Components import LineDef, Section
from typing import Tuple


'''Hbeta_section = Section(section_name='Hbeta', start_range=4000, end_range=5500)
line_OIII5007c_1 = LineDef(l_name='OIII5007c_1', l_center=5006.84, scale=0.003, default_nel=True)
line_OIII4959c_2 = LineDef(l_name='OIII4959c_2', l_center=4958.91, scale=0.003, profile_link='OIII5007c_1*1', flux_link='OIII5007c_1*0.003')
Hbeta_section.add_lines([line_OIII5007c_1,line_OIII4959c_2])
newdata = np.concatenate([Hbeta_section.lines])'''

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
        # print(xml.text)
        return xml.text if xml.get('type') == 's' else float(xml.text)

    def skew_dict(self, xml) -> Tuple[float, float]:
        if xml.get('mode') == "Free":
            return -10, 10
        elif xml.get('mode') == "Fixed":
            return self.xmlstr_to_float(xml), 0

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