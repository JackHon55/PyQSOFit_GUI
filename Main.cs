using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using PyQSOFit_SBLg.Properties;
using System.Drawing;
using System.Xml.Linq;

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
        static string _wkd;

        public Main()
        {
            InitializeComponent();
            Setup_DefaultWaveDisp();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Start_Python();
            Button_ConfigUpdate_Click(Button_ConfigUpdate, e);
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            Config_Main.Path_ConfigFolder = wkd + "fitting_configs";
            Config_Main.Path_DefaultLines = wkd + "Defaults/defLines.xml";
            Config_Main.ConfigDisplay_Shown();
            WaveDisp_Default.Path_ContiFolder = wkd + "conti_configs";
            WaveDisp_Default.WavelengthLine_Shown();
        }

        public static string wkd
        {
            get
            {
                if (string.IsNullOrEmpty(_wkd))
                {
                    _wkd = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));
                }
                return _wkd;
            }
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
                Text_PropName.Text = Path.GetFileName(FileOpener.FileName).Split('.')[0];
                Button_CreateFobject.Enabled = true;
                Button_RunFit.Enabled = false;
                Button_View.Enabled = false;
            }

        }

        private void Button_RunFit_Click(object sender, EventArgs e)
        {
            Reset_Quick();
            fobject xobj = Dict_fobject[Option_Objects.SelectedItem.ToString()];
            xobj.fit();
            Button_View.Enabled = true;
        }

        private void Save_SpecConfig()
        {
            string spec_name = Text_PropName.Text;

            if (!Dict_fobject.ContainsKey(spec_name))
            {
                Dict_fobject.Add(spec_name, new fobject { });
            }

            fobject xfit = Dict_fobject[spec_name];
            Save_SpecBasicConfig(xfit);
            Save_SpecContiConfig(xfit);

        }

        private void Save_SpecBasicConfig(fobject xfit)
        {
            xfit.spec_path = Pypath.T(Text_FilePath.Text);
            xfit.spec_name = Text_PropName.Text;
            xfit.z = float.Parse(Text_PropRedshift.Text);
            xfit.trimA = float.Parse(Text_PropFitRangeA.Text);
            xfit.trimB = float.Parse(Text_PropFitRangeB.Text);
            xfit.line_config = Pypath.T(wkd + $@"fitting_configs\{Option_ConfigLines.Text}");
            xfit.conti_config = Pypath.T(wkd + $@"conti_configs\{Option_ConfigConti.Text}");
        }

        private void Save_SpecContiConfig(fobject xfit)
        {
            xfit.kwargs.Clear();
            xfit.contiparams.Clear();
            if (Check_CFT.Checked) xfit.kwargs.Add($"'{Check_CFT.Tag}':True, '{VAL_CFTstrength.Tag}': {VAL_CFTstrength.Value}");

            if (!Panel_NormalFitConfig.Enabled) return;

            foreach (Control xobj in Panel_NormalFitConfig.Controls)
            {
                if (xobj is CheckBox xcheck && xcheck.Checked) xfit.kwargs.Add($"'{xcheck.Tag}': True");

                if (xobj is TextBox xtext && xtext.ForeColor == Color.Red)
                {
                    xfit.contiparams.Add($"'{xtext.Name.Split('_')[1]}': {xtext.Text}");
                }
            }
        }

        private void Start_Python()
        {
            ProcessStartInfo xprocess_info = new ProcessStartInfo
            {
                FileName = "ipython",
                Arguments = "--no-banner --no-confirm-exit",
                WorkingDirectory = wkd,
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
                if (line.Contains("RES:")) PyQSOFit_Output_Reader(line);
                if (line.Contains("Preview Ready")) Reset_WaveSpecDisp();
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
            Option_Objects.Items.Clear();
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
            WaveDisp_Default.MinValue = (int)xobj.trimA;
            WaveDisp_Default.MaxValue = (int)xobj.trimB;
            xobj.preview(WaveDisp_Default.ImgW, WaveDisp_Default.ImgH);
        }

        private void Button_SaveConfig_Click(object sender, EventArgs e)
        {

        }

        private void Button_ConfigUpdate_Click(object sender, EventArgs e)
        {
            string config_folder = Path.Combine(wkd, "fitting_configs");
            if (!Directory.Exists(config_folder))
            {
                MessageBox.Show("No configs found, check default path exist");
                return;
            }
            string[] fitsFiles = Directory.GetFiles(config_folder, "*.xml");

            // Clear existing items from the ComboBox
            Option_ConfigLines.Items.Clear();

            // Add file names to the ComboBox
            foreach (var file in fitsFiles)
            {
                Option_ConfigLines.Items.Add(Path.GetFileName(file));
            }

            string conti_folder = Path.Combine(wkd, "conti_configs");
            if (!Directory.Exists(conti_folder))
            {
                MessageBox.Show("No configs found, check default path exist");
                return;
            }
            string[] txtFiles = Directory.GetFiles(conti_folder, "*.txt");

            // Clear existing items from the ComboBox
            Option_ConfigConti.Items.Clear();

            // Add file names to the ComboBox
            foreach (var file in txtFiles)
            {
                Option_ConfigConti.Items.Add(Path.GetFileName(file));
            }

            // Optionally, select the first item if there are any
            if (Option_ConfigLines.Items.Count > 0)
            {
                if (Option_ConfigLines.Items.Contains("Default.xml")) Option_ConfigLines.SelectedItem = "Default.xml";
                else Option_ConfigLines.SelectedIndex = 0;
            }

            if (Option_ConfigConti.Items.Count > 0)
            {
                if (Option_ConfigConti.Items.Contains("Default.txt")) Option_ConfigConti.SelectedItem = "Default.txt";
                else Option_ConfigConti.SelectedIndex = 0;
            }
        }

        private void Setup_DefaultWaveDisp()
        {
            WaveDisp_Default.MinValue = 4000;
            WaveDisp_Default.MaxValue = 7000;
        }

        private void Reset_WaveSpecDisp()
        {
            using (FileStream fs = new FileStream(wkd + "fitting_plots/tmp.png", FileMode.Open, FileAccess.Read))
            {
                WaveDisp_Default.Preview_Image = Image.FromStream(fs); // Create a copy in memory to avoid locking
            }
        }

        private void Option_Config_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox xoption = sender as ComboBox;
            if (xoption.SelectedIndex == -1) { return; }
            List<int> line_list = new List<int> { };
            XDocument xconfig = XDocument.Load(wkd + "fitting_configs/" + xoption.Text);
            foreach (XElement xline in xconfig.Root.Elements("section").Elements("line"))
            {
                line_list.Add((int)float.Parse(xline.Element("l_center").Value));
            }
            WaveDisp_Default.EmissionLines = line_list;
        }

        private void CheckList_FitDataName_Resize(object sender, EventArgs e)
        {
            WaveDisp_Default.MaxValue = WaveDisp_Default.MaxValue;
        }

        private void Text_PropFitRangeA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                WaveDisp_Default.MinValue = int.Parse(Text_PropFitRangeA.Text);
                WaveDisp_Default.MaxValue = int.Parse(Text_PropFitRangeB.Text);
            }
        }

        private void Button_CreateFobject_Click(object sender, EventArgs e)
        {
            try
            {
                Save_SpecConfig();
                Button_RunFit.Enabled = true;
                Option_Objects.Items.Clear();
                Option_Objects.Items.AddRange(Dict_fobject.Keys.ToArray());
                Option_Objects.SelectedIndex = Option_Objects.Items.Count - 1;

            }
            catch (Exception) { }
        }

        private void Button_SaveFobject_Click(object sender, EventArgs e)
        {

        }

        private void Check_CFT_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_CFT.Checked)
            {
                Panel_NormalFitConfig.Enabled = false;
                Label_CFTstrength.Visible = true;
                VAL_CFTstrength.Visible = true;
            }
            else
            {
                Panel_NormalFitConfig.Enabled = true;
                Label_CFTstrength.Visible = false;
                VAL_CFTstrength.Visible = false;
            }
        }

        private void Text_FePLParam_TextChanged(object sender, EventArgs e)
        {
            TextBox xtext = sender as TextBox;
            if (xtext.Tag as string == xtext.Text)
            {
                xtext.ForeColor = SystemColors.WindowText;
            }
            else
            {
                xtext.ForeColor = Color.Red;
            }
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
