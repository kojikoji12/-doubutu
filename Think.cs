using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 動物将棋
{


    class Think :Te 
    {
        
        public const  int INFIIETE = 999999;
        public static int[] KomaValues =//空+コマ10＝11個
        {
            //EMP ひ　キ　ゾ　ニ　ラ　vひ　vキ　vぞ　vニ　vラ
            0,   100, 150,140,130,INFIIETE,-100,-150,-140,-130,-INFIIETE,
        };
        /// <summary>
        /// コマ得関数
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int CaluculateKomaValues(Board b,int teban)
        {
            if (b.KingCheck() != 0)
            {
                return b.KingCheck();
            }
            int sum = 0;
            //盤面のコマ
            for(int row = 0; row < Board.Masu_Ynum; row++)
            {
                for(int col = 0; col < Board.Masu_Xnum; col++)
                {
                    sum += KomaValues[b.board[row, col]];
                }
            }
            if (b.TryCheck(teban) != 0)
            {
                return b.TryCheck(teban);
            }
            //持ち駒

            for (int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (i == 0)
                    {
                        sum += KomaValues[j+1] * b.hand[0, j];
                    }else sum -= KomaValues[j + 1] * b.hand[1, j];
                }
            }
            return sum;
        }
        /// <summary>
        /// miimax
        /// </summary>
        /// <param name="depth">現在の読みの深さ</param>
        /// <param name="MaxDepth">最大の読みの深さ</param>
        /// <param name="b">盤面</param
        /// <param name="teban">手番</param>
        static public  int MinMax(int teban,int depth,int MaxDepth, Board b,ref SaShiteInf best)
        {
            
            if (b.KingCheck() != 0)
            {
                return b.KingCheck();
            }
            if (b.TryCheck(teban)!=0)
            {
                return b.TryCheck(teban);
            }
            if (depth == MaxDepth) 　　//MaxDepth手後の盤面の評価値
            {
                return CaluculateKomaValues(b,teban);
            }
            
            
           
            SaShiteInf[] buf = new SaShiteInf[100]; //合法手の保存
            int teNum = 0;             //合法手の数
            MakeLeagalMove(teban,ref buf,ref teNum, b);//合法手生成
            Board nextBoard=b.DeeoCopy();  //移動させるようの盤面                               
            nextBoard.move(teban,buf[0]);
           
            if (teNum == 0)
            {
                if (teban == 0) return -INFIIETE;
                else return INFIIETE;
            }
            SaShiteInf childbest = new SaShiteInf();
            int bestValue = MinMax(1-teban, depth+1, MaxDepth,nextBoard,ref childbest);
            best = buf[0];

            for (int i = 1; i < teNum; i++)
            {
                nextBoard = b.DeeoCopy();
                nextBoard.move(teban, buf[i]);
                int newValue = MinMax(1-teban, depth + 1, MaxDepth, nextBoard, ref childbest);
                if (teban == 0)                     //先手 Max
                {
                    if (bestValue < newValue)
                    {
                        bestValue =newValue;
                        best = buf[i];
                    }
                }
                else                                //後手　Min
                {
                    if (bestValue > newValue)
                    {
                        bestValue = newValue;
                        best = buf[i];
                    }
                }
            }
            return bestValue;
        }
        
    }

   
        
}
