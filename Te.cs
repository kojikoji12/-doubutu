using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 動物将棋
{
    class Te:Koma   
    {
        public struct From
        {
            public int x;
            public int y;
        }

        public struct To
        {
            public int x;
            public int y;
        }

        public struct SaShiteInf
        {
            public int kind;   //何が
            public From from;  //どこから
            public To to;　　  //どこへ
            public int get;    //敵のコマ
        }
        public static SaShiteInf sashite = new SaShiteInf();
        public static SaShiteInf []buff = new SaShiteInf[200];
       
        public static void MakeLeagalMove(int sengo,ref SaShiteInf[] buf,ref int teNum,Board bo)
        {
            //盤面のコマを動かす場合
            SaShiteInf now = new SaShiteInf();//*今調べるよう
            for (int y = 0; y<4; y++)
            {
                for(int x = 0; x< 3; x++)
                {
                    if (isSelf(sengo, y, x, bo))//自分のコマなら
                    {
                       
                        now.kind = bo.board[y, x];
                        //その駒の移動先を調べる
                        //移動できない条件　①駒の能力　②自分のコマがある　③盤外
                        for(int dir = 0; dir < 8; dir++)
                        {//8方向を調べる（①コマの能力）
                            if (canGo[dir,now.kind-1] == 1)
                            {
                                //移動先の座標
                                now.to.x= direction[dir].x+x;
                                now.to.y= direction[dir].y+y;
                                
                                //③盤外
                                if (now.to.x < 0 || now.to.x >= Board.Masu_Xnum || now.to.y < 0 || now.to.y >= Board.Masu_Ynum) continue;
                                //②自分駒
                                if (isSelf(sengo, now.to.y, now.to.x, bo)) continue;
                                //条件達成！！
                                now.from.x = x;
                                now.from.y = y;
                                now.get = bo.board[now.to.y, now.to.x];
                                //デバッグ用
                               // now.to.x = direction[dir].x + 1;
                                //now.to.y = direction[dir].y + 3;
                               // now.from.x = 1;
                               // now.from.y = 3;
                                //now.kind = 5;
                                buf[teNum] = now;
                                teNum++;
                            }
                        }

                    }
                }
            }
            //持ち駒を動かす場合
            for (int i = 0; i < 3; i++)
            {
                if (bo.hand[sengo, i] > 0)
                {
                    now.from.x = -1;
                    now.from.y = -1;
                    now.kind = i+1+(sengo*5);
                    //打てるのは空の場所だけ
                    for (int y = 0; y < Board.Masu_Ynum; y++)
                    {
                        for (int x = 0; x < Board.Masu_Xnum; x++)
                        {
                            //ひよこは奥に打てないようにする
                            if (now.kind == (int)KomaNumber.HIY && y == 0) continue;
                            if (now.kind == (int)KomaNumber.EHIY && y == 3) continue;
                          
                            
                            
                            if (bo.board[y, x] == (int)KomaNumber.EMP)
                            {
                                now.to.x = x;
                                now.to.y = y;
                                now.get = 0;
                                buf[teNum] = now;
                                teNum++;
                            }
                        }
                    }
                }
                
            }
            
        }

        public static bool CheckLeagalMove(int sengo,SaShiteInf te,Board bo)
        {
            SaShiteInf[] buf =new SaShiteInf [1000];           
            int teNum = 0;
            MakeLeagalMove(sengo,ref buf,ref teNum,bo);
            for(int i = 0; i < teNum; i++)
            {
                if (te.Equals(buf[i])==true)
                {
                   
                    return true;
                }
            }
            
            return false;
        }
    }
    
}
