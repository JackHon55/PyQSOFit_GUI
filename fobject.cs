using System.Collections.Generic;

namespace PyQSOFit_SBLg
{
    public class fobject
    {
        public string spec_path = "";
        public string spec_name = "";
        public float z = 0;
        public float trimA = 2500;
        public float trimB = 7000;
        public Dictionary<string, float> Dict_FitResult = new Dictionary<string, float>();

        public void fit(string config_path)
        {
            reset();
            Main.PythonInput.WriteLine($"spec_{spec_name}.fit('{config_path}')");
            Main.PythonInput.Flush();           
        }

        public void preview()
        {
            reset();
            Main.PythonInput.WriteLine($"spec_{spec_name}.create_preview()");
            Main.PythonInput.Flush();
        }

        public void plot()
        {
            Main.PythonInput.WriteLine($"with open(spec_{spec_name}.plot_file, 'rb') as f: fig = pickle.load(f)");
            Main.PythonInput.WriteLine("plt.figure(fig.number)");
            Main.PythonInput.WriteLine("plt.show()");
        }

        public void reset()
        {
            Main.PythonInput.WriteLine($"spec_{spec_name} = SixDFGSFitter(file_path='{spec_path}', spec_name='{spec_name}', z={z})");
            Main.PythonInput.WriteLine($"spec_{spec_name}.reset_output_spectrum()");
            Main.PythonInput.WriteLine($"spec_{spec_name}.trim_spec(({trimA}, {trimB}))");
        }
    }

    public class Pypath
    {
        public static string T(string xpath)
        {
            return xpath.Replace("\\", "/");
        }
    }
}
