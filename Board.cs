using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace 動物将棋
{
    class Board
    {
        private const int masu_Xnum = 3;
        private const int masu_Ynum = 4;
        public static int Masu_Xnum
        {
            get { return masu_Xnum; }
        }
        public static int Masu_Ynum
        {
            get { return masu_Ynum; }

        }

        
      
       
        /// <summary>
        /// 駒をint型で表現
        /// </summary>
        

        public int[,] board;
        public int [,]hand;
    
        public Board()
        {
            board = new int[4, 3] {
                { (int)Koma.KomaNumber.EKIR,(int)Koma.KomaNumber.ERAI,(int)Koma.KomaNumber.EZOU},
                { 0,(int)Koma.KomaNumber.EHIY,0},
                { 0,(int)Koma.KomaNumber.HIY,0},
                { (int)Koma.KomaNumber.KIR,(int)Koma.KomaNumber.RAI,(int)Koma.KomaNumber.ZOU},

            };
            hand = new int[2, 3]
            {
                { 0,0,0},
                { 0,0,0}
            };
        }
        public void BoardInit()
        {
            board = new int[4, 3] {
                { (int)Koma.KomaNumber.EKIR,(int)Koma.KomaNumber.ERAI,(int)Koma.KomaNumber.EZOU},
                { 0,(int)Koma.KomaNumber.EHIY,0},
                { 0,(int)Koma.KomaNumber.HIY,0},
                { (int)Koma.KomaNumber.KIR,(int)Koma.KomaNumber.RAI,(int)Koma.KomaNumber.ZOU},

            };
            hand = new int[2, 3]
            {
                { 0,0,0},
                { 0,0,0}
            };
        }
        public void move(int sengo,Te.SaShiteInf t)
        {
            //＜削除＞
            //持ち駒の時
            if (t.from.y ==-1) hand[sengo, t.kind-5*sengo-1]--;
            //盤面からの時
            else  board[t.from.y, t.from.x] = 0;

            //＜移動＞
            
            //ニワトリ進化
            if((t.kind==(int)Koma.KomaNumber.HIY && t.to.y == 0) || (t.kind == (int)Koma.KomaNumber.EHIY && t.to.y == 3))
            {
                t.kind = t.kind+3;
            }
            board[t.to.y, t.to.x] = t.kind;
            //＜獲得＞
            if (t.get!=0 && t.get != (int)Koma.KomaNumber.RAI && t.get != (int)Koma.KomaNumber.ERAI)
            {
                //ニワトリゲットしたとき
                if (t.get == (int)Koma.KomaNumber.ENIW || t.get == (int)Koma.KomaNumber.NIW) t.get -= 3;
                hand[sengo, t.get-5+sengo*5-1]++;
            }
            
        }

        public Board DeeoCopy()
        {
            
                Board b = (Board)MemberwiseClone();
                if(b.board!=null)
                    b.board = (int[,])this.board.Clone();
            if (b.hand != null) { 
                b.hand = (int[,])this.hand.Clone();
            }
                    
            
            return b;
        }

        public int KingCheck()
        {
            
            bool raion = false;
            bool Eraion = false;
            for(int row = 0; row < masu_Ynum; row++)
            {
                for(int col = 0; col < masu_Xnum; col++)
                {
                    if (board[row, col] == (int)Koma.KomaNumber.RAI)
                    {
                        raion = true;
                    }
                    if (board[row, col] == (int)Koma.KomaNumber.ERAI)
                    {
                        Eraion = true;
                    }
                }
            }
            if (raion == false) return -Think.INFIIETE;
            if (Eraion == false) return Think.INFIIETE;
            else return 0;
        }

        public int TryCheck(int teban)
        {
            //ﾗｲｵﾝが奥にいる
            //次のターンに自分が取られない
            int row=teban==0?0:3;
            int raion = teban == 0 ? (int)Koma.KomaNumber.RAI : (int)Koma.KomaNumber.ERAI;
            for(int i = 0; i < 3 ; i++)
            {
                if (board[row, i] == raion)
                {
                    return teban == 0 ? Think.INFIIETE : -Think.INFIIETE;
                }
            }
            return 0;
        }

    }     
}
