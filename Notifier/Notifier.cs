using System;
using System.Timers;
using System.Windows.Forms;

namespace Notifier
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Notifier : System.Windows.Forms.Form
	{
		//private Timer timer = new Timer();
		private System.Windows.Forms.Button starButton;
		//private bool visible = false;
	
		public Notifier()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.starButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// starButton
			// 
			this.starButton.Location = new System.Drawing.Point(8, 8);
			this.starButton.Name = "starButton";
			this.starButton.Size = new System.Drawing.Size(88, 40);
			this.starButton.TabIndex = 0;
			this.starButton.Text = "Star Note";
			this.starButton.Click += new EventHandler(starButtonHandler);

			// 
			// Notifier
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 59);
			this.Controls.Add(this.starButton);
			this.Name = "Notifier";
			this.ResumeLayout(false);

		}

		private void starButtonHandler(object sender, System.EventArgs e )
		{
			StarNote s = new StarNote( "You have been converted to a Paper Monk!", "agent", .9, 10000 );
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			//Application.EnableVisualStyles();
			Application.Run(new Notifier());
		}
	}
}
