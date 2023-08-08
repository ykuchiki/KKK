using KKK.Common; //Commonファイルの内容を呼び出し
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KKK.MEMO
{
    public class AppConfig 
    {
        const string PATH = "config.xml"; //Configのパス

        public void Save()
        {
            SerializeUtils.SerializeToFile(this, PATH);
        }

        ///<summary>
        ///Configを読み込みます
        ///</summary>
        public static AppConfig Load()
        {
            AppConfig config = null;
            if (File.Exists(PATH))
            {
                //ファイルがある場合
                config = (AppConfig)SerializeUtils.DeserializeFormFile(typeof(AppConfig), PATH);
            }
            return config;
        }

        //フォームの位置とサイズ
        public int form_left = 0;
        public int form_top  = 0;
        public int form_width = 0;
        public int fsfs = 0;
        public int form_height = 0;



        //フォント
        public string memo_font_name = "HG創英角ﾎﾟｯﾌﾟ体";
        public float memo_font_size = 21;
        public int memo_font_color = -1;
        public string memo_font_style = "Regular";

        //背景の色
        public int memo_back_color = -16777088;
        public double memo_opacity = 0.70;
    }
}
