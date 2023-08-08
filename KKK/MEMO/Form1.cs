using KKK.Common;
using KKK.MEMO;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
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
using System.Runtime;

namespace KKK
{
    public partial class Form1 : Form
    {
        private TextFile _textFile { get; set; }
        /// <summary>
        /// Config
        /// </summary>
        private AppConfig _config;

        public Form form1;
        public Form2 form2 = null;
        public Form3 form3 = null;

        DAndDSizeChanger sizeChanger; //サイズ変更のフィールド呼び出し
        public Form1()
        {
            InitializeComponent();
            LoadConfig(); //Configを読み込みます
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            richTextBox1.AllowDrop = true; //D&Dを許可します
            richTextBox1.DragEnter += TxtMain_DragEnter;
            richTextBox1.DragDrop += TxtMain_DragDrop;
            if (this.form1 == null || this.form1.IsDisposed)
            { /* ヌル、または破棄されていたら */
                this.form1 = new Form1();
            }
            if (this.form2 == null || this.form2.IsDisposed)
            { /* ヌル、または破棄されていたら */
                this.form2 = new Form2();
            }
            if (this.form3 == null || this.form2.IsDisposed)
            { /* ヌル、または破棄されていたら */
                this.form3 = new Form3();
            }

            form2.Owner= this;
            this.AddOwnedForm(form2);

            form1.BackColor = richTextBox1.BackColor;
            form2.Show(); //pictureBox用のフォームを作る
            form2.TransparencyKey = Color.Fuchsia;
            form2.pictureBox1.BackColor = Color.Fuchsia; //特定の色を透過したら後ろが操作出来る性質を利用

            sizeChanger = new DAndDSizeChanger(richTextBox1, this,form2,form3, DAndDArea.All, 8);
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                //コマンドライン引数がある場合
                //(EXEにファイルをD&D、送るメニューでファイル指定、ショートカットで引数指定による起動)

                //ファイルを開きます
                _textFile = new TextFile();
                try
                {
                    OpenFileSub(args[1], null);
                }
                catch (Exception ex)
                {
                    //例外が発生した場合は、アプリを終了させます
                    ShowErrorMsgBox(ex);
                    this.Dispose();
                    return;
                }

            }
            else
            {
                //コマンドライン引数がない場合(通常起動)

                //新規作成時の状態にします
                CreateNewFile();
            }
            //テキストボックスの初期化のinittexTextBoxメソッド呼び出し
            initTextBox(richTextBox1);

            //ダイアログの初期設定
            openFileDialog.Filter =
                "テキストファイル(*.txt)|*.txt" + "|" +
                "すべてのファイル(*.*)|*.*";
            saveFileDialog.Filter = openFileDialog.Filter;


        }
        //テキストボックスの初期化
        private void initTextBox(RichTextBox textBox)
        {
            //デフォルトでは、半角英数字は英語フォント、それ以外は日本語フォントを使う DualFont になっているため、AutoFont だけを再設定します
            //この設定をしないと、半角の「i」の幅がものすごく狭くなってしまう
            textBox.LanguageOption = RichTextBoxLanguageOptions.AutoFont;

            //テキストボックスの左の内側に余白を設定します
            textBox.SelectionIndent = 6;

            //URLが入力された時に書式が変わらないようにする
            textBox.DetectUrls = true; //true にすると、LinkClickedイベントでクリックされたURL(e.LinkText)が取得できる

            //変更なしにします
            textBox.Modified = false;

            //index.txtの呼び出し
            Readindex();

        }


        private void CreateNewFile()
        {
            //テキストに変更がある場合は、ファイルの保存確認をして、ファイルを保存します
            ConfirmAndSave();

            {
                //テキストボックスのテキストを初期化します
                richTextBox1.Clear(); //Modified は false になる

                //変更なしにします
                richTextBox1.Modified = false;

                //テキストファイルを生成して設定します
                _textFile = new TextFile();

                //文字コードをシフトJISにします
                _textFile.SetEncodingShiftJIS();

                //改行コードは \r\n にします
                _textFile.NewLineCode = "\r\n";

                //フォームのタイトルを設定します
                SetFormTitle();
            }
        }


        /// <summary>
        /// index.textが存在する場合、フォーム起動時にそのメモ内容を呼び起こす
        /// </summary>
        /// <param name="encoding"></param>
        public void Readindex(Encoding encoding = null)
        {
            openFileDialog.FileName = "index.txt";
            string path = openFileDialog.FileName;
            OpenFileSub(path, encoding);
        }

