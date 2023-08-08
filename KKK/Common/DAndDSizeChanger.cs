﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KKK.Common
{
    /// <summary>
    /// コントロールの端をD＆Dすることによってサイズを変更出来る機能を提供するクラス
    /// </summary>
    class DAndDSizeChanger
    {
        Control mouseListner;
        Control sizeChangeCtrl;
        Control sizeChangeCtrl1;
        Control sizeChangeCtrl2;
        DAndDArea sizeChangeArea;
        Size lastMouseDownSize;
        Point lastMouseDownPoint;
        DAndDArea status;
        int sizeChangeAreaWidth;
        Cursor defaultCursor;

        /// <param name="mouseListner">マウス入力を受け取るコントロール</param>
        /// <param name="sizeChangeCtrl">マウス入力によってサイズが変更されるコントロール</param>
        /// <param name="sizeChangeArea">上下左右のサイズ変更が有効になる範囲を指定</param>
        /// <param name="sizeChangeAreaWidth">サイズ変更が有効になる範囲の幅を指定</param>
        /// 
        //ここの変数を()の変数を変えれば適応するフォームの数も変えれる
        //フォーム1のサイズ変数はsizeChangeCtrl
        //フォーム2のサイズ変数はsizeChangeCtrl1
        //フォーム3のサイズ変数はsizeChangeCtrl2
        //thisだから正確にはフォーム1を指定されてないけど、フォーム1を変えたら他のフォームも変わるようにしたいからまとめる
        public DAndDSizeChanger(Control mouseListner, Control sizeChangeCtrl, Control sizeChangeCtrl1, Control sizeChangeCtrl2, DAndDArea sizeChangeArea, int sizeChangeAreaWidth)
        {
            this.mouseListner = mouseListner;
            this.sizeChangeCtrl = sizeChangeCtrl;
            this.sizeChangeCtrl1 = sizeChangeCtrl1;
            this.sizeChangeCtrl2 = sizeChangeCtrl2;
            this.sizeChangeAreaWidth = sizeChangeAreaWidth;
            this.sizeChangeArea = sizeChangeArea;
            defaultCursor = mouseListner.Cursor;

            mouseListner.MouseDown += new MouseEventHandler(mouseListner_MouseDown);
            mouseListner.MouseMove += new MouseEventHandler(mouseListner_MouseMove);
            mouseListner.MouseUp += new MouseEventHandler(mouseListner_MouseUp);
        }

        void mouseListner_MouseDown(object sender, MouseEventArgs e)
        {
            lastMouseDownPoint = e.Location;
            lastMouseDownSize = sizeChangeCtrl.Size;
            lastMouseDownSize = sizeChangeCtrl1.Size;
            lastMouseDownSize = sizeChangeCtrl2.Size;

            //動作を決定
            status = DAndDArea.None;
            if (getTop().Contains(e.Location))
            {
                status |= DAndDArea.Top;
            }
            if (getLeft().Contains(e.Location))
            {
                status |= DAndDArea.Left;
            }
            if (getBottom().Contains(e.Location))
            {
                status |= DAndDArea.Bottom;
            }
            if (getRight().Contains(e.Location))
            {
                status |= DAndDArea.Right;
            }

            if (status != DAndDArea.None)
            {
                mouseListner.Capture = true;
            }
        }

        void mouseListner_MouseMove(object sender, MouseEventArgs e)
        {
            //カーソルを変更
            if ((getTop().Contains(e.Location) &&
                getLeft().Contains(e.Location)) ||
                (getBottom().Contains(e.Location) &&
                getRight().Contains(e.Location)))
            {

                mouseListner.Cursor = Cursors.SizeNWSE;
            }
            else if ((getTop().Contains(e.Location) &&
                getRight().Contains(e.Location)) ||
                (getBottom().Contains(e.Location) &&
                getLeft().Contains(e.Location)))
            {

                mouseListner.Cursor = Cursors.SizeNESW;
            }
            else if (getTop().Contains(e.Location) ||
                getBottom().Contains(e.Location))
            {

                mouseListner.Cursor = Cursors.SizeNS;
            }
            else if (getLeft().Contains(e.Location) ||
                getRight().Contains(e.Location))
            {

                mouseListner.Cursor = Cursors.SizeWE;
            }
            else
            {
                mouseListner.Cursor = defaultCursor;
            }

            if (e.Button == MouseButtons.Left)
            {
                int diffX = e.X - lastMouseDownPoint.X;
                int diffY = e.Y - lastMouseDownPoint.Y;

                if ((status & DAndDArea.Top) == DAndDArea.Top)
                {
                    int h = sizeChangeCtrl.Height;
                    sizeChangeCtrl.Height -= diffY;
                    sizeChangeCtrl.Top += h - sizeChangeCtrl.Height;
                    int o = sizeChangeCtrl1.Height;
                    sizeChangeCtrl1.Height -= diffY;
                    sizeChangeCtrl1.Top += h - sizeChangeCtrl1.Height;
                    int p = sizeChangeCtrl2.Height;
                    sizeChangeCtrl2.Height -= diffY;
                    sizeChangeCtrl2.Top += h - sizeChangeCtrl2.Height;
                }
                if ((status & DAndDArea.Bottom) == DAndDArea.Bottom)
                {
                    sizeChangeCtrl.Height = lastMouseDownSize.Height + diffY;
                    sizeChangeCtrl1.Height = lastMouseDownSize.Height + diffY;
                    sizeChangeCtrl2.Height = lastMouseDownSize.Height + diffY;
                }
                if ((status & DAndDArea.Left) == DAndDArea.Left)
                {
                    int w = sizeChangeCtrl.Width;
                    sizeChangeCtrl.Width -= diffX;
                    sizeChangeCtrl.Left += w - sizeChangeCtrl.Width;
                    int j = sizeChangeCtrl1.Width;
                    sizeChangeCtrl1.Width -= diffX;
                    sizeChangeCtrl1.Left += w - sizeChangeCtrl1.Width;
                    int k = sizeChangeCtrl2.Width;
                    sizeChangeCtrl2.Width -= diffX;
                    sizeChangeCtrl2.Left += w - sizeChangeCtrl2.Width;
                }
                if ((status & DAndDArea.Right) == DAndDArea.Right)
                {
                    sizeChangeCtrl.Width = lastMouseDownSize.Width + diffX;
                    sizeChangeCtrl1.Width = lastMouseDownSize.Width + diffX;
                    sizeChangeCtrl2.Width = lastMouseDownSize.Width + diffX;
                }
            }
        }

        void mouseListner_MouseUp(object sender, MouseEventArgs e)
        {
            mouseListner.Capture = false;
        }

        /// <summary>
        /// ポイントがD＆Dするとサイズが変更されるエリア内にあるかどうかを判定します。
        /// </summary>
        public bool ContainsSizeChangeArea(Point p)
        {
            return getTop().Contains(p) ||
                getBottom().Contains(p) ||
                getLeft().Contains(p) ||
                getRight().Contains(p);
        }

        private Rectangle getTop()
        {
            if ((sizeChangeArea & DAndDArea.Top) == DAndDArea.Top)
            {
                return new Rectangle(0, 0, mouseListner.Width, sizeChangeAreaWidth);
            }
            else
            {
                return new Rectangle();
            }
        }

        private Rectangle getBottom()
        {
            if ((sizeChangeArea & DAndDArea.Bottom) == DAndDArea.Bottom)
            {
                return new Rectangle(0, mouseListner.Height - sizeChangeAreaWidth,
                    mouseListner.Width, sizeChangeAreaWidth);
            }
            else
            {
                return new Rectangle();
            }
        }

        private Rectangle getLeft()
        {
            if ((sizeChangeArea & DAndDArea.Left) == DAndDArea.Left)
            {
                return new Rectangle(0, 0,
                    sizeChangeAreaWidth, mouseListner.Height);
            }
            else
            {
                return new Rectangle();
            }
        }

        private Rectangle getRight()
        {
            if ((sizeChangeArea & DAndDArea.Right) == DAndDArea.Right)
            {
                return new Rectangle(mouseListner.Width - sizeChangeAreaWidth, 0,
                    sizeChangeAreaWidth, mouseListner.Height);
            }
            else
            {
                return new Rectangle();
            }
        }
    }

    public enum DAndDArea
    {
        None = 0,
        Top = 1,
        Bottom = 2,
        Left = 4,
        Right = 8,
        All = 15
    }
}
