using PyQSOFit_SBLg.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace PyQSOFit_SBLg
{
    public class LineDef
    {
        int sec_width = 0;
        string sec_name = "";

        public FlowLayoutPanel SectionHeaderOBJ(int yloc, int xwidth, ContextMenuStrip dropdown, string name = "Default Hb")
        {
            sec_width = xwidth;
            sec_name = name;
            Random colcode = new Random();          
            FlowLayoutPanel xflow = new FlowLayoutPanel
            {
                Name = $"FlowSec_{sec_name}",
                Width = xwidth,
                Height = 150,
                Location = new Point(0, yloc),
                BackColor = Color.FromArgb(125, colcode.Next(0, 125), colcode.Next(0, 125), colcode.Next(0, 125)),
                ContextMenuStrip = dropdown,
            };
            xflow.Controls.AddRange(Childs.ToArray());
            xflow.Tag = xflow.Controls.Count;
            return xflow;
        }

        private List<Control> Childs
        {
            get
            {
                Label label_save = new Label
                {
                    Width = 0,
                    Font = new Font("Microsoft Sans Serif", 8),
                    Name = "Save_info",
                    BackColor = Color.Transparent,
                };
                Label secname = new Label
                {
                    Text = "Section Name",
                    Width = (int)(0.32 * sec_width),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Microsoft Sans Serif", 8),
                    BackColor = Color.Transparent,
                };
                Label waverange = new Label
                {
                    Text = "Wavelength Range",
                    Width = (int)(0.52 * sec_width),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Microsoft Sans Serif", 8),
                    BackColor = Color.Transparent,
                };
                TextBox text_secname = new TextBox { Text = sec_name, Width = (int)(0.36 * sec_width) };
                TextBox text_wave1 = new TextBox { Width = (int)(0.22 * sec_width) };
                TextBox text_wave2 = new TextBox { Width = (int)(0.22 * sec_width) };

                label_save.Tag = new List<Control> { text_secname, text_wave1, text_wave2 };

                if (sec_name == "Default Hb")
                {
                    text_secname.Text = "Hb";
                    text_wave1.Text = "4000";
                    text_wave2.Text = "5500";
                }

                if (sec_name == "Default Ha")
                {
                    text_secname.Text = "Ha";
                    text_wave1.Text = "6000";
                    text_wave2.Text = "7000";
                }

                return new List<Control> { secname, waverange, text_secname, text_wave1, text_wave2, label_save };
            }          
        }      
    }

    public class LineObj
    {
        public string lname;
        public XElement linfo;
        public FlowLayoutPanel xflow;

        public LineObj(FlowLayoutPanel xflow) { this.xflow = xflow;  }

        public Button linebox
        {
            get
            {
                string xlname = $"{lname}_{xflow.Controls.Count - (int)xflow.Tag + 1}";
                Button xline = new Button
                {
                    Text = xlname,
                    Width = 75,
                    Name = $"line_{xlname}"
                };
                xline.Click += linebox_click;
                linfo.Element("l_name").Value = xlname;
                xline.Tag = linfo;
                return xline;
            }

        }

        private void linebox_click(object sender, EventArgs e)
        {
            Button xbutt = sender as Button;
            LineEdit xedit = new LineEdit(this, xbutt: xbutt);
            xedit.Show();
        }
    }

    public class AddedLine : LineObj
    {
        public AddedLine(FlowLayoutPanel xflow) : base(xflow)
        {
            LineEdit xline = new LineEdit(this);
            xline.Show();
        }

        public void Addnewline()
        {
            xflow.Controls.Add(linebox);
        }
    }

    public class DefaultLine : LineObj
    {
        public DefaultLine(FlowLayoutPanel xflow, string xsec) : base(xflow)
        {
            Populate_default(xsec);
        }

        public void Populate_default(string sec)
        {
            XDocument defLines = XDocument.Parse(Resources.defLines);
            XElement root = defLines.Root;
            foreach (XElement line in root.Elements("line"))
                if (line.Attribute("sec").Value == sec)
                    Add_defline(line);  
        }

        public void Add_defline(XElement xnode)
        {
            if (xnode.Element("l_name") == null)
                return;
            lname = xnode.Element("l_name").Value;
            linfo = xnode;
            linfo.Element("dname").Remove();

            Button xline = linebox;
            xline.BackColor = Color.Black;
            xline.ForeColor = Color.White;
            xflow.Controls.Add(xline);
        }
    }
}
