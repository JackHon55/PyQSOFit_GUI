using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PyQSOFit_SBLg
{
    public class fobject
    {
        public string spec_path = "";
        public string spec_name = "";
        public float z = 0;
        public float trimA = 2500;
        public float trimB = 7000;
        public string line_config = "";
        public string conti_config = "";
        public List<string> kwargs = new List<string> { };
        public List<string> contiparams = new List<string> { };
        public string result_path = "";
        public Dictionary<string, float> Dict_FitResult = new Dictionary<string, float>();
        private bool _created = false;
        private bool _fitted = false;

        public event EventHandler Created_StateChanged;
        public event EventHandler Fitted_StateChanged;

        public bool Created
        {
            get { return _created; }
            set 
            { 
                _created = value;
                if (!_created) Fitted = false;
                Created_StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool Fitted
        {
            get { return _fitted; }
            set 
            {
                _fitted = value;
                Fitted_StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void fit()
        {
            reset();
            string fitting_config = '{' + String.Join(",", kwargs) + '}';
            string conti_params = '{' + String.Join(",", contiparams) + '}';

            Main.PythonInput.WriteLine($"spec_{spec_name}.fit('{line_config}', '{conti_config}', " +
                $"contiparams={conti_params}, fittingparams={fitting_config})");
            Main.PythonInput.WriteLine($"spec_{spec_name}.q.line_result_toXML('{result_path}')");
            Main.PythonInput.Flush();
        }

        public void preview(int w, int h)
        {
            reset();
            Main.PythonInput.WriteLine($"spec_{spec_name}.create_preview({w}, {h})");
            Main.PythonInput.Flush();
        }

        public void plot()
        {
            Main.PythonInput.WriteLine($"with open(spec_{spec_name}.plot_file, 'rb') as f: fig = pickle.load(f)");
            Main.PythonInput.WriteLine("plt.figure(fig.number)");
            Main.PythonInput.WriteLine("plt.show()");
            Main.PythonInput.Flush();
        }

        public void reset()
        {
            Main.PythonInput.WriteLine($"spec_{spec_name} = SixDFGSFitter(file_path='{spec_path}', spec_name='{spec_name}', z={z})");
            Main.PythonInput.WriteLine($"spec_{spec_name}.reset_output_spectrum()");
            Main.PythonInput.WriteLine($"spec_{spec_name}.trim_spec(({trimA}, {trimB}))");
            Main.PythonInput.Flush();
        }

        public Dictionary<string, float> FitResults
        {
            get 
            {
                if (Dict_FitResult.Count > 0) return Dict_FitResult;                              
                Dict_FitResult = Read_Result();
                return Dict_FitResult;
            }
            set
            {
                Dict_FitResult.Clear();
            }
        }

        private Dictionary<string, float> Read_Result()
        {
            Dictionary<string, float> tmp = new Dictionary<string, float> { };
            XDocument xml = XDocument.Load($"{result_path}");
            foreach (XElement xline in xml.Root.Elements())
            {
                foreach (XElement xvalue in xline.Elements())
                {
                    tmp.Add($"{xline.Attribute("name").Value}_{xvalue.Name}", float.Parse(xvalue.Value));
                }
            }
            return tmp;
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
