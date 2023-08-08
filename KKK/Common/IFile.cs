using System;
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
    public interface IFile
    {
        ///ファイルのパス
        string Path { get; set; }

        /// <summary>
        /// ファイルが読み取り専用かどうか
        /// true:読み取り専用
        /// </summary>
        bool IsReadOnly { get; set; }

        /// <summary>
        /// テキストファイルを書き込みます
        /// <param name="path">パス</param>
        /// </summary>
        void Save(string path);

        /// <summary>
        /// テキストファイルを読み込みます
        /// </summary>
        /// <param name="path">パス</param>
        void Load(string path);

    } //interface
}
