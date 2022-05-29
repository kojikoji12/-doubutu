using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 動物将棋
{
    class Koma
    {
        //10種類のコマが　８方向に移動できるかできないかを作る
        public  struct Direction
        {
            public int x;
            public int y;
            public Direction(int X,int Y)
            {
                this.x = X;
                this.y = Y;
            }
        }
        public enum KomaNumber
        {
            EMP = 0,
            //先手のコマ
            HIY = 1,
            KIR = 2,
            ZOU = 3,
            NIW = 4,
            RAI = 5,
            //後手のコマ
            EHIY = 6,
            EKIR = 7,
            EZOU = 8,
            ENIW = 9,
            ERAI = 10,
        }
        //8方向
        public static Direction[] direction =  {
            new Direction(1,0),     //→
            new Direction(1,1),    //→↓
            new Direction(0,1),     //↓
            new Direction(-1,1),    //←↓
            new Direction(-1,0),    //←
            new Direction(-1,-1),   //←↑
            new Direction(0,-1),    //↑
            new Direction(1,-1),    //↑→
        };

        public static int[,] canGo =new int[8,(int)KomaNumber.ERAI]
        {   //→　　　　　　　　　
            //ヒ,キ,ゾ,ニ,ラ　先手
            { 0, 1 ,0, 1 ,1  ,
            //ヒ,キ,ゾ,ニ,ラ　 後手
              0 , 1, 0, 1, 1},
            //→↓
            //ヒ,キ,ゾ,ニ,ラ　先手
            { 0, 0, 1, 0, 1,
            //ヒ,キ,ゾ,ニ,ラ　 後手
              0, 0, 1, 1, 1},
            //↓
            //ヒ,キ,ゾ,ニ,ラ　先手
            { 0, 1, 0, 1, 1,
            //ヒ,キ,ゾ,ニ,ラ　 後手
              1, 1, 0, 1, 1},
            //←↓
            //ヒ,キ,ゾ,ニ,ラ　先手
            { 0, 0, 1, 0, 1,
            //ヒ,キ,ゾ,ニ,ラ　 後手
              0, 0, 1, 1, 1},
            //←
            //ヒ,キ,ゾ,ニ,ラ　先手
            { 0, 1, 0, 1, 1,
            //ヒ,キ,ゾ,ニ,ラ　 後手
              0, 1, 0, 1, 1},
            //←↑
            //ヒ,キ,ゾ,ニ,ラ　先手
            { 0, 0, 1, 1, 1,
            //ヒ,キ,ゾ,ニ,ラ　 後手
              0, 0, 1, 0, 1},
            //↑
            //ヒ,キ,ゾ,ニ,ラ　先手
            { 1, 1, 0, 1, 1,
            //ヒ,キ,ゾ,ニ,ラ　 後手
              0, 1, 0, 1, 1},
            //→↑
            //ヒ,キ,ゾ,ニ,ラ　先手
            { 0, 0, 1, 1, 1,
            //ヒ,キ,ゾ,ニ,ラ　 後手
              0, 0, 1, 0, 1},
        };
        public static bool isSelf(int teban, int y, int x,Board b)
        {
            int koma = b.board[y, x];
            if (teban == 0)
            {//先手の時
                if ((int)KomaNumber.HIY <= koma && koma <= (int)KomaNumber.RAI) return true;
                else return false;
            }
            else
            {//後手の時
                if ((int)KomaNumber.EHIY <= koma && koma <= (int)KomaNumber.ERAI) return true;
                else return false;
            }


        }

        public bool isEnemy()
        {
            return false;
        }
    }
}
