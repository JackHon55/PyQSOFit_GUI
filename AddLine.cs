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
        public XmlDocument defLines = new XmlDocument();
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
            defLines.LoadXml(Resources.defLines);
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
            Dictionary<string, string> xdict = xbutt.Tag as Dictionary<string, string>;
            Text_lname.Text = xdict["l_name"];
            Text_lcen.Text = xdict["l_cen"];
            Text_lscale.Text = xdict["l_scale"];
            if (xdict["defaults"].Contains("bel"))
                Radio_defBEL.Checked = true;
            else if (xdict["defaults"].Contains("nel"))
                Radio_defNEL.Checked = true;
            else
            {
                Radio_custom.Checked = true;
                string[] xinfo = xdict["defaults"].Split(',');
                Text_lfwhm1.Text = xinfo[0].Split('(')[1];
                Text_lfwhm2.Text = xinfo[1].TrimEnd(')').Trim(' ');
                Text_voff.Text = xinfo[2].Split('=')[1];

                if (xinfo[3].Contains("-"))
                    Option_voff.SelectedItem = "Negative";
                else if (xinfo[3].Contains("+"))
                    Option_voff.SelectedItem = "Positive";
                else if (xinfo[3].Contains("."))
                    Option_voff.SelectedItem = "Fixed";
                else
                    Option_voff.SelectedItem = "Free";

                Text_skew.Text = xinfo[4].Split('(')[1];

                if (xinfo[5].Contains("10)"))
                    Option_skew.SelectedItem = "Free";
                else
                    Option_skew.SelectedItem = "Fixed";

                try
                {
                    if (xinfo[6].Contains("On"))
                        Check_setgam.Checked = true;
                    else if (xinfo[6].Contains("f"))
                    {
                        Check_setgam.Checked = true;
                        Text_gamma.Text = xinfo[6].Split('f')[1];
                    }
                }
                catch (Exception) { }
            }
        }

        private void Button_def_Click(object sender, EventArgs e)
        {
            Button xbutton = sender as Button;
            Read_defLines(xbutton.Tag.ToString());
        }

        private void Populate_flowDefault()
        {
            XmlNode lines = defLines.DocumentElement;
            foreach (XmlNode xline in lines)
            {
                if (xline.SelectSingleNode("dname") != null)
                {
                    Button xbutton = new Button
                    {
                        Text = xline.SelectSingleNode("dname").InnerText,
                        Tag = xline.Name
                    };
                    Flow_defButtons.Controls.Add(xbutton);
                    xbutton.Click += Button_def_Click;
                }
            }
        }

        private void Read_defLines(string deflinename)
        {
            XmlNode linedata = defLines.DocumentElement.SelectSingleNode(deflinename);
            Text_lname.Text = linedata.SelectSingleNode("lname").InnerText;
            Text_lcen.Text = linedata.SelectSingleNode("lcen").InnerText;
            Text_lscale.Text = linedata.SelectSingleNode("lscale").InnerText;
            if (linedata.SelectSingleNode("default") != null)
            {
                string defprofile = linedata.SelectSingleNode("default").InnerText;
                if (defprofile == "BEL")
                    Radio_defBEL.Checked = true;
                else if (defprofile == "NEL")
                    Radio_defNEL.Checked = true;
                return;
            }
            Radio_custom.Checked = true;
            Text_lfwhm1.Text = linedata.SelectSingleNode("lfwhm1").InnerText;
            Text_lfwhm2.Text = linedata.SelectSingleNode("lfwhm2").InnerText;
            Text_voff.Text = linedata.SelectSingleNode("lvoff").InnerText;
            Text_skew.Text = linedata.SelectSingleNode("lskew").InnerText;
            Option_voff.SelectedItem = linedata.SelectSingleNode("vmode").InnerText;
            Option_skew.SelectedItem = linedata.SelectSingleNode("smode").InnerText;
        }

        private void Read_defaults(string defname)
        {
            XmlNode linedata = defLines.DocumentElement.SelectSingleNode(defname);
            Text_lfwhm1.Text = linedata.SelectSingleNode("lfwhm1").InnerText;
            Text_lfwhm2.Text = linedata.SelectSingleNode("lfwhm2").InnerText;
            Text_voff.Text = linedata.SelectSingleNode("lvoff").InnerText;
            Text_skew.Text = linedata.SelectSingleNode("lskew").InnerText;
            Option_voff.SelectedItem = linedata.SelectSingleNode("vmode").InnerText;
            Option_skew.SelectedItem = linedata.SelectSingleNode("smode").InnerText;
        }

        private void Radio_defBEL_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control xobj in infoprofile)
                xobj.Enabled = false;
            Read_defaults("defBEL");
            Text_skew.Enabled = false;
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            lobj_current.lname = Text_lname.Text;
            lobj_current.lcen = Text_lcen.Text;
            lobj_current.lscale = Text_lscale.Text;
            if (Radio_defBEL.Checked)
                lobj_current.def = lobj_current.defBEL;
            else if (Radio_defNEL.Checked)
                lobj_current.def = lobj_current.defNEL;
            else
                lobj_current.def = Fillin_custominfo();

            if (Text_gamma.Enabled)
                lobj_current.def += $", gamma='f{Text_gamma.Text}'";
            else if (Check_usegamma.Checked)
                lobj_current.def += ", gamma='On'";

            if (lobj_current is AddedLine xline)
                xline.Addnewline();
        }

        public string Fillin_custominfo()
        {
            string lfwhm1 = Text_lfwhm1.Text;
            string lfwhm2 = Text_lfwhm2.Text;
            string lskew = Text_skew.Text;
            string lvoff = Text_voff.Text;
            string xvmode = Option_voff.SelectedItem as string;
            string xsmode = Option_skew.SelectedItem as string;

            string fwhms = $"fwhm=({lfwhm1}, {lfwhm2})";
            string voffset;
            string skew;
            if (xvmode == "Negative")
                voffset = $"voffset={lvoff}, vmode='-'";
            else if (xvmode == "Fixed")
                voffset = $"voffset={lvoff}, vmode='.'";
            else if (xvmode == "Positive")
                voffset = $"voffset={lvoff}, vmode='+'";
            else
                voffset = $"voffset={lvoff}, vmode=''";

            if (xsmode == "Fixed")
                skew = $"skew=({lskew},)";
            else
                skew = $"skew=(-10, 10)";

            return $"{fwhms}, {voffset}, {skew}";
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
            Text_skew.Enabled = false;
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
            lobj_current.lcen = Text_lcen.Text;
            lobj_current.lscale = Text_lscale.Text;
            if (Radio_defBEL.Checked)
                lobj_current.def = lobj_current.defBEL;
            else if (Radio_defNEL.Checked)
                lobj_current.def = lobj_current.defNEL;
            else
                lobj_current.def = Fillin_custominfo();

            if (Text_gamma.Enabled)
                lobj_current.def += $", gamma='f{Text_gamma.Text}'";
            else if (Check_usegamma.Checked)
                lobj_current.def += ", gamma='On'";

            xbutt.Tag = lobj_current.line_dict_gen(xbutt.Text);
            this.Close();
        }
    }
}
