namespace ToDo
{
    partial class FontColorSettings
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
            this.textSizeButton = new System.Windows.Forms.Button();
            this.taskDoneColorButton = new System.Windows.Forms.Button();
            this.taskMissedDeadlineColorButton = new System.Windows.Forms.Button();
            this.taskDeadlineDayColor = new System.Windows.Forms.Button();
            this.taskEventColor = new System.Windows.Forms.Button();
            this.previewBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // textSizeButton
            // 
            this.textSizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textSizeButton.BackColor = System.Drawing.Color.Gray;
            this.textSizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.textSizeButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSizeButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.textSizeButton.Location = new System.Drawing.Point(3, 3);
            this.textSizeButton.Name = "textSizeButton";
            this.textSizeButton.Size = new System.Drawing.Size(320, 23);
            this.textSizeButton.TabIndex = 1;
            this.textSizeButton.Text = "Font and Size";
            this.textSizeButton.UseVisualStyleBackColor = false;
            this.textSizeButton.Click += new System.EventHandler(this.textSizeButton_Click);
            this.textSizeButton.MouseEnter += new System.EventHandler(this.textSizeButton_MouseEnter);
            this.textSizeButton.MouseLeave += new System.EventHandler(this.textSizeButton_MouseLeave);
            // 
            // taskDoneColorButton
            // 
            this.taskDoneColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskDoneColorButton.BackColor = System.Drawing.Color.Gray;
            this.taskDoneColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.taskDoneColorButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskDoneColorButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.taskDoneColorButton.Location = new System.Drawing.Point(3, 32);
            this.taskDoneColorButton.Name = "taskDoneColorButton";
            this.taskDoneColorButton.Size = new System.Drawing.Size(320, 23);
            this.taskDoneColorButton.TabIndex = 2;
            this.taskDoneColorButton.Text = "Task Done Color";
            this.taskDoneColorButton.UseVisualStyleBackColor = false;
            this.taskDoneColorButton.Click += new System.EventHandler(this.taskDoneColorButton_Click);
            this.taskDoneColorButton.MouseEnter += new System.EventHandler(this.taskDoneColorButton_MouseEnter);
            this.taskDoneColorButton.MouseLeave += new System.EventHandler(this.taskDoneColorButton_MouseLeave);
            // 
            // taskMissedDeadlineColorButton
            // 
            this.taskMissedDeadlineColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskMissedDeadlineColorButton.BackColor = System.Drawing.Color.Gray;
            this.taskMissedDeadlineColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.taskMissedDeadlineColorButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskMissedDeadlineColorButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.taskMissedDeadlineColorButton.Location = new System.Drawing.Point(3, 61);
            this.taskMissedDeadlineColorButton.Name = "taskMissedDeadlineColorButton";
            this.taskMissedDeadlineColorButton.Size = new System.Drawing.Size(320, 23);
            this.taskMissedDeadlineColorButton.TabIndex = 3;
            this.taskMissedDeadlineColorButton.Text = "Task Missed Deadline Color";
            this.taskMissedDeadlineColorButton.UseVisualStyleBackColor = false;
            this.taskMissedDeadlineColorButton.Click += new System.EventHandler(this.taskDeadlineColorButton_Click);
            this.taskMissedDeadlineColorButton.MouseEnter += new System.EventHandler(this.taskMissedDeadlineColorButton_MouseEnter);
            this.taskMissedDeadlineColorButton.MouseLeave += new System.EventHandler(this.taskMissedDeadlineColorButton_MouseLeave);
            // 
            // taskDeadlineDayColor
            // 
            this.taskDeadlineDayColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskDeadlineDayColor.BackColor = System.Drawing.Color.Gray;
            this.taskDeadlineDayColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.taskDeadlineDayColor.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskDeadlineDayColor.ForeColor = System.Drawing.Color.Gainsboro;
            this.taskDeadlineDayColor.Location = new System.Drawing.Point(3, 90);
            this.taskDeadlineDayColor.Name = "taskDeadlineDayColor";
            this.taskDeadlineDayColor.Size = new System.Drawing.Size(320, 23);
            this.taskDeadlineDayColor.TabIndex = 4;
            this.taskDeadlineDayColor.Text = "Task Nearing Deadline Color";
            this.taskDeadlineDayColor.UseVisualStyleBackColor = false;
            this.taskDeadlineDayColor.Click += new System.EventHandler(this.taskDeadlineDayColor_Click);
            this.taskDeadlineDayColor.MouseEnter += new System.EventHandler(this.taskDeadlineDayColor_MouseEnter);
            this.taskDeadlineDayColor.MouseLeave += new System.EventHandler(this.taskDeadlineDayColor_MouseLeave);
            // 
            // taskEventColor
            // 
            this.taskEventColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskEventColor.BackColor = System.Drawing.Color.Gray;
            this.taskEventColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.taskEventColor.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskEventColor.ForeColor = System.Drawing.Color.Gainsboro;
            this.taskEventColor.Location = new System.Drawing.Point(3, 119);
            this.taskEventColor.Name = "taskEventColor";
            this.taskEventColor.Size = new System.Drawing.Size(320, 23);
            this.taskEventColor.TabIndex = 5;
            this.taskEventColor.Text = "Task Over Color";
            this.taskEventColor.UseVisualStyleBackColor = false;
            this.taskEventColor.Click += new System.EventHandler(this.taskEventColor_Click);
            this.taskEventColor.MouseEnter += new System.EventHandler(this.taskEventColor_MouseEnter);
            this.taskEventColor.MouseLeave += new System.EventHandler(this.taskEventColor_MouseLeave);
            // 
            // previewBox
            // 
            this.previewBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewBox.BackColor = System.Drawing.Color.Gainsboro;
            this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.previewBox.Location = new System.Drawing.Point(3, 148);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(320, 96);
            this.previewBox.TabIndex = 6;
            this.previewBox.Text = "";
            // 
            // FontColorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.previewBox);
            this.Controls.Add(this.taskEventColor);
            this.Controls.Add(this.taskDeadlineDayColor);
            this.Controls.Add(this.taskMissedDeadlineColorButton);
            this.Controls.Add(this.taskDoneColorButton);
            this.Controls.Add(this.textSizeButton);
            this.Name = "FontColorSettings";
            this.Size = new System.Drawing.Size(326, 248);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button textSizeButton;
        private System.Windows.Forms.Button taskDoneColorButton;
        private System.Windows.Forms.Button taskMissedDeadlineColorButton;
        private System.Windows.Forms.Button taskDeadlineDayColor;
        private System.Windows.Forms.Button taskEventColor;
        private System.Windows.Forms.RichTextBox previewBox;

    }
}
