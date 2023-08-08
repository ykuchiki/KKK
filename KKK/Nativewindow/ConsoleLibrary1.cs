using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ConsoleLibrary1.NativeWindow;

namespace ConsoleLibrary1
{
    /// <summary>
    /// コントロールを選択不可能な状態にすることが可能であることを
    /// 明示するためのインターフェイス
    /// 
    /// ※元々選択可能であるコントロールについて、
    /// 　選択不可能な状態にすることを提供するためのインターフェイスです。
    /// ※これは選択不可能なコントロールを
    /// 　選択可能状態へすることを提供するためのものではありません。
    /// 
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// コントロールが選択可能であるかどうかを取得または設定します。
        /// </summary>
        bool Selectable { get; set; }

        /// <summary>
        /// NativeWindowList
        /// </summary>
        List<SelectableNativeWindow> NativeWindowList { set; }

        /// <summary>
        /// NativeWindowのAssignHandle対象ハンドルを取得します。
        /// </summary>
        /// <returns></returns>
        IEnumerable<IntPtr> GetAssignHandles();

        /// <summary>
        /// デザインモードか否かを取得します。
        /// </summary>
        bool DesignMode { get; }
    }

    public static class ISelectableExtensions
    {
        /// <summary>
        /// コントロールの選択可否についての初期化処理
        /// </summary>
        /// <param name="self"></param>
        public static void InitializeSelectable(this ISelectable self)
        {
            var control = self as Control;
            if (control == null) throw new ArgumentException("コントロールである必要があります。");

            var list = new List<SelectableNativeWindow>();
            foreach (var handle in self.GetAssignHandles())
            {
                var snw = new SelectableNativeWindow(control, handle);
                list.Add(snw);
            }
            self.NativeWindowList = list;
        }
    }
}