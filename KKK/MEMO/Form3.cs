using KKK.Common;
using ConsoleLibrary1.NativeWindow;
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
    public partial class Form3 : Form
    {
        private AppConfig _config;
        public Form1 form1 = null;
        public Form2 form2 = null;
        public Form3()
        {
            InitializeComponent();
            LoadConfig(); //Configを読み込みます
        }
        public void Load3(object sender, EventArgs e)
        {

            if (this.form1 == null || this.form1.IsDisposed)
            { /* ヌル、または破棄されていたら */
                this.form1 = new Form1();
            }
            if (this.form2 == null || this.form2.IsDisposed)
            { /* ヌル、または破棄されていたら */
                this.form2 = new Form2();
            }
            form1.Owner = this;
            this.AddOwnedForm(form1);
            
            //var currentImage = ImageFileOpen();
            //panel1.BackgroundImage = currentImage;
        }

        //public sealed partial class PanelEx : Panel, ISelectable
        //{

        //}
        
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

        /// <summary> 
        /// ファイルを開くダイアログボックスを表示して画像ファイルを開く 
        /// </summary> 
        /// <returns>生成したBitmapクラスオブジェクト</returns>
        private Bitmap ImageFileOpen()
        {
            //ファイルを開くダイアログボックスの作成  
            var ofd = new System.Windows.Forms.OpenFileDialog();
            //ファイルフィルタ  
            ofd.Filter = "Image File(*.bmp,*.jpg,*.png,*.tif)|*.bmp;*.jpg;*.png;*.tif|Bitmap(*.bmp)|*.bmp|Jpeg(*.jpg)|*.jpg|PNG(*.png)|*.png";
            //ダイアログの表示 （Cancelボタンがクリックされた場合は何もしない）
            if (ofd.ShowDialog() == DialogResult.Cancel) return null;

            return ImageFileOpen(ofd.FileName);
        }

        /// <summary>
        /// ファイルパスを指定して画像ファイルを開く
        /// </summary>
        /// <param name="fileName">画像ファイルのファイルパスを指定します。</param>
        /// <returns>生成したBitmapクラスオブジェクト</returns>
        private Bitmap ImageFileOpen(string fileName)
        {
            // 指定したファイルが存在するか？確認
            if (System.IO.File.Exists(fileName) == false) return null;

            // 拡張子の確認
            var ext = System.IO.Path.GetExtension(fileName).ToLower();

            // ファイルの拡張子が対応しているファイルかどうか調べる
            if (
                (ext != ".bmp") &&
                (ext != ".jpg") &&
                (ext != ".png") &&
                (ext != ".tif")
                )
            {
                return null;
            }

            Bitmap bmp;

            // ファイルストリームでファイルを開く
            using (var fs = new System.IO.FileStream(
                fileName,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read))
            {
                bmp = new Bitmap(fs);
            }
            return bmp;
        }

        private void resieze(object sender, EventArgs e)
        {
            LoadConfig();
        }

    }
}
