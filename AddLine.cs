using PyQSOFit_SBLg.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace PyQSOFit_SBLg
{
    public partial class LineEdit : Form
    {
        public List<Control> infobox = new List<Control>();
        public List<Control> infoprofile = new List<Control>();
        public XDocument defLines = XDocument.Parse(Resources.defLines);
        private LineObj lobj_current = null;
        Button xbutt = null;
        public LineEdit(LineObj xobj, Button xbutt = null)
        {
            InitializeComponent();
            lobj_current = xobj;
            this.xbutt = xbutt;
        }

        private void AddLine_Load(object sender, EventArgs e)
        {
            infobox.AddRange(new List<Control> { Text_lname, Text_lcen, Text_lscale, Text_lfwhm1, Text_lfwhm2, Text_voff,
            Text_skew, Text_gamma, Radio_defBEL, Radio_defNEL, Check_usegamma});
            infoprofile.AddRange(new List<Control> { Text_lfwhm1, Text_lfwhm2, Text_voff, Option_skew, Option_voff });

            Populate_flowDefault();
            Read_Buttoninfo();

            if (xbutt == null)
                Text = "Add Line";
            else if (xbutt.BackColor == Color.Black)
            {
                Text = "Default Line";
                foreach (Control xobj in this.Controls) xobj.Enabled = false;
            }
            else
            {
                Text = "Edit Line";
                Text_lname.Enabled = false;
                Button_Remove.Location = Button_Add.Location;
                Button_Add.Visible = false;
                Button_Remove.Visible = true;
                Button_save.Visible = true;
            }
        }

        private void Read_Buttoninfo()
        {
            if (xbutt == null)
                return;
            XElement xml = xbutt.Tag as XElement;
            Text_lname.Text = xml.Element("l_name")?.Value;
            Text_lcen.Text = xml.Element("l_center")?.Value;
            Text_lscale.Text = xml.Element("scale")?.Value;

            if (xml.Element("default") != null)
            {
                if (xml.Element("default").Value == "BEL") Radio_defBEL.Checked = true;
                else if (xml.Element("default").Value == "NEL") Radio_defNEL.Checked = true;
            }
            else if (xml.Element("profile_link") != null)
            {
                Radio_Link.Checked = true;
                Refresh_LinkOption(Option_ProfileLink);
                Option_ProfileLink.SelectedItem = xml.Element("profile_link").Value;
            }
            else
            {
                Radio_custom.Checked = true;
                Read_ButtonCustomInfo(xml);
            }

            if (xml.Element("flux_link") != null)
            {
                Check_linkscale.Checked = true;
                Refresh_LinkOption(Option_ScaleLink);
                Option_ScaleLink.SelectedItem = xml.Element("flux_link").Value;
            }
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

        private void Button_def_Click(object sender, EventArgs e)
        {
            Check_linkscale.Checked = false;
            Button xbutton = sender as Button;
            Read_defLines(xbutton.Tag as XElement);
        }

        private void Populate_flowDefault()
        {
            XElement lines = defLines.Root;
            foreach (XElement xline in lines.Elements())
            {
                if (xline.Element("dname") != null)
                {
                    Button xbutton = new Button
                    {
                        Text = xline.Element("dname").Value,
                        Tag = xline
                    };
                    Flow_defButtons.Controls.Add(xbutton);
                    xbutton.Click += Button_def_Click;
                }
            }
        }

        private void Read_defLines(XElement linedata)
        {
            Text_lname.Text = linedata.Element("l_name").Value;
            Text_lcen.Text = linedata.Element("l_center").Value;
            Text_lscale.Text = linedata.Element("scale").Value;
            if (linedata.Element("default") != null)
            {
                string defprofile = linedata.Element("default").Value;
                if (defprofile == "BEL")
                    Radio_defBEL.Checked = true;
                else if (defprofile == "NEL")
                    Radio_defNEL.Checked = true;
                return;
            }
            if (linedata.Element("flux_link") != null)
            {
                Check_linkscale.Checked = true;
                Refresh_LinkOption(Option_ScaleLink);
                string xx = "";
                foreach (Control xobj in lobj_current.xflow.Controls)
                {
                    if (xobj.Name.Contains(linedata.Element("flux_link").Value)) xx = xobj.Text;
                }
                Option_ScaleLink.SelectedItem = xx;
            }
            if (linedata.Element("profile_link") != null)
            {
                Radio_Link.Checked = true;
                Refresh_LinkOption(Option_ProfileLink);
                string xx = "";
                foreach (Control xobj in lobj_current.xflow.Controls)
                {
                    if (xobj.Name.Contains(linedata.Element("profile_link").Value)) xx = xobj.Text;
                }
                Option_ProfileLink.SelectedItem = xx;
                return;
            }

            Radio_custom.Checked = true;
            Text_lfwhm1.Text = linedata.Element("fwhm1").Value;
            Text_lfwhm2.Text = linedata.Element("fwhm2").Value;
            Text_voff.Text = linedata.Element("voffset").Value;
            Text_skew.Text = linedata.Element("skew").Value;
            Option_voff.SelectedItem = linedata.Element("voffset").Attribute("mode").Value;
            Option_skew.SelectedItem = linedata.Element("skew").Attribute("mode").Value;
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

        private void Radio_defBEL_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control xobj in infoprofile)
                xobj.Enabled = false;
            Read_defaults("defBEL");
            Text_skew.Enabled = false;
            Panel_ProfileSetup.Height = 24;
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            lobj_current.lname = Text_lname.Text;
            XElement xml = Build_lineXml();
            if (xml == null)
                return;
            lobj_current.linfo = xml;
            if (lobj_current is AddedLine xline) xline.Addnewline();
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

        private void Radio_custom_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control xobj in infoprofile)
                xobj.Enabled = true;
            Option_skew.SelectedItem = "Free";
            Panel_ProfileSetup.Height = 24;
        }

        private void Radio_defNEL_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control xobj in infoprofile)
                xobj.Enabled = false;
            Read_defaults("defNEL");
            Text_skew.Enabled = false;
            Panel_ProfileSetup.Height = 24;
        }

        private void Radio_Link_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control xobj in infoprofile)
                xobj.Enabled = false;
            Text_skew.Enabled = false;
            Panel_ProfileSetup.Height = 55;
            Refresh_LinkOption(Option_ProfileLink);
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

        private void Option_skew_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Option_skew.SelectedItem == "Free")
                Text_skew.Enabled = false;
            else
                Text_skew.Enabled = true;
        }

        private void Button_Remove_Click(object sender, EventArgs e)
        {
            if (xbutt == null)
                return;
            xbutt.Parent.Tag = (int)xbutt.Parent.Tag - 1;
            xbutt.Parent.Controls.Remove(xbutt);
            this.Close();
        }

        private void Button_save_Click(object sender, EventArgs e)
        {
            if (xbutt == null)
                return;
            lobj_current.lname = Text_lname.Text;
            XElement xml = Build_lineXml();
            if (xml == null)
                return;

            xbutt.Tag = xml;
            this.Close();
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

        private void Refresh_LinkOption(ComboBox xoptions)
        {
            List<string> xlines = new List<string>();
            foreach (Control xobj in lobj_current.xflow.Controls)
            {
                if (xobj is Button xline) xlines.Add(xline.Text);
            }
            xoptions.Items.Clear();
            xoptions.Items.AddRange(xlines.ToArray());
        }
    }
}
