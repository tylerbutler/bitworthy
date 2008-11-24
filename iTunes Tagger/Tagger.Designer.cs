using System.Collections;
using iTunesLib;
using System.Windows.Forms;
using System;
namespace iTunes_Tagger
{
    partial class Tagger
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
            this.Tags = new System.Windows.Forms.ListBox();
            this.TagsLabel = new System.Windows.Forms.Label();
            this.rescanButton = new System.Windows.Forms.Button();
            this.progressLabel = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.TrackIDHeader = new System.Windows.Forms.ColumnHeader();
            this.NameHeader = new System.Windows.Forms.ColumnHeader();
            this.ArtistHeader = new System.Windows.Forms.ColumnHeader();
            this.AlbumHeader = new System.Windows.Forms.ColumnHeader();
            this.tracksLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackTagsListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Tags
            // 
            this.Tags.FormattingEnabled = true;
            this.Tags.Location = new System.Drawing.Point(12, 98);
            this.Tags.Name = "Tags";
            this.Tags.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.Tags.Size = new System.Drawing.Size(113, 420);
            this.Tags.TabIndex = 0;
            this.Tags.SelectedIndexChanged += new System.EventHandler(this.Tags_SelectedIndexChanged);
            // 
            // TagsLabel
            // 
            this.TagsLabel.AutoSize = true;
            this.TagsLabel.Location = new System.Drawing.Point(9, 82);
            this.TagsLabel.Name = "TagsLabel";
            this.TagsLabel.Size = new System.Drawing.Size(48, 13);
            this.TagsLabel.TabIndex = 1;
            this.TagsLabel.Text = "All Tags:";
            // 
            // rescanButton
            // 
            this.rescanButton.Location = new System.Drawing.Point(12, 12);
            this.rescanButton.Name = "rescanButton";
            this.rescanButton.Size = new System.Drawing.Size(113, 23);
            this.rescanButton.TabIndex = 3;
            this.rescanButton.Text = "Scan iTunes Library";
            this.rescanButton.UseVisualStyleBackColor = true;
            this.rescanButton.Click += new System.EventHandler(this.rescanButton_Click);
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressLabel.Location = new System.Drawing.Point(12, 531);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(114, 17);
            this.progressLabel.TabIndex = 5;
            this.progressLabel.Text = "Progress:  N/A";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TrackIDHeader,
            this.NameHeader,
            this.ArtistHeader,
            this.AlbumHeader});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(131, 98);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(903, 420);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // TrackIDHeader
            // 
            this.TrackIDHeader.Text = "TrackID";
            // 
            // NameHeader
            // 
            this.NameHeader.Text = "Name";
            this.NameHeader.Width = 281;
            // 
            // ArtistHeader
            // 
            this.ArtistHeader.DisplayIndex = 3;
            this.ArtistHeader.Text = "Artist";
            this.ArtistHeader.Width = 274;
            // 
            // AlbumHeader
            // 
            this.AlbumHeader.DisplayIndex = 2;
            this.AlbumHeader.Text = "Album";
            this.AlbumHeader.Width = 302;
            // 
            // tracksLabel
            // 
            this.tracksLabel.AutoSize = true;
            this.tracksLabel.Location = new System.Drawing.Point(128, 82);
            this.tracksLabel.Name = "tracksLabel";
            this.tracksLabel.Size = new System.Drawing.Size(43, 13);
            this.tracksLabel.TabIndex = 7;
            this.tracksLabel.Text = "Tracks:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1037, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Track Tags:";
            // 
            // trackTagsListBox
            // 
            this.trackTagsListBox.FormattingEnabled = true;
            this.trackTagsListBox.Location = new System.Drawing.Point(1040, 98);
            this.trackTagsListBox.Name = "trackTagsListBox";
            this.trackTagsListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.trackTagsListBox.Size = new System.Drawing.Size(113, 420);
            this.trackTagsListBox.TabIndex = 0;
            this.trackTagsListBox.SelectedIndexChanged += new System.EventHandler(this.Tags_SelectedIndexChanged);
            // 
            // Tagger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 557);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tracksLabel);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.rescanButton);
            this.Controls.Add(this.TagsLabel);
            this.Controls.Add(this.trackTagsListBox);
            this.Controls.Add(this.Tags);
            this.Name = "Tagger";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.trackTagsListBox.Items.Clear();

            //get track object from selected item
            if (this.listView1.SelectedItems.Count > 0)
            {
                ListViewItem i = this.listView1.SelectedItems[0];
                IITTrack track = db.GetTrackByID(int.Parse(i.SubItems[0].Text));

                // update track tag list
                foreach (Tag tag in db.GetTagsForTrack(track))
                {
                    this.trackTagsListBox.Items.Add(tag);
                }
            }
        }

        void Tags_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.listView1.Items.Clear();
            //get tracks for the selected tags
            Tag selected = ((Tag)this.Tags.Items[this.Tags.SelectedIndex]);
            ArrayList tracks = db.GetTracksForTag(selected);
            listView1.BeginUpdate();
            foreach (IITTrack track in tracks)
            {
                string[] columns = new string[4];
                columns[0] = track.TrackDatabaseID.ToString();
                columns[1] = track.Name;
                columns[2] = track.Artist;
                columns[3] = track.Album;
                ListViewItem item = new ListViewItem(columns);
                this.listView1.Items.Add(item);
            }
            listView1.EndUpdate();
            this.tracksLabel.Text = "Tracks (" + tracks.Count + "):";
        }

        private void rescanButton_Click(object sender, EventArgs e)
        {
            this.Tags.Items.Clear();
            //db.RebuildLibrary(iTunes.LibrarySource.Playlists.get_ItemByName("tagger test"), progressBar1);
            db.RebuildLibrary(iTunes.LibraryPlaylist, progressLabel);
            foreach (Tag tag in db.GetTags())
            {
                this.Tags.Items.Add(tag);
            }
        }

        #endregion

        private System.Windows.Forms.ListBox Tags;
        private System.Windows.Forms.Label TagsLabel;
        private System.Windows.Forms.Button rescanButton;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader NameHeader;
        private System.Windows.Forms.ColumnHeader AlbumHeader;
        private System.Windows.Forms.ColumnHeader ArtistHeader;
        private Label tracksLabel;
        private Label label2;
        private ListBox trackTagsListBox;
        private ColumnHeader TrackIDHeader;
    }
}

