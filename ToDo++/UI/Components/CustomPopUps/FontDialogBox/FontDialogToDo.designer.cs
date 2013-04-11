namespace ToDo
{
    partial class FontDialogToDo
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.previewLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.font = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fontSelection = new FontControl.CGFontCombo();
            this.sizeSelection = new ToDo.SizeComboBox();
            this.colorSelection = new ColorComboTestApp.ColorComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.DimGray;
            this.cancelButton.Location = new System.Drawing.Point(5, 100);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(169, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.ForeColor = System.Drawing.Color.DimGray;
            this.okButton.Location = new System.Drawing.Point(182, 100);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(162, 23);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // previewLabel
            // 
            this.previewLabel.AutoSize = true;
            this.previewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.previewLabel.Location = new System.Drawing.Point(10, 69);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Size = new System.Drawing.Size(124, 24);
            this.previewLabel.TabIndex = 10;
            this.previewLabel.Text = "Preview Here";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(157, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(229, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "Color";
            // 
            // font
            // 
            this.font.AutoSize = true;
            this.font.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.font.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.font.Location = new System.Drawing.Point(2, 12);
            this.font.Name = "font";
            this.font.Size = new System.Drawing.Size(45, 21);
            this.font.TabIndex = 5;
            this.font.Text = "Font";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.fontSelection);
            this.groupBox1.Controls.Add(this.sizeSelection);
            this.groupBox1.Controls.Add(this.colorSelection);
            this.groupBox1.Controls.Add(this.font);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 64);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // fontSelection
            // 
            this.fontSelection.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fontSelection.ForeColor = System.Drawing.Color.Black;
            this.fontSelection.Location = new System.Drawing.Point(6, 37);
            this.fontSelection.Name = "fontSelection";
            this.fontSelection.Size = new System.Drawing.Size(149, 21);
            this.fontSelection.TabIndex = 0;
            this.fontSelection.Text = "cgFontCombo1";
            // 
            // sizeSelection
            // 
            this.sizeSelection.BackColor = System.Drawing.Color.WhiteSmoke;
            this.sizeSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sizeSelection.FormattingEnabled = true;
            this.sizeSelection.Items.AddRange(new object[] {
            8,
            9,
            10,
            11,
            12,
            13,
            14,
            8,
            9,
            10,
            11,
            12,
            13,
            14});
            this.sizeSelection.Location = new System.Drawing.Point(161, 37);
            this.sizeSelection.Name = "sizeSelection";
            this.sizeSelection.Size = new System.Drawing.Size(68, 21);
            this.sizeSelection.TabIndex = 4;
            this.sizeSelection.SelectionChangeCommitted += new System.EventHandler(this.sizeComboBox1_SelectionChangeCommitted);
            // 
            // colorSelection
            // 
            this.colorSelection.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colorSelection.Extended = false;
            this.colorSelection.Location = new System.Drawing.Point(233, 36);
            this.colorSelection.Name = "colorSelection";
            this.colorSelection.SelectedColor = System.Drawing.Color.Black;
            this.colorSelection.Size = new System.Drawing.Size(103, 23);
            this.colorSelection.TabIndex = 3;
            this.colorSelection.ColorChanged += new ColorComboTestApp.ColorChangedHandler(this.colorSelection_ColorChanged);
            // 
            // FontDialogToDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(348, 129);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.previewLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FontDialogToDo";
            this.Text = "Font Dialog";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label previewLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label font;
        private ColorComboTestApp.ColorComboBox colorSelection;
        private SizeComboBox sizeSelection;
        private FontControl.CGFontCombo fontSelection;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}

