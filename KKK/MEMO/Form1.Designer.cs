
namespace KKK
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.開く = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.名前を付けて保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上書き保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.切り取りTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.コピーCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.貼り付けPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.絵を描くモードToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.絵をリセットToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.プロパティToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.フォントの設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.背景の設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.画像を読み込むToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.contextMenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.contextMenuStrip1.Font = new System.Drawing.Font("UD デジタル 教科書体 NP-B", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.開く,
            this.toolStripMenuItem3,
            this.toolStripMenuItem1,
            this.切り取りTToolStripMenuItem,
            this.コピーCToolStripMenuItem,
            this.貼り付けPToolStripMenuItem,
            this.toolStripMenuItem2,
            this.絵を描くモードToolStripMenuItem,
            this.絵をリセットToolStripMenuItem,
            this.toolStripSeparator1,
            this.プロパティToolStripMenuItem,
            this.終了XToolStripMenuItem,
            this.画像を読み込むToolStripMenuItem});
            this.contextMenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(167, 242);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 開く
            // 
            this.開く.Name = "開く";
            this.開く.Size = new System.Drawing.Size(166, 22);
            this.開く.Text = "開く";
            this.開く.Click += new System.EventHandler(this.Open_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.名前を付けて保存ToolStripMenuItem,
            this.上書き保存ToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem3.Text = "保存";
            // 
            // 名前を付けて保存ToolStripMenuItem
            // 
            this.名前を付けて保存ToolStripMenuItem.Name = "名前を付けて保存ToolStripMenuItem";
            this.名前を付けて保存ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.名前を付けて保存ToolStripMenuItem.Text = "名前を付けて保存";
            this.名前を付けて保存ToolStripMenuItem.Click += new System.EventHandler(this.SaveAs_Click);
            // 
            // 上書き保存ToolStripMenuItem
            // 
            this.上書き保存ToolStripMenuItem.Name = "上書き保存ToolStripMenuItem";
            this.上書き保存ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.上書き保存ToolStripMenuItem.Text = "上書き保存";
            this.上書き保存ToolStripMenuItem.Click += new System.EventHandler(this.Save_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
            // 
            // 切り取りTToolStripMenuItem
            // 
            this.切り取りTToolStripMenuItem.Name = "切り取りTToolStripMenuItem";
            this.切り取りTToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.切り取りTToolStripMenuItem.Text = "切り取り(&T)";
            this.切り取りTToolStripMenuItem.Click += new System.EventHandler(this.text_cut);
            // 
            // コピーCToolStripMenuItem
            // 
            this.コピーCToolStripMenuItem.Name = "コピーCToolStripMenuItem";
            this.コピーCToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.コピーCToolStripMenuItem.Text = "コピー(&C)";
            this.コピーCToolStripMenuItem.Click += new System.EventHandler(this.text_copy);
            // 
            // 貼り付けPToolStripMenuItem
            // 
            this.貼り付けPToolStripMenuItem.Name = "貼り付けPToolStripMenuItem";
            this.貼り付けPToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.貼り付けPToolStripMenuItem.Text = "貼り付け(&P)";
            this.貼り付けPToolStripMenuItem.Click += new System.EventHandler(this.text_paste);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(163, 6);
            // 
            // 絵を描くモードToolStripMenuItem
            // 
            this.絵を描くモードToolStripMenuItem.Name = "絵を描くモードToolStripMenuItem";
            this.絵を描くモードToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.絵を描くモードToolStripMenuItem.Text = "絵を描くモード起動";
            this.絵を描くモードToolStripMenuItem.Click += new System.EventHandler(this.Tofont2);
            // 
            // 絵をリセットToolStripMenuItem
            // 
            this.絵をリセットToolStripMenuItem.Name = "絵をリセットToolStripMenuItem";
            this.絵をリセットToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.絵をリセットToolStripMenuItem.Text = "絵をリセット";
            this.絵をリセットToolStripMenuItem.Click += new System.EventHandler(this.reset);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(163, 6);
            // 
            // プロパティToolStripMenuItem
            // 
            this.プロパティToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.フォントの設定ToolStripMenuItem,
            this.背景の設定ToolStripMenuItem});
            this.プロパティToolStripMenuItem.Name = "プロパティToolStripMenuItem";
            this.プロパティToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.プロパティToolStripMenuItem.Text = "プロパティ";
            // 
            // フォントの設定ToolStripMenuItem
            // 
            this.フォントの設定ToolStripMenuItem.Name = "フォントの設定ToolStripMenuItem";
            this.フォントの設定ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.フォントの設定ToolStripMenuItem.Text = "フォントの設定";
            this.フォントの設定ToolStripMenuItem.Click += new System.EventHandler(this.fontDialog1_Apply);
            // 
            // 背景の設定ToolStripMenuItem
            // 
            this.背景の設定ToolStripMenuItem.Name = "背景の設定ToolStripMenuItem";
            this.背景の設定ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.背景の設定ToolStripMenuItem.Text = "背景色の設定";
            this.背景の設定ToolStripMenuItem.Click += new System.EventHandler(this.colorDialog1_Apply);
            // 
            // 終了XToolStripMenuItem
            // 
            this.終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            this.終了XToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.終了XToolStripMenuItem.Text = "終了(&X)";
            this.終了XToolStripMenuItem.Click += new System.EventHandler(this.Exit);
            // 
            // 画像を読み込むToolStripMenuItem
            // 
            this.画像を読み込むToolStripMenuItem.Name = "画像を読み込むToolStripMenuItem";
            this.画像を読み込むToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.画像を読み込むToolStripMenuItem.Text = "画像を読み込む";
            this.画像を読み込むToolStripMenuItem.Click += new System.EventHandler(this.BackImage);
            // 
            // richTextBox1
            // 
            this.richTextBox1.AcceptsTab = true;
            this.richTextBox1.BackColor = System.Drawing.Color.Navy;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.ForeColor = System.Drawing.Color.Transparent;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(467, 290);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.UrlOpen_click);
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.text_paste_key);
            this.richTextBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDowned);
            this.richTextBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoved);
            this.richTextBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sizchange);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // fontDialog1
            // 
            this.fontDialog1.Font = new System.Drawing.Font("ほのか新明朝 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.fontDialog1.ShowApply = true;
            this.fontDialog1.ShowColor = true;
            this.fontDialog1.Apply += new System.EventHandler(this.fontDialog1_Apply);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 290);
            this.Controls.Add(this.richTextBox1);
            this.Font = new System.Drawing.Font("HG創英角ﾎﾟｯﾌﾟ体", 21F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(700, 700);
            this.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.MinimumSize = new System.Drawing.Size(93, 97);
            this.Name = "Form1";
            this.Opacity = 0.7D;
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem プロパティToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 切り取りTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem コピーCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 貼り付けPToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 終了XToolStripMenuItem;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 名前を付けて保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上書き保存ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem 開く;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem フォントの設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 背景の設定ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 絵を描くモードToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 絵をリセットToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 画像を読み込むToolStripMenuItem;
    }
}

