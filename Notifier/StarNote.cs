using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Resources;
using System.Reflection;

namespace Notifier
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class StarNote : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private double MAX_TRANSPARENCY = 1.0;
		private System.ComponentModel.Container components = null;
		private int timerTime = 3000, timerCount = 0;
		private Timer timer = new Timer(), visibleTimer = new Timer();
		private bool visible = false;
		private System.Windows.Forms.Panel panel1;
		private string text = "";
		private System.Windows.Forms.Label label1;
		private Stream stream;
		private String[] types = { "star", "update", "mail", "agent" };

		public StarNote( string textIn, string type, double maxTransparency, int notificationTime )
		{
			this.text = textIn;
			MAX_TRANSPARENCY = maxTransparency;
			

			switch( type )
			{
				case "update":
					stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Notifier.images.update.png");
					break;
				case "mail":
					stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Notifier.images.mail.png");
					break;
				case "agent":
					stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Notifier.images.agent.png");
					break;
				default:
					stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Notifier.images.star.png");
					break;
			}
			
			visibleTimer.Tick += new EventHandler( visibleTimerTick );
			visibleTimer.Interval = this.timerTime;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.SetVisibleCore(true);
			this.panel1.BackgroundImage = new System.Drawing.Bitmap(stream);
			this.label1.Text = this.text;

			timer.Tick += new EventHandler( timerTick );
			timer.Interval = 50;
			timer.Start();
		}

		public StarNote( string textIn ) : this( textIn, "star", 1.0, 3000 ) {}
		public StarNote( string textIn, string type ) : this( textIn, type, 1.0, 3000 ) {}
		public StarNote( string textIn, string type, double maxTransparency ) : this( textIn, type, maxTransparency, 3000 ) {}

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

		private void timerTick(object sender, System.EventArgs e )
		{
			if (visible == true) // fade out
			{    
				this.Opacity -= (timer.Interval / 1000.0);
				//this.SetDesktopLocation(Screen.getWorkingArea(this).Width - 275, Screen.getWorkingArea(this).Height - 100);

				// Should we continue to fade?
				if (this.Opacity > 0)
					timer.Enabled = true;
				else
				{            
					timer.Enabled = false;
					Close();
				} // End else we should close the form.

			} 
			else //fade in
			{
				Opacity += ( timer.Interval / 1000.0 );
				timer.Enabled = ( Opacity < MAX_TRANSPARENCY );
				visible = ( Opacity >= MAX_TRANSPARENCY );
				visibleTimer.Enabled = ( Opacity >= MAX_TRANSPARENCY );
			} // End else we should fade out.
		}

		private void visibleTimerTick(object sender, System.EventArgs e )
		{
			// This is so the first timer tick is ignored
			if(timerCount <= 0 )
			{
				timerCount++;
			}
			else
			{
				timerCount = 0;
				timer.Start();
				visibleTimer.Stop();
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(275, 100);
			this.panel1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(88, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(176, 80);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// StarNote
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(275, 100);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StarNote";
			this.Opacity = 0;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Notifier";
			this.TopMost = true;
			this.TransparencyKey = System.Drawing.Color.Black;
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
