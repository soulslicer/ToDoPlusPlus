using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Zzzz;

namespace TestRangeBar
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private Zzzz.ZzzzRangeBar zzzzRangeBar1;
		private System.Windows.Forms.TextBox textBoxMinimum;
		private System.Windows.Forms.TextBox textBoxMaximum;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBoxRangeMin;
		private System.Windows.Forms.TextBox textBoxRangeMax;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonInnerColor;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Button buttonSetMinMax;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown numericUpDownDivisionNum;
		private System.Windows.Forms.Button buttonSetRange;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.RadioButton radioButton4;
		private System.Windows.Forms.RadioButton radioButton5;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TrackBar trackBar2;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TrackBar trackBar3;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBox1;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBoxMinimum = new System.Windows.Forms.TextBox();
            this.textBoxMaximum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSetMinMax = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSetRange = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRangeMax = new System.Windows.Forms.TextBox();
            this.textBoxRangeMin = new System.Windows.Forms.TextBox();
            this.buttonInnerColor = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownDivisionNum = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.zzzzRangeBar1 = new Zzzz.ZzzzRangeBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDivisionNum)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxMinimum
            // 
            this.textBoxMinimum.Location = new System.Drawing.Point(64, 24);
            this.textBoxMinimum.Name = "textBoxMinimum";
            this.textBoxMinimum.Size = new System.Drawing.Size(64, 20);
            this.textBoxMinimum.TabIndex = 1;
            // 
            // textBoxMaximum
            // 
            this.textBoxMaximum.Location = new System.Drawing.Point(64, 48);
            this.textBoxMaximum.Name = "textBoxMaximum";
            this.textBoxMaximum.Size = new System.Drawing.Size(64, 20);
            this.textBoxMaximum.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Minimum";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Maximum";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxMaximum);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxMinimum);
            this.groupBox1.Controls.Add(this.buttonSetMinMax);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 128);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Total Range";
            // 
            // buttonSetMinMax
            // 
            this.buttonSetMinMax.Location = new System.Drawing.Point(24, 80);
            this.buttonSetMinMax.Name = "buttonSetMinMax";
            this.buttonSetMinMax.Size = new System.Drawing.Size(96, 24);
            this.buttonSetMinMax.TabIndex = 8;
            this.buttonSetMinMax.Text = "Set";
            this.buttonSetMinMax.Click += new System.EventHandler(this.buttonSetMinMax_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSetRange);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxRangeMax);
            this.groupBox2.Controls.Add(this.textBoxRangeMin);
            this.groupBox2.Location = new System.Drawing.Point(160, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(144, 128);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Range";
            // 
            // buttonSetRange
            // 
            this.buttonSetRange.Location = new System.Drawing.Point(24, 80);
            this.buttonSetRange.Name = "buttonSetRange";
            this.buttonSetRange.Size = new System.Drawing.Size(96, 24);
            this.buttonSetRange.TabIndex = 7;
            this.buttonSetRange.Text = "Set";
            this.buttonSetRange.Click += new System.EventHandler(this.buttonSetRange_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Maximum";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Minimum";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxRangeMax
            // 
            this.textBoxRangeMax.Location = new System.Drawing.Point(64, 48);
            this.textBoxRangeMax.Name = "textBoxRangeMax";
            this.textBoxRangeMax.Size = new System.Drawing.Size(64, 20);
            this.textBoxRangeMax.TabIndex = 1;
            // 
            // textBoxRangeMin
            // 
            this.textBoxRangeMin.Location = new System.Drawing.Point(64, 24);
            this.textBoxRangeMin.Name = "textBoxRangeMin";
            this.textBoxRangeMin.Size = new System.Drawing.Size(64, 20);
            this.textBoxRangeMin.TabIndex = 0;
            // 
            // buttonInnerColor
            // 
            this.buttonInnerColor.Location = new System.Drawing.Point(448, 16);
            this.buttonInnerColor.Name = "buttonInnerColor";
            this.buttonInnerColor.Size = new System.Drawing.Size(120, 24);
            this.buttonInnerColor.TabIndex = 7;
            this.buttonInnerColor.Text = "Inner Color";
            this.buttonInnerColor.Click += new System.EventHandler(this.buttonInnerColor_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(448, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "DivisionNum";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownDivisionNum
            // 
            this.numericUpDownDivisionNum.Location = new System.Drawing.Point(520, 48);
            this.numericUpDownDivisionNum.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownDivisionNum.Name = "numericUpDownDivisionNum";
            this.numericUpDownDivisionNum.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownDivisionNum.TabIndex = 10;
            this.numericUpDownDivisionNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownDivisionNum.TextChanged += new System.EventHandler(this.OnDivisionNumTextChanged);
            this.numericUpDownDivisionNum.ValueChanged += new System.EventHandler(this.OnDivisionNumValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton5);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Location = new System.Drawing.Point(312, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(112, 72);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Scale Orientation";
            // 
            // radioButton5
            // 
            this.radioButton5.Location = new System.Drawing.Point(20, 48);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(56, 16);
            this.radioButton5.TabIndex = 16;
            this.radioButton5.Text = "Both";
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(20, 32);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(64, 16);
            this.radioButton2.TabIndex = 15;
            this.radioButton2.Text = "Bottom";
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.Location = new System.Drawing.Point(20, 16);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(80, 16);
            this.radioButton1.TabIndex = 14;
            this.radioButton1.Text = "Top";
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton4);
            this.groupBox4.Controls.Add(this.radioButton3);
            this.groupBox4.Location = new System.Drawing.Point(312, 80);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(112, 56);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bar Orientation";
            // 
            // radioButton4
            // 
            this.radioButton4.Location = new System.Drawing.Point(16, 32);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(64, 16);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.Text = "vertical";
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.Location = new System.Drawing.Point(16, 16);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(72, 16);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.Text = "horizontal";
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(672, 8);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 16;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(592, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 16);
            this.label6.TabIndex = 17;
            this.label6.Text = "Bar Height";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(672, 56);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(104, 45);
            this.trackBar2.TabIndex = 18;
            this.trackBar2.TickFrequency = 2;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(592, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = "Mark Height";
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(672, 112);
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(104, 45);
            this.trackBar3.TabIndex = 20;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(592, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "Tick Height";
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(448, 100);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 24);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "enable";
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // zzzzRangeBar1
            // 
            this.zzzzRangeBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zzzzRangeBar1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.zzzzRangeBar1.DivisionNum = 10;
            this.zzzzRangeBar1.HeightOfBar = 8;
            this.zzzzRangeBar1.HeightOfMark = 24;
            this.zzzzRangeBar1.HeightOfTick = 6;
            this.zzzzRangeBar1.InnerColor = System.Drawing.Color.Red;
            this.zzzzRangeBar1.Location = new System.Drawing.Point(8, 160);
            this.zzzzRangeBar1.Name = "zzzzRangeBar1";
            this.zzzzRangeBar1.Orientation = Zzzz.ZzzzRangeBar.RangeBarOrientation.horizontal;
            this.zzzzRangeBar1.RangeMaximum = 5;
            this.zzzzRangeBar1.RangeMinimum = 3;
            this.zzzzRangeBar1.ScaleOrientation = Zzzz.ZzzzRangeBar.TopBottomOrientation.bottom;
            this.zzzzRangeBar1.Size = new System.Drawing.Size(304, 372);
            this.zzzzRangeBar1.TabIndex = 0;
            this.zzzzRangeBar1.TotalMaximum = 20;
            this.zzzzRangeBar1.TotalMinimum = 0;
            this.zzzzRangeBar1.RangeChanged += new Zzzz.ZzzzRangeBar.RangeChangedEventHandler(this.OnRangeChanged);
            this.zzzzRangeBar1.RangeChanging += new Zzzz.ZzzzRangeBar.RangeChangedEventHandler(this.OnRangeChanging);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(776, 533);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.numericUpDownDivisionNum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonInnerColor);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.zzzzRangeBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(448, 248);
            this.Name = "Form1";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDivisionNum)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			zzzzRangeBar1.SetRangeLimit(-400,500);
			zzzzRangeBar1.SelectRange( 100,200 );	

			buttonInnerColor.BackColor = zzzzRangeBar1.InnerColor;
			buttonInnerColor.ForeColor = Color.FromArgb(Color.White.R - buttonInnerColor.BackColor.R,
														Color.White.G - buttonInnerColor.BackColor.G,
														Color.White.B - buttonInnerColor.BackColor.B);
			
			textBoxMinimum.Text = zzzzRangeBar1.TotalMinimum.ToString();
			textBoxMaximum.Text = zzzzRangeBar1.TotalMaximum.ToString();

			numericUpDownDivisionNum.Value = zzzzRangeBar1.DivisionNum;

			textBoxRangeMin.Text = this.zzzzRangeBar1.RangeMinimum.ToString();		
			textBoxRangeMax.Text = this.zzzzRangeBar1.RangeMaximum.ToString();		

			if( this.zzzzRangeBar1.ScaleOrientation == ZzzzRangeBar.TopBottomOrientation.bottom )
				radioButton2.Checked = true;
			else
				radioButton1.Checked = true;

			if( this.zzzzRangeBar1.Orientation == ZzzzRangeBar.RangeBarOrientation.horizontal )
				radioButton3.Checked = true;
			else
				radioButton4.Checked = true;

			trackBar1.SetRange(1,zzzzRangeBar1.HeightOfMark);
			trackBar1.Value = zzzzRangeBar1.HeightOfBar;

			trackBar2.SetRange(zzzzRangeBar1.HeightOfBar,64);
			trackBar2.Value = zzzzRangeBar1.HeightOfMark;

			trackBar3.SetRange(1,zzzzRangeBar1.HeightOfBar);
			trackBar3.Value = zzzzRangeBar1.HeightOfTick;
		}

		private void buttonInnerColor_Click(object sender, System.EventArgs e)
		{
			colorDialog1.Color = zzzzRangeBar1.InnerColor;
			if( colorDialog1.ShowDialog(this)==DialogResult.OK )
			{
				buttonInnerColor.BackColor = colorDialog1.Color;
				buttonInnerColor.ForeColor = Color.FromArgb(Color.White.R - buttonInnerColor.BackColor.R,
															Color.White.G - buttonInnerColor.BackColor.G,
															Color.White.B - buttonInnerColor.BackColor.B);
				zzzzRangeBar1.InnerColor = colorDialog1.Color;
			}
		}

		private void buttonSetMinMax_Click(object sender, System.EventArgs e)
		{
			int Mini,Maxi;

			Mini = int.Parse(textBoxMinimum.Text);
			Maxi = int.Parse(textBoxMaximum.Text);
			zzzzRangeBar1.TotalMinimum = Mini;
			zzzzRangeBar1.TotalMaximum = Maxi;
			textBoxMinimum.Text = zzzzRangeBar1.TotalMinimum.ToString();
			textBoxMaximum.Text = zzzzRangeBar1.TotalMaximum.ToString();
		}

		private void OnDivisionNumTextChanged(object sender, System.EventArgs e)
		{			
			zzzzRangeBar1.DivisionNum = (int)numericUpDownDivisionNum.Value;
		}

		private void OnDivisionNumValueChanged(object sender, System.EventArgs e)
		{
			zzzzRangeBar1.DivisionNum = (int)numericUpDownDivisionNum.Value;
		}

		private void OnRangeChanged(object sender, System.EventArgs e)
		{
			this.textBoxRangeMin.Text = this.zzzzRangeBar1.RangeMinimum.ToString();		
			this.textBoxRangeMax.Text = this.zzzzRangeBar1.RangeMaximum.ToString();		
			textBoxRangeMin.BackColor = Color.FromKnownColor( KnownColor.ControlLightLight );
			textBoxRangeMax.BackColor = Color.FromKnownColor( KnownColor.ControlLightLight );
		}

		private void OnRangeChanging(object sender, System.EventArgs e)
		{
			this.textBoxRangeMin.Text = this.zzzzRangeBar1.RangeMinimum.ToString();		
			this.textBoxRangeMax.Text = this.zzzzRangeBar1.RangeMaximum.ToString();
			textBoxRangeMin.BackColor = Color.FromKnownColor( KnownColor.ControlLight );
			textBoxRangeMax.BackColor = Color.FromKnownColor( KnownColor.ControlLight );
		}

		private void buttonSetRange_Click(object sender, System.EventArgs e)
		{
			int Mini,Maxi;

			Mini = int.Parse(textBoxRangeMin.Text);
			Maxi = int.Parse(textBoxRangeMax.Text);
			zzzzRangeBar1.SelectRange(Mini,Maxi);
			textBoxRangeMin.Text = this.zzzzRangeBar1.RangeMinimum.ToString();		
			textBoxRangeMax.Text = this.zzzzRangeBar1.RangeMaximum.ToString();	
		}

		private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
		{
			this.zzzzRangeBar1.ScaleOrientation = ZzzzRangeBar.TopBottomOrientation.top;
		}

		private void radioButton2_CheckedChanged(object sender, System.EventArgs e)
		{
			this.zzzzRangeBar1.ScaleOrientation = ZzzzRangeBar.TopBottomOrientation.bottom;
		}

		private void radioButton3_CheckedChanged(object sender, System.EventArgs e)
		{
			this.zzzzRangeBar1.Orientation = ZzzzRangeBar.RangeBarOrientation.horizontal;
		}

		private void radioButton4_CheckedChanged(object sender, System.EventArgs e)
		{
			this.zzzzRangeBar1.Orientation = ZzzzRangeBar.RangeBarOrientation.vertical;
		}

		private void radioButton5_CheckedChanged(object sender, System.EventArgs e)
		{
			this.zzzzRangeBar1.ScaleOrientation = ZzzzRangeBar.TopBottomOrientation.both;
		}

		// Bar Height
		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
			zzzzRangeBar1.HeightOfBar = trackBar1.Value;
		}

		// Mark Height
		private void trackBar2_Scroll(object sender, System.EventArgs e)
		{
			zzzzRangeBar1.HeightOfMark = trackBar2.Value;	
		}

		// Tick Height
		private void trackBar3_Scroll(object sender, System.EventArgs e)
		{
			zzzzRangeBar1.HeightOfTick = trackBar3.Value;	
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			zzzzRangeBar1.Enabled = this.checkBox1.Checked;
		}
	}
}
