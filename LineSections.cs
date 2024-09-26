using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Drawing;
using static System.Windows.Forms.LinkLabel;

namespace PyQSOFit_SBLg
{
    public partial class LineSections : UserControl
    {
        XElement _xsec;
        public event EventHandler lineClicked;
        public event EventHandler addlineClicked;
        public event EventHandler removeSectionClicked;

        public XElement XSection
        { 
            get { return _xsec; } 
            set { _xsec = value; }
        }

        public LineSections()
        {
            InitializeComponent();
        }

        public FlowLayoutPanel LineList
        {
            get { return Flow_LineDisp; }
        }

        private void LineSections_Load(object sender, EventArgs e)
        {
            Random colcode = new Random();
            this.BackColor = Color.FromArgb(125, colcode.Next(0, 125), colcode.Next(0, 125), colcode.Next(0, 125));
            Button_Add.Tag = Flow_LineDisp;
            Flow_LineDisp.Tag = (int)0;

            if (XSection == null) return;

            Text_SecName.Text = XSection.Element("section_name").Value;
            Text_RangeA.Text = XSection.Element("start_range").Value;
            Text_RangeB.Text = XSection.Element("end_range").Value;

            foreach (XElement xline in XSection.Elements("line"))
            {
                Flow_LineDisp.Controls.Add(linebutt(xline, true));
            }
        }

        private Button linebutt(XElement lineinfo, bool fromFile)
        {
            string lname = lineinfo.Element("l_name").Value;
            string xlname = $"{lname}";
            if (!fromFile) xlname = $"{lname}_{Flow_LineDisp.Controls.Count - (int)Flow_LineDisp.Tag + 1}";

            Button xline = new Button
            {
                Text = xlname,
                Width = 75,
                Height = 21,
                Margin = new Padding(0),
                Name = $"line_{xlname}"
            };
            xline.Click += linebutt_click;
            lineinfo.Element("l_name").Value = xlname;
            xline.Tag = lineinfo;
            return xline;
        }

        private void linebutt_click(object sender, EventArgs e)
        {
            lineClicked?.Invoke(sender, EventArgs.Empty);
        }

        public void Default_Section()
        {
            Button_Add.Visible = false;
            Button_Remove.Visible = false;
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            if (Button_Add.Text == "Add Line")
            {
                Flow_LineDisp.Enabled = false;
                Button_Remove.Enabled = false;
            }
            else
            {
                Flow_LineDisp.Enabled = true;
                Button_Remove.Enabled = true;
            }
            addlineClicked?.Invoke(sender, EventArgs.Empty);
        }

        public void Add_newLine(XElement xline)
        {
            Button newline = linebutt(xline, false);
            Flow_LineDisp.Controls.Add(newline);
            xline.Element("l_name").Value = newline.Text;
        }

        private void Button_Remove_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
            removeSectionClicked?.Invoke(sender, EventArgs.Empty);
        }

        public XElement BuildSection_Xml()
        {
            if (!Test_Buildable()) return null;
            XElement xsection = new XElement("section",
                 new XElement("section_name", new XAttribute("type", "s"), $"{Text_SecName.Text}"),
                 new XElement("start_range", new XAttribute("type", "f"), $"{Text_RangeA.Text}"),
                 new XElement("end_range", new XAttribute("type", "f"), $"{Text_RangeB.Text}")
                 );

            foreach(Button xobj in Flow_LineDisp.Controls)
            {
                XElement xline = xobj.Tag as XElement;
                xsection.Add(new XElement("line", xline.Elements()));
            }

            return xsection;
        }

        private bool Test_Buildable()
        {
            if (String.IsNullOrEmpty(Text_SecName.Text)) return false;
            if (String.IsNullOrEmpty(Text_RangeA.Text)) return false;
            if (String.IsNullOrEmpty(Text_RangeB.Text)) return false;
            if (Flow_LineDisp.Controls.Count == 0) return false;

            return true;
        }
    }
}
