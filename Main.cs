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
using System.Xml.Serialization;

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
            Text_ResultPath.Text = wkd + "results";
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
                Button_Value.Enabled = false;
            }
        }

        private void Button_RunFit_Click(object sender, EventArgs e)
        {
            Reset_Quick();
            fobject xobj = Dict_fobject[Option_Objects.SelectedItem.ToString()];
            xobj.fit();
        }

        private void Save_SpecConfig()
        {
            string spec_name = Text_PropName.Text;

            if (!Dict_fobject.ContainsKey(spec_name))
            {
                fobject xobject = new fobject { };
                xobject.Created_StateChanged += fobject_Created;
                xobject.Fitted_StateChanged += fobject_Fitted;
                Dict_fobject.Add(spec_name, xobject);
            }

            fobject xfit = Dict_fobject[spec_name];
            Save_SpecBasicConfig(xfit);
            Save_SpecContiConfig(xfit);
        }

        private void fobject_Created(object sender, EventArgs e)
        {
            fobject xobj = sender as fobject;
            if (xobj.Created) Button_RunFit.Enabled = true;
            else Button_RunFit.Enabled = false;
        }

        private void fobject_Fitted(object sender, EventArgs e)
        {
            fobject xobj = sender as fobject;
            if (xobj.Fitted)
            {
                Button_View.Enabled = true;
                Button_Value.Enabled = true;
            }
            else
            {
                Button_View.Enabled = false;
                Button_Value.Enabled = false;
            }
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

            if (Check_Error.Checked) xfit.kwargs.Add($"'{Check_Error.Tag}':True, '{Val_ErrorCount.Tag}': {Val_ErrorCount.Value}");

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
                if (line.Contains("Preview Ready"))
                {
                    if (InvokeRequired) Invoke(new Action(UI_DoneCreating));
                    else UI_DoneCreating();
                }
                if (line.Contains("Fitting finished"))
                {
                    if (InvokeRequired) Invoke(new Action(UI_DoneFitting));
                    else UI_DoneFitting();
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
            CheckList_FitDataName.Items.Clear();
        }


        private void CheckList_FitDataName_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void Button_View_Click(object sender, EventArgs e)
        {
            fobject xobj = Dict_fobject[Option_Objects.SelectedItem.ToString()];
            xobj.plot();
        }


        private void Option_Objects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox xoption = sender as ComboBox;
            if (xoption.SelectedIndex == -1) return;

            fobject xobj = Dict_fobject[xoption.SelectedItem.ToString()];
            Reset();
            Text_FilePath.Text = xobj.spec_path;
            Text_PropName.Text = xobj.spec_name;
            Text_PropRedshift.Text = xobj.z.ToString();
            Text_PropFitRangeA.Text = xobj.trimA.ToString();
            Text_PropFitRangeB.Text = xobj.trimB.ToString();
            WaveDisp_Default.MinValue = (int)xobj.trimA;
            WaveDisp_Default.MaxValue = (int)xobj.trimB;

            foreach (string xitem in Option_ConfigLines.Items)
            {
                if (xobj.line_config.Contains(xitem)) Option_ConfigLines.SelectedItem = xitem;
            }

            foreach (string xitem in Option_ConfigConti.Items)
            {
                if (xobj.conti_config.Contains(xitem)) Option_ConfigConti.SelectedItem = xitem;
            }

            xobj.preview(WaveDisp_Default.ImgW, WaveDisp_Default.ImgH);
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

        private void UI_DoneCreating()
        {
            using (FileStream fs = new FileStream(wkd + "fitting_plots/tmp.png", FileMode.Open, FileAccess.Read))
            {
                WaveDisp_Default.Preview_Image = Image.FromStream(fs); 
            }
            Dict_fobject[Text_PropName.Text].Created = true;
        }

        private void UI_DoneFitting()
        {
            Dict_fobject[Text_PropName.Text].Fitted = true;
        }

        private void Option_Config_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Text_PropName.Text)) Dict_fobject[Text_PropName.Text].Created = false;
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
                Option_Objects.Items.Clear();
                Option_Objects.Items.AddRange(Dict_fobject.Keys.ToArray());
                Option_Objects.SelectedIndex = Option_Objects.Items.Count - 1;
                Dict_fobject[Text_PropName.Text].result_path = Pypath.T($"{Text_ResultPath.Text}/{Text_PropName.Text}.xml");
                Dict_fobject[Text_PropName.Text].FitResults = null;
                ///Dict_fobject[Text_PropName.Text].Created = true;
            }
            catch (Exception) { }
        }

        private void Check_CFT_CheckedChanged(object sender, EventArgs e)
        {
            Dict_fobject[Text_PropName.Text].Created = false;
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
            Dict_fobject[Text_PropName.Text].Created = false;
            if (xtext.Tag as string == xtext.Text)
            {
                xtext.ForeColor = SystemColors.WindowText;
            }
            else
            {
                xtext.ForeColor = Color.Red;
            }
        }

        private void Button_ResultsOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a directory to save the file.";
                folderDialog.ShowNewFolderButton = true; // Allow the creation of new folders
                folderDialog.SelectedPath = wkd + "results";
                // Show the dialog and get the result
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    // The user selected a valid directory
                    Text_ResultPath.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void Button_Value_Click(object sender, EventArgs e)
        {
            CheckList_FitDataName.Items.Clear();
            CheckList_FitDataName.Items.AddRange(Dict_fobject[Text_PropName.Text].FitResults.Keys.ToArray());
            for (int i = 0; i < CheckList_FitDataName.Items.Count; i++)
            {
                CheckList_FitDataName.SetItemChecked(i, true); // Check each item
            }
        }

        private void Button_ResultsShow_Click(object sender, EventArgs e)
        {
            List<float> values = new List<float> { };
            foreach (string xvalue in CheckList_FitDataName.CheckedItems)
            {
                values.Add(Dict_fobject[Text_PropName.Text].FitResults[xvalue]);
            }
            RichText_Console.AppendText(String.Join("\t", values) + "\n");
            RichText_Console.ScrollToCaret();
        }

        private void Check_Error_CheckedChanged(object sender, EventArgs e)
        {
            Dict_fobject[Text_PropName.Text].Created = false;
            if (Check_Error.Checked) Val_ErrorCount.Enabled = true;
            else Val_ErrorCount.Enabled = false;
        }

        private void Check_ContiParamCheckedChanged(object sender, EventArgs e)
        {
            Dict_fobject[Text_PropName.Text].Created = false;
        }

        private void VAL_CFTstrength_ValueChanged(object sender, EventArgs e)
        {
            Dict_fobject[Text_PropName.Text].Created = false;
        }

        private void Option_ConfigConti_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Text_PropName.Text)) Dict_fobject[Text_PropName.Text].Created = false;
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
