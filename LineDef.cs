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
}
