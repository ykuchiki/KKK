using System;
using System.Security.Permissions;
using System.Windows.Forms;

namespace ConsoleLibrary1.NativeWindow
{
    /// <summary>
    /// コントロールを選択不可能な状態にすることを提供するNativeWindow
    /// 
    /// NativeWindowはウィンドウ ハンドルとウィンドウ プロシージャの下位のカプセル化を提供します。
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public sealed class SelectableNativeWindow : System.Windows.Forms.NativeWindow
    {
        /// <summary>
        /// 対象コントロール
        /// </summary>
        private ISelectable _parentControl;

        /// <summary>
        /// サブクラス化対象コントロール
        /// </summary>
        private IntPtr _target;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">対象コントロール</param>
        /// <param name="target">サブクラス化対象ハンドル</param>
        public SelectableNativeWindow(Control parent, IntPtr target)
        {
            var notSelectable = parent as ISelectable;
            if (notSelectable == null) throw new ArgumentException("ISelectableインターフェイスを実装している必要があります。");

            _target = target;

            parent.HandleCreated += new EventHandler(this.OnHandleCreated);
            parent.HandleDestroyed += new EventHandler(this.OnHandleDestroyed);

            this._parentControl = notSelectable;
        }

        /// <summary>
        /// AssignHandle
        /// </summary>
        /// <param name="sender">senderオブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal void OnHandleCreated(object sender, EventArgs e)
        {
            AssignHandle(_target);
        }

        /// <summary>
        /// ReleaseHandle
        /// </summary>
        /// <param name="sender">senderオブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal void OnHandleDestroyed(object sender, EventArgs e)
        {
            ReleaseHandle();
        }

        /// <summary>
        /// WndProcのオーバーライド
        /// </summary>
        /// <param name="m">Windows Message</param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (_parentControl.DesignMode)
            {
                base.WndProc(ref m);
                return;
            }

            if (!_parentControl.Selectable)
            {
                // このあたりはお好みの挙動にあわせて、取ったり付けたりして利用しましょう
                if (m.Msg == (int)PlatformInvoke.WM_MOUSE.WM_LBUTTONDOWN) return;
                if (m.Msg == (int)PlatformInvoke.WM_MOUSE.WM_RBUTTONDOWN) return;
                if (m.Msg == (int)PlatformInvoke.WM_MOUSE.WM_MBUTTONDOWN) return;

                if (m.Msg == (int)PlatformInvoke.WM_MOUSE.WM_LBUTTONDBLCLK) return;
                if (m.Msg == (int)PlatformInvoke.WM_MOUSE.WM_RBUTTONDBLCLK) return;
                if (m.Msg == (int)PlatformInvoke.WM_MOUSE.WM_MBUTTONDBLCLK) return;

                if (m.Msg == (int)PlatformInvoke.WM_MOUSE.WM_MBUTTONDOWN) return;

                if (m.Msg == PlatformInvoke.WM_PASTE) return;

                if (m.Msg == PlatformInvoke.WM_SETFOCUS)
                {
                    PlatformInvoke.SetFocus(m.WParam);
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}