        private void OpenFile(Encoding encoding = null)
        {
            //テキストに変更がある場合は、ファイルの保存確認をして、ファイルを保存します
            ConfirmAndSave();

            {
                //開くダイアログを表示します
                openFileDialog.FileName = "";
                var dialogResult = openFileDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    //「OK」の場合

                    //ファイルの読み込み
                    string path = openFileDialog.FileName;
                    OpenFileSub(path, encoding);
                }
            }
        }

        /// <summary>
        /// テキストファイルを開きます
        /// 保存確認と開くダイアログの表示は行いません
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        public void OpenFileSub(string path, Encoding encoding = null)
        {
            _textFile.TextEncoding = encoding; //文字コード
            _textFile.NewLineCode = null;      //改行コード(自動判別)
            _textFile.Load(path);
            richTextBox1.Text = _textFile.Text;

            //変更なしにします
            richTextBox1.Modified = false;

            //フォームのタイトルを設定します
            SetFormTitle();
        }


        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //テキストファイルを閉じます
                CloseFile();
            } catch (CancelExeption)
            {
                //キャンセルされた場合、フォームが閉じるのを中止します
                e.Cancel = true;
            } catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMsgBox(ex);
            }
        }

        /// <summary>
        /// コンテキストメニュー開く の Clickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, EventArgs e)
        {
            try
            {
                //テキストファイルを開きます
                OpenFile();
            }
            catch (CancelExeption)
            {
                //キャンセル時
            }
            catch (Exception ex)
            {
                ShowErrorMsgBox(ex);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                //テキストファイルに保存します
                SaveFile();
            }
            catch (CancelExeption)
            {
                //キャンセル時
            }
            catch (Exception ex)
            {
                ShowErrorMsgBox(ex);
            }
        }

        /// <summary>
        /// メニュー・ファイル・名前を付けて保存 の Clickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                //テキストファイルに保存します
                SaveFile(true);
            }
            catch (CancelExeption)
            {
                //キャンセル時
            }
            catch (Exception ex)
            {
                ShowErrorMsgBox(ex);
            }
        }

        ///テキストファイルを閉じる
        private void CloseFile()
        {
            //テキストに変更がある場合、ファイルの保存確認をして、保存する
            ConfirmAndSave();
        }

        private bool ConfirmAndSave()
        {
            if (richTextBox1.Modified)
            {
                //ファイルの保存確認をします
                var dialogResult = ShowConfirmSavingMsgBox();
                if (dialogResult == DialogResult.Cancel) throw new CancelExeption();
                if (dialogResult == DialogResult.Yes)
                {
                    //「はい」の場合、ファイルを保存します
                    SaveFile();
                }
            }
            return true;
        }
            
        private void SaveFile(bool showDialog = false)
        {
            string path = _textFile.Path;
            saveFileDialog.FileName = "index.txt";
            //パスが未設定、ダイアログを表示、読み取り専用の場合は保存ダイアログを表示
            if (path == null || showDialog == true || _textFile.IsReadOnly)
            {
                //パスが未設定の場合保存ダイアログを表示
                var result = saveFileDialog.ShowDialog();
                if (result == DialogResult.Cancel) throw new CancelExeption();
                if (result == DialogResult.No) return;

                //選択されたファイルのパスを保持
                path = saveFileDialog.FileName;
            }

            //ファイルを保存します
            _textFile.Text = richTextBox1.Text;
            _textFile.Save(path);

            //読み込み専用を解除
            _textFile.IsReadOnly = false;

            //変更なしにします
            richTextBox1.Modified = false;

            //フォームのタイトルを設定
            SetFormTitle();


            //-------------------------------------------------
            //Font関連のconfigはファイルが保存された時のみ保存させるのでこっち
            //---------------------------------------------------
            FontStyle fs = richTextBox1.Font.Style;　      //ここに書いてるのダサいけど、フォントスタイルをstringに変換してxmlに保存してる
            string ft = fs.ToString();
            _config.memo_font_style = ft;
            int fc = (int)richTextBox1.ForeColor.ToArgb(); //色をARBG形式に変換して保存
            _config.memo_font_color = fc;
            _config.memo_font_name = richTextBox1.Font.Name;
            _config.memo_font_size = richTextBox1.Font.Size;


            _config.Save();
        }

        private void SetFormTitle()
        {

            var title = new StringBuilder();

            //読み取り専用の場合は、(読み取り専用) をつける
            if (_textFile.Path != null)
            {
                title.Append(_textFile.IsReadOnly ? "(読み取り専用)" : "");
            }

            //パスが未設定の場合は「無題」にする
            title.Append(_textFile.Path ?? "無題");

            //文字コードを追加します
            title.Append(" [" + _textFile.TextEncoding.EncodingName);

            //BOMの有無を追加します
            title.Append(((_textFile.TextEncoding.GetPreamble().Length > 0) ? ":BOMあり" : ""));

            //改行コードを追加します
            if (_textFile.NewLineCode.Length == 2)
            {
                title.Append(":CRLF");
            }
            else
            {
                title.Append(_textFile.NewLineCode.Equals("\r") ? ":CR" : ":LF");
            }
            title.Append("]");

            //テキストが変更されている場合は、(*) をつける
            title.Append(richTextBox1.Modified ? "(*)" : "");

            //フォームのタイトルを設定します
            this.Text = title.ToString();
        }



        private void ShowErrorMsgBox(Exception e)
        {
            MessageBox.Show("エラーが発生しちゃったよ。\n" + e.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        /// <summary>
        /// 保存確認メッセージを表示します
        /// </summary>
        /// <returns>押されたボタン</returns>
        private DialogResult ShowConfirmSavingMsgBox()
        {
            var dialogResult = MessageBox.Show("変更あるけど保存する？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            return dialogResult;
        }


        ///Form1のFormClosedイベント
        ///フォームが閉じられた時に呼ばれる
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Configを保存
            SaveConfig();
        }


        /// <summary>
        /// アプリケーションを終了するイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit(object sender, System.EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        /// <summary>
        /// 影つけるのと最背面に固定
        /// </summary>
        const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
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
        private Point mousePoint;  //マウスのクリック位置を記憶

        private void MouseDowned(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            SaveConfig();
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control && (e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                mousePoint = new Point(e.X, e.Y); //位置を記憶する
                form2.Size = this.Size;
                form3.Size = this.Size;
            }
        }
        //マウスが動いたとき
        private void MouseMoved(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            SaveConfig();
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control && (e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
                form2.Left = this.Left;
                form2.Top = this.Top;
                form2.Height = this.Height;
                form2.Width = this.Width;
                form3.Left = this.Left;
                form3.Top = this.Top;
                form3.Height = this.Height;
                form3.Width = this.Width;
            }

        }

        /// <summary>
        /// 書式なしのテキストフォーマットでペーストします。
        /// </summary>
        private void paste(RichTextBox textBox)
        {
            var textFormat = DataFormats.GetFormat(DataFormats.Text);
            textBox.Paste(textFormat);
        }

        private void text_paste_key(object sender, KeyEventArgs e)
        {
            //デフォルトのペーストは書式付きになるため、独自のペーストで処理します
            if (e.Control && e.KeyCode.Equals(Keys.V))
            {
                e.Handled = true; //Ctrl+V のKeyDownを処理済みにします。デフォルトのペーストが実行されなくなる。
                paste(richTextBox1);
                return;
            }
        }

        private void text_paste(object sender, EventArgs e)
        {
            paste(richTextBox1);
            return;
        }

        ///選択テキストの切り取りイベント
        private void text_cut(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void text_copy(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        ///<summary>
        ///Configを読み込みます
        /// </summary>
        private void LoadConfig()
        {
            _config = AppConfig.Load();
            if(_config != null)
            {
                //Configが読み込めた場合

                //フォームの位置とサイズを設定
                this.StartPosition = FormStartPosition.Manual;
                this.Left  = _config.form_left;
                this.Top   = _config.form_top;
                this.Width = _config.form_width;
                this.Height = _config.form_height;

                

                
                Color fc = Color.FromArgb(_config.memo_font_color); //ARBGのintからフォントのを取得
                richTextBox1.ForeColor = fc;
                FontStyle FS = (FontStyle)Enum.Parse(typeof(FontStyle), _config.memo_font_style); //フォントスタイルをxmlのstringから読み込める形式に変換
                richTextBox1.Font = new System.Drawing.Font(_config.memo_font_name,_config.memo_font_size,FS);
                Color BC = Color.FromArgb(_config.memo_back_color);//ARBGのintから背景の色を取得
                richTextBox1.BackColor = BC;
                this.Opacity = _config.memo_opacity;

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

        ///<summary>
        ///Configの保存を行います
        ///このconfigのセーブは設定が変わったとき自動で行われる。保存時のみ変えたい時はSaveFileへどうぞ
        /// </summary>
        private void SaveConfig()
        {
            //ウィンドウが最小化、最大化されている場合は標準に戻します
            //(Configに保存する際のサイズが分からないため)
            if(this.WindowState != FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Normal;
            }

            //Configに現在の状態を設定します
            _config.form_left = this.Left;
            _config.form_top = this.Top;
            _config.form_width = this.Width;
            _config.form_height = this.Height;
            int BC = (int)richTextBox1.BackColor.ToArgb();
            _config.memo_back_color = BC;
            _config.memo_opacity = this.Opacity;
            //Configを保存します
            _config.Save();

        }
        



        //ハイパーリンクがctrl+クリックされた時にそのリンクを開くイベント
        private void UrlOpen_click(object sender, LinkClickedEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }
        }


        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            //FontDialogクラスのインスタンスを作成
            FontDialog fd = new FontDialog();

            //初期のフォントを設定
            fd.Font = richTextBox1.Font;
            //初期の色を設定
            fd.Color = richTextBox1.ForeColor;
            //ユーザーが選択できるポイントサイズの最大値を設定する
            fd.MaxSize = 100;
            fd.MinSize = 1;
            //存在しないフォントやスタイルをユーザーが選択すると
            //エラーメッセージを表示する
            fd.FontMustExist = true;
            //横書きフォントだけを表示する
            fd.AllowVerticalFonts = false;
            //色を選択できるようにする
            fd.ShowColor = true;
            //取り消し線、下線、テキストの色などのオプションを指定可能にする
            //デフォルトがTrueのため必要はない
            fd.ShowEffects = true;
            //固定ピッチフォント以外も表示する
            //デフォルトがFalseのため必要はない
            fd.FixedPitchOnly = false;
            //ベクタ フォントを選択できるようにする
            //デフォルトがTrueのため必要はない
            fd.AllowVectorFonts = true;

            //ダイアログを表示する
            if (fd.ShowDialog() != DialogResult.Cancel)
            {
                //TextBox1のフォントと色を変える
                richTextBox1.Font = fd.Font;
                richTextBox1.ForeColor = fd.Color;
            }
        }

        private void colorDialog1_Apply(object sender, EventArgs e)
        {
            //ColorDialogクラスのインスタンスを作成
            ColorDialog cd = new ColorDialog();

            //はじめに選択されている色を設定
            cd.Color = richTextBox1.BackColor;
            //色の作成部分を表示可能にする
            //デフォルトがTrueのため必要はない
            cd.AllowFullOpen = true;
            //純色だけに制限しない
            //デフォルトがFalseのため必要はない
            cd.SolidColorOnly = false;
            //[作成した色]に指定した色（RGB値）を表示する
            cd.CustomColors = new int[] {
              0x33, 0x66, 0x99, 0xCC, 0x3300, 0x3333,
              0x3366, 0x3399, 0x33CC, 0x6600, 0x6633,
               0x6666, 0x6699, 0x66CC, 0x9900, 0x9933};

            //ダイアログを表示する
            if (cd.ShowDialog() == DialogResult.OK)
            {
                //選択された色の取得
                richTextBox1.BackColor = cd.Color;
                form1.BackColor = cd.Color;
            }
        }

        private void sizchange(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            form2.Size = this.Size;
        }
        /*private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            //グループのToolStripMenuItemを配列にしておく
            ToolStripMenuItem[] groupMenuItems = new ToolStripMenuItem[]
            {
        this.起動ToolStripMenuItem,
        this.終了ToolStripMenuItem,
            };

            //グループのToolStripMenuItemを列挙する
            foreach (ToolStripMenuItem item in groupMenuItems)
            {    //チェック状態を反転させる
                if (object.ReferenceEquals(item, sender))
                {
                    //ClickされたToolStripMenuItemならば、Indeterminateにする
                    item.CheckState = CheckState.Indeterminate;
                }
                else
                {
                    //ClickされたToolStripMenuItemでなければ、Uncheckedにする
                    item.CheckState = CheckState.Unchecked;
                }
            }
        }*/

        private void Tofont2 (object sender, EventArgs e)
        {
            SaveFile();
            SaveConfig();
            form2.Height = this.Height;
            form2.Width = this.Width;
            form2.Left = this.Left;
            form2.Top = this.Top;
            form2.pictureBox1.BackColor = Color.Olive;
            form2.TransparencyKey = Color.Olive;
            //form2.Visible = true;
        }
        

        private void reset(object sender, EventArgs e) //絵をリセット無理だから再起動
        {
            Application.Restart();
        }

        private void TxtMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        private void TxtMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string path in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    try
                    {
                        //テキストに変更がある場合は、ファイルの保存確認をして、ファイルを保存します
                        ConfirmAndSave();

                        //ファイルを開きます
                        OpenFileSub(path);
                    }
                    catch (Exception)
                    {
                    }
                    return;
                }
            }
        }




        
        private void BackImage(object sender, EventArgs e)
        {
            // 画像ファイルを開く
            SaveFile();
            SaveConfig();
            Color BC = richTextBox1.BackColor;//ARBGのintから背景の色を取得
            TransparencyKey = BC;
            form3.Height = this.Height;
            form3.Width= this.Width;
            form3.Left= this.Left;
            form3.Top= this.Top;
            form3.Show();

            //this.BackgroundImage = currentImage;
            //this.BackgroundImageLayout = ImageLayout.Tile;
        }
    }




}
