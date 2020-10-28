using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace MontgomerySoftware.Controls
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class ProgressBar : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private long _Value=0, _Max=100, _Min=0;
		private System.Windows.Forms.Panel pnlFill;
		private System.Windows.Forms.Label lblPercent1;
		private System.Windows.Forms.Label lblPercent2;
		
		private Color _DefaultFontColorLow = Color.Black;
		private Color _DefaultFontColorHigh = Color.White;
		private Color _DefaultForeColor = Color.Red;
		private Color _DefaultBackColor = SystemColors.ControlDark;
		private Font _DefaultFont = new Font(System.Drawing.FontFamily.GenericSansSerif,8);
		private byte _LowDecimalPlaces=0, _HighDecimalPlaces=1, _HighLowCutoff=90;

		public ProgressBar()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			RecalcProgress();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblPercent2 = new System.Windows.Forms.Label();
			this.pnlFill = new System.Windows.Forms.Panel();
			this.lblPercent1 = new System.Windows.Forms.Label();
			this.pnlFill.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblPercent2
			// 
			this.lblPercent2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblPercent2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPercent2.ForeColor = System.Drawing.Color.Black;
			this.lblPercent2.Location = new System.Drawing.Point(0, 0);
			this.lblPercent2.Name = "lblPercent2";
			this.lblPercent2.Size = new System.Drawing.Size(150, 24);
			this.lblPercent2.TabIndex = 2;
			this.lblPercent2.Text = "50%";
			this.lblPercent2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlFill
			// 
			this.pnlFill.BackColor = System.Drawing.Color.Red;
			this.pnlFill.Controls.Add(this.lblPercent1);
			this.pnlFill.Location = new System.Drawing.Point(0, 0);
			this.pnlFill.Name = "pnlFill";
			this.pnlFill.Size = new System.Drawing.Size(72, 24);
			this.pnlFill.TabIndex = 4;
			// 
			// lblPercent1
			// 
			this.lblPercent1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPercent1.ForeColor = System.Drawing.Color.White;
			this.lblPercent1.Location = new System.Drawing.Point(0, 0);
			this.lblPercent1.Name = "lblPercent1";
			this.lblPercent1.Size = new System.Drawing.Size(150, 24);
			this.lblPercent1.TabIndex = 5;
			this.lblPercent1.Text = "50%";
			this.lblPercent1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ProgressBar
			// 
			this.Controls.Add(this.pnlFill);
			this.Controls.Add(this.lblPercent2);
			this.Name = "ProgressBar";
			this.Size = new System.Drawing.Size(150, 24);
			this.Resize += new System.EventHandler(this.ProgressBar_Resize);
			this.pnlFill.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void RecalcProgress()
		{
			double dblPercent;
			
			try
			{
				dblPercent = Convert.ToDouble(_Value) / Convert.ToDouble(_Max);
				pnlFill.Width = Convert.ToInt16(this.Width * dblPercent);
				pnlFill.Height=this.Height;

				// Here we want to apply the right formatting based on high/low settings
				string strFormatString = "##0";
				string strExtraFormatting = "";
				if(dblPercent * 100 < _HighLowCutoff)
				{
					for(int i=0;i<_LowDecimalPlaces;i++)
						strExtraFormatting += "0";
				}
				else
				{
					for(int i=0;i<_HighDecimalPlaces;i++)
						strExtraFormatting += "0";
				}
				if(strExtraFormatting.Length>0)
					strExtraFormatting = "." + strExtraFormatting;
				strFormatString += strExtraFormatting + "%";
				lblPercent2.Text = dblPercent.ToString(strFormatString);


				lblPercent2.Height = this.Height;
				lblPercent2.Width = this.Width;
				lblPercent1.Text = lblPercent2.Text;
				lblPercent1.Width = this.Width;
				lblPercent1.Height = this.Height;
			}
			catch(Exception ex)
			{
				// Do nothing, this happens when the value passed in is zero
				// MessageBox.Show(ex.ToString());
			}
		}

		private void ProgressBar_Resize(object sender, System.EventArgs e)
		{
			RecalcProgress();
		}

		[Description("Gets or sets the current value")]
		[Browsable(true)] 
		[DefaultValue(0)]
		[Category("Behavior")]
		public long Value
		{
			get {return _Value;}
			set 
			{
				if(value>_Max)
					_Value = _Max;
				else if(value<_Min)
					_Value = _Min;
				else
					_Value = value;
				
				RecalcProgress();
			}
		}
		[Description("Gets or sets the maximum value")]
		[Browsable(true)] 
		[DefaultValue(100)]
		[Category("Behavior")]
		public long Maximum
		{
			get {return _Max;}
			set 
			{
				if(value<=0)
					throw new Exception("Max must be greater than zero.");
				_Max = value;
				RecalcProgress();
			}
		}
		[Description("Gets or sets the minimum value")]
		[Browsable(true)] 
		[DefaultValue(0)]
		[Category("Behavior")]
		public long Minimum
		{
			get {return _Min;}
			set 
			{
				if(value<0)
					throw new Exception("Min may not be less than zero.");
				_Min = value;
				RecalcProgress();
			}
		}
		[Description("Gets or sets the font of the progress text")]
		[Browsable(true)] 
		[Category("Appearance")]
		public override Font Font
		{
			get
			{
				return lblPercent1.Font;
			}
			set
			{
				lblPercent1.Font = value;
				lblPercent2.Font = value;
			}
		}
		public override void ResetFont()
		{
			Font = _DefaultFont;
		}
		public bool ShouldSerializeFont()
		{
			return Font != _DefaultFont;
		}


		[Description("Gets or sets the fill color")]
		[Browsable(true)] 
		[Category("Appearance")]
		public override Color ForeColor
		{
			get
			{
				return pnlFill.BackColor;
			}
			set
			{
				pnlFill.BackColor = value;
			}
		}
		public override void ResetForeColor()
		{
			ForeColor = _DefaultForeColor;
		}
		public bool ShouldSerializeForeColor()
		{
			return ForeColor != _DefaultForeColor;
		}


		[Description("Gets or sets the color to use while the progress is below 50%")]
		[Browsable(true)] 
		[Category("Appearance")]
		public Color FontColorLow
		{
			get
			{
				return lblPercent2.ForeColor;
			}
			set
			{
				lblPercent2.ForeColor = value;
			}
		}
		public void ResetFontColorLow()
		{
			FontColorLow = _DefaultFontColorLow;
		}
		public bool ShouldSerializeFontColorLow()
		{
			return FontColorLow != _DefaultFontColorLow;
		}

		[Description("Gets or sets the color to use as the progress passes 50%")]
		[Browsable(true)] 
		[Category("Appearance")]
		public Color FontColorHigh
		{
			get
			{
				return lblPercent1.ForeColor;
			}
			set
			{
				lblPercent1.ForeColor = value;
			}
		}
		public void ResetFontColorHigh()
		{
			FontColorHigh = _DefaultFontColorHigh;
		}
		public bool ShouldSerializeFontColorHigh()
		{
			return FontColorHigh != _DefaultFontColorHigh;
		}

		[Description("Gets or sets whether or not the progress text should show")]
		[Browsable(true)]
		[DefaultValue(true)]
		[Category("Appearance")]
		public bool TextVisible
		{
			get
			{
				return lblPercent1.Visible;
			}
			set
			{
				lblPercent1.Visible = value;
				lblPercent2.Visible = value;
			}
		}
		[Description("Gets or sets the number of decimal places to show when progress is below HighLowCutoff")]
		[Browsable(true)] 
		[DefaultValue(0)]
		[Category("Appearance")]
		public byte LowTextDecimalPlaces
		{
			get {return _LowDecimalPlaces;}
			set 
			{
				if(value>5)
					_LowDecimalPlaces = 5;
				else if(value<0)
					_LowDecimalPlaces = 0;
				else
					_LowDecimalPlaces = value;
				
				RecalcProgress();
			}
		}
		[Description("Gets or sets the number of decimal places to show when progress is at or above HighLowCutoff")]
		[Browsable(true)] 
		[DefaultValue(1)]
		[Category("Appearance")]
		public byte HighTextDecimalPlaces
		{
			get {return _HighDecimalPlaces;}
			set 
			{
				if(value>5)
					_HighDecimalPlaces = 5;
				else if(value<0)
					_HighDecimalPlaces = 0;
				else
					_HighDecimalPlaces = value;
				
				RecalcProgress();
			}
		}

		[Description("Gets or sets the point at which the HighTextDecimalPlaces replaces LowTextDecimalPlaces when formatting the progress text")]
		[Browsable(true)] 
		[DefaultValue(0)]
		[Category("Appearance")]
		public byte HighLowCutoff
		{
			get {return _HighLowCutoff;}
			set 
			{
				if(value>100)
					_HighLowCutoff = 100;
				else if(value<0)
					_HighLowCutoff = 0;
				else
					_HighLowCutoff = value;
				
				RecalcProgress();
			}
		}

	}
}
