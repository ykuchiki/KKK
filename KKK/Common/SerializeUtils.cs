using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace KKK.Common
{
    public partial class SerializeUtils
    {
        //フォームの位置とサイズを記憶して、次回起動時に同じ場所サイズで起動します
        /// <summary>
        /// オブジェクトをシリアライズ化してXMLファイルとして出力します
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <param name="path">出力先のパス</param>
        /// <param name="encoding">出力時のエンコーディング</param>
        public static void SerializeToFile(object obj, string path, Encoding encoding = null)
        {
            if (encoding == null) encoding = new UTF8Encoding(false);

            var serializer = new XmlSerializer(obj.GetType());
            using (var stream = new FileStream(path, FileMode.Create))
            using (var writer = new StreamWriter(stream, encoding))
            {
                serializer.Serialize(writer, obj);
            }
        }

        /// <summary>
        /// XMLファイルからオブジェクトを復元します。
        /// </summary>
        /// <param name="type">オブジェクトの型</param>
        /// <param name="path">入力元のパス</param>
        /// <param name="encoding">入力時のエンコーディング</param>
        /// <returns>XMLファイルから復元したオブジェクト</returns>
        public static object DeserializeFormFile(Type type, string path, Encoding encoding = null)
        {
            if (encoding == null) encoding = new UTF8Encoding(false);

            object obj = null;
            var serializer = new XmlSerializer(type);
            using (var stream = new FileStream(path, FileMode.Open))
            using (var reader = new StreamReader(stream, encoding))
            {
                obj = serializer.Deserialize(reader);
            }
            return obj;
        }


    }   
}

