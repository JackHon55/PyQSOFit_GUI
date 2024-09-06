namespace PyQSOFit_SBLg
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label_PropName = new System.Windows.Forms.Label();
            this.Button_FileOpen = new System.Windows.Forms.Button();
            this.Text_FilePath = new System.Windows.Forms.TextBox();
            this.Text_PropName = new System.Windows.Forms.TextBox();
            this.Text_PropFitRangeA = new System.Windows.Forms.TextBox();
            this.Label_PropFitRange = new System.Windows.Forms.Label();
            this.Text_PropFitRangeB = new System.Windows.Forms.TextBox();
            this.Text_PropRedshift = new System.Windows.Forms.TextBox();
            this.Label_PropRedshift = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Button_RunFit = new System.Windows.Forms.Button();
            this.Button_Clear = new System.Windows.Forms.Button();
            this.RichText_FitDataValues = new System.Windows.Forms.RichTextBox();
            this.CheckList_FitDataName = new System.Windows.Forms.CheckedListBox();
            this.RichText_Console = new System.Windows.Forms.RichTextBox();
            this.Button_View = new System.Windows.Forms.Button();
            this.Option_Objects = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Button_ObjectAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label_PropName
            // 
            this.Label_PropName.AutoSize = true;
            this.Label_PropName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_PropName.Location = new System.Drawing.Point(55, 51);
            this.Label_PropName.Name = "Label_PropName";
            this.Label_PropName.Size = new System.Drawing.Size(45, 17);
            this.Label_PropName.TabIndex = 0;
            this.Label_PropName.Text = "Name";
            // 
            // Button_FileOpen
            // 
            this.Button_FileOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_FileOpen.Location = new System.Drawing.Point(25, 10);
            this.Button_FileOpen.Name = "Button_FileOpen";
            this.Button_FileOpen.Size = new System.Drawing.Size(75, 23);
            this.Button_FileOpen.TabIndex = 1;
            this.Button_FileOpen.Text = "Open";
            this.Button_FileOpen.UseVisualStyleBackColor = true;
            this.Button_FileOpen.Click += new System.EventHandler(this.Button_FileOpen_Click);
            // 
            // Text_FilePath
            // 
            this.Text_FilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_FilePath.Location = new System.Drawing.Point(115, 10);
            this.Text_FilePath.Name = "Text_FilePath";
            this.Text_FilePath.Size = new System.Drawing.Size(979, 23);
            this.Text_FilePath.TabIndex = 2;
            // 
            // Text_PropName
            // 
            this.Text_PropName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_PropName.Location = new System.Drawing.Point(115, 48);
            this.Text_PropName.Name = "Text_PropName";
            this.Text_PropName.Size = new System.Drawing.Size(100, 23);
            this.Text_PropName.TabIndex = 3;
            // 
            // Text_PropFitRangeA
            // 
            this.Text_PropFitRangeA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_PropFitRangeA.Location = new System.Drawing.Point(115, 106);
            this.Text_PropFitRangeA.Name = "Text_PropFitRangeA";
            this.Text_PropFitRangeA.Size = new System.Drawing.Size(100, 23);
            this.Text_PropFitRangeA.TabIndex = 5;
            this.Text_PropFitRangeA.Text = "4000";
            // 
            // Label_PropFitRange
            // 
            this.Label_PropFitRange.AutoSize = true;
            this.Label_PropFitRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_PropFitRange.Location = new System.Drawing.Point(12, 109);
            this.Label_PropFitRange.Name = "Label_PropFitRange";
            this.Label_PropFitRange.Size = new System.Drawing.Size(92, 17);
            this.Label_PropFitRange.TabIndex = 4;
            this.Label_PropFitRange.Text = "Fitting Range";
            // 
            // Text_PropFitRangeB
            // 
            this.Text_PropFitRangeB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_PropFitRangeB.Location = new System.Drawing.Point(240, 106);
            this.Text_PropFitRangeB.Name = "Text_PropFitRangeB";
            this.Text_PropFitRangeB.Size = new System.Drawing.Size(100, 23);
            this.Text_PropFitRangeB.TabIndex = 7;
            this.Text_PropFitRangeB.Text = "7000";
            // 
            // Text_PropRedshift
            // 
            this.Text_PropRedshift.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_PropRedshift.Location = new System.Drawing.Point(115, 77);
            this.Text_PropRedshift.Name = "Text_PropRedshift";
            this.Text_PropRedshift.Size = new System.Drawing.Size(100, 23);
            this.Text_PropRedshift.TabIndex = 9;
            this.Text_PropRedshift.Text = "0.06915";
            // 
            // Label_PropRedshift
            // 
            this.Label_PropRedshift.AutoSize = true;
            this.Label_PropRedshift.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_PropRedshift.Location = new System.Drawing.Point(40, 80);
            this.Label_PropRedshift.Name = "Label_PropRedshift";
            this.Label_PropRedshift.Size = new System.Drawing.Size(60, 17);
            this.Label_PropRedshift.TabIndex = 8;
            this.Label_PropRedshift.Text = "Redshift";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(221, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "-";
            // 
            // Button_RunFit
            // 
            this.Button_RunFit.Location = new System.Drawing.Point(15, 319);
            this.Button_RunFit.Name = "Button_RunFit";
            this.Button_RunFit.Size = new System.Drawing.Size(75, 23);
            this.Button_RunFit.TabIndex = 11;
            this.Button_RunFit.Text = "Run";
            this.Button_RunFit.UseVisualStyleBackColor = true;
            this.Button_RunFit.Click += new System.EventHandler(this.Button_RunFit_Click);
            // 
            // Button_Clear
            // 
            this.Button_Clear.Location = new System.Drawing.Point(96, 319);
            this.Button_Clear.Name = "Button_Clear";
            this.Button_Clear.Size = new System.Drawing.Size(75, 23);
            this.Button_Clear.TabIndex = 13;
            this.Button_Clear.Text = "Clear";
            this.Button_Clear.UseVisualStyleBackColor = true;
            this.Button_Clear.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // RichText_FitDataValues
            // 
            this.RichText_FitDataValues.Location = new System.Drawing.Point(356, 348);
            this.RichText_FitDataValues.Name = "RichText_FitDataValues";
            this.RichText_FitDataValues.Size = new System.Drawing.Size(783, 139);
            this.RichText_FitDataValues.TabIndex = 14;
            this.RichText_FitDataValues.Text = "";
            // 
            // CheckList_FitDataName
            // 
            this.CheckList_FitDataName.CheckOnClick = true;
            this.CheckList_FitDataName.ColumnWidth = 150;
            this.CheckList_FitDataName.FormattingEnabled = true;
            this.CheckList_FitDataName.Location = new System.Drawing.Point(15, 348);
            this.CheckList_FitDataName.MultiColumn = true;
            this.CheckList_FitDataName.Name = "CheckList_FitDataName";
            this.CheckList_FitDataName.Size = new System.Drawing.Size(325, 139);
            this.CheckList_FitDataName.TabIndex = 15;
            this.CheckList_FitDataName.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckList_FitDataName_ItemCheck);
            // 
            // RichText_Console
            // 
            this.RichText_Console.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RichText_Console.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichText_Console.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.RichText_Console.Location = new System.Drawing.Point(356, 48);
            this.RichText_Console.Name = "RichText_Console";
            this.RichText_Console.ReadOnly = true;
            this.RichText_Console.Size = new System.Drawing.Size(783, 294);
            this.RichText_Console.TabIndex = 16;
            this.RichText_Console.Text = "";
            // 
            // Button_View
            // 
            this.Button_View.Location = new System.Drawing.Point(177, 319);
            this.Button_View.Name = "Button_View";
            this.Button_View.Size = new System.Drawing.Size(75, 23);
            this.Button_View.TabIndex = 17;
            this.Button_View.Text = "View";
            this.Button_View.UseVisualStyleBackColor = true;
            this.Button_View.Click += new System.EventHandler(this.Button_View_Click);
            // 
            // Option_Objects
            // 
            this.Option_Objects.FormattingEnabled = true;
            this.Option_Objects.Location = new System.Drawing.Point(67, 257);
            this.Option_Objects.Name = "Option_Objects";
            this.Option_Objects.Size = new System.Drawing.Size(185, 21);
            this.Option_Objects.TabIndex = 18;
            this.Option_Objects.SelectedIndexChanged += new System.EventHandler(this.Option_Objects_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Object";
            // 
            // Button_ObjectAdd
            // 
            this.Button_ObjectAdd.Location = new System.Drawing.Point(258, 256);
            this.Button_ObjectAdd.Name = "Button_ObjectAdd";
            this.Button_ObjectAdd.Size = new System.Drawing.Size(27, 23);
            this.Button_ObjectAdd.TabIndex = 20;
            this.Button_ObjectAdd.Text = "+";
            this.Button_ObjectAdd.UseVisualStyleBackColor = true;
            this.Button_ObjectAdd.Click += new System.EventHandler(this.Button_ObjectAdd_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 500);
            this.Controls.Add(this.Button_ObjectAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Option_Objects);
            this.Controls.Add(this.Button_View);
            this.Controls.Add(this.RichText_Console);
            this.Controls.Add(this.CheckList_FitDataName);
            this.Controls.Add(this.RichText_FitDataValues);
            this.Controls.Add(this.Button_Clear);
            this.Controls.Add(this.Button_RunFit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Text_PropRedshift);
            this.Controls.Add(this.Label_PropRedshift);
            this.Controls.Add(this.Text_PropFitRangeB);
            this.Controls.Add(this.Text_PropFitRangeA);
            this.Controls.Add(this.Label_PropFitRange);
            this.Controls.Add(this.Text_PropName);
            this.Controls.Add(this.Text_FilePath);
            this.Controls.Add(this.Button_FileOpen);
            this.Controls.Add(this.Label_PropName);
            this.Name = "Main";
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_PropName;
        private System.Windows.Forms.Button Button_FileOpen;
        private System.Windows.Forms.TextBox Text_FilePath;
        private System.Windows.Forms.TextBox Text_PropName;
        private System.Windows.Forms.TextBox Text_PropFitRangeA;
        private System.Windows.Forms.Label Label_PropFitRange;
        private System.Windows.Forms.TextBox Text_PropFitRangeB;
        private System.Windows.Forms.TextBox Text_PropRedshift;
        private System.Windows.Forms.Label Label_PropRedshift;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Button_RunFit;
        private System.Windows.Forms.Button Button_Clear;
        private System.Windows.Forms.RichTextBox RichText_FitDataValues;
        private System.Windows.Forms.CheckedListBox CheckList_FitDataName;
        private System.Windows.Forms.RichTextBox RichText_Console;
        private System.Windows.Forms.Button Button_View;
        private System.Windows.Forms.ComboBox Option_Objects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Button_ObjectAdd;
    }
}