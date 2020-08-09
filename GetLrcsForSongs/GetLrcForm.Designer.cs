namespace GetLrcsForSongs
{
    partial class GetLrcForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetLrcForm));
            this.lyricListView = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.singerTextBox = new GetLrcsForSongs.BetterTextBox();
            this.songTextBox = new GetLrcsForSongs.BetterTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.durationMmTextbox = new GetLrcsForSongs.BetterTextBox();
            this.durationSsTextbox = new GetLrcsForSongs.BetterTextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.downLoadButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lyricListView
            // 
            this.lyricListView.AllowDrop = true;
            this.lyricListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lyricListView.AutoArrange = false;
            this.lyricListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.lyricListView, 3);
            this.lyricListView.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lyricListView.Location = new System.Drawing.Point(0, 41);
            this.lyricListView.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lyricListView.Name = "lyricListView";
            this.lyricListView.Size = new System.Drawing.Size(947, 422);
            this.lyricListView.TabIndex = 0;
            this.lyricListView.UseCompatibleStateImageBehavior = false;
            this.lyricListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.lyricListView_DragDrop);
            this.lyricListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.lyricListView_DragEnter);
            this.lyricListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lyricListView_MouseDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lyricListView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.searchButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.downLoadButton, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(947, 506);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(520, 35);
            this.panel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.singerTextBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.songTextBox, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(520, 35);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.label1.Location = new System.Drawing.Point(200, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "-";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // singerTextBox
            // 
            this.singerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.singerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.singerTextBox.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.singerTextBox.IsDigital = false;
            this.singerTextBox.Location = new System.Drawing.Point(0, 0);
            this.singerTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.singerTextBox.MaxInput = 2147483647;
            this.singerTextBox.Name = "singerTextBox";
            this.singerTextBox.Size = new System.Drawing.Size(200, 25);
            this.singerTextBox.TabIndex = 3;
            this.singerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.singerTextBox.WatermarkText = "歌手";
            this.singerTextBox.TextChanged += new System.EventHandler(this.textBoxes_TextChanged);
            // 
            // songTextBox
            // 
            this.songTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.songTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.songTextBox.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.songTextBox.IsDigital = false;
            this.songTextBox.Location = new System.Drawing.Point(220, 0);
            this.songTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.songTextBox.MaxInput = 2147483647;
            this.songTextBox.Name = "songTextBox";
            this.songTextBox.Size = new System.Drawing.Size(300, 25);
            this.songTextBox.TabIndex = 4;
            this.songTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.songTextBox.WatermarkText = "歌曲";
            this.songTextBox.TextChanged += new System.EventHandler(this.textBoxes_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(520, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(236, 35);
            this.panel2.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.durationMmTextbox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.durationSsTextbox, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(236, 35);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 35);
            this.label2.TabIndex = 0;
            this.label2.Text = "时长";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.label3.Location = new System.Drawing.Point(134, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 35);
            this.label3.TabIndex = 1;
            this.label3.Text = ":";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // durationMmTextbox
            // 
            this.durationMmTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.durationMmTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.durationMmTextbox.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.durationMmTextbox.IsDigital = true;
            this.durationMmTextbox.Location = new System.Drawing.Point(50, 0);
            this.durationMmTextbox.Margin = new System.Windows.Forms.Padding(0);
            this.durationMmTextbox.MaxInput = 59;
            this.durationMmTextbox.MaxLength = 2;
            this.durationMmTextbox.Name = "durationMmTextbox";
            this.durationMmTextbox.Size = new System.Drawing.Size(84, 25);
            this.durationMmTextbox.TabIndex = 2;
            this.durationMmTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.durationMmTextbox.WatermarkText = "分";
            this.durationMmTextbox.TextChanged += new System.EventHandler(this.textBoxes_TextChanged);
            // 
            // durationSsTextbox
            // 
            this.durationSsTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.durationSsTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.durationSsTextbox.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.durationSsTextbox.IsDigital = true;
            this.durationSsTextbox.Location = new System.Drawing.Point(151, 0);
            this.durationSsTextbox.Margin = new System.Windows.Forms.Padding(0);
            this.durationSsTextbox.MaxInput = 59;
            this.durationSsTextbox.MaxLength = 2;
            this.durationSsTextbox.Name = "durationSsTextbox";
            this.durationSsTextbox.Size = new System.Drawing.Size(85, 25);
            this.durationSsTextbox.TabIndex = 3;
            this.durationSsTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.durationSsTextbox.WatermarkText = "秒";
            this.durationSsTextbox.TextChanged += new System.EventHandler(this.textBoxes_TextChanged);
            // 
            // searchButton
            // 
            this.searchButton.AutoSize = true;
            this.searchButton.BackColor = System.Drawing.Color.White;
            this.searchButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchButton.Enabled = false;
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Font = new System.Drawing.Font("Microsoft YaHei", 3.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Millimeter);
            this.searchButton.Location = new System.Drawing.Point(756, 3);
            this.searchButton.Margin = new System.Windows.Forms.Padding(0);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(191, 35);
            this.searchButton.TabIndex = 4;
            this.searchButton.Text = "搜索";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // downLoadButton
            // 
            this.downLoadButton.AutoSize = true;
            this.downLoadButton.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.downLoadButton, 3);
            this.downLoadButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downLoadButton.FlatAppearance.BorderSize = 0;
            this.downLoadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downLoadButton.Font = new System.Drawing.Font("Microsoft YaHei", 14F);
            this.downLoadButton.Location = new System.Drawing.Point(0, 463);
            this.downLoadButton.Margin = new System.Windows.Forms.Padding(0);
            this.downLoadButton.Name = "downLoadButton";
            this.downLoadButton.Size = new System.Drawing.Size(947, 43);
            this.downLoadButton.TabIndex = 5;
            this.downLoadButton.Text = "下载";
            this.downLoadButton.UseVisualStyleBackColor = false;
            this.downLoadButton.Click += new System.EventHandler(this.downLoadButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GetLrcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(947, 506);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(650, 360);
            this.Name = "GetLrcForm";
            this.Text = "酷狗LRC歌词搜索";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetLrcForm_FormClosing);
            this.Load += new System.EventHandler(this.GetLrcForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lyricListView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button searchButton;
        private BetterTextBox singerTextBox;
        private BetterTextBox songTextBox;
        private BetterTextBox durationMmTextbox;
        private BetterTextBox durationSsTextbox;
        private System.Windows.Forms.Button downLoadButton;
        private System.Windows.Forms.Timer timer1;
    }
}

