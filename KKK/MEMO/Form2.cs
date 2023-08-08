using KKK.Common;
using KKK.MEMO;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using System.Xml;
using System.Drawing.Drawing2D;
using System.Runtime;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Windows.Documents;
using Microsoft.Win32;

namespace KKK.MEMO
{
    public partial class Form2 : Form
    {

        public Form1 form1 = null;
        public Form2 form2 = null;


        private TextFile _textFile { get; set; }
        /// <summary>
        /// Config
        /// </summary>
        private AppConfig _config;
        Graphics gra;
        Pen p = new Pen(Brushes.Red);
        bool drag = false;
        int startX, startY;
        public Form2()
        {
            InitializeComponent();
            LoadConfig(); //Configを読み込みます
            this.pictureBox1.BackColor = Color.Olive;
        }
        private void Loadd(object sender, EventArgs e)
        {
            LoadConfig();
        }


        private void Form2_Load(object sender, System.EventArgs e)
        {
            if (this.form1 == null || this.form1.IsDisposed)
            { /* ヌル、または破棄されていたら */
                this.form1 = new Form1();
            }
            if (this.form2 == null || this.form2.IsDisposed)
            { /* ヌル、または破棄されていたら */
                this.form2 = new Form2();
            }
            //透明を指定する
            this.TransparencyKey = Color.Olive;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gra = Graphics.FromImage(pictureBox1.Image);
        }



        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Left = _config.form_left;
            this.Top = _config.form_top;
            this.Width = _config.form_width;
            this.Height = _config.form_height;
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                pictureBox1.BackColor = Color.Olive;
            }
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                drag = true;
                startX = e.X;
                startY = e.Y;
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.Left = _config.form_left;
            this.Top = _config.form_top;
            this.Width = _config.form_width;
            this.Height = _config.form_height;
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) == Keys.Control && (System.Windows.Forms.Control.ModifierKeys & Keys.Shift) == Keys.Shift && (e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                drag = false;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Left = _config.form_left;
            this.Top = _config.form_top;
            this.Width = _config.form_width;
            this.Height = _config.form_height;
            if ( (e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (!drag) return;
                gra.DrawLine(p, new Point(startX, startY), new Point(e.X, e.Y));
                pictureBox1.Refresh();
                startX = e.X;
                startY = e.Y;
            }
        }

        private void pictureBox1_key(object sender, KeyEventArgs e)
        {
            //if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) == Keys.Control && (System.Windows.Forms.Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            //{
            //    pictureBox1.BackColor = Color.Olive;
          //  }
        }


        private void MouseDowned(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Left = _config.form_left;
            this.Top = _config.form_top;
            this.Width = _config.form_width;
            this.Height = _config.form_height;
            LoadConfig(); //Configを読み込みます
        }

        private void LoadConfig()
        {
            _config = AppConfig.Load();
            if (_config != null)
            {
                //Configが読み込めた場合

                //フォームの位置とサイズを設定
                this.StartPosition = FormStartPosition.Manual;
                this.Left = _config.form_left;
                this.Top = _config.form_top;
                this.Width = _config.form_width;
                this.Height = _config.form_height;
                

            }
            else
            {
                //Configが読み込めなかった場合

                //デフォルト値を設定します
                _config = new AppConfig();

                //フォームを中央に配置
                var screenBounds = Screen.PrimaryScreen.Bounds;
                this.Width = (int)(screenBounds.Width * 0.25);
                this.Height = (int)(screenBounds.Height * 0.25);
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }
        /// <summary>
        /// 影つけるのと最背面に固定
        /// </summary>
        /*const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }*/
        //
        private const int WM_WINDOWPOSCHANGING = 0x0046;
        private IntPtr HWND_BOTTOM = (IntPtr)1;

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_WINDOWPOSCHANGING:
                    WINDOWPOS wp = (WINDOWPOS)System.Runtime.InteropServices.Marshal.PtrToStructure(m.LParam, typeof(WINDOWPOS));
                    wp.hwndInsertAfter = HWND_BOTTOM;
                    System.Runtime.InteropServices.Marshal.StructureToPtr(wp, m.LParam, true);
                    break;
            }
            base.WndProc(ref m);
        }

        private void Toform1(object sender, EventArgs e)
        {
            this.TransparencyKey = Color.Fuchsia;
            this.pictureBox1.BackColor = Color.Fuchsia; //特定の色を透過したら後ろが操作出来る性質を利用
            //this.Visible = false;            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }



        //ピクチャーボックスの絵をリセット

        
        private void reset(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
    }
}
