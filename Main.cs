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
        string wkd = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));

        public Main()
        {
            InitializeComponent();
            Setup_DefaultWaveDisp();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Start_Python();
            Setup_defaultConfig();
            Button_ConfigUpdate_Click(Button_ConfigUpdate, e);
        }

        private void Setup_defaultConfig()
        {
            FlowLayoutPanel flow_defHb = new LineDef().SectionHeaderOBJ(0, Page_Default.Width, DropDown_Lines, "Default Hb");
            FlowLayoutPanel flow_defHa = new LineDef().SectionHeaderOBJ(flow_defHb.Height, Page_Default.Width, DropDown_Lines, "Default Ha");
            Page_Default.Controls.Add(flow_defHa);
            Page_Default.Controls.Add(flow_defHb);

            foreach (Control xobj in flow_defHa.Controls)
                if (!(xobj is Label)) xobj.Enabled = false;
            foreach (Control xobj in flow_defHb.Controls)
                if (!(xobj is Label)) xobj.Enabled = false;

            new DefaultLine(flow_defHb, "Hb");
            new DefaultLine(flow_defHa, "Ha");


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
            }

        }

        private void Button_RunFit_Click(object sender, EventArgs e)
        {
            Reset_Quick();
            fobject xobj = Dict_fobject[Option_Objects.SelectedItem.ToString()];
            if (Option_Config.SelectedIndex == -1) xobj.fit(Pypath.T(wkd + @"fitting_configs\Default.xml"));
            else xobj.fit(Pypath.T(wkd + $@"fitting_configs\{Option_Config.SelectedItem}"));
        }

        private void Save_SpecConfig()
        {
            string spec_name = Text_PropName.Text;
            string spec_path = Pypath.T(Text_FilePath.Text);
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
            Save_SpecConfig();
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
            WaveDisp_Default.MinValue = (int)xobj.trimA;
            WaveDisp_Default.MaxValue = (int)xobj.trimB;
            xobj.preview(WaveDisp_Default.ImgW, WaveDisp_Default.ImgH);
        }

        private void addLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem xmenu = sender as ToolStripMenuItem;
            if (xmenu.GetCurrentParent() is ContextMenuStrip contextMenu)
            {
                if (contextMenu.SourceControl is FlowLayoutPanel xflow)
                {
                    new AddedLine(xflow);
                }
            }
        }

        private void Add_newdefinition(object sender, EventArgs e)
        {
            string xname = Interaction.InputBox("New line definition name:", "New line definitions", "LineGroup");
            if (string.IsNullOrEmpty(xname))
                return;
            Tab_Lines.TabPages.Add(xname);
            Tab_Lines.SelectTab(Tab_Lines.TabPages.Count - 1);
        }

        private void Add_newSection(object sender, EventArgs e)
        {
            string xname = Interaction.InputBox("New section name:", "New section", "Hbeta");
            if (string.IsNullOrEmpty(xname))
                return;
            int yloc = 0;
            foreach (Control xobj in Tab_Lines.SelectedTab.Controls)
            {
                if (xobj is FlowLayoutPanel && xobj.Bottom > yloc) { yloc = xobj.Bottom; }
            }

            FlowLayoutPanel flow_new = new LineDef().SectionHeaderOBJ(yloc, Tab_Lines.SelectedTab.Width, DropDown_Lines, xname);
            Tab_Lines.SelectedTab.Controls.Add(flow_new);
        }

        private void Button_SaveConfig_Click(object sender, EventArgs e)
        {
            if (!Test_Saveable()) return;
            SaveFileDialog xsave = new SaveFileDialog();
            xsave.InitialDirectory = wkd + @"fitting_configs";
            xsave.Filter = "XML file (*.xml)|*.xml|All files (*.*)|*.*";
            xsave.DefaultExt = "xml";
            if (xsave.ShowDialog() == DialogResult.OK)
            {
                string savefilename = xsave.FileName;
                Construct_ConfigFile(savefilename);
            }
        }

        private bool Test_Saveable()
        {
            if (Tab_Lines.SelectedIndex == 0)
            {
                MessageBox.Show("Default is already saved as Default.fits");
                return false;
            }
            if (Tab_Lines.SelectedTab.Controls.Count == 0)
            {
                MessageBox.Show("No defined sections to save");
                return false;
            }
            foreach (Control xflow in Tab_Lines.SelectedTab.Controls)
            {
                List<Button> valid_button = new List<Button>();
                foreach (Control xobj in xflow.Controls)
                {
                    if (xobj is TextBox box && String.IsNullOrEmpty(box.Text))
                    {
                        MessageBox.Show($"Missing section definitions for {xflow.Name.Split('_')[1]} (Name, or wavelength range)");
                        return false;
                    }
                    if (xobj is Button button) valid_button.Add(button);
                }
                if (valid_button.Count == 0)
                {
                    MessageBox.Show($"No lines in section {xflow.Name.Split('_')[1]} to save");
                    return false;
                }
            }
            return true;
        }

        private XElement SaveSection(Label xsave)
        {
            List<Control> xtexts = xsave.Tag as List<Control>;
            XElement xsection = new XElement("section",
                    new XElement("section_name", new XAttribute("type", "s"), $"{xtexts[0].Text}"),
                    new XElement("start_range", new XAttribute("type", "f"), $"{xtexts[1].Text}"),
                    new XElement("end_range", new XAttribute("type", "f"), $"{xtexts[2].Text}")
                );
            foreach (Control xobj in xsave.Parent.Controls)
            {
                if (xobj is Button && xobj.Name.StartsWith("line"))
                {
                    XElement xline = xobj.Tag as XElement;
                    xsection.Add(new XElement("line", xline.Elements()));
                }
            }
            return xsection;
        }

        private void Construct_ConfigFile(string savefile)
        {
            XDocument xconfig = new XDocument(
                    new XElement("config")
                );
            List<string> sec_names = new List<string>();
            List<string> sec_defs = new List<string>();
            foreach (Control xobj in Tab_Lines.SelectedTab.Controls)
            {
                Label xsave = xobj.Controls["Save_info"] as Label;
                List<Control> xlist = xsave.Tag as List<Control>;
                sec_names.Add($"{xlist[0].Text}_section.lines");
                xconfig.Element("config").Add(SaveSection(xsave));
            }

            xconfig.Save(savefile);
            MessageBox.Show($"{savefile} saved successful");
        }

        private void DropDown_Lines_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Tab_Lines.SelectedIndex == 0)
            {
                MenuItem_addSection.Enabled = false;
                MenuItem_addLine.Enabled = false;
            }
            else
            {
                MenuItem_addSection.Enabled = true;
                MenuItem_addLine.Enabled = true;
            }
            ContextMenuStrip contextMenu = sender as ContextMenuStrip;
            if (contextMenu.SourceControl is FlowLayoutPanel && Tab_Lines.SelectedIndex != 0)
                MenuItem_removeSection.Enabled = true;
            else MenuItem_removeSection.Enabled = false;
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
            Option_Config.Items.Clear();

            // Add file names to the ComboBox
            foreach (var file in fitsFiles)
            {
                Option_Config.Items.Add(Path.GetFileName(file));
            }

            // Optionally, select the first item if there are any
            if (Option_Config.Items.Count > 0)
            {
                if (Option_Config.Items.Contains("Default.xml")) Option_Config.SelectedItem = "Default.xml";
                else Option_Config.SelectedIndex = 0;
            }
        }

        private void MenuItem_removeSection_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem xmenu_item = sender as ToolStripMenuItem;
            ContextMenuStrip xmenu = xmenu_item.GetCurrentParent() as ContextMenuStrip;
            if (xmenu.SourceControl is FlowLayoutPanel xflow)
            {
                foreach (Control xobj in xflow.Controls)
                {
                    xobj.Dispose();
                }
                xflow.Controls.Clear();
                Tab_Lines.SelectedTab.Controls.Remove(xflow);
                xflow.Dispose();
            }
        }

        private void Setup_DefaultWaveDisp()
        {
            WaveDisp_Default.MinValue = 4000;
            WaveDisp_Default.MaxValue = 7000;
            WaveDisp_Default.ContinuumWindow = new List<int[]> {
                new int[]{4000, 4050}, new int[]{4200, 4230}, new int[]{4435, 4640}, new int[]{5100, 5535}, new int[]{6005, 6035},
                new int[]{ 6100, 6250}, new int[]{6800, 7000}, new int[]{7160, 7180},
            };
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
