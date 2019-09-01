using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace 木板最优切割方案
{
    public partial class showPicture : Form
    {
        //Thread thread1 = null;//声明一个线程
        int scale = 5;
        int moveX = 50;
        public showPicture()
        {
            InitializeComponent();
            
        }
        private void btnBest_Click(object sender, EventArgs e)
        {
            //Thread thread1 = new Thread(new ThreadStart(DrawBest));//声明一个线程
            //thread1.Start();   //启动线程
            DrawBest();
        }

        private void DrawBest()
        {
            txtbox.Clear();
            if (control.theBest.Count != 0)
            {
                foreach (var item in control.theBest)
                {
                    txtbox.AppendText(item.ToString() + ',');
                }
            }
            coordinate();
            aaaaaaaaa(control.theBest);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //control control = new control();
           
            string[] str = txtbox.Text.Trim().Split(',');
            //string[] str = "-1,-1,-4,3,3,-1,-3,1,-1,3,-1,-1,-3,-3,1,-1,3,-1,-1,-4,-1,1,-4,3,-1,-1,-4,-1,1,1,3,-1,-1,-1,2,1,3,-1,3,-3,4,4,-4,4,4,4,4,4,1,-4,1,".Trim().Split(',');
            List<int> vs = new List<int>();
            foreach (var item in str)
            {
                if (item != "")
                {
                    vs.Add(Int32.Parse(item));
                }
            }
            
            coordinate();
            //aaaaaaaaa(control.theBest);
            aaaaaaaaa(vs);

        }
        void coordinate()
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Brushes.Black, 2);
            int unit = 50;
            int lenY = 3000 / scale;
            int lenX = 1500 / scale;
            for (int i = 0; i <= lenY/unit; i++)
            {
                PointF px1 = new PointF(moveX, i * unit);
                PointF px2 = new PointF(moveX+4, i * unit);
                graphics.DrawLine(pen, px1, px2);
                string str = ((lenY - i * unit) * scale).ToString();
                graphics.DrawString(str, Font, Brushes.Black, new PointF(20, i * unit+3));
            }
            for (int i = 1; i <= lenX / unit; i++) 
            {
                PointF px1 = new PointF(moveX + i * unit, lenY);
                PointF px2 = new PointF(moveX + i * unit, lenY - 4);
                graphics.DrawLine(pen, px1, px2);
                string str = (i * unit * scale).ToString();
                graphics.DrawString(str, Font, Brushes.Black, new PointF(moveX+i * unit, lenY+10));
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            txtbox.Clear();
        }
        void Algorithm()
        {
            
            pictureBox1.Width = 1500 / scale+1000;
            pictureBox1.Height = 3000 / scale + 1000;
            
            //Graphics graphics = pictureBox1.CreateGraphics();

            List<int> gene = new List<int>();
           // StartPopulation_new(gene);

        }

        private static System.Drawing.Rectangle P1_L = new System.Drawing.Rectangle { Width = 373, Height = 201 };//水平
        private static System.Drawing.Rectangle P1_V = new System.Drawing.Rectangle { Width = 201, Height = 373 };//垂直
        private static System.Drawing.Rectangle P2_L = new System.Drawing.Rectangle { Width = 477, Height = 282 };//水平
        private static System.Drawing.Rectangle P2_V = new System.Drawing.Rectangle { Width = 282, Height = 477 };//垂直
        private static System.Drawing.Rectangle P3_L = new System.Drawing.Rectangle { Width = 406, Height = 229 };//水平
        private static System.Drawing.Rectangle P3_V = new System.Drawing.Rectangle { Width = 229, Height = 406 };//垂直
        private static System.Drawing.Rectangle P4_L = new System.Drawing.Rectangle { Width = 311, Height = 225 };//水平
        private static System.Drawing.Rectangle P4_V = new System.Drawing.Rectangle { Width = 225, Height = 311 };//垂直


        private static Rectangle Xy = new Rectangle();
        private bool Mapping_new(bool[,] pic, int gene)
        {
            bool flag = true;
            switch (gene)
            {
                case 1:
                    flag = Check_new(P1_L, pic);
                    break;
                case -1:
                    flag = Check_new(P1_V, pic);
                    break;
                case 2:
                    flag = Check_new(P2_L, pic);
                    break;
                case -2:
                    flag = Check_new(P2_V, pic);
                    break;
                case 3:
                    flag = Check_new(P3_L, pic);
                    break;
                case -3:
                    flag = Check_new(P3_V, pic);
                    break;
                case 4:
                    flag = Check_new(P4_L, pic);
                    break;
                case -4:
                    flag = Check_new(P4_V, pic);
                    break;
                default:
                    Console.WriteLine("基因有问题！！！！\r\n基因有问题！！！！\r\n基因有问题！！！！\r\n基因有问题！！！！\r\n");
                    break;
            }
            return flag;
        }
        private void Decode_new(List<int> gene)
        {
            bool[,] pic = new bool[1501, 3001];//记录点的坐标
            for (int i = 0; i < 1501; i++)
            {
                for (int j = 0; j < 3001; j++)
                {
                    pic[i, j] = false;
                }
            }
            for (int i = 0; i < 3001; i++)
            {
                pic[0, i] = true;//把方框左边填充
            }
            for (int i = 0; i < 1501; i++)
            {
                pic[i, 0] = true;//把方框底边填充
            }
            List<int> library_num = new List<int>() { 1, -1, 2, -2, 3, -3, 4, -4 };
            for (int i = 0; i < gene.Count; i++)
            {
                if (!Mapping_new(pic, gene[i]))
                {
                    //检测到非法基因，开始更改基因点！
                    if (library_num.Count == 0)
                    {
                        //尝试所有类型方块都不行,开始删除后面的基因点
                        gene.RemoveRange(i, gene.Count - i);
                        return;
                    }
                    Random r = new Random();
                    library_num.Remove(gene[i]);
                    gene[i] = library_num[r.Next(0, library_num.Count)];
                    i--;
                }
                else
                {
                    library_num = new List<int>() { 1, -1, 2, -2, 3, -3, 4, -4 };
                }
            }
            //到达此处说明基因现在没问题，可尝试接着添加基因点
            List<int> library_num2 = new List<int>() { 1, -1, 2, -2, 3, -3, 4, -4 };
            Random rand = new Random();
            while (library_num2.Count != 0)
            {
                int index = rand.Next(0, library_num2.Count);
                if (Mapping_new(pic, library_num2[index]))
                {
                    //如果可行
                    gene.Add(library_num2[index]);
                    library_num2 = new List<int>() { 1, -1, 2, -2, 3, -3, 4, -4 };
                }
                else
                {
                    //如果不可行
                    library_num2.Remove(library_num2[index]);
                }
            }
        }
        private void StartPopulation_new(List<int> gene)//初始种群
        {
            bool[,] pic = new bool[1501, 3001];//记录点的坐标
            for (int i = 0; i < 1501; i++)
            {
                for (int j = 0; j < 3001; j++)
                {
                    pic[i, j] = false;
                }
            }
            for (int i = 0; i < 3001; i++)
            {
                pic[0, i] = true;//把方框左边填充
            }
            for (int i = 0; i < 1501; i++)
            {
                pic[i, 0] = true;//把方框底边填充
            }
            List<int> library_num2 = new List<int>() { 1, -1, 2, -2, 3, -3, 4, -4 };
            Random rand = new Random();
            while (library_num2.Count != 0)
            {
                int index = rand.Next(0, library_num2.Count);
                if (Mapping_new(pic, library_num2[index]))
                {
                    //如果可行
                    gene.Add(library_num2[index]);
                    library_num2 = new List<int>() { 1, -1, 2, -2, 3, -3, 4, -4 };
                }
                else
                {
                    //如果不可行
                    library_num2.Remove(library_num2[index]);
                }
            }
        }
        private bool Check_new(System.Drawing.Rectangle rect, bool[,] pic)
        {
            Point low1 = new Point { X = 1500 - rect.Width, Y = 3000 };
            Point low2 = new Point { X = 1500 - rect.Width + rect.Width / 3, Y = 3000 };
            Point low3 = new Point { X = 1500 - rect.Width + rect.Width * 2 / 3, Y = 3000 };
            Point low4 = new Point { X = 1500, Y = 3000 };
            Point left1 = new Point { X = 1500 - rect.Width, Y = 3000 + rect.Height };
            Point left2 = new Point { X = 1500 - rect.Width, Y = 3000 + rect.Height * 2 / 3 };
            Point left3 = new Point { X = 1500 - rect.Width, Y = 3000 + rect.Height / 3 };
            do
            {
                if (!CheckLowLine(low1, low2, low3, low4, pic))//不接触底线时
                {
                    low1.Y--; low2.Y--; low3.Y--; low4.Y--; left1.Y--; left2.Y--; left3.Y--;
                }
                else if (!CheckLeftLine(left1, left2, left3, low1, pic))//接触底线而不接触左线时
                {
                    low1.X--; low2.X--; low3.X--; low4.X--; left1.X--; left2.X--; left3.X--;
                }
                else//都接触时
                {
                    if (CheckTop(low1, rect))
                        break;
                    Xy.X = low1.X;
                    Xy.Y = low1.Y;
                    Xy.Height = rect.Height;
                    Xy.Width = rect.Width;
                    FullPic(low1, pic, rect);
                    if (low1.X < 0 || low1.Y < 0)
                    {
                        MessageBox.Show("小于小于0");
                    }
                    Graphics graphics = pictureBox1.CreateGraphics();
                    graphics.DrawRectangle(Pens.Black, Xy.X / scale + moveX, (3000 - Xy.Y - rect.Height) / scale, Xy.Width / scale, Xy.Height / scale);
                    return true;
                }
            } while (true);
            return false;

            bool CheckLowLine(Point point1, Point point2, Point point3, Point point4, bool[,] pic1)
            {
                if (point1.X >= 0)
                {
                    if (pic1[point1.X, point1.Y] && pic1[point1.X + 1, point1.Y])
                        return true;
                    else if (pic1[point2.X, point2.Y] && pic1[point2.X + 1, point2.Y])
                        return true;
                    else if (pic1[point3.X, point3.Y] && pic1[point3.X + 1, point3.Y])
                        return true;
                    else if (pic1[point4.X, point4.Y] && pic1[point4.X - 1, point4.Y])
                        return true;
                }
                return false;
            }
            bool CheckLeftLine(Point point1, Point point2, Point point3, Point point4, bool[,] pic1)
            {
                if (point1.Y <= 3000 && pic1[point1.X, point1.Y] && pic1[point1.X, point1.Y - 1])
                    return true;
                else if (point2.Y <= 3000 && pic1[point2.X, point2.Y] && pic1[point2.X, point2.Y - 1])
                    return true;
                else if (point3.Y <= 3000 && pic1[point3.X, point3.Y] && pic1[point3.X, point3.Y - 1])
                    return true;
                else if (point4.Y <= 3000 && pic1[point4.X, point4.Y] && pic1[point4.X, point4.Y + 1])
                    return true;
                return false;
            }
            bool CheckTop(Point point, System.Drawing.Rectangle rect1)
            {
                if (point.Y + rect1.Height > 3000)
                    return true;
                return false;
            }
            void FullPic(Point point, bool[,] pic1, System.Drawing.Rectangle rect1)
            {
                for (int i = point.X; i <= point.X + rect1.Width; i++)
                {
                    for (int j = point.Y; j <= point.Y + rect1.Height; j++)
                    {
                        pic1[i, j] = true;
                    }
                }
            }
        }


        void DrawPic(List<int> line)
        {
            //coordinate();
            //aaaaaaaaa(control.)
        }
        private void aaaaaaaaa(List<int> line)
        {
            bool[,] pic = new bool[1501, 3001];//记录点的坐标
            for (int i = 0; i < 1501; i++)
            {
                for (int j = 0; j < 3001; j++)
                {
                    pic[i, j] = false;
                }
            }
            for (int i = 0; i < 3001; i++)
            {
                pic[0, i] = true;//把方框左边填充
            }
            for (int i = 0; i < 1501; i++)
            {
                pic[i, 0] = true;//把方框底边填充
            }
            Rectangle rect = P1_L;
            
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.DrawRectangle(Pens.Black, 0 + moveX, 0, 1500 / scale, 3000 / scale);
            //bool flag = false;
            //Bitmap map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            foreach (var item in line)
            {
                Thread.Sleep(100);
                Brush brush= Brushes.Blue;
                switch (item)
                {
                    case 1:
                        rect = P1_L;
                        break;
                    case -1:
                        rect = P1_V;
                        break;
                    case 2:
                        rect = P2_L;
                        break;
                    case -2:
                        rect = P2_V;
                        break;
                    case 3:
                        rect = P3_L;
                        break;
                    case -3:
                        rect = P3_V;
                        break;
                    case 4:
                        rect = P4_L;
                        break;
                    case -4:
                        rect = P4_V;
                        break;
                    default:
                        break;
                }
                int type = Math.Abs(item);
                switch (type)
                {
                    case 1:
                        brush = Brushes.Red;
                        break;
                    case 2:
                        brush = Brushes.Yellow;
                        break;
                    case 3:
                        brush = Brushes.Blue;
                        break;
                    case 4:
                        brush = Brushes.Green;
                        break;
                    default:
                        break;
                }

                Point low1 = new Point { X = 1500 - rect.Width, Y = 3000 };
                Point low2 = new Point { X = 1500 - rect.Width + rect.Width / 3, Y = 3000 };
                Point low3 = new Point { X = 1500 - rect.Width + rect.Width * 2 / 3, Y = 3000 };
                Point low4 = new Point { X = 1500, Y = 3000 };
                Point left1 = new Point { X = 1500 - rect.Width, Y = 3000 + rect.Height };
                Point left2 = new Point { X = 1500 - rect.Width, Y = 3000 + rect.Height * 2 / 3 };
                Point left3 = new Point { X = 1500 - rect.Width, Y = 3000 + rect.Height / 3 };
                
                while (true)
                {
                    if (!CheckLowLine(low1, low2, low3, low4, pic))//不接触底线时
                    {
                        low1.Y--; low2.Y--; low3.Y--; low4.Y--; left1.Y--; left2.Y--; left3.Y--;
                    }
                    else if (!CheckLeftLine(left1, left2, left3, low1, pic))//接触底线而不接触左线时
                    {
                        low1.X--; low2.X--; low3.X--; low4.X--; left1.X--; left2.X--; left3.X--;
                    }
                    else//都接触时
                    {
                        if (CheckTop(low1, rect))
                            break;
                        Xy.X = low1.X;
                        Xy.Y = low1.Y;
                        Xy.Height = rect.Height;
                        Xy.Width = rect.Width;
                        FullPic(low1, pic, rect);
                        graphics.FillRectangle(brush, Xy.X / scale + moveX, (3000 - Xy.Y - rect.Height) / scale, Xy.Width / scale, Xy.Height / scale);
                        graphics.DrawRectangle(Pens.Black, Xy.X / scale + moveX, (3000 - Xy.Y - rect.Height) / scale, Xy.Width / scale, Xy.Height / scale);
                        //flag = false;
                        //if (low1.X == 406 && low1.Y == 2665)
                        //{
                        //    map=DrawBoolPic(pic);
                        //    flag = true;
                        //}
                        
                        break;
                    }
                    //if(flag)
                    //{
                    //    map.SetPixel(low1.X / 5, (3000 - low1.Y) / 5, Color.Red);
                    //    pictureBox2.Image = map;
                    //}
                }
            }
        
            bool CheckLowLine(Point point1, Point point2, Point point3, Point point4, bool[,] pic1)
            {
                if (point1.X >= 0)
                {
                    if (pic1[point1.X, point1.Y] && pic1[point1.X + 1, point1.Y])
                        return true;
                    else if (pic1[point2.X, point2.Y] && pic1[point2.X + 1, point2.Y])
                        return true;
                    else if (pic1[point3.X, point3.Y] && pic1[point3.X + 1, point3.Y])
                        return true;
                    else if (pic1[point4.X, point4.Y] && pic1[point4.X - 1, point4.Y])
                        return true;
                }
                return false;
            }
            bool CheckLeftLine(Point point1, Point point2, Point point3, Point point4, bool[,] pic1)
            {
                if (point1.Y <= 3000 && pic1[point1.X, point1.Y] && pic1[point1.X, point1.Y - 1])
                    return true;
                else if (point2.Y <= 3000 && pic1[point2.X, point2.Y] && pic1[point2.X, point2.Y - 1])
                    return true;
                else if (point3.Y <= 3000 && pic1[point3.X, point3.Y] && pic1[point3.X, point3.Y - 1])
                    return true;
                else if (point4.Y <= 3000 && pic1[point4.X, point4.Y] && pic1[point4.X, point4.Y + 1])
                    return true;
                return false;
            }
            bool CheckTop(Point point, System.Drawing.Rectangle rect1)
            {
                if (point.Y + rect1.Height > 3000)
                    return true;
                return false;
            }
            void FullPic(Point point, bool[,] pic1, System.Drawing.Rectangle rect1)
            {
                for (int i = point.X; i <= point.X + rect1.Width; i++)
                {
                    for (int j = point.Y; j <= point.Y + rect1.Height; j++)
                    {
                        pic1[i, j] = true;
                    }
                }
            }
        }
        Bitmap DrawBoolPic(bool[,] pic)
        {
            //Graphics graphics = pictureBox1.CreateGraphics();
            Bitmap map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int i = 0; i < 1501; i++)
            {
                for (int j = 0; j < 3001; j++)
                {
                    if(pic[i, j])
                    {
                        map.SetPixel(i/5,(3000-j) / 5, Color.Black);
                        //graphics.DrawRectangle(Pens.Black, Convert.ToSingle(i / 10.00) , Convert.ToSingle(j / 10.00), 1, 1);
                    }
                    
                }
            }
            pictureBox2.Image = map;
            return map;
        }

        private void showPicture_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if(thread1!=null)
            //{
            //    thread1.Abort();
            //    thread1.Join(10);
            //}
            
        }
    }

}
