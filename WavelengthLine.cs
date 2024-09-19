using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PyQSOFit_SBLg
{
    public partial class WavelengthLine : UserControl
    {
        private int minValue = 4000;      // Minimum value on the number line
        private int maxValue = 7000;    // Maximum value on the number line
        public List<int> markedValues = new List<int>(); // List to store the marked values
        public List<int[]> markedLines = new List<int[]>(); // List to store the marked set of lines
        private int numberLineHeight = 20;   // Height of the number line
        private Brush mark_point = Brushes.Red;
        private Color mark_line = Color.Green;

        public WavelengthLine()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Enable double buffering to reduce flickering
            this.ResizeRedraw = true;   // Redraw the control on resize
        }

        private void WavelengthLine_Load(object sender, EventArgs e)
        {

        }

        public int MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                Invalidate(); // Redraw when the value changes
            }
        }

        // Property to set the maximum value of the number line
        public int MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                Invalidate(); // Redraw when the value changes
            }
        }

        public Brush MarkPoint
        {
            get { return mark_point; }
            set
            {
                mark_point = value;
                Invalidate();
            }
        }

        public Color MarkLine
        {
            get { return mark_line; }
            set
            {
                mark_line = value;
                Invalidate();
            }
        }

        // Paint event to draw the number line and marked values
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Draw the number line
            int lineY = this.Height / 2;
            g.DrawLine(Pens.Black, 10, lineY, this.Width - 20, lineY);

            // Draw tick marks and labels
            int tickCount = maxValue - minValue;
            for (int i = 0; i <= maxValue; i += (int)((maxValue - minValue) / 6))
            {
                int x = 10 + (i * (this.Width - 30)) / tickCount;
                g.DrawLine(Pens.Black, x, lineY - 5, x, lineY + 5);

                // Draw labels for the ticks
                string label = (minValue + i).ToString();
                g.DrawString(label, this.Font, Brushes.Black, new PointF(x - 10, lineY + 10));
            }


            foreach (int[] markedline in markedLines)
            {
                int startX = 10 + ((markedline[0] - minValue) * (this.Width - 30)) / (maxValue - minValue);
                int endX = 10 + ((markedline[1] - minValue) * (this.Width - 30)) / (maxValue - minValue);
                g.DrawLine(new Pen(MarkLine, 5), startX, lineY, endX, lineY);
            }

            // Draw marked values
            foreach (int markedValue in markedValues)
            {
                int markedX = 10 + ((markedValue - minValue) * (this.Width - 30)) / (maxValue - minValue);
                g.FillEllipse(MarkPoint, markedX - 5, lineY - 5, 10, 10); // Draw a small circle
                g.DrawString($"{markedValue}", this.Font, Brushes.Red, new PointF(markedX -10 , lineY - 15));
            }

        }

        // Mouse click event to mark a value on the number line
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // Calculate the clicked value
            int clickedValue = MouseXValue(e);

            foreach (int[] xrange in markedLines)
            {
                if (xrange[0] <= clickedValue && clickedValue <= xrange[1])
                {
                    markedLines.Remove(xrange);
                    Invalidate();
                    return;
                }
            }
            if ( markedValues.Count == 0)
            {
                // Add the clicked value to the list of marked values if not already marked
                if (!markedValues.Contains(clickedValue))
                {
                    markedValues.Add(clickedValue);
                }
                else
                {
                    // If the value is already marked, remove it
                    markedValues.Remove(clickedValue);
                }
            }
            else
            {
                markedLines.Add(new int[] { markedValues[0], clickedValue });
                markedValues.Clear();
            }
            

            Invalidate(); // Redraw the control to show the updated marked values
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            int hovervalue = MouseXValue(e);
            toolTip1.SetToolTip(this, hovervalue.ToString());
        }

        private int MouseXValue(MouseEventArgs e)
        {
            int xval = minValue + (e.X - 10) * (maxValue - minValue) / (this.Width - 30);
            xval = (int)(Math.Round((double)(xval / 5) * 5));
            // Ensure the value is within the range
            if (xval < minValue) xval = minValue;
            if (xval > maxValue) xval = maxValue;
            return xval;
        }
    }
}
