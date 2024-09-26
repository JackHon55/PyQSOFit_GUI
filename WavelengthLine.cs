using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.LinkLabel;

namespace PyQSOFit_SBLg
{
    public partial class WavelengthLine : UserControl
    {
        private int minValue = 4000;      // Minimum value on the number line
        private int maxValue = 7000;    // Maximum value on the number line
        public List<int> _EmissionLines = new List<int>(); 
        private int tmpMarker = 0;
        public List<int[]> _ContinuumWindow = new List<int[]>(); // List to store the marked set of lines
        private int numberLineHeight = 20;   // Height of the number line
        private Color mark_point = Color.Red;
        private Color mark_cwindow = Color.Gray;
        private Color mark_line = Color.LimeGreen;
        private Image _preview_Image = null;
        private List<int[]> DefaultWindow = new List<int[]>();
        private string _contifolder;

        public string Path_ContiFolder
        {
            get { return _contifolder; } 
            set { _contifolder = value; } 
        }

        public WavelengthLine()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Enable double buffering to reduce flickering
            this.ResizeRedraw = true;   // Redraw the control on resize
        }

        private void WavelengthLine_Load(object sender, EventArgs e)
        {

        }

        public void WavelengthLine_Shown()
        {
            Update_ConfigList();
        }

        public void Update_ConfigList()
        {
            if (!Directory.Exists(Path_ContiFolder))
            {
                MessageBox.Show("No configs found, check default path exist");
                return;
            }
            string[] txtFiles = Directory.GetFiles(Path_ContiFolder, "*.txt");

            // Clear existing items from the ComboBox
            Option_Config.Items.Clear();

            // Add file names to the ComboBox
            foreach (var file in txtFiles)
            {
                Option_Config.Items.Add(Path.GetFileName(file));
            }

            // Optionally, select the first item if there are any
            if (Option_Config.Items.Count > 0)
            {
                if (Option_Config.Items.Contains("Default.txt")) Option_Config.SelectedItem = "Default.txt";
                else Option_Config.SelectedIndex = 0;
            }
        }

