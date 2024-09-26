using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PyQSOFit_SBLg
{
    public partial class ConfigDisplay : UserControl
    {
        private string _configfolder;
        private string _defaultlines;
        private Button EditingButton;
        private XDocument defLines;
        private LineSections _sec;
        ///private XElement Xinfo;
        ///private FlowLayoutPanel XFlow;
        private List<Control> infoprofile = new List<Control>();
        private List<Control> infoall = new List<Control>();

        public string Path_ConfigFolder
        {
            get { return _configfolder; }
            set { _configfolder = value; }
        }

        public string Path_DefaultLines
        {
            get { return _defaultlines; }
            set 
            { 
                _defaultlines = value;
                if (value != null) defLines = XDocument.Load(value);
            }
        }

        public bool isDefault
        {
            get
            {
                if (Option_Config.Text == "Default.xml") return true;
                else return false;
            }
        }

        public LineSections EditingSection
        {
            get{ return _sec; }
            set { _sec = value; }
        }

        public ConfigDisplay()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void ConfigDisplay_Load(object sender, EventArgs e)
        {
            infoprofile.AddRange(new List<Control> { Text_lfwhm1, Text_lfwhm2, Text_voff, Text_skew, Option_skew, Option_voff });
            infoall.AddRange(new List<Control> { Text_lname, Text_lcen, Text_lscale, Option_ScaleLink, Option_ProfileLink, 
                Text_lfwhm1, Text_lfwhm2, Text_voff, Text_skew, Option_skew, Option_voff, Text_gamma});

        }

        public void ConfigDisplay_Shown()
        {
            Update_ConfigList();
            Populate_flowDefault();
            InfoDisplay.Enabled = false;
        }

        public void Update_ConfigList()
        {
            if (!Directory.Exists(Path_ConfigFolder))
            {
                MessageBox.Show("No configs found, check default path exist");
                return;
            }
            string[] fitsFiles = Directory.GetFiles(Path_ConfigFolder, "*.xml");

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

        private void Populate_flowDefault()
        {
            XElement lines = defLines.Root;
            foreach (XElement xline in lines.Elements())
            {
                if (xline.Element("dname") != null)
                {
                    XElement tmpline = new XElement("lines");
                    tmpline.Add(xline);
                    tmpline.Element("line").Element("dname")?.Remove();
                    Button xbutton = new Button
                    {
                        Text = xline.Element("dname").Value,
                        Tag = tmpline.Element("line"),
                        Margin = new Padding(0),
                        Padding = new Padding(0),
                    };
                    Flow_defButtons.Controls.Add(xbutton);
                    xbutton.Click += Button_def_Click;
                }
            }
        }

        private void Button_def_Click(object sender, EventArgs e)
        {
            Check_linkscale.Checked = false;
            EditingButton = sender as Button;
            Reset_InfoCheck();
            Reset_InfoText(true, true);
            Display_Info();
            /// Read_defLines(xbutton.Tag as XElement);
        }


        private void Button_Update_Click(object sender, EventArgs e)
        {
            Update_ConfigList();
        }

        private void Option_Config_SelectedIndexChanged(object sender, EventArgs e)
        {
            Flow_SectionDisplay.Controls.Clear();
            Button_AddSec.Enabled = true;
            ComboBox xoption = sender as ComboBox;
            if (xoption.SelectedIndex == -1) return; 
            
            List<int> line_list = new List<int> { };
            XDocument xconfig = XDocument.Load($"{Path_ConfigFolder}/{xoption.Text}");

            foreach (XElement xsec in xconfig.Root.Elements("section"))
            {
                LineSections lineSections = new LineSections { XSection = xsec };
                Flow_SectionDisplay.Controls.Add(lineSections);
                lineSections.lineClicked += LineSections_lineClicked;
                lineSections.addlineClicked += LineSections_addlineClicked;
                lineSections.removeSectionClicked += LineSections_removeSectionClicked;

                if (xoption.Text == "Default.xml")
                {
                    lineSections.Default_Section();
                    Button_AddSec.Enabled = false;
                }
            }

            EditingSection = null;
            InfoDisplay.Enabled = false;
            Reset_Info();
        }

        private void LineSections_removeSectionClicked(object sender, EventArgs e)
        {
            Reset_Info();
        }

        private void LineSections_lineClicked(object sender, EventArgs e)
        {
            EditingButton = sender as Button;

            if (EditingSection == EditingButton.Parent.Parent)
            {
                Reset_InfoCheck();
                Reset_InfoText(true, true);
                Button_Add.Text = "Add";
            }
            else
            {
                EditingSection = EditingButton.Parent.Parent as LineSections;
                Reset_Info();
            }

            InfoDisplay.BackColor = EditingButton.Parent.Parent.BackColor;
            Display_Info();

            if (isDefault) InfoDisplay.Enabled = false;
            else InfoDisplay.Enabled = true;

            Edit_Mode();
        }

        private void LineSections_addlineClicked(object sender, EventArgs e)
        {
            Button xadd = sender as Button;
            EditingSection = xadd.Parent as LineSections;

            if (xadd.Text == "Add Line")
            {
                xadd.BackColor = Color.SkyBlue;
                xadd.Text = "Done";
                InfoDisplay.Enabled = true;
                ///XFlow = xadd.Tag as FlowLayoutPanel;
                Add_Mode();
                InfoDisplay.BackColor = xadd.Parent.BackColor;
            }
            else
            {
                xadd.BackColor = SystemColors.Control;
                xadd.Text = "Add Line";
                Reset_Info();
                InfoDisplay.Enabled = false;
                InfoDisplay.BackColor = SystemColors.Control;
            }
        }

        private void Display_Info()
        {
            XElement Xinfo = EditingButton.Tag as XElement;
            if (Xinfo == null) { return; }
            Text_lname.Text = Xinfo.Element("l_name")?.Value;
            Text_lcen.Text = Xinfo.Element("l_center")?.Value;
            Text_lscale.Text = Xinfo.Element("scale")?.Value;

            if (Xinfo.Element("default") != null)
            {
                if (Xinfo.Element("default").Value == "BEL") Radio_defBEL.Checked = true;
                else if (Xinfo.Element("default").Value == "NEL") Radio_defNEL.Checked = true;
            }
            else if (Xinfo.Element("profile_link") != null)
            {
                Check_ProfileLink.Checked = true;
                Refresh_LinkOption(Option_ProfileLink);
                Option_ProfileLink.SelectedItem = Xinfo.Element("profile_link").Value;
            }
            else
            {
                Radio_custom.Checked = true;
                Read_ButtonCustomInfo(Xinfo);
            }

            if (Xinfo.Element("flux_link") != null)
            {
                Check_linkscale.Checked = true;
                Refresh_LinkOption(Option_ScaleLink);
                Option_ScaleLink.SelectedItem = Xinfo.Element("flux_link").Value;
            }
        }

        private void Refresh_LinkOption(ComboBox xoptions)
        {          
            if (EditingSection.LineList == null) return;
            List<string> xlines = new List<string>();
            foreach (Control xobj in EditingSection.LineList.Controls)
            {
                if (xobj is Button xline) xlines.Add(xline.Text);
            }
            xoptions.Items.Clear();
            xoptions.Items.AddRange(xlines.ToArray());
        }

        private void Read_ButtonCustomInfo(XElement xml)
        {
            Text_lfwhm1.Text = xml.Element("fwhm1").Value;
            Text_lfwhm2.Text = xml.Element("fwhm2").Value;
            Text_voff.Text = xml.Element("voffset").Value;
            Option_voff.SelectedItem = xml.Element("voffset").Attribute("mode").Value;

            Text_skew.Text = xml.Element("skew").Value;
            Option_skew.SelectedItem = xml.Element("skew").Attribute("mode").Value;

            if (xml.Element("gamma") != null)
                if (xml.Element("gamma").Value == "On") Check_setgam.Checked = true;
                else if (xml.Element("gamma").Value.Contains("f"))
                {
                    Check_setgam.Checked = true;
                    Text_gamma.Text = xml.Element("gamma").Value.Split('f')[1];
                }
        }

        private void Radio_defBEL_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control xobj in infoprofile)
                xobj.Enabled = false;
            Read_defaults("defBEL");
        }

        private void Radio_custom_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control xobj in infoprofile)
                xobj.Enabled = true;
            Option_skew.SelectedItem = "Free";
        }

        private void Radio_defNEL_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control xobj in infoprofile)
                xobj.Enabled = false;
            Read_defaults("defNEL");
        }

        private void Read_defaults(string defname)
        {
            XElement linedata = defLines.Root.Element(defname);
            Text_lfwhm1.Text = linedata.Element("fwhm1").Value;
            Text_lfwhm2.Text = linedata.Element("fwhm2").Value;
            Text_voff.Text = linedata.Element("voffset").Value;
            Text_skew.Text = linedata.Element("skew").Value;
            Option_voff.SelectedItem = linedata.Element("voffset").Attribute("mode").Value;
            Option_skew.SelectedItem = linedata.Element("skew").Attribute("mode").Value;
        }

        private void Check_ProfileLink_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_ProfileLink.Checked)
            {
                panel2.Enabled = false;
                label10.Visible = true;
                Option_ProfileLink.Visible = true;
                Refresh_LinkOption(Option_ProfileLink);
            }
            else
            {
                panel2.Enabled = true;
                label10.Visible = false;
                Option_ProfileLink.Visible = false;
            }
        }

        public void Reset_Info()
        {
            Reset_InfoCheck();
            Reset_InfoText(true, true);
            InfoDisplay.BackColor = SystemColors.Control;
            InfoDisplay.Enabled = false;
            Button_Remove.Visible = true;
            Button_Add.Text = "Add";
            Flow_defButtons.Enabled = false;
            Flow_defButtons.BackColor = Color.Gray;
        }

        public void Reset_InfoCheck()
        {
            Check_ProfileLink.Checked = false;
            Check_linkscale.Checked = false;
            Check_usegamma.Checked = false;
            Check_setgam.Checked = false;
            Radio_custom.Checked = false;
            Radio_defBEL.Checked = false;
            Radio_defNEL.Checked = false;
        }

        public void Reset_InfoText(bool doColour, bool doText)
        {
            foreach (Control xobj in infoall)
            {
                if (doText) xobj.Text = "";
                if (doColour) xobj.ForeColor = SystemColors.ControlText;
            }
        }

        public void Add_Mode()
        {
            Button_Remove.Visible = false;
            Button_Add.Text = "Add";
            Flow_defButtons.Enabled = true;
            Flow_defButtons.BackColor = Color.Transparent;
            Text_lname.Enabled = true;
        }

        public void Edit_Mode()
        {
            Button_Remove.Visible = true;
            Button_Add.Text = "Edit";
            Flow_defButtons.Enabled = false;
            Flow_defButtons.BackColor = Color.Gray;
            Text_lname.Enabled = false;
        }
        private void Check_usegamma_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_usegamma.Checked)
                Check_setgam.Enabled = true;
            else if (!Check_usegamma.Checked)
                Check_setgam.Enabled = false;
        }

        private void Check_setgam_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_setgam.Checked)
                Text_gamma.Enabled = true;
            else if (!Check_setgam.Checked)
                Text_gamma.Enabled = false;
        }

        private void Check_linkscale_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox xlink = sender as CheckBox;
            if (xlink.Checked)
            {
                label9.Visible = true;
                Option_ScaleLink.Enabled = true;
                Refresh_LinkOption(Option_ScaleLink);
            }
            else
            {
                label9.Visible = false;
                Option_ScaleLink.Items.Clear();
                Option_ScaleLink.Enabled = false;
            }
        }

        private void Option_skew_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Option_skew.Text == "Free")
                Label_Skew.Text = "Max Skew";
            else if (Option_skew.Text == "Fixed")
                Label_Skew.Text = "Skew";
        }

        public XElement Build_lineXml()
        {
            XElement xml = new XElement("line",
                    new XElement("l_name",
                        new XAttribute("type", "s"),
                        new XAttribute("info", "fixed"),
                        $"{Text_lname.Text}"),
                    new XElement("l_center",
                        new XAttribute("type", "f"),
                        new XAttribute("info", "fixed"),
                        $"{Text_lcen.Text}"),
                    new XElement("scale",
                        new XAttribute("type", "f"),
                        new XAttribute("info", "fixed"),
                        $"{Text_lscale.Text}")
                );

            if (Radio_defBEL.Checked)
                xml.Add(new XElement("default", new XAttribute("type", "s"), "BEL"));
            else if (Radio_defNEL.Checked)
                xml.Add(new XElement("default", new XAttribute("type", "s"), "NEL"));
            else if (Radio_custom.Checked)
                xml.Add(Fillin_custominfo().Elements());

            else if (Option_ProfileLink.SelectedIndex == -1)
            {
                MessageBox.Show("No linked line selected for profile");
                return null;
            }
            else
                xml.Add(new XElement("profile_link", new XAttribute("type", "s"), $"{Option_ProfileLink.SelectedItem}"));

            if (Text_gamma.Enabled)
                xml.Add(new XElement("gamma", new XAttribute("type", "s"), $"f{Text_gamma.Text}"));
            else if (Check_usegamma.Checked)
                xml.Add(new XElement("gamma", new XAttribute("type", "s"), "On"));

            if (Check_linkscale.Checked)
            {
                if (Option_ScaleLink.SelectedIndex == -1)
                {
                    MessageBox.Show("No linked line selected for scale");
                    return null;
                }
                else xml.Add(new XElement("flux_link", new XAttribute("type", "s"), $"{Option_ScaleLink.SelectedItem}"));
            }
            return xml;
        }

        public XElement Fillin_custominfo()
        {
            XElement xml = new XElement("Extra",
                new XElement("fwhm1", new XAttribute("type", "f"), $"{Text_lfwhm1.Text}"),
                new XElement("fwhm2", new XAttribute("type", "f"), $"{Text_lfwhm2.Text}"),
                new XElement("skew",
                    new XAttribute("type", "f"),
                    new XAttribute("mode", $"{Option_skew.SelectedItem}"),
                    $"{Text_skew.Text}"),
                new XElement("voffset",
                    new XAttribute("type", "f"),
                    new XAttribute("mode", $"{Option_voff.SelectedItem}"),
                    $"{Text_voff.Text}")
                );
            return xml;
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            if (EditingSection == null) return;

            if (Button_Add.Text == "Add")
            {
                XElement linexml = Build_lineXml();
                if (linexml == null)
                {
                    MessageBox.Show("Missing cruicial line information");
                    return;
                }
                EditingSection.Add_newLine(linexml);
            }
            else if (Button_Add.Text == "Edit")
            {
                EditingButton.Tag = Build_lineXml();
                Reset_InfoText(true, false);
            }
        }

        private void Button_Remove_Click(object sender, EventArgs e)
        {
            EditingSection.LineList.Controls.Remove(EditingButton);
            Reset_Info();
        }

        private void Text_lname_TextChanged(object sender, EventArgs e)
        {
            Control xobj = sender as Control;
            if (Button_Add.Text == "Edit")
            {
                xobj.ForeColor = Color.Red;
            }
        }

        private void AddNewSection_Click(object sender, EventArgs e)
        {
            LineSections newsection = new LineSections();
            Flow_SectionDisplay.Controls.Add(newsection);
            newsection.lineClicked += LineSections_lineClicked;
            newsection.addlineClicked += LineSections_addlineClicked;
            newsection.removeSectionClicked += LineSections_removeSectionClicked;
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog xsave = new SaveFileDialog();
            xsave.InitialDirectory = Path_ConfigFolder;
            xsave.Filter = "XML file (*.xml)|*.xml|All files (*.*)|*.*";
            xsave.DefaultExt = "xml";
            if (xsave.ShowDialog() == DialogResult.OK)
            {
                string savefilename = xsave.FileName;
                Construct_ConfigFile(savefilename);
            }
        }

        private void Construct_ConfigFile(string savefile)
        {
            XDocument xconfig = new XDocument(new XElement("config"));

            foreach (LineSections xsec in Flow_SectionDisplay.Controls)
            {
                XElement sectionxml = xsec.BuildSection_Xml();
                if (sectionxml == null)
                {
                    MessageBox.Show("Cannot save Configuration. Contains at least one incomplete section.");
                    return;
                }
                xconfig.Element("config").Add(sectionxml);
            }

            xconfig.Save(savefile);
            MessageBox.Show($"{savefile} saved successful");
        }

        private void Button_New_Click(object sender, EventArgs e)
        {
            SaveFileDialog xsave = new SaveFileDialog();
            xsave.InitialDirectory = Path_ConfigFolder;
            xsave.Filter = "XML file (*.xml)|*.xml|All files (*.*)|*.*";
            xsave.DefaultExt = "xml";

            if (xsave.ShowDialog() == DialogResult.OK)
            {
                string savefilename = xsave.FileName;
                XDocument xconfig = new XDocument(new XElement("config"));
                xconfig.Save(savefilename);
                Reset_Info();

                Flow_SectionDisplay.Controls.Clear();
                Button_AddSec.Enabled = true;

                Option_Config.Items.Add(Path.GetFileName(savefilename));
                Option_Config.Text = Path.GetFileName(savefilename);
            }
        }

        private void btnScrollLeft_Click(object sender, EventArgs e)
        {
            // Scroll 50 pixels left (negative scroll offset)
            ScrollHorizontally(Flow_SectionDisplay, -Flow_SectionDisplay.Controls[0].Width);
        }

        // Button to scroll right
        private void btnScrollRight_Click(object sender, EventArgs e)
        {
            // Scroll 50 pixels right (positive scroll offset)
            ScrollHorizontally(Flow_SectionDisplay, Flow_SectionDisplay.Controls[0].Width);
        }

        // Function to scroll horizontally
        private void ScrollHorizontally(FlowLayoutPanel panel, int offset)
        {
            // Calculate new scroll position
            int newScrollX = -panel.AutoScrollPosition.X + offset;
            int scrollY = -panel.AutoScrollPosition.Y;  // Preserve vertical scroll

            // Apply the new scroll position
            panel.AutoScrollPosition = new Point(newScrollX, scrollY);
        }
    }
}
