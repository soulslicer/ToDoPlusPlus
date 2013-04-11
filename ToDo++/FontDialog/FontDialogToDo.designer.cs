namespace TestFontControls
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
            this.Font = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.previewLabel = new System.Windows.Forms.Label();
            this.sizeSelection = new TestFontControls.SizeComboBox();
            this.colorSelection = new ColorComboTestApp.ColorComboBox();
            this.fontSelection = new FontControl.CGFontCombo();
            this.SuspendLayout();
            // 
            // Font
            // 
            this.Font.AutoSize = true;
            this.Font.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Font.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Font.Location = new System.Drawing.Point(12, 9);
            this.Font.Name = "Font";
            this.Font.Size = new System.Drawing.Size(42, 20);
            this.Font.TabIndex = 5;
            this.Font.Text = "Font";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(163, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(237, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Color";
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.LightCyan;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cancelButton.Location = new System.Drawing.Point(12, 100);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(162, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.LightCyan;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.ForeColor = System.Drawing.Color.DodgerBlue;
            this.okButton.Location = new System.Drawing.Point(182, 100);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(162, 23);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            // 
            // previewLabel
            // 
            this.previewLabel.AutoSize = true;
            this.previewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.previewLabel.Location = new System.Drawing.Point(12, 67);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Size = new System.Drawing.Size(124, 24);
            this.previewLabel.TabIndex = 10;
            this.previewLabel.Text = "Preview Here";
            // 
            // sizeSelection
            // 
            this.sizeSelection.BackColor = System.Drawing.Color.PowderBlue;
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
            this.sizeSelection.Location = new System.Drawing.Point(167, 35);
            this.sizeSelection.Name = "sizeSelection";
            this.sizeSelection.Size = new System.Drawing.Size(68, 21);
            this.sizeSelection.TabIndex = 4;
            this.sizeSelection.SelectionChangeCommitted += new System.EventHandler(this.sizeComboBox1_SelectionChangeCommitted);
            // 
            // colorSelection
            // 
            this.colorSelection.BackColor = System.Drawing.Color.PowderBlue;
            this.colorSelection.Extended = false;
            this.colorSelection.Location = new System.Drawing.Point(241, 33);
            this.colorSelection.Name = "colorSelection";
            this.colorSelection.SelectedColor = System.Drawing.Color.Black;
            this.colorSelection.Size = new System.Drawing.Size(103, 23);
            this.colorSelection.TabIndex = 3;
            this.colorSelection.ColorChanged += new ColorComboTestApp.ColorChangedHandler(this.colorSelection_ColorChanged);
            // 
            // fontSelection
            // 
            this.fontSelection.BackColor = System.Drawing.Color.PowderBlue;
            this.fontSelection.ForeColor = System.Drawing.Color.Black;
            this.fontSelection.Location = new System.Drawing.Point(12, 35);
            this.fontSelection.Name = "fontSelection";
            this.fontSelection.Size = new System.Drawing.Size(149, 21);
            this.fontSelection.TabIndex = 0;
            this.fontSelection.Text = "cgFontCombo1";
            this.fontSelection.FontChanged += new System.EventHandler(this.cgFontCombo1_FontChanged);
            this.fontSelection.Click += new System.EventHandler(this.fontSelection_Click);
            this.fontSelection.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fontSelection_MouseClick);
            this.fontSelection.MouseLeave += new System.EventHandler(this.fontSelection_MouseLeave);
            // 
            // FontDialogToDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(348, 129);
            this.Controls.Add(this.previewLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Font);
            this.Controls.Add(this.sizeSelection);
            this.Controls.Add(this.colorSelection);
            this.Controls.Add(this.fontSelection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FontDialogToDo";
            this.Text = "Font Dialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontControl.CGFontCombo fontSelection;
        private ColorComboTestApp.ColorComboBox colorSelection;
        private SizeComboBox sizeSelection;
        private System.Windows.Forms.Label Font;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label previewLabel;

    }
}

