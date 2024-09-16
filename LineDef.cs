using PyQSOFit_SBLg.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace PyQSOFit_SBLg
{
    public class LineDef
    {
        int sec_width = 0;
        string sec_name = "";
        public LineDef()
        {

        }

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
                Button button_save = new Button
                {
                    Text = "Save",
                    Width = (int)(0.2 * sec_width),
                    Font = new Font("Microsoft Sans Serif", 8),
                    Name = $"Save_{sec_name}",
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

                button_save.Click += SaveSection;
                button_save.Tag = new List<Control> { text_secname, text_wave1, text_wave2 };

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

                return new List<Control> { secname, waverange, text_secname, text_wave1, text_wave2, button_save };
            }          
        }

        private void SaveSection(object sender, EventArgs e)
        {           
            Button xsave = sender as Button;

            if (xsave.BackColor == Color.Transparent)
            {
                SaveSection_initial(xsave);
            }
            else if (xsave.BackColor == Color.Green)
            {
                SaveSection_editing(xsave);
                SaveSection_initial(xsave);
            }
            
        }

        private void SaveSection_initial(Button xsave)
        {
            List<Control> xtexts = xsave.Tag as List<Control>;
            string sec_ini = $"{xtexts[0].Text}_section = Section(section_name='{xtexts[0].Text}', " +
                $"start_range={xtexts[1].Text}, end_range={xtexts[2].Text})";

            List<string> line_ini = new List<string>();
            List<string> line_names = new List<string>();
            foreach (Control xobj in xsave.Parent.Controls)
            {
                if (xobj is Button && xobj.Name.StartsWith("line"))
                {
                    line_ini.Add($"{xobj.Name} = {xobj.Tag}");
                    line_names.Add(xobj.Name);
                }
            }
            string line_into_sec = $"{xtexts[0].Text}_section.add_lines([{String.Join(",", line_names)}])";

            List<string> sec_defs = new List<string>();
            sec_defs.Add($"# {xsave.Name}");
            sec_defs.Add(sec_ini);
            sec_defs.AddRange(line_ini);
            sec_defs.Add(line_into_sec);

            File.AppendAllLines(xsave.Parent.Parent.Tag as string, sec_defs);
            xsave.BackColor = Color.Green;
        }

        private void SaveSection_editing(Button xsave)
        {
            string[] xfile = File.ReadAllLines(xsave.Parent.Parent.Tag.ToString());
            List<string> dump_lines = new List<string>();
            List<string> keep_lines = new List<string>();
            List<string> from_file = keep_lines;
            foreach (string xline in xfile)
            {
                if (xline.Contains('#') && xline.Contains(xsave.Name))
                {
                    from_file = dump_lines;
                }              
                else if (xline.Contains('#'))
                {
                    from_file = keep_lines;
                }
                from_file.Add(xline);
            }
            File.WriteAllLines(xsave.Parent.Parent.Tag.ToString(), keep_lines);
        }

        private void Save_toFile(object sender, EventArgs e)
        {
            ProcessStartInfo xprocess_info = new ProcessStartInfo
            {
                FileName = "ipython",
                WorkingDirectory = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\")),
                UseShellExecute = false,
                RedirectStandardInput = true,
                CreateNoWindow = true,
            };

            Process xpython = new Process { StartInfo = xprocess_info };
            xpython.Start();
            StreamWriter xpython_in = xpython.StandardInput;
            xpython_in.WriteLine("run Component_definitions.py");
            xpython_in.Flush();
            xpython_in.WriteLine("exit");
            xpython.Close();
        }

    }

    public class LineObj
    {
        public string lname;
        public string lcen;
        public string lscale;
        public string def;

        public string defBEL = "default_bel=True";
        public string defNEL = "default_nel=True";
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
                    ///Tag = $"LineDef(l_name='{xlname}', l_center={lcen}, scale={lscale}, {def})",
                    Tag = line_dict_gen(xlname),
                    Name = $"line_{xlname}"
                };
                xline.Click += linebox_click;
                return xline;
            }

        }

        public Dictionary<string, string> line_dict_gen(string xlname)
        {
            Dictionary<string, string> xinfo = new Dictionary<string, string>
            {
                {"l_name",  xlname},
                {"l_cen", lcen},
                {"l_scale", lscale},
                {"defaults", def},
            };

            return xinfo;
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
            XmlDocument defLines = new XmlDocument();
            defLines.LoadXml(Resources.defLines);
            XmlNode root = defLines.DocumentElement;
            if (sec == "Hb")
            {
                Add_defline(root.SelectSingleNode("hbbroad"), "BEL");
                Add_defline(root.SelectSingleNode("hbnarrow"), "");
                Add_defline(root.SelectSingleNode("oiiir"), "NEL");
                Add_defline(root.SelectSingleNode("oiiil"), "NEL");
            }
            else if (sec == "Ha")
            {
                Add_defline(root.SelectSingleNode("habroad"), "BEL");
                Add_defline(root.SelectSingleNode("hanarrow"), "");
                Add_defline(root.SelectSingleNode("niir"), "NEL");
                Add_defline(root.SelectSingleNode("niil"), "NEL");
            }
        }

        public void Add_defline(XmlNode xnode, string xdef)
        {
            lname = xnode.SelectSingleNode("lname").InnerText;
            lcen = xnode.SelectSingleNode("lcen").InnerText;
            lscale = xnode.SelectSingleNode("lscale").InnerText;
            if (xdef == "BEL")
            {
                def = defBEL;
            }
            else if (xdef == "NEL")
            {
                def = defNEL;
            }
            else
            {
                def = Fillin_custominfo(xnode);
            }
            Button xline = linebox;
            xline.BackColor = Color.Black;
            xline.ForeColor = Color.White;
            xflow.Controls.Add(xline);
        }

        public string Fillin_custominfo(XmlNode xnode)
        {
            string lfwhm1 = xnode.SelectSingleNode("lfwhm1").InnerText;
            string lfwhm2 = xnode.SelectSingleNode("lfwhm2").InnerText;
            string lskew = xnode.SelectSingleNode("lskew").InnerText;
            string lvoff = xnode.SelectSingleNode("lvoff").InnerText;
            string xvmode = xnode.SelectSingleNode("vmode").InnerText;
            string xsmode = xnode.SelectSingleNode("smode").InnerText;

            string fwhms = $"fwhm=({lfwhm1}, {lfwhm2})";
            string voffset = "";
            string skew = "";
            if (xvmode == "Negative")
            {
                voffset = $"voffset={lvoff}, vmode='-'";
            }
            else if (xvmode == "Fixed")
            {
                voffset = $"voffset={lvoff}, vmode='.'";
            }
            else if (xvmode == "Positive")
            {
                voffset = $"voffset={lvoff}, vmode='+'";
            }
            else
            {
                voffset = $"voffset={lvoff}, vmode=''";
            }
            if (xsmode == "Fixed")
            {
                skew = $"skew=({lskew},)";
            }
            else
            {
                skew = $"skew=(-10, 10)";
            }

            return $"{fwhms}, {voffset}, {skew}";
        }
    }
}
