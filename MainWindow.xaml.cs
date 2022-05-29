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
using PlayMp3;

namespace 動物将棋
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Board mainBoard = new Board();
        private int teban;　　　　　　　　//現在の手番
        private int yourTurn;　　　　　//人間の手番　　　
        public int Maxdepth{get;set;}   //深さ
        private int value;
        string greeting;
        string bgm;
        string komaSe;
        public MainWindow()
        {
            yourTurn = 0;
            teban = 0;
            InitializeComponent();
            Maxdepth = 2;
            DrawBorder();
            ImageInit();
            ReNewBoard();
            yourTurn = 0;
            bgm = Music.LoadMp3(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\音楽\bgm.mp3", "bgm");
            
            Music.ChanegeVolume(bgm, 100);
            Music.Repeat(bgm);

          greeting= Music.LoadMp3(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\音楽\「よ、よろしくお願いします」.mp3", "greeting");
            komaSe = Music.LoadMp3(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\音楽\将棋の駒を打つ.mp3", "komaSe"); 
        }
        /// <summary>
        /// 盤面に線を引く
        /// </summary>
        private void DrawBorder()
        {
            
            for(int row=0;row<Board.Masu_Ynum; row++)
            {
                for(int clm = 0; clm < Board.Masu_Xnum; clm++)
                {
                    Border border = new Border();
                    border.BorderBrush = Brushes.SkyBlue;
                    border.BorderThickness = new Thickness(1);
                    border.SetValue(Grid.RowProperty, row);
                    border.SetValue(Grid.ColumnProperty, clm);
                    GridBoard.Children.Add(border);
                }
            }
           
        }
        private BitmapImage[] komaImage=new BitmapImage[11];    //画像読み込みよう
        private Image[,] image = new Image[4,3];　　　　　　　　//盤面にコマ表示用
        private Image haveImage = new Image();                          //選択中のコマ表示
        private Image[,] handImage = new Image[2,3];
        
        /// <summary>
        /// 画像の初期設定
        /// </summary>
        private void ImageInit()
        {
            ////////////////////////駒の画像読み込み/////////////////////////////////////
            for (int i = (int)Koma.KomaNumber.EMP; i <= (int)Koma.KomaNumber.ERAI; i++)
            {
                komaImage[i] = new BitmapImage();
                komaImage[i].BeginInit();
            }

            //気が向いたらやろう!!
            //strinｇ　filename　for　でちょっと楽する

            //から
            komaImage[(int)Koma.KomaNumber.EMP].UriSource = new Uri(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\画像\から.png");
            //ひよこ
            komaImage[(int)Koma.KomaNumber.HIY].UriSource = new Uri(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\画像\ひよこ.png");
            komaImage[(int)Koma.KomaNumber.EHIY].UriSource = new Uri(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\画像\ひよこ.png");
            //ぞう
            komaImage[(int)Koma.KomaNumber.ZOU].UriSource = new Uri(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\画像\ぞう.png");
            komaImage[(int)Koma.KomaNumber.EZOU].UriSource = new Uri(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\画像\ぞう.png");
            //きりん
            komaImage[(int)Koma.KomaNumber.KIR].UriSource = new Uri(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\画像\きりん.png");
            komaImage[(int)Koma.KomaNumber.EKIR].UriSource = new Uri(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\画像\きりん.png");
            //にわとり
            komaImage[(int)Koma.KomaNumber.NIW].UriSource = new Uri(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\画像\にわとり.png");
            komaImage[(int)Koma.KomaNumber.ENIW].UriSource = new Uri(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\画像\にわとり.png");
            //らいおん
            komaImage[(int)Koma.KomaNumber.RAI].UriSource = new Uri(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\画像\らいおん.png");
            komaImage[(int)Koma.KomaNumber.ERAI].UriSource = new Uri(@"C:\Users\qub85\OneDrive\Desktop\動物将棋\画像\らいおん.png");
            //後手のコマを回転させる
            for (int i = (int)Koma.KomaNumber.EHIY; i <= (int)Koma.KomaNumber.ERAI; i++)
            {
                komaImage[i].Rotation = Rotation.Rotate180;
            }
            for (int i = (int)Koma.KomaNumber.HIY; i <= (int)Koma.KomaNumber.ERAI; i++)
            {
                komaImage[i].EndInit();
            }

            ////////////////////ここからはimageを張り付けていく！！/////////////////////////////////////
            //ボード
            for (int row = 0; row < Board.Masu_Ynum; row++)
            {
                for (int col = 0; col < Board.Masu_Xnum; col++)
                {
                    image[row, col] = new Image();
                    image[row,col].SetValue(Grid.RowProperty, row);
                    image[row,col].SetValue(Grid.ColumnProperty, col);
                    GridBoard.Children.Add(image[row,col]);
                }
            }

            //駒台
            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    handImage[i, j] = new Image();
                    handImage[i, j].SetValue(Grid.RowProperty, j);
                    if (i ==0 )
                    {
                        My駒台.Children.Add(handImage[i, j]);
                    }
                    else
                    {
                        AI駒台.Children.Add(handImage[i, j]);
                    }
                }
            }
           
           

        }


        /// <summary>
        /// 駒の描画
        /// </summary>
        ///
        private void ReNewBoard()
        {
            //盤面
            for (int row = 0; row < Board.Masu_Ynum; row++)
            {
                for (int col = 0; col < Board.Masu_Xnum; col++)
                {
                    int komaKind = mainBoard.board[row, col];
                    {
                        image[row, col].Source = komaImage[komaKind];
                    }
                }
            }
            //持ち駒
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    if (mainBoard.hand[i, j] > 0)
                    {
                        handImage[i, j].Source = komaImage[j + i * 5 + 1];
                    }
                    else handImage[i, j].Source = komaImage[0];
                }
            }
            //デバッグ用

            /*
            
            sashiteinf.Text = "from x:"+Te.sashite.from.x.ToString()+"\n";
            sashiteinf.AppendText("from y:"+Te.sashite.from.y.ToString()+"\n");
            sashiteinf.AppendText("to x:" + Te.sashite.to.x.ToString() + "\n");
            sashiteinf.AppendText("to y:" + Te.sashite.to.y.ToString() + "\n");
            sashiteinf.AppendText("駒:" + Te.sashite.kind.ToString() + "\n");
            sashiteinf.AppendText("評価値:" + value + "\n");*/

        }

        private static int leftMouseFlag = 0;

        private void window_MouseMove(object sender, MouseEventArgs e)
        {

            if (yourTurn == teban) return;
            Think.MinMax(teban, 0, Maxdepth, mainBoard, ref Te.sashite);
            mainBoard.move(teban, Te.sashite);
            value = Think.CaluculateKomaValues(mainBoard, teban);
            if (value >= Think.INFIIETE) MessageBox.Show("あなたの勝ちです");
            if (value <= -Think.INFIIETE) MessageBox.Show("あなた負けです");
            ReNewBoard();
            teban = 1 - teban;

        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (yourTurn != teban) return; //自分のターンのみ操作可
            Point MouseBoardPos = Mouse.GetPosition(GridBoard);
            //ボードの場所を計算
            int x = (int)MouseBoardPos.X / ((int)GridBoard.ActualWidth / 3);
            int y = (int)MouseBoardPos.Y / ((int)GridBoard.ActualHeight / 4);
            int teNum = 0;

            Te.MakeLeagalMove(teban, ref Te.buff, ref teNum, mainBoard);
            //デバッグよう
            /*sashiteinf.AppendText("合法手:" + teNum.ToString() + "\n");
            for (int i = 0; i < teNum; i++)
            {
                sashiteinf.AppendText("x=" + Te.buff[i].from.x.ToString() + "y=" + Te.buff[i].from.y.ToString() + " 駒" + Te.buff[i].kind.ToString() + " " +
                    "→x=" + Te.buff[i].to.x.ToString() + "y=" + Te.buff[i].to.y.ToString() + "\n");
            }
            */
            if (0 <= x && x < 3 && 0 <= y && y < 4)
            {

                switch (leftMouseFlag)
                {
                    case 0: //駒を未選択時
                        
                        
                        if (!Koma.isSelf(teban, y, x,mainBoard)) return;//自分のコマ以外を選択なら終わり
                        //駒の種類を保存
                        Te.sashite.kind = mainBoard.board[y, x];
                        //座標を保存
                        Te.sashite.from.x = x;
                        Te.sashite.from.y = y;

                        
                        //選択中のコマの画像を変更する
                        Point MousePos = Mouse.GetPosition(canvas);
                        canvas.Children.Add(haveImage);
                        haveImage.Source = komaImage[Te.sashite.kind];
                        Canvas.SetTop(haveImage, MousePos.Y - 50);
                        Canvas.SetLeft(haveImage, MousePos.X - 50);

                        //＜コマ選択中＞のフラグにする
                        leftMouseFlag = 1;
                        break;
                    case 1:                             
                        //座標を保存
                        Te.sashite.to.x = x;
                        Te.sashite.to.y = y;
                        Te.sashite.get = mainBoard.board[y, x];


                        //移動できないよ
                        if (Te.CheckLeagalMove(teban, Te.sashite, mainBoard) == false) {
                            leftMouseFlag = 0;                          //＜コマ未選択＞のフラグにする
                            canvas.Children.Remove(haveImage);          //駒の画像削除
                            MessageBox.Show("そこには移動できません。\nルールはググってね💛");
                            return;
                        }
                        
                        //移動する
                        if (Te.sashite.from.y < 0)
                        {//持ち駒を打つ場合
                            
                        }else mainBoard.board[Te.sashite.from.y, Te.sashite.from.x] = 0;
                        mainBoard.board[Te.sashite.to.y, Te.sashite.to.x] = Te.sashite.kind;

                        mainBoard.move(teban, Te.sashite);

                        Music.Operate(komaSe, "play");
                        leftMouseFlag = 0;                          //＜コマ未選択＞のフラグにする
                        canvas.Children.Remove(haveImage);          //駒の画像削除
                        value = Think.CaluculateKomaValues(mainBoard,teban);
                        teban = 1 - teban;
                        if (value >= Think.INFIIETE) MessageBox.Show("あなたの勝ちです");
                        if (value <= -Think.INFIIETE) MessageBox.Show("あなた負けです");
                        
                        
                        break;
                }
            }
            
           
           
            
            ReNewBoard();
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftMouseFlag == 0) return;
            Point MousePos = Mouse.GetPosition(canvas);

            Canvas.SetTop(haveImage, MousePos.Y - 50);
            Canvas.SetLeft(haveImage, MousePos.X - 50);

        }

        private void My駒台_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (yourTurn != teban) return; //自分のターンのみ操作可
            int y;
            if (teban == 0)//先手
            {
                Point MouseHandPos = Mouse.GetPosition(My駒台);
                y = (int)MouseHandPos.Y / ((int)My駒台.ActualHeight / 3);
                if (0 <= y && y < 3)
                {
                    if (mainBoard.hand[teban, y] == 0) return;//こまないのにクリックしたら進めない
                    Te.sashite.from.x = -1;
                    Te.sashite.from.y = - 1;     
                    Te.sashite.kind = y + 1;       //y　0ひよこ　1キリン　2像　となってるので　KomaNumberに合わせるために1+
                    Point MousePos = Mouse.GetPosition(canvas);
                    canvas.Children.Add(haveImage);
                    haveImage.Source = komaImage[Te.sashite.kind];
                    Canvas.SetTop(haveImage, MousePos.Y - 50);
                    Canvas.SetLeft(haveImage, MousePos.X - 50);
                    leftMouseFlag = 1;
                }

            }
            
           
        }
        /// <summary>
        /// デバッグ用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AI駒台_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            if (teban == 0) return;
            Point MouseHandPos = Mouse.GetPosition(AI駒台);
            int y = (int)MouseHandPos.Y / ((int)AI駒台.ActualHeight / 3);
            if (0 <= y && y < 3)
            {
                if (mainBoard.hand[teban, y] == 0) return;
                Te.sashite.from.x = -1;
                Te.sashite.from.y = -y - 1;
                Te.sashite.kind = y +5+ 1;
                Point MousePos = Mouse.GetPosition(canvas);
                canvas.Children.Add(haveImage);
                haveImage.Source = komaImage[Te.sashite.kind];
                Canvas.SetTop(haveImage, MousePos.Y - 50);
                Canvas.SetLeft(haveImage, MousePos.X - 50);
                leftMouseFlag = 1;
            }
        }

        private void canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            leftMouseFlag = 0;                          //＜コマ未選択＞のフラグにする
            canvas.Children.Remove(haveImage);          //駒の画像削除
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Music.Operate(greeting, "play");
        }
        private int bgm_F=0;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (bgm_F == 0)
                Music.Operate(bgm, "stop");                        
            else Music.Repeat(bgm);
            bgm_F = 1 - bgm_F;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            mainBoard.BoardInit();
            teban = 0;
            leftMouseFlag = 0;
            ReNewBoard();
        }

        private void Level_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Maxdepth = (Level.SelectedIndex)*2+1 ;
            
        }

       
    }
}
