using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayMp3
{
    class Music
    {
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int mciSendString(String command,
StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
        /// <summary>
        /// ファイルを読み込んで名前を付ける 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="name"></param>
        public static string LoadMp3(string fileName,string aliasName)
        {
            string cmd = "open \""+ fileName + "\" type mpegvideo alias " + aliasName;
           
            mciSendString(cmd, null, 0, IntPtr.Zero);
            return aliasName;
        }
        /// <summary>
        /// 音量変更
        /// </summary>
        /// <param name="name"></param>
        /// <param name="volume"></param>
        public static void ChanegeVolume(string name,int volume)
        {
            mciSendString("setaudio "+name+ " volume to "+volume.ToString(), null, 0, IntPtr.Zero); //ボリューム
        }

        public static void Operate(string aliasName,string play)
        {
            string cmd=play+" " + aliasName;
            mciSendString("seek "+aliasName +" to start",null, 0, IntPtr.Zero);
            mciSendString(cmd, null, 0, IntPtr.Zero);
        }

        public static void Repeat(string aliasName)
        {
            string cmd = "play " + aliasName+" repeat";
            mciSendString(cmd, null, 0, IntPtr.Zero);
        }
    }
}
