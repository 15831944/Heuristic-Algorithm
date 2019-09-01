using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 木板最优切割方案
{
    public partial class showPicture : Form
    {
        int scale = 5;
        public showPicture()
        {
            InitializeComponent();

        }
        void showPic(List<int> vs)
        {
            int scale = 5;
            control control = new control();
            Size s1 = new Size() { Width = control.s1.width / scale, Height = control.s1.height / scale };
            Size p1 = new Size() { Width = control.p1.width / scale, Height = control.p1.height / scale };
            Size p2 = new Size() { Width = control.p2.width / scale, Height = control.p2.height / scale };
            Size p3 = new Size() { Width = control.p3.width / scale, Height = control.p3.height / scale };
            Size p4 = new Size() { Width = control.p4.width / scale, Height = control.p4.height / scale };

            //Rectangle rectangle = new Rectangle() { X = 10, Y = 20, Width = 100, Height = 200 };
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.DrawRectangle(Pens.Black, 0, 0, s1.Width, s1.Height);
            int width = 0, height = 0;
            foreach (var item in vs)
            {
                switch (item)
                {
                    case 0:
                        for (int i = 0; i < s1.Width / p1.Width; i++)
                        {
                            graphics.DrawRectangle(Pens.Black, width + i * p1.Width, height, p1.Width, p1.Height);
                        }
                        s1.Height -= p1.Height;
                        height += p1.Height;
                        break;
                    case 1:
                        for (int i = 0; i < s1.Width / p1.Height; i++)
                        {
                            graphics.DrawRectangle(Pens.Black, width + i * p1.Height, height, p1.Height, p1.Width);
                        }
                        s1.Height -= p1.Width;
                        height += p1.Width;
                        break;
                    case 2:
                        for (int i = 0; i < s1.Height / p1.Height; i++)
                        {
                            graphics.DrawRectangle(Pens.Black, width, height + i * p1.Height, p1.Width, p1.Height);
                        }
                        s1.Width -= p1.Width;
                        width += p1.Width;
                        break;
                    case 3:
                        for (int i = 0; i < s1.Height / p1.Width; i++)
                        {
                            graphics.DrawRectangle(Pens.Black, width, height + i * p1.Width, p1.Height, p1.Width);
                        }
                        s1.Width -= p1.Height;
                        width += p1.Height;
                        break;
                    default:
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> vs = new List<int>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
            //showPic(vs);
            // pictureBox1.Image = null;
            List<int> nums=StringToInt("-1,-1,-4,3,3,-1,-3,1,-1,3,-1,-1,-3,-3,1,-1,3,-1,-1,-4,-1,1,-4,3,-1,-1,-4,-1,1,1,3,-1,-1,-1,2,1,3,-1,3,-3,4,4,-4,4,4,4,4,4,1,-4,1,");
            GeneToPic(nums);
            //Algorithm();
        }
        void Algorithm()
        {
            pictureBox1.Width = 1500 / scale+1000;
            pictureBox1.Height = 3000 / scale + 1000;
            
            Graphics graphics = pictureBox1.CreateGraphics();
            //graphics.draw

            //Graphics graphics = pictureBox1.CreateGraphics();

            List<int> gene = new List<int>();
            StartPopulation_new(gene);
            foreach (var item in gene)
            {
                Console.Write(item + " ");
            }
            Console.Read();
        }


        private static Rectangle P1_L = new Rectangle { Width = 373, Height = 201 };//水平
        private static Rectangle P1_V = new Rectangle { Width = 201, Height = 373 };//垂直
        private static Rectangle P2_L = new Rectangle { Width = 477, Height = 282 };//水平
        private static Rectangle P2_V = new Rectangle { Width = 282, Height = 477 };//垂直
        private static Rectangle P3_L = new Rectangle { Width = 406, Height = 229 };//水平
        private static Rectangle P3_V = new Rectangle { Width = 229, Height = 406 };//垂直
        private static Rectangle P4_L = new Rectangle { Width = 311, Height = 225 };//水平
        private static Rectangle P4_V = new Rectangle { Width = 225, Height = 311 };//垂直
        public class Left_lower_coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int wight { get; set; }
            public int hight { get; set; }
        }
        private static Left_lower_coordinate Xy = new Left_lower_coordinate();
        private void StartPopulation(List<int> gene)//初始种群
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.DrawLine(Pens.Black, (1500-10) / scale, (3000 - 500) / scale, (1500) / scale, (3000 - 500) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 1000) / scale, (1500) / scale, (3000 - 1000) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 1500) / scale, (1500) / scale, (3000 - 1500) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 2000) / scale, (1500) / scale, (3000 - 2000) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 2500) / scale, (1500) / scale, (3000 - 2500) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 3000) / scale, (1500) / scale, (3000 - 3000) / scale);

            List<Point> line = new List<Point>();//最低轮廓线
            line.Add(new Point { X = 0, Y = 3000 });//第一个点必须是(0,3000) 最后一个必为(1500,3000)
            line.Add(new Point { X = 0, Y = 0 });
            line.Add(new Point { X = 1500, Y = 0 });
            line.Add(new Point { X = 1500, Y = 3000 });//第一个点必须是(0,3000) 最后一个必为(1500,3000)
            List<int> library_num2 = new List<int>() { 1, -1, 2, -2, 3, -3, 4, -4 };
            Random rand = new Random();
            while (library_num2.Count != 0)
            {
                int index = rand.Next(0, library_num2.Count);
                if (Mapping(line, library_num2[index]))
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

            for (int i = 0; i < line.Count - 1; i++)
            {
                graphics.DrawLine(Pens.Red, line[i].X / scale, (3000 - line[i].Y) / scale, line[i + 1].X / scale, (3000 - line[i + 1].Y) / scale);
            }
            Print(line);
        }
        private void Decode(List<int> gene)
        {
            List<Point> line = new List<Point>();//最低轮廓线
            line.Add(new Point { X = 0, Y = 3000 });//第一个点必须是(0,3000) 最后一个必为(1500,3000)
            line.Add(new Point { X = 0, Y = 0 });
            line.Add(new Point { X = 1500, Y = 0 });
            line.Add(new Point { X = 1500, Y = 3000 });//第一个点必须是(0,3000) 最后一个必为(1500,3000)
            List<int> library_num = new List<int>() { 1, -1, 2, -2, 3, -3, 4, -4 };
            for (int i = 0; i < gene.Count; i++)
            {
                if (!Mapping(line, gene[i]))
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
                if (Mapping(line, library_num2[index]))
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
        private bool Mapping(List<Point> line, int gene)
        {
            bool flag = true;
            switch (gene)
            {
                case 1:
                    flag = Check(P1_L, line);
                    break;
                case -1:
                    flag = Check(P1_V, line);
                    break;
                case 2:
                    flag = Check(P2_L, line);
                    break;
                case -2:
                    flag = Check(P2_V, line);
                    break;
                case 3:
                    flag = Check(P3_L, line);
                    break;
                case -3:
                    flag = Check(P3_V, line);
                    break;
                case 4:
                    flag = Check(P4_L, line);
                    break;
                case -4:
                    flag = Check(P4_V, line);
                    break;
                default:
                    Console.WriteLine("基因有问题！！！！\r\n基因有问题！！！！\r\n基因有问题！！！！\r\n基因有问题！！！！\r\n");
                    break;
            }
            return flag;
        }
        private static void Print(List<Point> line2)
        {
            Console.WriteLine();
            foreach (var item in line2)
            {
                Console.Write("({0},{1}) ", item.X, item.Y);
            }
            Console.Read();
        }
        private bool Check(Rectangle rect, List<Point> line)
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.DrawRectangle(Pens.Black, 0, 0, 1500 / scale, 3000 / scale);
            Xy.hight = rect.Height;
            Xy.wight = rect.Width;
            for (int i = line.Count - 1; i >= 0; i--)
            {
                if (line[i] != null)
                {
                    //L
                    if (i - 2 >= 0 &&
                        (line[i].Y == line[i - 1].Y && line[i - 1].X == line[i - 2].X && line[i - 1].Y < line[i - 2].Y)
                        && line[i].X- line[i-1].X >= rect.Width && line[i - 2].Y - line[i - 1].Y > 0 && line[i].Y + rect.Height <= 3000)//此时插入的方块无法移动(掉到井里)
                    {

                        if (line[i].X - line[i - 1].X == rect.Width)//正好插入
                        {
                            Thread.Sleep(500);
                            Xy.X = line[i - 1].X;
                            Xy.Y = line[i - 1].Y;
                            if (!IsInternal(line, Xy.X, Xy.Y+ rect.Height))
                            {
                                graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                line[i - 1].Y = line[i - 1].Y + rect.Height;
                                line[i].Y = line[i].Y + rect.Height;

                                DeleteSamePoints(line);
                                return true;
                            }
                            
                        }
                        else
                        {
                            Thread.Sleep(500);
                            Xy.X = line[i - 1].X;
                            Xy.Y = line[i - 1].Y;
                            if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                            {
                                graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);
                                line[i - 1].Y = line[i - 1].Y + rect.Height;
                                line.Insert(i, new Point { X = line[i - 1].X + rect.Width, Y = line[i - 1].Y - rect.Height });//i
                                line.Insert(i, new Point { X = line[i - 1].X + rect.Width, Y = line[i - 1].Y });//i 上面变成i+1

                                DeleteSamePoints(line);
                                return true;
                            }
                            
                        }
                    }
                    //U
                    if (i - 4 >= 0 &&
                        (line[i - 1].Y == line[i - 2].Y && line[i].X == line[i - 1].X && line[i - 3].X == line[i - 2].X && line[i - 3].Y > line[i - 2].Y && line[i].Y > line[i - 1].Y && line[i-3].Y > line[i - 2].Y)
                        && (i + 1< line.Count && line[i + 1].X - line[i - 2].X>= rect.Width)//此处确保了没有洞穴方块组合
                        && line[i - 1].X - line[i - 2].X < rect.Width && line[i - 3].Y - line[i].Y > 0 && line[i].Y + rect.Height <= 3000 && line[i - 2].X + rect.Width <= 1500 && line[i - 1].X + rect.Width <= 1500
                        )//此时插入的方块无法移动(卡在井上)
                    {
                        
                        if (FindLongerX(line, i - 3, out int index5)!=-1 && FindLongerX(line, i - 3, out int index6) + (line[i - 3].Y - line[i].Y) > rect.Height)//找到屋檐
                        {
                            Thread.Sleep(500);
                            Xy.X = line[i - 2].X;
                            Xy.Y = line[i].Y;
                            if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                            {
                                graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                //如果正好方块右边和支撑它的方块右边重合 无非就是两点重合，可以删除重合点
                                line[i - 2].Y = line[i].Y + rect.Height;
                                line[i - 1].Y = line[i - 2].Y;
                                line[i - 1].X = line[i - 2].X + rect.Width;
                                line[i].X = line[i - 1].X;

                                DeleteSamePoints(line);
                                return true;
                            }
                            
                        }
                        else if (FindLongerX(line, i - 3, out int index7)==-1)//没找到屋檐
                        {
                            Thread.Sleep(500);
                            Xy.X = line[i - 2].X;
                            Xy.Y = line[i].Y;
                            if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                            {
                                graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                //如果正好方块右边和支撑它的方块右边重合 无非就是两点重合，可以删除重合点
                                line[i - 2].Y = line[i].Y + rect.Height;
                                line[i - 1].Y = line[i - 2].Y;
                                line[i - 1].X = line[i - 2].X + rect.Width;
                                line[i].X = line[i - 1].X;

                                DeleteSamePoints(line);
                                return true;
                            }

                            
                        }
                    }
                    //洞穴类型的方块组合 5
                    if (i - 5 >= 0 &&
                        (line[i].Y > line[i - 1].Y && line[i].X == line[i - 1].X && line[i - 1].Y == line[i - 2].Y && line[i - 1].X > line[i - 2].X && line[i - 2].X == line[i - 3].X && line[i - 2].Y < line[i - 3].Y && line[i - 4].Y == line[i - 3].Y && line[i - 4].X > line[i - 3].X
                        && line[i-4].X == line[i - 5].X && line[i - 4].Y < line[i - 5].Y))
                        
                    {
                        if(line[i].X - line[i - 4].X < rect.Width && line[i-4].Y - line[i].Y < rect.Height)
                        {
                            //未进入洞穴
                            if(i - 4 >= 0 && i + 2<line.Count-1 && line[i+2].Y> line[i].Y)
                            {
                                if (line[i + 1].X - line[i - 4].X > rect.Width)
                                {
                                    Thread.Sleep(500);
                                    Xy.X = line[i - 4].X;
                                    Xy.Y = line[i].Y;
                                    if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                                    {
                                        graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                        Point new_point1 = new Point() { X = line[i - 4].X, Y = line[i].Y + rect.Height };
                                        Point new_point2 = new Point() { X = line[i - 4].X + rect.Width, Y = line[i].Y + rect.Height };
                                        Point new_point3 = new Point() { X = line[i - 4].X + rect.Width, Y = line[i].Y };
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.Insert(i - 4, new_point3);
                                        line.Insert(i - 4, new_point2);
                                        line.Insert(i - 4, new_point1);

                                        DeleteSamePoints(line);
                                        return true;
                                    }
                                    
                                }
                                else if(line[i + 1].X - line[i - 4].X == rect.Width)
                                {
                                    Xy.X = line[i - 4].X;
                                    Xy.Y = line[i].Y;
                                    if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                                    {
                                        graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                        Point new_point1 = new Point() { X = line[i - 4].X, Y = line[i].Y + rect.Height };
                                        Point new_point2 = new Point() { X = line[i - 4].X + rect.Width, Y = line[i].Y + rect.Height };
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.Insert(i - 4, new_point2);
                                        line.Insert(i - 4, new_point1);

                                        DeleteSamePoints(line);
                                        return true;
                                    }
                                   
                                }
                            }
                            else if(i - 4 >= 0 && i + 2 < line.Count - 1 && line[i + 2].Y < line[i].Y)//可能碰到最右边
                            {
                                if (FindHigherY(line, i, out int index3)+ (line[i].X- line[i-4].X) > rect.Width)//不贴合最右边
                                {
                                    Thread.Sleep(500);
                                    Xy.X = line[i - 4].X;
                                    Xy.Y = line[i].Y;
                                    if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                                    {
                                        graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                        Point new_point1 = new Point() { X = line[i - 4].X, Y = line[i].Y + rect.Height };
                                        Point new_point2 = new Point() { X = line[i - 4].X + rect.Width, Y = line[i].Y + rect.Height };
                                        Point new_point3 = new Point() { X = line[i - 4].X + rect.Width, Y = line[i].Y };
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.RemoveAt(i - 4);
                                        line.Insert(i - 4, new_point3);
                                        line.Insert(i - 4, new_point2);
                                        line.Insert(i - 4, new_point1);

                                        DeleteSamePoints(line);
                                        return true;
                                    }
                                }
                                else if (FindHigherY(line, i, out int index4) + (line[i].X - line[i - 4].X) == rect.Width)//贴合最右边
                                {
                                    Thread.Sleep(500);
                                    Xy.X = line[i - 4].X;
                                    Xy.Y = line[i].Y;
                                    if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                                    {
                                        graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                        Point new_point1 = new Point() { X = line[i - 4].X, Y = line[i].Y + rect.Height };
                                        Point new_point2 = new Point() { X = line[i - 4].X + rect.Width, Y = line[i].Y + rect.Height };
                                        line.RemoveRange(i - 4, index4 - (i - 4));
                                        line.Insert(i - 4, new_point2);
                                        line.Insert(i - 4, new_point1);

                                        DeleteSamePoints(line);
                                        return true;
                                    }

                                   
                                }
                            }
                        }
                        else if(i - 4 >= 0 && line[i - 4].Y - line[i].Y >= rect.Height && line[i].X - line[i - 3].X < rect.Width)
                        {
                            //进入洞穴
                            if (line[i - 4].Y - line[i].Y == rect.Height)//贴合
                            {
                                if (i + 1 < line.Count - 1 && line[i + 1].X - line[i - 3].X > rect.Width)
                                {
                                    Thread.Sleep(500);
                                    Xy.X = line[i - 2].X;
                                    Xy.Y = line[i].Y;
                                    if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                                    {
                                        graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                        Point new_point1 = new Point() { X = line[i - 3].X + rect.Width, Y = line[i].Y + rect.Height };
                                        Point new_point2 = new Point() { X = line[i - 3].X + rect.Width, Y = line[i].Y };
                                        line.RemoveAt(i - 3);
                                        line.RemoveAt(i - 3);
                                        line.RemoveAt(i - 3);
                                        line.RemoveAt(i - 3);
                                        line.Insert(i - 3, new_point2);
                                        line.Insert(i - 3, new_point1);

                                        DeleteSamePoints(line);
                                        return true;
                                    }
                                    
                                }
                                else if(i + 1 < line.Count - 1 && line[i + 1].X - line[i - 3].X == rect.Width)
                                {
                                    Thread.Sleep(500);
                                    Xy.X = line[i - 2].X;
                                    Xy.Y = line[i].Y;
                                    if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                                    {
                                        graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                        Point new_point1 = new Point() { X = line[i - 3].X + rect.Width, Y = line[i].Y + rect.Height };
                                        line.RemoveAt(i - 3);
                                        line.RemoveAt(i - 3);
                                        line.RemoveAt(i - 3);
                                        line.RemoveAt(i - 3);
                                        line.RemoveAt(i - 3);
                                        line.Insert(i - 3, new_point1);

                                        DeleteSamePoints(line);
                                        return true;
                                    }
                                    
                                }
                            }
                            else if (i + 1 < line.Count - 1 && line[i - 4].Y - line[i].Y > rect.Height)//不贴合
                            {
                                if (line[i + 1].X - line[i - 3].X > rect.Width)//不贴合
                                {
                                    Thread.Sleep(500);
                                    Xy.X = line[i - 2].X;
                                    Xy.Y = line[i].Y;
                                    if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                                    {
                                        graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                        Point new_point1 = new Point() { X = line[i - 2].X, Y = line[i].Y + rect.Height };
                                        Point new_point2 = new Point() { X = line[i - 3].X + rect.Width, Y = line[i].Y + rect.Height };
                                        Point new_point3 = new Point() { X = line[i - 3].X + rect.Width, Y = line[i].Y };
                                        line.RemoveAt(i - 2);
                                        line.RemoveAt(i - 2);
                                        line.RemoveAt(i - 2);
                                        line.Insert(i - 2, new_point3);
                                        line.Insert(i - 2, new_point2);
                                        line.Insert(i - 2, new_point1);

                                        DeleteSamePoints(line);
                                        return true;
                                    }
                                    
                                }
                                else if (i + 1 < line.Count - 1 && line[i + 1].X - line[i - 3].X == rect.Width)//贴合
                                {
                                    Thread.Sleep(500);
                                    Xy.X = line[i - 2].X;
                                    Xy.Y = line[i].Y;
                                    if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                                    {
                                        graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);


                                        Point new_point1 = new Point() { X = line[i - 2].X, Y = line[i].Y + rect.Height };
                                        Point new_point2 = new Point() { X = line[i - 3].X + rect.Width, Y = line[i].Y + rect.Height };
                                        line.RemoveAt(i - 2);
                                        line.RemoveAt(i - 2);
                                        line.RemoveAt(i - 2);
                                        line.RemoveAt(i - 2);
                                        line.Insert(i - 2, new_point2);
                                        line.Insert(i - 2, new_point1);

                                        DeleteSamePoints(line);
                                        return true;
                                    }
                                   
                                }
                            }
                        }
                        else if (i - 4 >= 0 && line[i - 4].Y - line[i].Y >= rect.Height && line[i].X - line[i - 3].X >= rect.Width)
                        {
                            if (line[i].X - line[i - 3].X > rect.Width)//不贴合 能下来肯定两个地方不贴合
                            {
                                Thread.Sleep(500);
                                Xy.X = line[i - 2].X;
                                Xy.Y = line[i - 2].Y;
                                if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                                {
                                    graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                    Point new_point1 = new Point() { X = line[i - 2].X, Y = line[i - 2].Y + rect.Height };
                                    Point new_point2 = new Point() { X = line[i - 2].X + rect.Width, Y = line[i - 2].Y + rect.Height };
                                    Point new_point3 = new Point() { X = line[i - 2].X + rect.Width, Y = line[i - 2].Y };
                                    line.RemoveAt(i - 2);
                                    line.Insert(i - 2, new_point3);
                                    line.Insert(i - 2, new_point2);
                                    line.Insert(i - 2, new_point1);

                                    DeleteSamePoints(line);
                                    return true;
                                }
                                
                            }
                        }
                    }//U
                    //阶梯型 4
                    if(i - 3 >= 0 && i - 3 >= 0 && (line[i].Y < line[i - 1].Y && line[i].X == line[i - 1].X && line[i-2].Y == line[i - 1].Y && line[i-2].X < line[i - 1].X && line[i - 2].X == line[i - 3].X && line[i - 2].Y < line[i - 3].Y))
                    {
                        if (FindHigherY(line, i-2, out int index) > rect.Width)//不贴合
                        {
                            Thread.Sleep(500);
                            Xy.X = line[i - 2].X;
                            Xy.Y = line[i - 2].Y;
                            if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                            {
                                graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                Point new_point1 = new Point() { X = line[i - 2].X, Y = line[i - 2].Y + rect.Height };
                                Point new_point2 = new Point() { X = line[i - 2].X + rect.Width, Y = line[i - 2].Y + rect.Height };
                                Point new_point3 = new Point() { X = line[i - 2].X + rect.Width, Y = line[i - 2].Y };
                                line.RemoveAt(i - 2);
                                line.Insert(i - 2, new_point3);
                                line.Insert(i - 2, new_point2);
                                line.Insert(i - 2, new_point1);

                                DeleteSamePoints(line);
                                return true;
                            }
                            
                        }
                        else if (FindHigherY(line, i - 2, out int index1) == rect.Width)//贴合
                        {
                            Thread.Sleep(500);
                            Xy.X = line[i - 2].X;
                            Xy.Y = line[i - 2].Y;
                            if (!IsInternal(line, Xy.X, Xy.Y + rect.Height))
                            {
                                graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);

                                Point new_point1 = new Point() { X = line[i - 2].X, Y = line[i - 2].Y + rect.Height };
                                Point new_point2 = new Point() { X = line[i - 2].X + rect.Width, Y = line[i - 2].Y + rect.Height };
                                line.RemoveRange(i - 2, index1 - (i - 2));
                                line.Insert(i - 2, new_point2);
                                line.Insert(i - 2, new_point1);

                                DeleteSamePoints(line);
                                return true;
                            }
                            
                        }
                    }

                }
            }
            return false;
            void DeleteSamePoints(List<Point> line1)//删除重复点
            {
                for (int i = 0; i < line1.Count; i++)
                {
                    if (i + 1 < line1.Count && line1[i].X == line1[i + 1].X && line1[i].Y == line1[i + 1].Y)
                    {
                        line1.RemoveAt(i + 1);
                        line1.RemoveAt(i);
                    }
                }
            }
            int FindHigherY(List<Point> line2, int i,out int index)//找指定点后比指定点高的点,返回两点间水平距离
            {
                for (int j = i + 1; j < line2.Count; j++)
                {
                    if (line2[j].Y > line2[i].Y)
                    {
                        index = j;
                        return line2[j].X - line2[i].X;
                    }
                }
                index = -1;
                return -1;
            }
            int FindLongerX(List<Point> line2, int i, out int index)//找指定点前比指定点长的点,返回两点间垂直距离
            {
                for (int j = i - 1; j <= 0; j--)
                {
                    if (line2[j].X > line2[i].X)
                    {
                        index = j;
                        return line2[j].Y - line2[i].Y;
                    }
                }
                index = -1;
                return -1;
            }
            bool IsInternal(List<Point> line1,int x,int y)//点是否在内部
            {
                if(y<=3000 && x<=0)
                {
                    for (int j = 0; j < line.Count; j++)
                    {
                        if (j + 1 < line.Count - 1 && line[j].X < x && line[j + 1].X > x && line[j].Y == line[j + 1].Y)
                        {
                            if (line[j].Y > y)
                                return true;
                        }
                    }
                }
                return false;
            }
        }

        //new----------------------------
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
                    
                    Random r = new Random();
                    if (library_num.Count == 0)
                    {
                        //尝试所有类型方块都不行,开始删除后面的基因点
                        gene.RemoveRange(i, gene.Count - i);
                        return;
                    }
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
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 500) / scale, (1500) / scale, (3000 - 500) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 1000) / scale, (1500) / scale, (3000 - 1000) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 1500) / scale, (1500) / scale, (3000 - 1500) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 2000) / scale, (1500) / scale, (3000 - 2000) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 2500) / scale, (1500) / scale, (3000 - 2500) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 3000) / scale, (1500) / scale, (3000 - 3000) / scale);
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
            int a = 0;
            a = 1;
        }
        private bool Check_new(Rectangle rect, bool[,] pic)
        {
            Point low1 = new Point { X = 1500 - rect.Width ,Y=3000 };
            Point low2 = new Point { X = 1500 - rect.Width+ rect.Width/3, Y = 3000 };
            Point low3 = new Point { X = 1500 - rect.Width+ rect.Width*2 / 3, Y = 3000 };
            Point low4 = new Point { X = 1500, Y = 3000 };
            Point left1 = new Point { X = 1500 - rect.Width, Y = 3000+ rect.Height };
            Point left2 = new Point { X = 1500 - rect.Width, Y = 3000+ rect.Height*2/3 };
            Point left3 = new Point { X = 1500 - rect.Width, Y = 3000 + rect.Height/ 3 };
            do
            {
                if(!CheckLowLine(low1, low2, low3, low4, pic))//不接触底线时
                {
                    low1.Y--; low2.Y--; low3.Y--; low4.Y--; left1.Y--; left2.Y--; left3.Y--;
                }
                else if (!CheckLeftLine(left1, left2, left3, low1, pic))//接触底线而不接触左线时
                {
                    low1.X--; low2.X--; low3.X--; low4.X--; left1.X--; left2.X--; left3.X--;
                }
                else//都接触时
                {
                    if(CheckTop(low1, rect))
                        break;
                    Xy.X = low1.X;
                    Xy.Y = low1.Y;
                    Xy.hight = rect.Height;
                    Xy.wight = rect.Width;
                    FullPic(low1, pic, rect);
                    if(low1.X<0 || low1.Y<0)
                    {
                        MessageBox.Show("小于小于0");
                    }
                    Graphics graphics = pictureBox1.CreateGraphics();
                    graphics.DrawRectangle(Pens.Black, Xy.X / scale, (3000 - Xy.Y - rect.Height) / scale, Xy.wight / scale, Xy.hight / scale);
                    return true;
                }
            } while (true);
            return false;
            
            bool CheckLowLine(Point point1, Point point2, Point point3, Point point4, bool[,] pic1)
            {
                if (point1.X >=0  )
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
                if(point1.X==0)//接触到方框左壁
                {
                    return true;
                }
                if(point1.Y<=3000 && point2.Y <= 3000 && point3.Y <= 3000 && point4.Y <= 3000)
                {
                    if (pic1[point1.X, point1.Y] && pic1[point1.X, point1.Y - 1])
                        return true;
                    else if (pic1[point2.X, point2.Y] && pic1[point2.X, point2.Y - 1])
                        return true;
                    else if (pic1[point3.X, point3.Y] && pic1[point3.X, point3.Y - 1])
                        return true;
                    else if (pic1[point4.X, point4.Y] && pic1[point4.X, point4.Y + 1])
                        return true;
                }
                return false;
            }
            bool CheckTop(Point point, Rectangle rect1)
            {
                if (point.Y + rect1.Height > 3000)
                    return true;
                return false;
            }
            void FullPic (Point point,bool[,] pic1, Rectangle rect1)
            {
                for (int i = point.X; i <= point.X+ rect1.Width; i++)
                {
                    for (int j = point.Y; j <= point.Y + rect1.Height; j++)
                    {
                        pic1[i, j] = true;
                    }
                }
            }
        }
        private List<int> StringToInt(string str)
        {
            List<int> nums = new List<int>();
            string[] split = str.Split(new char[] { ',' });
            foreach(var item in split)
            {
                if(!String.IsNullOrWhiteSpace(item))
                    nums.Add(Convert.ToInt32(item));
            }
            return nums;
        }
        private void GeneToPic(List<int> gene)
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
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 500) / scale, (1500) / scale, (3000 - 500) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 1000) / scale, (1500) / scale, (3000 - 1000) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 1500) / scale, (1500) / scale, (3000 - 1500) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 2000) / scale, (1500) / scale, (3000 - 2000) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 2500) / scale, (1500) / scale, (3000 - 2500) / scale);
            graphics.DrawLine(Pens.Black, (1500 - 10) / scale, (3000 - 3000) / scale, (1500) / scale, (3000 - 3000) / scale);

            for (int i = 0; i < gene.Count; i++)
            {
                if (!Mapping_new(pic, gene[i]))
                {
                    //检测到非法基因
                    MessageBox.Show("基因有问题");
                }
            }
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }



    


}
