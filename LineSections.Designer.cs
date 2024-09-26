namespace PyQSOFit_SBLg
{
    partial class LineSections
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Text_SecName = new System.Windows.Forms.TextBox();
            this.Text_RangeA = new System.Windows.Forms.TextBox();
            this.Text_RangeB = new System.Windows.Forms.TextBox();
            this.Flow_LineDisp = new System.Windows.Forms.FlowLayoutPanel();
            this.Button_Remove = new System.Windows.Forms.Button();
            this.Button_Add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(29, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Section Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(18, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Wavelength Range";
            // 
            // Text_SecName
            // 
            this.Text_SecName.Location = new System.Drawing.Point(0, 16);
            this.Text_SecName.Name = "Text_SecName";
            this.Text_SecName.Size = new System.Drawing.Size(136, 20);
            this.Text_SecName.TabIndex = 2;
            // 
            // Text_RangeA
            // 
            this.Text_RangeA.Location = new System.Drawing.Point(0, 55);
            this.Text_RangeA.Name = "Text_RangeA";
            this.Text_RangeA.Size = new System.Drawing.Size(65, 20);
            this.Text_RangeA.TabIndex = 3;
            // 
            // Text_RangeB
            // 
            this.Text_RangeB.Location = new System.Drawing.Point(71, 55);
            this.Text_RangeB.Name = "Text_RangeB";
            this.Text_RangeB.Size = new System.Drawing.Size(65, 20);
            this.Text_RangeB.TabIndex = 4;
            // 
            // Flow_LineDisp
            // 
            this.Flow_LineDisp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Flow_LineDisp.AutoScroll = true;
            this.Flow_LineDisp.BackColor = System.Drawing.Color.Transparent;
            this.Flow_LineDisp.Location = new System.Drawing.Point(138, 0);
            this.Flow_LineDisp.Margin = new System.Windows.Forms.Padding(0);
            this.Flow_LineDisp.Name = "Flow_LineDisp";
            this.Flow_LineDisp.Size = new System.Drawing.Size(184, 100);
            this.Flow_LineDisp.TabIndex = 5;
            // 
            // Button_Remove
            // 
            this.Button_Remove.Location = new System.Drawing.Point(0, 76);
            this.Button_Remove.Name = "Button_Remove";
            this.Button_Remove.Size = new System.Drawing.Size(65, 23);
            this.Button_Remove.TabIndex = 6;
            this.Button_Remove.Text = "Remove";
            this.Button_Remove.UseVisualStyleBackColor = true;
            this.Button_Remove.Click += new System.EventHandler(this.Button_Remove_Click);
            // 
            // Button_Add
            // 
            this.Button_Add.Location = new System.Drawing.Point(71, 76);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(65, 23);
            this.Button_Add.TabIndex = 7;
            this.Button_Add.Tag = "";
            this.Button_Add.Text = "Add Line";
            this.Button_Add.UseVisualStyleBackColor = true;
            this.Button_Add.Click += new System.EventHandler(this.Button_Add_Click);
            // 
            // LineSections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Controls.Add(this.Button_Add);
            this.Controls.Add(this.Button_Remove);
            this.Controls.Add(this.Flow_LineDisp);
            this.Controls.Add(this.Text_RangeB);
            this.Controls.Add(this.Text_RangeA);
            this.Controls.Add(this.Text_SecName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LineSections";
            this.Size = new System.Drawing.Size(322, 100);
            this.Load += new System.EventHandler(this.LineSections_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Text_SecName;
        private System.Windows.Forms.TextBox Text_RangeA;
        private System.Windows.Forms.TextBox Text_RangeB;
        private System.Windows.Forms.FlowLayoutPanel Flow_LineDisp;
        private System.Windows.Forms.Button Button_Remove;
        private System.Windows.Forms.Button Button_Add;
    }
}
