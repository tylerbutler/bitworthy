using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.IO;
using iTunesLib;

namespace NowPlaying
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.NotifyIcon trayIcon;
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem exitMenu;
		private System.Windows.Forms.Label usernameLabel;
		private System.Windows.Forms.Button loginButton;
		private System.Windows.Forms.Label passwordLabel;
		private System.Windows.Forms.Label loggedInAsLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox username;
		private System.Windows.Forms.TextBox password;
		private System.Windows.Forms.Label uid;
		private System.Windows.Forms.Label loggedInAs;

		// iTunes data members
		//private iTunesLib.IiTunes iTunes = new iTunesLib.iTunesAppClass();
		iTunesApp iTunes = new iTunesAppClass();
		private System.Windows.Forms.LinkLabel linkLabel1;

		// other data members
		private bool isLoggedIn = false;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			new StarNote( "Tuniverse Client for iTunes started...", "star", 1.0, 200 );

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			iTunes.OnPlayerPlayEvent += new iTunesLib._IiTunesEvents_OnPlayerPlayEventEventHandler(playEvent);
			iTunes.OnPlayerPlayingTrackChangedEvent +=  new iTunesLib._IiTunesEvents_OnPlayerPlayingTrackChangedEventEventHandler(trackChangedEvent);
		}

		private void trackChangedEvent( object o )
		{
			playEvent( o );
		}

		private void playEvent( object o )
		{
			// Check if we're logged in
			if( isLoggedIn )
			{
				IITTrack track = (IITTrack)o;
				string s = "user_id=" + uid.Text.Trim() + "&song=" + track.Name + "&artist=" + track.Artist + "&album=" + track.Album + 
					"&genre=" + track.Genre;
				string url = "http://omega.cs.iit.edu/~butltyl/tuniverse/postmusic.php";
				HttpWebRequest post = (HttpWebRequest)WebRequest.Create( url );
				post.Method = "POST";
				post.ContentLength = s.Length;
				post.ContentType = "application/x-www-form-urlencoded";
				post.KeepAlive = false;

				StreamWriter writer = new StreamWriter(post.GetRequestStream());

				try
				{
					writer.Write(s);
				}
				catch( Exception ex )
				{
					Console.WriteLine("Error: " + ex.Message);
					Dispose();
				}
				writer.Close();

				StreamReader reader = new StreamReader(post.GetResponse().GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
				string response = reader.ReadToEnd();
				reader.Close();
				
				Console.WriteLine(response);
				//new StarNote( response, "star", 1.0, 200 );
			}
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.username = new System.Windows.Forms.TextBox();
			this.usernameLabel = new System.Windows.Forms.Label();
			this.loginButton = new System.Windows.Forms.Button();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.exitMenu = new System.Windows.Forms.MenuItem();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.password = new System.Windows.Forms.TextBox();
			this.loggedInAsLabel = new System.Windows.Forms.Label();
			this.loggedInAs = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.uid = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// username
			// 
			this.username.Location = new System.Drawing.Point(88, 8);
			this.username.Name = "username";
			this.username.Size = new System.Drawing.Size(120, 20);
			this.username.TabIndex = 0;
			this.username.Text = "";
			// 
			// usernameLabel
			// 
			this.usernameLabel.Location = new System.Drawing.Point(16, 8);
			this.usernameLabel.Name = "usernameLabel";
			this.usernameLabel.Size = new System.Drawing.Size(64, 24);
			this.usernameLabel.TabIndex = 1;
			this.usernameLabel.Text = "Username:";
			// 
			// loginButton
			// 
			this.loginButton.Location = new System.Drawing.Point(16, 72);
			this.loginButton.Name = "loginButton";
			this.loginButton.Size = new System.Drawing.Size(312, 24);
			this.loginButton.TabIndex = 2;
			this.loginButton.Text = "Login";
			this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
			// 
			// trayIcon
			// 
			this.trayIcon.ContextMenu = this.contextMenu;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "Now Playing";
			this.trayIcon.Visible = true;
			this.trayIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trayIcon_DoubleClick);
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.exitMenu});
			// 
			// exitMenu
			// 
			this.exitMenu.Index = 0;
			this.exitMenu.Text = "Exit";
			this.exitMenu.Click += new System.EventHandler(this.exitMenu_Click);
			// 
			// passwordLabel
			// 
			this.passwordLabel.Location = new System.Drawing.Point(16, 32);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new System.Drawing.Size(64, 24);
			this.passwordLabel.TabIndex = 1;
			this.passwordLabel.Text = "Password:";
			// 
			// password
			// 
			this.password.Location = new System.Drawing.Point(88, 32);
			this.password.MaxLength = 20;
			this.password.Name = "password";
			this.password.PasswordChar = '*';
			this.password.Size = new System.Drawing.Size(120, 20);
			this.password.TabIndex = 0;
			this.password.Text = "";
			// 
			// loggedInAsLabel
			// 
			this.loggedInAsLabel.Location = new System.Drawing.Point(16, 112);
			this.loggedInAsLabel.Name = "loggedInAsLabel";
			this.loggedInAsLabel.Size = new System.Drawing.Size(72, 24);
			this.loggedInAsLabel.TabIndex = 1;
			this.loggedInAsLabel.Text = "Logged in as:";
			// 
			// loggedInAs
			// 
			this.loggedInAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.loggedInAs.Location = new System.Drawing.Point(96, 104);
			this.loggedInAs.Name = "loggedInAs";
			this.loggedInAs.Size = new System.Drawing.Size(112, 32);
			this.loggedInAs.TabIndex = 1;
			this.loggedInAs.Text = "Not logged in...";
			this.loggedInAs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(216, 112);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "UID:";
			// 
			// uid
			// 
			this.uid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.uid.Location = new System.Drawing.Point(256, 104);
			this.uid.Name = "uid";
			this.uid.Size = new System.Drawing.Size(88, 32);
			this.uid.TabIndex = 1;
			this.uid.Text = "None";
			this.uid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(216, 8);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(128, 24);
			this.linkLabel1.TabIndex = 3;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Register Here";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(346, 140);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.username);
			this.Controls.Add(this.loginButton);
			this.Controls.Add(this.usernameLabel);
			this.Controls.Add(this.passwordLabel);
			this.Controls.Add(this.password);
			this.Controls.Add(this.loggedInAsLabel);
			this.Controls.Add(this.loggedInAs);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.uid);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.Text = "Tuniverse Client v .01";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Load += new System.EventHandler(this.MainForm_Resize);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private IITTrack getLatestTrack()
		{
			IITTrack toReturn = iTunes.CurrentTrack;
			return toReturn;
		}

		private void trayIcon_DoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Show();
			WindowState = FormWindowState.Normal;
			BringToFront();
		}

		private void MainForm_Resize(object sender, System.EventArgs e)
		{
			if (FormWindowState.Minimized == WindowState)
			{
				Hide();
			}
		}

		// Click handler for the login/logout button
		private void loginButton_Click(object sender, System.EventArgs e)
		{
			if( ( username.Text.Trim() == "" || password.Text == "") && !isLoggedIn )
			{
				//MessageBox.Show("Please enter a username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				new StarNote( "Please enter a username and password.", "agent", 1.0, 200 );
			}
			
			// Check if we're logging in or logging out
			if( !isLoggedIn )
			{
				string s = "username=" + username.Text.Trim() + "&password=" + password.Text.Trim();
				string url = "http://omega.cs.iit.edu/~butltyl/tuniverse/login.php";
				StreamWriter writer;
				HttpWebRequest post = (HttpWebRequest)WebRequest.Create( url );
				post.Method = "POST";
				post.ContentLength = s.Length;
				post.ContentType = "application/x-www-form-urlencoded";
				post.KeepAlive = false;

				try
				{
					writer = new StreamWriter(post.GetRequestStream());
					writer.Write(s);
					writer.Close();
				}
				catch( Exception ex )
				{
					Console.WriteLine(ex.Message);
				}

				StreamReader reader = new StreamReader(post.GetResponse().GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
				string response = reader.ReadToEnd();
				reader.Close();

				uid.Text = response;

				if( uid.Text == "Authentication failed." )
				{
					password.ResetText();
					new StarNote("Authentication failed.  Check your password.", "agent", 1.0, 200);
				}
				else
				{
					loginButton.Text = "Logout";
					loggedInAs.Text = username.Text.Trim();
					isLoggedIn = true;
					new StarNote("Logged in successfully as " + username.Text + ".", "agent", 1.0, 200);

					// clear password for security
					password.ResetText();
				}
			}
			else
			{
				// Do logout stuff here
				isLoggedIn = false;
				loginButton.Text = "Login";
				loggedInAs.Text = "Not Logged In...";
				uid.Text = "None";
				new StarNote("Logged out successfully.", "agent", 1.0, 200);
			}
		}

		private void exitMenu_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			
		}
	}
}
