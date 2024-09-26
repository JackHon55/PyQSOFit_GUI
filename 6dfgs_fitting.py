import numpy as np
# import matplotlib

# matplotlib.use('agg')
import os

os.environ['TF_ENABLE_ONEDNN_OPTS']='0'
import matplotlib.pyplot as plt
from Spectra_handling.AllSpectrum import *
from Spectra_handling.Spectrum_utls import bpt_plotter, bpt_test, continuum_fitting
from PyQSOFit.PyQSOFit_SVL import *
import Component_definitions
from rebin_spec import rebin_spec
from scipy.signal import medfilt
from tqdm import tqdm

import warnings
import pickle

warnings.filterwarnings("ignore")

HA = 6562.819
NL = 6548.050
NR = 6583.460
SL = 6716.440
SR = 6730.810

E_LINES = np.asarray([NL, HA, NR])
S_LINES = np.asarray([SL, SR])

HB = 4861.333
OL = 4958.911
OR = 5006.843
B_LINES = np.asarray([HB, OL, OR])

MG = 2799.117

path1 = os.getcwd() + '/PyQSOFit/'
fig_path = os.getcwd() + '/fitting_plots/'


class SixDFGSFitter:
    def __init__(self, *, file_path: str, spec_name: str, z: float):
        self.file = file_path
        self.spec_name = spec_name
        self.z = z
        a = fits.open(self.file)
        self._input_wave = a[1].data['WAVE'].copy()
        self._input_flux = a[1].data['FLUX'].copy()
        self._output_wave = np.empty((len(self._input_wave),))
        self._output_flux = np.empty((len(self._input_flux),))
        self.q = None
        self.plot_file = f'{fig_path}{spec_name}.pkl'

    @property
    def input_spectrum(self):
        return np.array([self._input_wave, self._input_flux])

    @property
    def output_spectrum(self):
        if len(self._output_wave) == 0:
            self.reset_output_spectrum()
        return np.array([self._output_wave, self._output_flux])

    def clean_input_spec(self, nsmooth: float = 0.5) -> Tuple[np.array, np.array]:
        clean_bool = self._input_flux > 1e-5
        rawwave = self._input_wave[clean_bool]
        rawflux = self._input_flux[clean_bool]
        rawflux = g_filt(rawflux, nsmooth)
        return rawwave, rawflux

    @staticmethod
    def normalise_input_spec(flux: np.array) -> np.array:
        return flux / np.mean(flux)

    @staticmethod
    def blueshift_spec(wave: np.array, flux: np.array, z: float) -> Tuple[np.array, np.array]:
        spec_w, spec_f = blueshifting([wave, flux], z)
        return spec_w, spec_f

    def reset_output_spectrum(self):
        wave_tmp, flux_tmp = self.clean_input_spec()
        flux_tmp = self.normalise_input_spec(flux_tmp)
        self._output_wave, self._output_flux = self.blueshift_spec(wave_tmp, flux_tmp, self.z)
        # self._output_wave, self._output_flux = wave_tmp, flux_tmp

    def trim_spec(self, wave_range: Tuple, start_pixel_rm: int = 0, end_pixel_rm: int = 0):
        trim_bool = (self._output_wave > wave_range[0]) & (self._output_wave < wave_range[1])
        self._output_wave = self._output_wave[trim_bool]
        if end_pixel_rm == 0:
            end_pixel_rm = len(self._output_wave)
        self._output_wave = self._output_wave[start_pixel_rm:end_pixel_rm]
        self._output_flux = self._output_flux[trim_bool][start_pixel_rm:end_pixel_rm]

    def noise_remove(self, noise_start: float, noise_end: float):
        self._output_wave, self._output_flux = noise_to_linear([self._output_wave, self._output_flux],
                                                               [noise_start, noise_end])

    def flux_use_CFT(self, nsmooth: float = 100):
        self._output_flux -= continuum_fitting(self._output_wave, self._output_flux, smooth_val=nsmooth)

    @property
    def err(self):
        return np.ones_like(self._output_flux)

    def create_preview(self, w, h):
        fig = plt.figure(frameon=False)
        fig.set_size_inches(w/100, h/100)
        ax = plt.Axes(fig, [0., 0., 1., 1.])
        ax.set_axis_off()
        fig.add_axes(ax)
        ax.plot(*self.output_spectrum)
        ax.set_xlim((self._output_wave[0], self._output_wave[-1]))
        fig.savefig(os.getcwd() + "/fitting_plots/tmp.png")
        plt.close(fig)
        print("Preview Ready")


    @staticmethod
    def save_result(spec_id: str, results: list, save_properties: list = None, save_error: bool = False,
                    save_path: str = ""):
        """
        Call function to write to file or print.

        Parameters:

        spec_id: int
            6dfgs id for the row entry

        results: list
            A from pyqsofit line_result_output

        save_properties: list, optional
            If None, generates header with fwhm, sigma, skew, ew, peak, area
            A list of the six measured properties can be given to selectively save those values only

        save_error: bool
            If True, saves the measured values of lines. If False, saves the errors only.

        save_path: str, optional
            If provided, will create, if file does not exist, and append results.
        """
        r_index = 1 if save_error else 0
        tmp_results = [i[r_index] for i in results]
        if save_properties is None:
            save_properties = ['fwhm', 'sigma', 'skew', 'ew', 'peak', 'area']

        tmp_results = [[i[xkey] for xkey in save_properties if xkey in i] for i in tmp_results]

        to_save = np.round(tmp_results, 3).flatten()
        to_save = "\t".join(map(str, to_save))
        if save_path != "":
            with open(save_path, 'a') as save_file:
                save_file.seek(0, 2)

                if save_file.tell() == 0:
                    heading = "\t".join(map(str, save_properties * len(results)))
                    save_file.write(f'spec_id\t{heading}\n')

                save_file.write(f'{spec_id}\t{to_save}\n')
        print(f'RES:{spec_id}\t{to_save}\n')


    def fit(self, config_path, conti_path, contiparams: dict, fittingparams: dict):
        save_path = Component_definitions.write_file(config_path, conti_path, contiparams)

        self.q = QSOFit(*self.output_spectrum, self.err, z=self.z, path=path1, config=save_path)
        start = timeit.default_timer()
        self.q.Fit(name=self.spec_name, redshift=False, save_result=False, plot_fig=True, save_fig=True,
                   save_fig_path=fig_path + str(self.spec_name), save_fits_path=None, save_fits_name=None,
                   **fittingparams)

        plt.axvline(6885 / (1 + self.z))
        plt.axvline(5577 / (1 + self.z))

        with open(self.plot_file, 'wb') as f:
            pickle.dump(self.q.fig, f)

        plt.show()
        end = timeit.default_timer()
        print('Fitting finished in : ' + str(np.round(end - start)) + 's')

        a = self.q.line_result_output('Hb_br')
        b = self.q.line_result_output('Hb_na')
        c = self.q.line_result_output('OIII5007c')

        d = self.q.line_result_output('Ha_br')
        e = self.q.line_result_output('NII6549c')

        self.save_result(self.spec_name, [a, b, c, d, e], save_properties=['fwhm', 'skew', 'peak', 'area', 'ew'],
                         save_path=os.getcwd() + "/fitting_results.txt", save_error=False)

print("Ready to fit!")
'''a = SixDFGSFitter(file_path='test_spectra/0022878_1d.fits', spec_name='22878', z=0.06915)
a.reset_output_spectrum()
a.trim_spec((4000, 7000))
a.fit('fitting_configs/Default.xml', 'conti_configs/Default.txt', CFT=True)'''