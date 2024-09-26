using System.Collections.Generic;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
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
            this.label2 = new System.Windows.Forms.Label();
            this.Option_ConfigLines = new System.Windows.Forms.ComboBox();
            this.Button_ConfigUpdate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Panel_NormalFitConfig = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.Text_FeOffMax = new System.Windows.Forms.TextBox();
            this.Text_FeOffVal = new System.Windows.Forms.TextBox();
            this.Text_FeOffMin = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Text_FeFWHMMax = new System.Windows.Forms.TextBox();
            this.Text_FeScaleMax = new System.Windows.Forms.TextBox();
            this.Text_FeFWHMVAL = new System.Windows.Forms.TextBox();
            this.Text_FeScaleVal = new System.Windows.Forms.TextBox();
            this.Text_FeFWHMMin = new System.Windows.Forms.TextBox();
            this.Text_FeScaleMin = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Text_PLSlopeMax = new System.Windows.Forms.TextBox();
            this.Text_PLScaleMax = new System.Windows.Forms.TextBox();
            this.Text_PLSlopeVal = new System.Windows.Forms.TextBox();
            this.Text_PLScaleVal = new System.Windows.Forms.TextBox();
            this.Text_PLSlopeMin = new System.Windows.Forms.TextBox();
            this.Text_PLScaleMin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Check_hostdecomp = new System.Windows.Forms.CheckBox();
            this.Check_PL = new System.Windows.Forms.CheckBox();
            this.Check_poly = new System.Windows.Forms.CheckBox();
            this.Check_Fe = new System.Windows.Forms.CheckBox();
            this.Check_BC = new System.Windows.Forms.CheckBox();
            this.VAL_CFTstrength = new System.Windows.Forms.NumericUpDown();
            this.Label_CFTstrength = new System.Windows.Forms.Label();
            this.Check_CFT = new System.Windows.Forms.CheckBox();
            this.Button_CreateFobject = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Option_ConfigConti = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Button_SaveFObject = new System.Windows.Forms.Button();
            this.Config_Main = new PyQSOFit_SBLg.ConfigDisplay();
            this.WaveDisp_Default = new PyQSOFit_SBLg.WavelengthLine();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.Panel_NormalFitConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VAL_CFTstrength)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_PropName
            // 
            this.Label_PropName.AutoSize = true;
            this.Label_PropName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_PropName.Location = new System.Drawing.Point(55, 40);
            this.Label_PropName.Name = "Label_PropName";
            this.Label_PropName.Size = new System.Drawing.Size(45, 17);
            this.Label_PropName.TabIndex = 0;
            this.Label_PropName.Text = "Name";
            // 
            // Button_FileOpen
            // 
            this.Button_FileOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_FileOpen.Location = new System.Drawing.Point(12, 3);
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
            this.Text_FilePath.Location = new System.Drawing.Point(102, 3);
            this.Text_FilePath.Name = "Text_FilePath";
            this.Text_FilePath.Size = new System.Drawing.Size(225, 23);
            this.Text_FilePath.TabIndex = 2;
            // 
            // Text_PropName
            // 
            this.Text_PropName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_PropName.Location = new System.Drawing.Point(115, 37);
            this.Text_PropName.Name = "Text_PropName";
            this.Text_PropName.Size = new System.Drawing.Size(85, 23);
            this.Text_PropName.TabIndex = 3;
            // 
            // Text_PropFitRangeA
            // 
            this.Text_PropFitRangeA.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_PropFitRangeA.Location = new System.Drawing.Point(115, 95);
            this.Text_PropFitRangeA.Name = "Text_PropFitRangeA";
            this.Text_PropFitRangeA.Size = new System.Drawing.Size(85, 23);
            this.Text_PropFitRangeA.TabIndex = 5;
            this.Text_PropFitRangeA.Text = "4000";
            this.Text_PropFitRangeA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Text_PropFitRangeA_KeyDown);
            // 
            // Label_PropFitRange
            // 
            this.Label_PropFitRange.AutoSize = true;
            this.Label_PropFitRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_PropFitRange.Location = new System.Drawing.Point(12, 98);
            this.Label_PropFitRange.Name = "Label_PropFitRange";
            this.Label_PropFitRange.Size = new System.Drawing.Size(92, 17);
            this.Label_PropFitRange.TabIndex = 4;
            this.Label_PropFitRange.Text = "Fitting Range";
            // 
            // Text_PropFitRangeB
            // 
            this.Text_PropFitRangeB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_PropFitRangeB.Location = new System.Drawing.Point(227, 95);
            this.Text_PropFitRangeB.Name = "Text_PropFitRangeB";
            this.Text_PropFitRangeB.Size = new System.Drawing.Size(85, 23);
            this.Text_PropFitRangeB.TabIndex = 7;
            this.Text_PropFitRangeB.Text = "7000";
            this.Text_PropFitRangeB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Text_PropFitRangeA_KeyDown);
            // 
            // Text_PropRedshift
            // 
            this.Text_PropRedshift.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text_PropRedshift.Location = new System.Drawing.Point(115, 66);
            this.Text_PropRedshift.Name = "Text_PropRedshift";
            this.Text_PropRedshift.Size = new System.Drawing.Size(85, 23);
            this.Text_PropRedshift.TabIndex = 9;
            this.Text_PropRedshift.Text = "0.06915";
            // 
            // Label_PropRedshift
            // 
            this.Label_PropRedshift.AutoSize = true;
            this.Label_PropRedshift.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_PropRedshift.Location = new System.Drawing.Point(40, 69);
            this.Label_PropRedshift.Name = "Label_PropRedshift";
            this.Label_PropRedshift.Size = new System.Drawing.Size(60, 17);
            this.Label_PropRedshift.TabIndex = 8;
            this.Label_PropRedshift.Text = "Redshift";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(208, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "-";
            // 
            // Button_RunFit
            // 
            this.Button_RunFit.Enabled = false;
            this.Button_RunFit.Location = new System.Drawing.Point(171, 476);
            this.Button_RunFit.Name = "Button_RunFit";
            this.Button_RunFit.Size = new System.Drawing.Size(45, 23);
            this.Button_RunFit.TabIndex = 11;
            this.Button_RunFit.Text = "Run";
            this.Button_RunFit.UseVisualStyleBackColor = true;
            this.Button_RunFit.Click += new System.EventHandler(this.Button_RunFit_Click);
            // 
            // Button_Clear
            // 
            this.Button_Clear.Location = new System.Drawing.Point(106, 39);
            this.Button_Clear.Name = "Button_Clear";
            this.Button_Clear.Size = new System.Drawing.Size(75, 23);
            this.Button_Clear.TabIndex = 13;
            this.Button_Clear.Text = "Clear";
            this.Button_Clear.UseVisualStyleBackColor = true;
            this.Button_Clear.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // RichText_FitDataValues
            // 
            this.RichText_FitDataValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichText_FitDataValues.Location = new System.Drawing.Point(12, 695);
            this.RichText_FitDataValues.Name = "RichText_FitDataValues";
            this.RichText_FitDataValues.Size = new System.Drawing.Size(336, 113);
            this.RichText_FitDataValues.TabIndex = 14;
            this.RichText_FitDataValues.Text = "";
            // 
            // CheckList_FitDataName
            // 
            this.CheckList_FitDataName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckList_FitDataName.CheckOnClick = true;
            this.CheckList_FitDataName.ColumnWidth = 150;
            this.CheckList_FitDataName.FormattingEnabled = true;
            this.CheckList_FitDataName.Location = new System.Drawing.Point(12, 580);
            this.CheckList_FitDataName.MultiColumn = true;
            this.CheckList_FitDataName.Name = "CheckList_FitDataName";
            this.CheckList_FitDataName.Size = new System.Drawing.Size(336, 109);
            this.CheckList_FitDataName.TabIndex = 15;
            this.CheckList_FitDataName.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckList_FitDataName_ItemCheck);
            this.CheckList_FitDataName.Resize += new System.EventHandler(this.CheckList_FitDataName_Resize);
            // 
            // RichText_Console
            // 
            this.RichText_Console.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichText_Console.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RichText_Console.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichText_Console.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.RichText_Console.Location = new System.Drawing.Point(359, 695);
            this.RichText_Console.Name = "RichText_Console";
            this.RichText_Console.ReadOnly = true;
            this.RichText_Console.Size = new System.Drawing.Size(1010, 113);
            this.RichText_Console.TabIndex = 16;
            this.RichText_Console.Text = "";
            // 
            // Button_View
            // 
            this.Button_View.Enabled = false;
            this.Button_View.Location = new System.Drawing.Point(223, 476);
            this.Button_View.Name = "Button_View";
            this.Button_View.Size = new System.Drawing.Size(45, 23);
            this.Button_View.TabIndex = 17;
            this.Button_View.Text = "View";
            this.Button_View.UseVisualStyleBackColor = true;
            this.Button_View.Click += new System.EventHandler(this.Button_View_Click);
            // 
            // Option_Objects
            // 
            this.Option_Objects.FormattingEnabled = true;
            this.Option_Objects.Location = new System.Drawing.Point(68, 12);
            this.Option_Objects.Name = "Option_Objects";
            this.Option_Objects.Size = new System.Drawing.Size(185, 21);
            this.Option_Objects.TabIndex = 18;
            this.Option_Objects.SelectedIndexChanged += new System.EventHandler(this.Option_Objects_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Object";
            // 
            // Button_ObjectAdd
            // 
            this.Button_ObjectAdd.Location = new System.Drawing.Point(259, 11);
            this.Button_ObjectAdd.Name = "Button_ObjectAdd";
            this.Button_ObjectAdd.Size = new System.Drawing.Size(63, 23);
            this.Button_ObjectAdd.TabIndex = 20;
            this.Button_ObjectAdd.Text = "Update";
            this.Button_ObjectAdd.UseVisualStyleBackColor = true;
            this.Button_ObjectAdd.Click += new System.EventHandler(this.Button_ObjectAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 400);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "Fitting Configurations";
            // 
            // Option_ConfigLines
            // 
            this.Option_ConfigLines.FormattingEnabled = true;
            this.Option_ConfigLines.Location = new System.Drawing.Point(62, 422);
            this.Option_ConfigLines.Name = "Option_ConfigLines";
            this.Option_ConfigLines.Size = new System.Drawing.Size(185, 21);
            this.Option_ConfigLines.TabIndex = 22;
            this.Option_ConfigLines.SelectedIndexChanged += new System.EventHandler(this.Option_Config_SelectedIndexChanged);
            // 
            // Button_ConfigUpdate
            // 
            this.Button_ConfigUpdate.Location = new System.Drawing.Point(256, 434);
            this.Button_ConfigUpdate.Name = "Button_ConfigUpdate";
            this.Button_ConfigUpdate.Size = new System.Drawing.Size(63, 23);
            this.Button_ConfigUpdate.TabIndex = 24;
            this.Button_ConfigUpdate.Text = "Update";
            this.Button_ConfigUpdate.UseVisualStyleBackColor = true;
            this.Button_ConfigUpdate.Click += new System.EventHandler(this.Button_ConfigUpdate_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.numericUpDown2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.Panel_NormalFitConfig);
            this.panel1.Controls.Add(this.VAL_CFTstrength);
            this.panel1.Controls.Add(this.Label_CFTstrength);
            this.panel1.Controls.Add(this.Check_CFT);
            this.panel1.Controls.Add(this.Button_CreateFobject);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.Option_ConfigConti);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Label_PropName);
            this.panel1.Controls.Add(this.Text_PropName);
            this.panel1.Controls.Add(this.Label_PropFitRange);
            this.panel1.Controls.Add(this.Button_View);
            this.panel1.Controls.Add(this.Button_ConfigUpdate);
            this.panel1.Controls.Add(this.Text_PropFitRangeA);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Option_ConfigLines);
            this.panel1.Controls.Add(this.Text_PropFitRangeB);
            this.panel1.Controls.Add(this.Button_RunFit);
            this.panel1.Controls.Add(this.Label_PropRedshift);
            this.panel1.Controls.Add(this.Text_PropRedshift);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Button_FileOpen);
            this.panel1.Controls.Add(this.Text_FilePath);
            this.panel1.Location = new System.Drawing.Point(12, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 506);
            this.panel1.TabIndex = 27;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown2.Location = new System.Drawing.Point(165, 367);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown2.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(76, 371);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Bootstrap Count";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 370);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(53, 17);
            this.checkBox1.TabIndex = 39;
            this.checkBox1.Tag = "CFT";
            this.checkBox1.Text = "Errors";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Panel_NormalFitConfig
            // 
            this.Panel_NormalFitConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_NormalFitConfig.Controls.Add(this.label15);
            this.Panel_NormalFitConfig.Controls.Add(this.label14);
            this.Panel_NormalFitConfig.Controls.Add(this.label13);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_FeOffMax);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_FeOffVal);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_FeOffMin);
            this.Panel_NormalFitConfig.Controls.Add(this.label12);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_FeFWHMMax);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_FeScaleMax);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_FeFWHMVAL);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_FeScaleVal);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_FeFWHMMin);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_FeScaleMin);
            this.Panel_NormalFitConfig.Controls.Add(this.label10);
            this.Panel_NormalFitConfig.Controls.Add(this.label11);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_PLSlopeMax);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_PLScaleMax);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_PLSlopeVal);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_PLScaleVal);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_PLSlopeMin);
            this.Panel_NormalFitConfig.Controls.Add(this.Text_PLScaleMin);
            this.Panel_NormalFitConfig.Controls.Add(this.label9);
            this.Panel_NormalFitConfig.Controls.Add(this.label8);
            this.Panel_NormalFitConfig.Controls.Add(this.Check_hostdecomp);
            this.Panel_NormalFitConfig.Controls.Add(this.Check_PL);
            this.Panel_NormalFitConfig.Controls.Add(this.Check_poly);
            this.Panel_NormalFitConfig.Controls.Add(this.Check_Fe);
            this.Panel_NormalFitConfig.Controls.Add(this.Check_BC);
            this.Panel_NormalFitConfig.Location = new System.Drawing.Point(12, 153);
            this.Panel_NormalFitConfig.Name = "Panel_NormalFitConfig";
            this.Panel_NormalFitConfig.Size = new System.Drawing.Size(307, 198);
            this.Panel_NormalFitConfig.TabIndex = 38;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(166, 45);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 57;
            this.label15.Text = "Value";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(235, 45);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(27, 13);
            this.label14.TabIndex = 56;
            this.label14.Text = "Max";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(102, 45);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 55;
            this.label13.Text = "Min";
            // 
            // Text_FeOffMax
            // 
            this.Text_FeOffMax.Location = new System.Drawing.Point(218, 99);
            this.Text_FeOffMax.Name = "Text_FeOffMax";
            this.Text_FeOffMax.Size = new System.Drawing.Size(59, 20);
            this.Text_FeOffMax.TabIndex = 53;
            this.Text_FeOffMax.Tag = "0.01";
            this.Text_FeOffMax.Text = "0.01";
            this.Text_FeOffMax.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // Text_FeOffVal
            // 
            this.Text_FeOffVal.Location = new System.Drawing.Point(153, 99);
            this.Text_FeOffVal.Name = "Text_FeOffVal";
            this.Text_FeOffVal.Size = new System.Drawing.Size(59, 20);
            this.Text_FeOffVal.TabIndex = 52;
            this.Text_FeOffVal.Tag = "0";
            this.Text_FeOffVal.Text = "0";
            this.Text_FeOffVal.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // Text_FeOffMin
            // 
            this.Text_FeOffMin.Location = new System.Drawing.Point(87, 99);
            this.Text_FeOffMin.Name = "Text_FeOffMin";
            this.Text_FeOffMin.Size = new System.Drawing.Size(59, 20);
            this.Text_FeOffMin.TabIndex = 51;
            this.Text_FeOffMin.Tag = "-0.01";
            this.Text_FeOffMin.Text = "-0.01";
            this.Text_FeOffMin.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(44, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 51;
            this.label12.Text = "Offset";
            // 
            // Text_FeFWHMMax
            // 
            this.Text_FeFWHMMax.Location = new System.Drawing.Point(218, 80);
            this.Text_FeFWHMMax.Name = "Text_FeFWHMMax";
            this.Text_FeFWHMMax.Size = new System.Drawing.Size(59, 20);
            this.Text_FeFWHMMax.TabIndex = 50;
            this.Text_FeFWHMMax.Tag = "5000";
            this.Text_FeFWHMMax.Text = "5000";
            this.Text_FeFWHMMax.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // Text_FeScaleMax
            // 
            this.Text_FeScaleMax.Location = new System.Drawing.Point(218, 61);
            this.Text_FeScaleMax.Name = "Text_FeScaleMax";
            this.Text_FeScaleMax.Size = new System.Drawing.Size(59, 20);
            this.Text_FeScaleMax.TabIndex = 47;
            this.Text_FeScaleMax.Tag = "1000";
            this.Text_FeScaleMax.Text = "1000";
            this.Text_FeScaleMax.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // Text_FeFWHMVAL
            // 
            this.Text_FeFWHMVAL.Location = new System.Drawing.Point(153, 80);
            this.Text_FeFWHMVAL.Name = "Text_FeFWHMVAL";
            this.Text_FeFWHMVAL.Size = new System.Drawing.Size(59, 20);
            this.Text_FeFWHMVAL.TabIndex = 49;
            this.Text_FeFWHMVAL.Tag = "3000";
            this.Text_FeFWHMVAL.Text = "3000";
            this.Text_FeFWHMVAL.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // Text_FeScaleVal
            // 
            this.Text_FeScaleVal.Location = new System.Drawing.Point(153, 61);
            this.Text_FeScaleVal.Name = "Text_FeScaleVal";
            this.Text_FeScaleVal.Size = new System.Drawing.Size(59, 20);
            this.Text_FeScaleVal.TabIndex = 46;
            this.Text_FeScaleVal.Tag = "0";
            this.Text_FeScaleVal.Text = "0";
            this.Text_FeScaleVal.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // Text_FeFWHMMin
            // 
            this.Text_FeFWHMMin.Location = new System.Drawing.Point(87, 80);
            this.Text_FeFWHMMin.Name = "Text_FeFWHMMin";
            this.Text_FeFWHMMin.Size = new System.Drawing.Size(59, 20);
            this.Text_FeFWHMMin.TabIndex = 48;
            this.Text_FeFWHMMin.Tag = "1200";
            this.Text_FeFWHMMin.Text = "1200";
            this.Text_FeFWHMMin.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // Text_FeScaleMin
            // 
            this.Text_FeScaleMin.Location = new System.Drawing.Point(87, 61);
            this.Text_FeScaleMin.Name = "Text_FeScaleMin";
            this.Text_FeScaleMin.Size = new System.Drawing.Size(59, 20);
            this.Text_FeScaleMin.TabIndex = 45;
            this.Text_FeScaleMin.Tag = "0";
            this.Text_FeScaleMin.Text = "0";
            this.Text_FeScaleMin.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(44, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 44;
            this.label10.Text = "FWHM";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(44, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 43;
            this.label11.Text = "Scale";
            // 
            // Text_PLSlopeMax
            // 
            this.Text_PLSlopeMax.Location = new System.Drawing.Point(218, 168);
            this.Text_PLSlopeMax.Name = "Text_PLSlopeMax";
            this.Text_PLSlopeMax.Size = new System.Drawing.Size(59, 20);
            this.Text_PLSlopeMax.TabIndex = 59;
            this.Text_PLSlopeMax.Tag = "3";
            this.Text_PLSlopeMax.Text = "3";
            this.Text_PLSlopeMax.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // Text_PLScaleMax
            // 
            this.Text_PLScaleMax.Location = new System.Drawing.Point(218, 149);
            this.Text_PLScaleMax.Name = "Text_PLScaleMax";
            this.Text_PLScaleMax.Size = new System.Drawing.Size(59, 20);
            this.Text_PLScaleMax.TabIndex = 56;
            this.Text_PLScaleMax.Tag = "100";
            this.Text_PLScaleMax.Text = "100";
            this.Text_PLScaleMax.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // Text_PLSlopeVal
            // 
            this.Text_PLSlopeVal.Location = new System.Drawing.Point(153, 168);
            this.Text_PLSlopeVal.Name = "Text_PLSlopeVal";
            this.Text_PLSlopeVal.Size = new System.Drawing.Size(59, 20);
            this.Text_PLSlopeVal.TabIndex = 58;
            this.Text_PLSlopeVal.Tag = "0";
            this.Text_PLSlopeVal.Text = "0";
            this.Text_PLSlopeVal.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // Text_PLScaleVal
            // 
            this.Text_PLScaleVal.Location = new System.Drawing.Point(153, 149);
            this.Text_PLScaleVal.Name = "Text_PLScaleVal";
            this.Text_PLScaleVal.Size = new System.Drawing.Size(59, 20);
            this.Text_PLScaleVal.TabIndex = 55;
            this.Text_PLScaleVal.Tag = "0.001";
            this.Text_PLScaleVal.Text = "0.001";
            this.Text_PLScaleVal.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // Text_PLSlopeMin
            // 
            this.Text_PLSlopeMin.Location = new System.Drawing.Point(87, 168);
            this.Text_PLSlopeMin.Name = "Text_PLSlopeMin";
            this.Text_PLSlopeMin.Size = new System.Drawing.Size(59, 20);
            this.Text_PLSlopeMin.TabIndex = 57;
            this.Text_PLSlopeMin.Tag = "-5";
            this.Text_PLSlopeMin.Text = "-5";
            this.Text_PLSlopeMin.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // Text_PLScaleMin
            // 
            this.Text_PLScaleMin.Location = new System.Drawing.Point(87, 149);
            this.Text_PLScaleMin.Name = "Text_PLScaleMin";
            this.Text_PLScaleMin.Size = new System.Drawing.Size(59, 20);
            this.Text_PLScaleMin.TabIndex = 54;
            this.Text_PLScaleMin.Tag = "0";
            this.Text_PLScaleMin.Text = "0";
            this.Text_PLScaleMin.TextChanged += new System.EventHandler(this.Text_FePLParam_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 171);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Slope";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Scale";
            // 
            // Check_hostdecomp
            // 
            this.Check_hostdecomp.AutoSize = true;
            this.Check_hostdecomp.Location = new System.Drawing.Point(2, 3);
            this.Check_hostdecomp.Name = "Check_hostdecomp";
            this.Check_hostdecomp.Size = new System.Drawing.Size(121, 17);
            this.Check_hostdecomp.TabIndex = 30;
            this.Check_hostdecomp.Tag = "decomposition_host";
            this.Check_hostdecomp.Text = "Host Decomposition";
            this.Check_hostdecomp.UseVisualStyleBackColor = true;
            // 
            // Check_PL
            // 
            this.Check_PL.AutoSize = true;
            this.Check_PL.Location = new System.Drawing.Point(2, 129);
            this.Check_PL.Name = "Check_PL";
            this.Check_PL.Size = new System.Drawing.Size(79, 17);
            this.Check_PL.TabIndex = 31;
            this.Check_PL.Tag = "PL";
            this.Check_PL.Text = "Power Law";
            this.Check_PL.UseVisualStyleBackColor = true;
            // 
            // Check_poly
            // 
            this.Check_poly.AutoSize = true;
            this.Check_poly.Location = new System.Drawing.Point(226, 3);
            this.Check_poly.Name = "Check_poly";
            this.Check_poly.Size = new System.Drawing.Size(76, 17);
            this.Check_poly.TabIndex = 32;
            this.Check_poly.Tag = "poly";
            this.Check_poly.Text = "Polynomial";
            this.Check_poly.UseVisualStyleBackColor = true;
            // 
            // Check_Fe
            // 
            this.Check_Fe.AutoSize = true;
            this.Check_Fe.Location = new System.Drawing.Point(2, 26);
            this.Check_Fe.Name = "Check_Fe";
            this.Check_Fe.Size = new System.Drawing.Size(96, 17);
            this.Check_Fe.TabIndex = 33;
            this.Check_Fe.Tag = "Fe_uv_op";
            this.Check_Fe.Text = "Fe II Emissions";
            this.Check_Fe.UseVisualStyleBackColor = true;
            // 
            // Check_BC
            // 
            this.Check_BC.AutoSize = true;
            this.Check_BC.Location = new System.Drawing.Point(129, 3);
            this.Check_BC.Name = "Check_BC";
            this.Check_BC.Size = new System.Drawing.Size(88, 17);
            this.Check_BC.TabIndex = 34;
            this.Check_BC.Tag = "BC";
            this.Check_BC.Text = "Balmer Conti.";
            this.Check_BC.UseVisualStyleBackColor = true;
            // 
            // VAL_CFTstrength
            // 
            this.VAL_CFTstrength.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.VAL_CFTstrength.Location = new System.Drawing.Point(150, 130);
            this.VAL_CFTstrength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.VAL_CFTstrength.Name = "VAL_CFTstrength";
            this.VAL_CFTstrength.Size = new System.Drawing.Size(50, 20);
            this.VAL_CFTstrength.TabIndex = 37;
            this.VAL_CFTstrength.Tag = "CFT_smooth";
            this.VAL_CFTstrength.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.VAL_CFTstrength.Visible = false;
            // 
            // Label_CFTstrength
            // 
            this.Label_CFTstrength.AutoSize = true;
            this.Label_CFTstrength.Location = new System.Drawing.Point(76, 134);
            this.Label_CFTstrength.Name = "Label_CFTstrength";
            this.Label_CFTstrength.Size = new System.Drawing.Size(68, 13);
            this.Label_CFTstrength.TabIndex = 36;
            this.Label_CFTstrength.Text = "CFT strength";
            this.Label_CFTstrength.Visible = false;
            // 
            // Check_CFT
            // 
            this.Check_CFT.AutoSize = true;
            this.Check_CFT.Location = new System.Drawing.Point(15, 133);
            this.Check_CFT.Name = "Check_CFT";
            this.Check_CFT.Size = new System.Drawing.Size(46, 17);
            this.Check_CFT.TabIndex = 35;
            this.Check_CFT.Tag = "CFT";
            this.Check_CFT.Text = "CFT";
            this.Check_CFT.UseVisualStyleBackColor = true;
            this.Check_CFT.CheckedChanged += new System.EventHandler(this.Check_CFT_CheckedChanged);
            // 
            // Button_CreateFobject
            // 
            this.Button_CreateFobject.Enabled = false;
            this.Button_CreateFobject.Location = new System.Drawing.Point(55, 476);
            this.Button_CreateFobject.Name = "Button_CreateFobject";
            this.Button_CreateFobject.Size = new System.Drawing.Size(110, 23);
            this.Button_CreateFobject.TabIndex = 29;
            this.Button_CreateFobject.Text = "Create Fit Object";
            this.Button_CreateFobject.UseVisualStyleBackColor = true;
            this.Button_CreateFobject.Click += new System.EventHandler(this.Button_CreateFobject_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 451);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 28;
            this.label5.Text = "Conti.";
            // 
            // Option_ConfigConti
            // 
            this.Option_ConfigConti.FormattingEnabled = true;
            this.Option_ConfigConti.Location = new System.Drawing.Point(62, 449);
            this.Option_ConfigConti.Name = "Option_ConfigConti";
            this.Option_ConfigConti.Size = new System.Drawing.Size(185, 21);
            this.Option_ConfigConti.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 424);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "Lines";
            // 
            // Button_SaveFObject
            // 
            this.Button_SaveFObject.Location = new System.Drawing.Point(12, 39);
            this.Button_SaveFObject.Name = "Button_SaveFObject";
            this.Button_SaveFObject.Size = new System.Drawing.Size(88, 23);
            this.Button_SaveFObject.TabIndex = 28;
            this.Button_SaveFObject.Text = "Save Fit Object";
            this.Button_SaveFObject.UseVisualStyleBackColor = true;
            this.Button_SaveFObject.Click += new System.EventHandler(this.Button_SaveFobject_Click);
            // 
            // Config_Main
            // 
            this.Config_Main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Config_Main.EditingSection = null;
            this.Config_Main.Location = new System.Drawing.Point(359, 436);
            this.Config_Main.MaximumSize = new System.Drawing.Size(2000, 253);
            this.Config_Main.MinimumSize = new System.Drawing.Size(100, 253);
            this.Config_Main.Name = "Config_Main";
            this.Config_Main.Path_ConfigFolder = null;
            this.Config_Main.Path_DefaultLines = null;
            this.Config_Main.Size = new System.Drawing.Size(1010, 253);
            this.Config_Main.TabIndex = 26;
            // 
            // WaveDisp_Default
            // 
            this.WaveDisp_Default.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WaveDisp_Default.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WaveDisp_Default.ContinuumWindow = ((System.Collections.Generic.List<int[]>)(resources.GetObject("WaveDisp_Default.ContinuumWindow")));
            this.WaveDisp_Default.EmissionLines = ((System.Collections.Generic.List<int>)(resources.GetObject("WaveDisp_Default.EmissionLines")));
            this.WaveDisp_Default.Location = new System.Drawing.Point(359, 11);
            this.WaveDisp_Default.MaxValue = 100;
            this.WaveDisp_Default.MinValue = 0;
            this.WaveDisp_Default.Name = "WaveDisp_Default";
            this.WaveDisp_Default.Path_ContiFolder = null;
            this.WaveDisp_Default.Preview_Image = null;
            this.WaveDisp_Default.Size = new System.Drawing.Size(1010, 419);
            this.WaveDisp_Default.TabIndex = 25;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 820);
            this.Controls.Add(this.Button_SaveFObject);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Config_Main);
            this.Controls.Add(this.WaveDisp_Default);
            this.Controls.Add(this.Button_ObjectAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Option_Objects);
            this.Controls.Add(this.RichText_Console);
            this.Controls.Add(this.CheckList_FitDataName);
            this.Controls.Add(this.RichText_FitDataValues);
            this.Controls.Add(this.Button_Clear);
            this.Name = "Main";
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.Panel_NormalFitConfig.ResumeLayout(false);
            this.Panel_NormalFitConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VAL_CFTstrength)).EndInit();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Option_ConfigLines;
        private System.Windows.Forms.Button Button_ConfigUpdate;
        private WavelengthLine WaveDisp_Default;
        private ConfigDisplay Config_Main;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox Option_ConfigConti;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Button_CreateFobject;
        private System.Windows.Forms.Button Button_SaveFObject;
        private System.Windows.Forms.NumericUpDown VAL_CFTstrength;
        private System.Windows.Forms.Label Label_CFTstrength;
        private System.Windows.Forms.CheckBox Check_CFT;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel Panel_NormalFitConfig;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox Text_FeOffMax;
        private System.Windows.Forms.TextBox Text_FeOffVal;
        private System.Windows.Forms.TextBox Text_FeOffMin;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox Text_FeFWHMMax;
        private System.Windows.Forms.TextBox Text_FeScaleMax;
        private System.Windows.Forms.TextBox Text_FeFWHMVAL;
        private System.Windows.Forms.TextBox Text_FeScaleVal;
        private System.Windows.Forms.TextBox Text_FeFWHMMin;
        private System.Windows.Forms.TextBox Text_FeScaleMin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Text_PLSlopeMax;
        private System.Windows.Forms.TextBox Text_PLScaleMax;
        private System.Windows.Forms.TextBox Text_PLSlopeVal;
        private System.Windows.Forms.TextBox Text_PLScaleVal;
        private System.Windows.Forms.TextBox Text_PLSlopeMin;
        private System.Windows.Forms.TextBox Text_PLScaleMin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox Check_hostdecomp;
        private System.Windows.Forms.CheckBox Check_PL;
        private System.Windows.Forms.CheckBox Check_poly;
        private System.Windows.Forms.CheckBox Check_Fe;
        private System.Windows.Forms.CheckBox Check_BC;
    }
}