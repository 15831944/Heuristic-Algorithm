using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 矩形排样_数学建模
{
    class Program
    {
        private static Rectangle P1_L = new Rectangle { Wight = 373, Hight = 201 };//水平
        private static Rectangle P1_V = new Rectangle { Wight = 201, Hight = 373 };//垂直
        private static Rectangle P2_L = new Rectangle { Wight = 477, Hight = 282 };//水平
        private static Rectangle P2_V = new Rectangle { Wight = 282, Hight = 477 };//垂直
        private static Rectangle P3_L = new Rectangle { Wight = 406, Hight = 229 };//水平
        private static Rectangle P3_V = new Rectangle { Wight = 229, Hight = 406 };//垂直
        private static Rectangle P4_L = new Rectangle { Wight = 311, Hight = 225 };//水平
        private static Rectangle P4_V = new Rectangle { Wight = 225, Hight = 311 };//垂直
        public class Left_lower_coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Wight { get; set; }
            public int Hight { get; set; }
        }
        private static Left_lower_coordinate Xy = new Left_lower_coordinate();
        static void Main(string[] args)
        {
            //List<int> gene = new List<int>();
            //StartPopulation(gene);
            //foreach(var item in gene)
            //{
            //    Console.Write(item+" ");
            //}
            Console.Read();
            
            
            Console.Read();
        }
        private static void StartPopulation(List<int> gene)
        {
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
            Print(line);
        }
        private static void Decode(List<int> gene)
        {
            List<Point> line = new List<Point>();//最低轮廓线
            line.Add(new Point {X=0,Y=3000 });//第一个点必须是(0,3000) 最后一个必为(1500,3000)
            line.Add(new Point { X = 0, Y = 0 });
            line.Add(new Point { X = 1500, Y = 0 });
            line.Add(new Point { X = 1500, Y = 3000 });//第一个点必须是(0,3000) 最后一个必为(1500,3000)
            List<int> library_num = new List<int>() {1,-1,2,-2,3,-3,4,-4 };
            for(int i=0;i<gene.Count;i++)
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
            while (library_num2.Count!=0)
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

        private static bool Mapping(List<Point> line, int gene)
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
            if(!flag)
            {
                Console.Write(gene + " ");
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
        private static bool Check(Rectangle rect, List<Point> line)
        {
            for (int i = line.Count - 1; i >= 0; i--)
            {
                if (line[i] != null)
                {
                    //L
                    if (i - 2 >= 0 && 
                        (line[i].Y== line[i-1].Y && line[i-1].X == line[i - 2].X && line[i - 1].Y < line[i - 2].Y)
                        && line[i].X - line[i - 1].X >= rect.Wight && line[i - 2].Y - line[i - 1].Y > 0 && line[i].Y + rect.Hight <= 3000)//此时插入的方块无法移动(掉到井里)
                    {
                        Xy.X = line[i-1].X;
                        Xy.Y = line[i - 1].Y;
                        Xy.Hight = rect.Hight;
                        Xy.Wight = rect.Wight;
                        if (line[i].X - line[i - 1].X == rect.Wight)//正好插入
                        {
                            line[i - 1].Y = line[i - 1].Y + rect.Hight;
                            line[i].Y = line[i].Y + rect.Hight;
                        }
                        else
                        {
                            line[i - 1].Y = line[i - 1].Y + rect.Hight;
                            line.Insert(i, new Point { X = line[i - 1].X + rect.Wight, Y = line[i - 1].Y - rect.Hight });//i
                            line.Insert(i, new Point { X = line[i - 1].X + rect.Wight, Y = line[i - 1].Y });//i 上面变成i+1
                        }
                        //print(line);
                        DeleteSamePoints(line);
                        //print(line);
                        
                        return true;
                    }//U
                    if (i - 3 >= 0 &&
                        (line[i - 1].Y == line[i - 2].Y && line[i].X == line[i - 1].X && line[i - 3].X == line[i - 2].X && line[i - 3].Y > line[i - 2].Y)
                        && line[i - 1].X - line[i - 2].X < rect.Wight && line[i - 3].Y - line[i].Y > 0 && line[i].Y + rect.Hight <= 3000 && line[i - 2].X + rect.Wight <= 1500 && line[i - 1].X + rect.Wight <= 1500)//此时插入的方块无法移动(卡在井上)
                    {
                        Xy.X = line[i - 2].X;
                        Xy.Y = line[i].Y;
                        Xy.Hight = rect.Hight;
                        Xy.Wight = rect.Wight;
                        //如果正好方块右边和支撑它的方块右边重合 无非就是两点重合，可以删除重合点
                        line[i - 2].Y = line[i].Y + rect.Hight;
                        line[i - 1].Y = line[i - 2].Y;
                        line[i - 1].X = line[i - 2].X + rect.Wight;
                        line[i].X = line[i - 1].X;
                        //print(line);
                        DeleteSamePoints(line);
                        //print(line);
                        //foreach (var item in line)
                        //{
                        //    if (item.X > 1500)
                        //    {
                        //        int a = 0;
                        //    }
                        //}
                        return true;
                    }
                    //暂时不写洞穴类型的方块组合。。。。。。。。。。。。。
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
        }
    }
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class Rectangle
    {
        public int Wight { get; set; }
        public int Hight { get; set; }
    }
}
