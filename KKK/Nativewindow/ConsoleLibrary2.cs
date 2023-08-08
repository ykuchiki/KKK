using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace ConsoleLibrary1
{
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public static class PlatformInvoke
    {
        #region 定数
        /// <summary>
        /// フォーカスを取得のメッセージ
        /// </summary>
        public const uint WM_SETFOCUS = 0x0007;
        /// <summary>
        /// フォーカス喪失のメッセージ
        /// </summary>
        public const uint WM_KILLFOCUS = 0x0008;
        /// <summary>
        /// キーDownのメッセージ
        /// </summary>
        public const uint WM_KEYDOWN = 0x0100;
        /// <summary>
        /// キーUpのメッセージ
        /// </summary>
        public const uint WM_KEYUP = 0x0101;

        /// <summary>
        /// ペーストのメッセージ
        /// </summary>
        public const uint WM_PASTE = 0x302;
        #endregion

        #region 列挙型
        /// <summary>
        /// マウスに関するウィンドウメッセージ
        /// </summary>
        public enum WM_MOUSE : int
        {
            WM_MOUSEFIRST = 0x200,
            WM_MOUSEMOVE = 0x200,
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202,
            WM_LBUTTONDBLCLK = 0x203,
            WM_RBUTTONDOWN = 0x204,
            WM_RBUTTONUP = 0x205,
            WM_RBUTTONDBLCLK = 0x206,
            WM_MBUTTONDOWN = 0x207,
            WM_MBUTTONUP = 0x208,
            WM_MBUTTONDBLCLK = 0x209,
            WM_MOUSEWHEEL = 0x020A
        }
        #endregion

        #region 構造体
        /// <summary>
        /// ComboBoxInfo構造体
        /// </summary>
        [StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct ComboBoxInfo
        {
            public int Size;
            public Rectangle RectItem;
            public Rectangle RectButton;
            public int ButtonState;
            public IntPtr ComboBoxHandle;
            public IntPtr EditBoxHandle;
            public IntPtr ListBoxHandle;
        }
        #endregion

        #region DllImport

        /// <summary>
        /// ComboBoxの情報を取得します。
        /// </summary>
        /// <param name="comboBoxHandle">対象コンボボックスのハンドル</param>
        /// <param name="pComboBoxInfo">ComboBoxInfo構造体</param>
        /// <returns></returns>
        [SuppressUnmanagedCodeSecurity()]
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static bool GetComboBoxInfo(
            IntPtr comboBoxHandle,
            ref ComboBoxInfo pComboBoxInfo
        );

        /// <summary>
        /// ウィンドウにメッセージを送信します。この関数は、
        /// 指定したウィンドウのウィンドウプロシージャが処理を終了するまで制御を返しません。
        /// メッセージを送信して直ちに制御を返すには、 SendMessageCallback 関数または SendNotifyMessage 関数を使います。
        /// メッセージをスレッドのメッセージキューにポストして直ちに制御を返すには、 
        /// PostMessage 関数または PostThreadMessage 関数を使います。
        /// </summary>
        /// <param name="hWnd">
        /// メッセージを受け取るウィンドウのハンドルを指定します。0xFFFF (HWND_BROADCAST) を指定すると、すべてのトップレベルウィンドウに送られます。
        /// 0xFFFF (HWND_BROADCAST) を指定すると、システムにあるすべてのトップレベルウィンドウに送られます。
        /// 子ウィンドウに対してはメッセージはメッセージは送られません。
        /// </param>
        /// <param name="Msg">
        /// 送信されるメッセージコードを指定します。
        /// </param>
        /// <param name="wParam">メッセージ固有情報（ウィンドウプロシージャの wParam パラメータ）を指定します。</param>
        /// <param name="lParam">メッセージ固有情報（ウィンドウプロシージャの lParam パラメータ）を指定します。</param>
        /// <returns>メッセージ処理の結果（ウィンドウプロシージャの戻り値）が返ります。戻り値は送られたメッセージによって異なります。</returns>
        [SuppressUnmanagedCodeSecurity()]
        [DllImport("User32.dll", EntryPoint = "SendMessageA", SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 指定されたウィンドウを作成したスレッドに関連付けられているメッセージキューにメッセージをポストします。
        /// この関数は、メッセージの処理の完了を待たずに制御を返します。
        /// 
        /// スレッドに関連付けられているメッセージキューにメッセージをポストするには、 PostThreadMessage 関数を使います。
        /// </summary>
        /// <param name="hwnd">
        /// メッセージを受け取るウィンドウのハンドルを指定します。
        /// 0xFFFF (HWND_BROADCAST) を指定すると、システムにあるすべてのトップレベルウィンドウにポストされます。
        /// 子ウィンドウに対してはメッセージはメッセージはポストされません。
        /// 0 (NULL) を指定すると、 dwThreadId パラメータに現在のスレッド ID を設定して PostThreadMessage 関数を呼び出したかのように動作します。
        /// </param>
        /// <param name="wMsg">
        /// ポストされるメッセージコードを指定します。
        /// </param>
        /// <param name="wParam">メッセージ固有情報（ウィンドウプロシージャの wParam パラメータ）を指定します。</param>
        /// <param name="lParam">メッセージ固有情報（ウィンドウプロシージャの lParam パラメータ）を指定します。</param>
        /// <returns>
        /// 成功すると 0 以外の値が返ります。
        /// 失敗すると 0 が返ります。拡張エラー情報を取得するには、 GetLastError 関数を使います。
        /// </returns>
        [SuppressUnmanagedCodeSecurity()]
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private extern static IntPtr PostMessage(
            IntPtr hwnd,
            uint wMsg,
            IntPtr wParam,
            IntPtr lParam
        );

        /// <summary>
        /// 指定されたウィンドウをキーボードフォーカスを持つウィンドウにします。
        /// 
        /// hWndが示すウィンドウは、この関数を呼び出したスレッドが持つウィンドウである必要があります。
        /// 他のスレッドが持つウィンドウに対するSetFocusの呼び出しは、効果がありません。 
        /// </summary>
        /// <param name="hwnd">対象のウィンドウハンドル</param>
        /// <returns></returns>
        [SuppressUnmanagedCodeSecurity()]
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr SetFocus(
            IntPtr hwnd
        );

        #endregion
    }
}
