namespace PyQSOFit_SBLg
{
    partial class WavelengthLine
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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Button_Edit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Button_Reset = new System.Windows.Forms.Button();
            this.Button_ShowConti = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Option_Config = new System.Windows.Forms.ComboBox();
            this.Button_New = new System.Windows.Forms.Button();
            this.Button_RefreshList = new System.Windows.Forms.Button();
            this.Button_Save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_Edit
            // 
            this.Button_Edit.Location = new System.Drawing.Point(0, 19);
            this.Button_Edit.Name = "Button_Edit";
            this.Button_Edit.Size = new System.Drawing.Size(45, 23);
            this.Button_Edit.TabIndex = 0;
            this.Button_Edit.Text = "Edit";
            this.Button_Edit.UseVisualStyleBackColor = true;
            this.Button_Edit.Click += new System.EventHandler(this.EditMode);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(0, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(517, 168);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // Button_Reset
            // 
            this.Button_Reset.Location = new System.Drawing.Point(47, 19);
            this.Button_Reset.Name = "Button_Reset";
            this.Button_Reset.Size = new System.Drawing.Size(45, 23);
            this.Button_Reset.TabIndex = 1;
            this.Button_Reset.Text = "Reset";
            this.Button_Reset.UseVisualStyleBackColor = true;
            this.Button_Reset.Click += new System.EventHandler(this.Reset_ContiW);
            // 
            // Button_ShowConti
            // 
            this.Button_ShowConti.Location = new System.Drawing.Point(94, 19);
            this.Button_ShowConti.Name = "Button_ShowConti";
            this.Button_ShowConti.Size = new System.Drawing.Size(45, 23);
            this.Button_ShowConti.TabIndex = 3;
            this.Button_ShowConti.Text = "Hide";
            this.Button_ShowConti.UseVisualStyleBackColor = true;
            this.Button_ShowConti.Click += new System.EventHandler(this.Button_ShowConti_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Continuum Fitting Area";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Button_ShowConti);
            this.panel1.Controls.Add(this.Button_Edit);
            this.panel1.Controls.Add(this.Button_Reset);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(141, 45);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Option_Config);
            this.panel2.Controls.Add(this.Button_New);
            this.panel2.Controls.Add(this.Button_RefreshList);
            this.panel2.Controls.Add(this.Button_Save);
            this.panel2.Location = new System.Drawing.Point(139, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(141, 45);
            this.panel2.TabIndex = 6;
            // 
            // Option_Config
            // 
            this.Option_Config.FormattingEnabled = true;
            this.Option_Config.Location = new System.Drawing.Point(-1, -1);
            this.Option_Config.Name = "Option_Config";
            this.Option_Config.Size = new System.Drawing.Size(141, 21);
            this.Option_Config.TabIndex = 4;
            this.Option_Config.SelectedIndexChanged += new System.EventHandler(this.Option_Config_SelectedIndexChanged);
            // 
            // Button_New
            // 
            this.Button_New.Location = new System.Drawing.Point(94, 19);
            this.Button_New.Name = "Button_New";
            this.Button_New.Size = new System.Drawing.Size(45, 23);
            this.Button_New.TabIndex = 3;
            this.Button_New.Text = "New";
            this.Button_New.UseVisualStyleBackColor = true;
            this.Button_New.Click += new System.EventHandler(this.Button_NewClick);
            // 
            // Button_RefreshList
            // 
            this.Button_RefreshList.Location = new System.Drawing.Point(0, 19);
            this.Button_RefreshList.Name = "Button_RefreshList";
            this.Button_RefreshList.Size = new System.Drawing.Size(45, 23);
            this.Button_RefreshList.TabIndex = 0;
            this.Button_RefreshList.Text = "Re";
            this.Button_RefreshList.UseVisualStyleBackColor = true;
            this.Button_RefreshList.Click += new System.EventHandler(this.Button_RefreshListClick);
            // 
            // Button_Save
            // 
            this.Button_Save.Location = new System.Drawing.Point(47, 19);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(45, 23);
            this.Button_Save.TabIndex = 1;
            this.Button_Save.Text = "Save";
            this.Button_Save.UseVisualStyleBackColor = true;
            this.Button_Save.Click += new System.EventHandler(this.Button_SaveClick);
            // 
            // WavelengthLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "WavelengthLine";
            this.Size = new System.Drawing.Size(517, 203);
            this.Load += new System.EventHandler(this.WavelengthLine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button Button_Edit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Button_Reset;
        private System.Windows.Forms.Button Button_ShowConti;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox Option_Config;
        private System.Windows.Forms.Button Button_New;
        private System.Windows.Forms.Button Button_RefreshList;
        private System.Windows.Forms.Button Button_Save;
    }
}