        public int MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                pictureBox1.Invalidate(); // Redraw when the value changes
            }
        }

        // Property to set the maximum value of the number line
        public int MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                pictureBox1.Invalidate(); // Redraw when the value changes
            }
        }

        public Image Preview_Image
        {
            get { return _preview_Image; }
            set
            {
                _preview_Image = value;
                if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                pictureBox1.Image = value;
            }
        }

        public int ImgW
        { get { return pictureBox1.Width - 20; } }

        public int ImgH
        {  get { return pictureBox1.Height - 20; } }

        public List<int> EmissionLines
        { 
            get { return _EmissionLines; } 
            set
            {
                _EmissionLines = value;
                pictureBox1.Invalidate();
            }
        }

        public List<int[]> ContinuumWindow
        {
            get { return _ContinuumWindow; }
            set
            {
                if (value == null)
                {
                    _ContinuumWindow.Clear();
                    foreach (int[] xrange in DefaultWindow) _ContinuumWindow.Add(xrange);
                }
                else _ContinuumWindow = value;
                pictureBox1.Invalidate();
            }
        }

        private int MouseXValue(MouseEventArgs e)
        {
            int xval = minValue + e.X * (maxValue - minValue) / ImgW;
            xval = (int)(Math.Round((double)(xval / 5) * 5));
            // Ensure the value is within the range
            if (xval < minValue) xval = minValue;
            if (xval > maxValue) xval = maxValue;
            return xval;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw the number line
            int lineY = pictureBox1.Height - 25;
            g.DrawLine(Pens.Black, 0, lineY, ImgW, lineY);


            // Draw tick marks and labels
            for (int i = minValue; i <= maxValue; i += (int)((maxValue - minValue) / 6))
            {
                string label = i.ToString();
                int x = (i - minValue) * ImgW / (maxValue - minValue);
                g.DrawLine(Pens.Black, x, lineY - 5, x, lineY + 5);
                g.DrawString(label, pictureBox1.Font, Brushes.Black, Point_Boundary(x - 10, lineY + 10));
            }

            if (Button_ShowConti.Text == "Hide") Paint_ContiW(g, lineY);

            // Draw marked values
            foreach (int markedValue in EmissionLines)
            {
                int markedX = (markedValue - minValue) * ImgW / (maxValue - minValue);
                g.DrawLine(new Pen(mark_line), markedX, lineY, markedX, 25);
                g.DrawString($"{markedValue}", pictureBox1.Font, new SolidBrush(mark_line), Point_Boundary(markedX - 10, 5));
            }

            if (tmpMarker != 0)
            {
                int tmpx = (tmpMarker - minValue) * ImgW / (maxValue - minValue);
                g.FillEllipse(new SolidBrush(mark_point), tmpx - 5, lineY - 5, 10, 10); // Draw a small circle
                g.DrawString($"{tmpMarker}", pictureBox1.Font, new SolidBrush(mark_point), Point_Boundary(tmpx - 10, lineY - 20));
            }

        }

        private void Paint_ContiW(Graphics g, int lineY)
        {
            foreach (int[] markedline in ContinuumWindow)
            {
                int startX = (markedline[0] - minValue) * ImgW / (maxValue - minValue);
                int endX = (markedline[1] - minValue) * ImgW / (maxValue - minValue);
                if (startX < 0 || startX > ImgW) continue;
                if (endX < 0 || endX > ImgW) continue;
                g.DrawLine(new Pen(mark_cwindow, 5), startX, lineY, endX, lineY);

                Brush semiT = new SolidBrush(Color.FromArgb(128, mark_cwindow));
                Rectangle conti_rect = new Rectangle(startX, lineY / 2, endX - startX, lineY / 2);
                g.FillRectangle(semiT, conti_rect);
            }
        }

        private PointF Point_Boundary(int x, int y)
        {
            if (x <= 0) x = 5;
            if (x >= ImgW - 35) x = ImgW - 35;
            return new PointF(x, y);
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Button_Edit.BackColor == SystemColors.Control) return;
            // Calculate the clicked value
            int clickedValue = MouseXValue(e);

            foreach (int[] xrange in ContinuumWindow)
            {
                if (xrange[0] <= clickedValue && clickedValue <= xrange[1])
                {
                    ContinuumWindow.Remove(xrange);
                    pictureBox1.Invalidate();
                    return;
                }
            }
            if (tmpMarker == 0) tmpMarker = clickedValue;
            else
            {
                Avoid_DoubleWindow(clickedValue);
                tmpMarker = 0;
            }
            pictureBox1.Invalidate(); // Redraw the control to show the updated marked values
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int hovervalue = MouseXValue(e);
            toolTip1.SetToolTip(pictureBox1, hovervalue.ToString());
        }

        private void Avoid_DoubleWindow(int clickedValue)
        {
            int y1 = Math.Min(tmpMarker, clickedValue);
            int y2 = Math.Max(tmpMarker, clickedValue);
            List<int[]> tmpList = new List<int[]> { };
            foreach (int[] xrange in ContinuumWindow)
            {
                if (xrange[0] >= y1 && xrange[1] <= y2)
                {
                    continue;
                }
                tmpList.Add(xrange);
            }
            ContinuumWindow = tmpList;
            ContinuumWindow.Add(new int[] { y1, y2 });
        }

        private void EditMode(object sender, EventArgs e)
        {
            Button xbutt = sender as Button;
            if (xbutt.BackColor == SystemColors.Control)
                xbutt.BackColor = Color.SkyBlue;
            else xbutt.BackColor = SystemColors.Control;
        }

        private void Reset_ContiW(object sender, EventArgs e)
        {
            ContinuumWindow = null;
        }

        private void Button_ShowConti_Click(object sender, EventArgs e)
        {
            Button xbutt = sender as Button;
            if (xbutt.Text == "Show")
                xbutt.Text = "Hide";
            else xbutt.Text = "Show";
            pictureBox1.Invalidate();
        }

        private void Option_Config_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines($"{Path_ContiFolder}/{Option_Config.Text}");
            lines = lines.Skip(1).ToArray();
            DefaultWindow.Clear();
            foreach (string xline in lines)
            {
                if (String.IsNullOrEmpty(xline)) continue;
                if (!xline.Contains(',')) continue;
                int[] tmp = new int[] { int.Parse(xline.Split(',')[0]), int.Parse(xline.Split(',')[1]) };
                DefaultWindow.Add(tmp);
            }
            ContinuumWindow = null;
        }

        private void Button_RefreshListClick(object sender, EventArgs e)
        {
            Update_ConfigList();
        }

        private void Button_SaveClick(object sender, EventArgs e)
        {
            SaveFileDialog xsave = new SaveFileDialog();
            xsave.InitialDirectory = Path_ContiFolder;
            xsave.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            xsave.DefaultExt = "txt";
            if (xsave.ShowDialog() == DialogResult.OK)
            {
                string savefilename = xsave.FileName;
                Construct_ConfigFile(savefilename);

                MessageBox.Show($"{Path.GetFileName(savefilename)} saved successfully");
                Option_Config.SelectedItem = Path.GetFileName(savefilename);
            }
        }

        private void Construct_ConfigFile(string savefile)
        {
            ContinuumWindow.Sort((a, b) => a[0].CompareTo(b[0]));
            List<string> xlines = new List<string> { "# Continuum Window Ranges" };
            xlines.AddRange(ContinuumWindow.Select(arr => $"{arr[0]}, {arr[1]}").ToList());

            File.WriteAllLines(savefile, xlines.ToArray());
        }

        private void Button_NewClick(object sender, EventArgs e)
        {
            SaveFileDialog xsave = new SaveFileDialog();
            xsave.InitialDirectory = Path_ContiFolder;
            xsave.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            xsave.DefaultExt = "txt";

            if (xsave.ShowDialog() == DialogResult.OK)
            {
                string savefilename = xsave.FileName;
                File.WriteAllLines(savefilename, new string[] { });

                Option_Config.SelectedItem = Path.GetFileName(savefilename);
            }
        }
    }
}
