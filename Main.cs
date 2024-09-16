using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using PyQSOFit_SBLg.Properties;

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
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Start_Python();
            Setup_defaultConfig();
        }

        private void Setup_defaultConfig()
        {
            FlowLayoutPanel flow_defHb = new LineDef().SectionHeaderOBJ(0, Page_Default.Width, DropDown_Lines, "Default Hb");
            FlowLayoutPanel flow_defHa = new LineDef().SectionHeaderOBJ(flow_defHb.Height, Page_Default.Width, DropDown_Lines, "Default Ha");
            Page_Default.Controls.Add(flow_defHa);
            Page_Default.Controls.Add(flow_defHb);

            foreach (Control xobj in flow_defHa.Controls) xobj.Enabled = false;
            foreach (Control xobj in flow_defHb.Controls) xobj.Enabled = false;

            new DefaultLine(flow_defHb, "Hb");
            new DefaultLine(flow_defHa, "Ha");


        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PythonInput.WriteLine("exit");
            PythonProcess.Close();
            foreach (TabPage xpage in Tab_Lines.TabPages)
            {
                if (xpage.Tag != null)
                { File.Delete(xpage.Tag.ToString()); }
            }
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
            string tmpFilePath = Path.GetTempFileName();
            Tab_Lines.SelectedTab.Tag = tmpFilePath;
            Console.WriteLine(tmpFilePath);
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
            SaveFileDialog xsave = new SaveFileDialog();
            if (xsave.ShowDialog() == DialogResult.OK)
            {
                string savefilename = xsave.FileName;
                Construct_ConfigFile(savefilename);
            }
        }

        private void Construct_ConfigFile(string savefile)
        {
            string[] xfile = File.ReadAllLines(Tab_Lines.SelectedTab.Tag.ToString());
            string[] xtemplate = Resources.Component_template.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None); ;
            List<string> to_save = xtemplate.ToList();
            int markerIndex = to_save.IndexOf("# Insert here");
            if (markerIndex != -1) { to_save.InsertRange(markerIndex, xfile.ToList()); }
            File.WriteAllLines(savefile, to_save);
        }

        private void DropDown_Lines_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Tab_Lines.SelectedIndex == 0)
            {
                toolStripMenuItem1.Enabled = false;
                addLineToolStripMenuItem.Enabled = false;
            }
            else
            {
                toolStripMenuItem1.Enabled = true;
                addLineToolStripMenuItem.Enabled = true;
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
