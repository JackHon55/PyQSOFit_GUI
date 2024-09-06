using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PyQSOFit_SBLg
{
    public partial class Main : Form
    {
        public Dictionary<string, fobject> Dict_fobject = new Dictionary<string, fobject>();
        public List<string> line_names = new List<string>
        {
            "Hb_br1",
            "Hb_na",
            "OIII",
            "Ha_br1",
            "NII"
        };
        public List<string> line_prop = new List<string>
        {
            "FWHM",
            "skew",
            "peak",
            "area",
            "ew"
        };
        public static Process PythonProcess;
        public static StreamWriter PythonInput;
        public static StreamReader PythonOutput;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Start_Python();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PythonInput.WriteLine("exit");
            PythonProcess.Close();
        }

        private void Button_FileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileOpener = new OpenFileDialog();
            FileOpener.Multiselect = false;

            if (FileOpener.ShowDialog() == DialogResult.OK)
            {
                Text_FilePath.Text = FileOpener.FileName;
                Text_PropName.Text = System.IO.Path.GetFileName(FileOpener.FileName).Split('.')[0];
            }

        }

        private void Button_RunFit_Click(object sender, EventArgs e)
        {
            Reset_Quick();
            fobject xobj = Dict_fobject[Option_Objects.SelectedItem.ToString()];
            xobj.fit();
        }

        private void Save_Config()
        {           
            string spec_name = Text_PropName.Text;
            string spec_path = Text_FilePath.Text.Replace("\\", "/");
            float spec_z = float.Parse(Text_PropRedshift.Text);
            float trimA = float.Parse(Text_PropFitRangeA.Text);
            float trimB = float.Parse(Text_PropFitRangeB.Text);

            if (!Dict_fobject.ContainsKey(spec_name))
            {
                Dict_fobject.Add(spec_name, new fobject { });
            }

            fobject xfit = Dict_fobject[spec_name];
            xfit.spec_path = spec_path;
            xfit.spec_name = spec_name;
            xfit.z = spec_z;
            xfit.trimA = trimA;
            xfit.trimB = trimB;
        }


        private void Start_Python()
        {
            ProcessStartInfo xprocess_info = new ProcessStartInfo
            {
                FileName = "ipython",
                Arguments = "--no-banner --no-confirm-exit",
                WorkingDirectory = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\")),
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            PythonProcess = new Process { StartInfo = xprocess_info };
            PythonProcess.Start();

            PythonInput = PythonProcess.StandardInput;
            PythonOutput = PythonProcess.StandardOutput;

            Task.Run(ReadOutputFromIpython);

            PythonInput.WriteLine($"run 6dfgs_fitting");
            PythonInput.Flush();
        }

        private async Task<string> ReadOutputFromIpython()
        {
            string line;

            while ((line = await PythonOutput.ReadLineAsync()) != null)
            {
                AppendTextToTextBox(line + Environment.NewLine);
                if (line.StartsWith("RES:"))
                {
                    PyQSOFit_Output_Reader(line);
                }
            }
            return line;
        }

        private void AppendTextToTextBox(string text)
        {
            if (RichText_Console.InvokeRequired)
            {
                RichText_Console.Invoke(new Action(() => AppendTextToTextBox(text)));
            }
            else
            {
                RichText_Console.AppendText(text);
                RichText_Console.ScrollToCaret();
            }
        }


        private void PyQSOFit_Output_Reader(string line)
        {
            line = line.Split(':')[1];
            List<string> line_data = line.Split('\t').ToList();

            Dictionary<string, float> xdict = Dict_fobject[Text_PropName.Text].Dict_FitResult;
            xdict.Clear();

            int i = 1;
            foreach (string xline in line_names)
            {
                foreach (string xprop in line_prop)
                {
                    xdict.Add($"{xline}_{xprop}", float.Parse(line_data[i]));
                    if (CheckList_FitDataName.InvokeRequired)
                    {
                        CheckList_FitDataName.Invoke(new Action(() =>
                            CheckList_FitDataName.Items.Add($"{xline}_{xprop}", true)
                        ));
                    }
                    else
                    {
                        CheckList_FitDataName.Items.Add($"{xline}_{xprop}", true);
                    }
                    i++;
                }
            }            
        }


        private void Button_Clear_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            Reset_Quick();
            Text_FilePath.Text = "";
            Text_PropName.Text = "";
            Text_PropRedshift.Text = "";
        }

        private void Reset_Quick()
        {
            RichText_FitDataValues.Clear();
            CheckList_FitDataName.Items.Clear();
        }


        private void CheckList_FitDataName_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox obj = sender as CheckedListBox;
            Dictionary<string, float> xdict = Dict_fobject[Text_PropName.Text].Dict_FitResult;
            RichText_FitDataValues.Clear();
            obj.BeginInvoke(new Action(() =>
            {
                List<float> print_data = new List<float>();
                foreach (string xprop in obj.CheckedItems)
                {
                    print_data.Add(xdict[xprop]);
                }
                RichText_FitDataValues.Text = String.Join("\t", print_data.ToArray());
            }));
            
        }

        private void Button_View_Click(object sender, EventArgs e)
        {
            fobject xobj = Dict_fobject[Option_Objects.SelectedItem.ToString()];
            xobj.plot();
        }

        private void Button_ObjectAdd_Click(object sender, EventArgs e)
        {
            Save_Config();
            Option_Objects.Items.Clear();
            Option_Objects.Items.AddRange(Dict_fobject.Keys.ToArray());
            Option_Objects.SelectedIndex = Option_Objects.Items.Count - 1;
        }

        private void Option_Objects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox xoption = sender as ComboBox;
            fobject xobj = Dict_fobject[xoption.SelectedItem.ToString()];
            Reset();
            Text_FilePath.Text = xobj.spec_path;
            Text_PropName.Text = xobj.spec_name;
            Text_PropRedshift.Text = xobj.z.ToString();
            Text_PropFitRangeA.Text = xobj.trimA.ToString();
            Text_PropFitRangeB.Text = xobj.trimB.ToString();
        }
    }

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
