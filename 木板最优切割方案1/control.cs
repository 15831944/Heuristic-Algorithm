using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 木板最优切割方案
{
    public partial class control : Form
    {
        public control()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 选择方式
        /// </summary>
        enum SelectType
        {
            ratio,//按面积利用率
            profit,//按利润
        }
        public Board s1 = new Board() { width = 3000, height = 1500 };
        public Board p1 = new Board() { width = 373, height = 201, needNum = 774, profit = 19.9 };
        public Board p2 = new Board() { width = 477, height = 282, needNum = 2153, profit = 23.0 };
        public Board p3 = new Board() { width = 406, height = 229, needNum = 1623, profit = 21.0 };
        public Board p4 = new Board() { width = 311, height = 225, needNum = 1614, profit = 16.0 };

        private static System.Drawing.Rectangle P1_L = new System.Drawing.Rectangle { Width = 373, Height = 201 };//水平
        private static System.Drawing.Rectangle P1_V = new System.Drawing.Rectangle { Width = 201, Height = 373 };//垂直
        private static System.Drawing.Rectangle P2_L = new System.Drawing.Rectangle { Width = 477, Height = 282 };//水平
        private static System.Drawing.Rectangle P2_V = new System.Drawing.Rectangle { Width = 282, Height = 477 };//垂直
        private static System.Drawing.Rectangle P3_L = new System.Drawing.Rectangle { Width = 406, Height = 229 };//水平
        private static System.Drawing.Rectangle P3_V = new System.Drawing.Rectangle { Width = 229, Height = 406 };//垂直
        private static System.Drawing.Rectangle P4_L = new System.Drawing.Rectangle { Width = 311, Height = 225 };//水平
        private static System.Drawing.Rectangle P4_V = new System.Drawing.Rectangle { Width = 225, Height = 311 };//垂直



        int geneLength = 200;
        int populationNum = 60;//种群数量
        int interationTime = 60;//迭代次数
        int selectNum = 5;
        int crossNum = 6;
        List<int> genes = new List<int>();
        List<Chromosome> population = new List<Chromosome>();//种群集合
        List<Chromosome> crossChro = new List<Chromosome>();//交叉后的种群
        List<Chromosome> allChro = new List<Chromosome>();//总的记录


        public static List<int> theBest = new List<int>();

        private void btnGo_Click(object sender, EventArgs e)
        {
            prbInitial.Maximum = populationNum;
            prbCross.Maximum = 15;
            prbIteration.Maximum = interationTime;
            prbInitial.Minimum = 0;
            prbCross.Minimum = 0;
            prbIteration.Minimum = 0;

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            for (int j = 0; j < populationNum; j++)
            {
                prbInitial.Value = j + 1;

                population.Add(new Chromosome() { genes = StartPopulation_new().ToList() });
            }// //生成初始种群
            //GetFitness(population);
            //showMessage(population, 0);

            for (int i = 0; i < interationTime; i++)
            {
                GetFitness(population);
                allChro.AddRange(population.ToList());
                SelectChro(SelectType.ratio);

                population.Add(new Chromosome() { genes = StartPopulation_new() });
                showMessage(population, i);
                int num = 0;
                for (int j = 0; j < crossNum; j++)
                {
                    for (int k = j+1; k < crossNum; k++)
                    {
                        cross(population[j].genes, population[k].genes);
                        prbCross.Value = num++;
                    }
                }

                prbIteration.Value = i + 1;
               // showMessage(population, i);

            }//迭代
            stopwatch.Stop();
            rictxt.AppendText("\r\n\t" + stopwatch.Elapsed.TotalSeconds.ToString());
            theBest = population[0].genes;
            allChro.AddRange(population.ToList());
            mes(allChro);
    }
        //生成初始种群
        void Initial()
        {
            int num;
            Random random = new Random(new Guid().GetHashCode());
            for (int j = 0; j < populationNum; j++)
            {
                prbInitial.Value = j + 1;
                for (int i = 0; i < geneLength; i++)
                {
                    num = random.Next(1, 5);
                    if (random.Next(2) == 0)
                    {
                        num = -num;
                    }
                    genes.Add(num);
                }
                Decode(genes);
                population.Add(new Chromosome() { genes = genes.ToList() });
                genes.Clear();
            }
        }
        /// <summary>
        /// 解码，减去不必要的基因段
        /// </summary>
        /// <param name="chromosome"></param>
        void Decode(List<int> chromosome)
        {
            int num = 0;

            chromosome = chromosome.Take(num).ToList();
        }
        /// <summary>
        /// 更新适应度
        /// </summary>
        /// <param name="chromosome"></param>
        void GetFitness(List<Chromosome> population)
        {
            foreach (var item in population)
            {
                int cnt1 = 0, cnt2 = 0, cnt3 = 0, cnt4 = 0; ;
                foreach (var gene in item.genes)
                {
                    switch (gene)
                    {
                        case 1:
                            cnt1 += 1;
                            break;
                        case 2:
                            cnt2 += 1;
                            break;
                        case 3:
                            cnt3 += 1;
                            break;
                        case 4:
                            cnt4 += 1;
                            break;

                        case -1:
                            cnt1 += 1;
                            break;
                        case -2:
                            cnt2 += 1;
                            break;
                        case -3:
                            cnt3 += 1;
                            break;
                        case -4:
                            cnt4 += 1;
                            break;
                        default:
                            break;
                    }
                }
                double area = cnt1 * p1.width * p1.height
                    + cnt2 * p2.width * p2.height
                    + cnt3 * p3.width * p3.height
                    + cnt4 * p4.width * p4.height;
                item.fitness = area / (s1.height * s1.width);
                double profit = cnt1 * p1.profit
                                + cnt2 * p2.profit
                                + cnt3 * p3.profit
                                + cnt4 * p4.profit;
                item.profit = profit;
            }
        }
        /// <summary>
        /// 选出最优的一批
        /// </summary>
        /// <param name="population"></param>
        void SelectChro(SelectType type)
        {
            //population.AddRange(crossChro.ToList());
            //crossChro.Clear();
            switch (type)
            {
                case SelectType.ratio:
                    population= population.OrderByDescending(s => s.fitness).ToList();
                    deleteSame(population);
                    population = population.Take(selectNum).ToList();
                    break;
                case SelectType.profit:
                    //population.OrderByDescending(s => s.profit);
                    //population = population.Take(selectNum).ToList();
                    population = population.OrderByDescending(s => s.profit).ToList().Take(selectNum).ToList();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 删除重复解
        /// </summary>
        /// <param name="population"></param>
        private void deleteSame(List<Chromosome> population)
        {
            int i = 0;
            while (i < population.Count - 1)
            {
                if (population[i].fitness == population[i + 1].fitness)
                {
                    population.RemoveAt(i + 1);
                    continue;
                }
                i++;
            }
        }
        /// <summary>
        /// 交叉
        /// </summary>
        /// <param name="genes1"></param>
        /// <param name="genes2"></param>
        void cross(List<int> genes1, List<int> genes2)
        {
            int minLength = 5;
            int maxLength = Math.Min(genes1.Count, genes2.Count);
            int crossLength = new Random(new Guid().GetHashCode()).Next(minLength, maxLength);
            //交叉点
            //int index1 = genes1.Count - crossLength;
            //int index2 = genes2.Count - crossLength;
            int index1 = new Random(new Guid().GetHashCode()).Next(genes1.Count - crossLength );
            int index2 = new Random(new Guid().GetHashCode()).Next(genes2.Count - crossLength );
            //取出交叉段
            List<int> temp1 = genes1.Skip(index1 ).Take(crossLength).ToList();
            List<int> temp2 = genes2.Skip(index2 ).Take(crossLength).ToList();

            List<int> newChro1 = genes1.ToList();
            newChro1.RemoveRange(index1, crossLength);
            newChro1.InsertRange(index1, temp2);

            List<int> newChro2 = genes2.ToList();
            newChro2.RemoveRange(index2, crossLength);
            newChro2.InsertRange(index2, temp1);


            //List<int> newChro1 = genes1.Take(index1).ToList();
            //newChro1.AddRange(temp2);
            //List<int> newChro2 = genes2.Take(index2).ToList();
            //newChro2.AddRange(temp1);

            Decode_new(newChro1);
            Decode_new(newChro2);
            population.Add(new Chromosome() { genes = newChro1 });
            population.Add(new Chromosome() { genes = newChro2 });

        }

        void showMessage(List<Chromosome> population,int times)
        {
            rictxt.AppendText("第" + times + "代\r\n");

           int num = 0;
            foreach (var item in population)
            {
                rictxt.AppendText("\t"+ (++ num) + "\t" + item.fitness + "\t" + item.profit + "\r\n");
            }
            rictxt.AppendText("--------------------\r\n\n");
        }

        void mes(List<Chromosome> population)
        {
            List<int> p1Num = new List<int>();
            List<int> p2Num = new List<int>();
            List<int> p3Num = new List<int>();
            List<int> p4Num = new List<int>();
           
            GetFitness(population);
            population = population.OrderByDescending(s => s.fitness).ToList();
            deleteSame(population);
            rictxt.AppendText("\r\n\r\n");
            int n = 1;
            foreach (var item in population)
            {
                int cnt1 = 0, cnt2 = 0, cnt3 = 0, cnt4 = 0; ;
                foreach (var gene in item.genes)
                {
                    switch (gene)
                    {
                        case 1:
                            cnt1 += 1;
                            break;
                        case 2:
                            cnt2 += 1;
                            break;
                        case 3:
                            cnt3 += 1;
                            break;
                        case 4:
                            cnt4 += 1;
                            break;

                        case -1:
                            cnt1 += 1;
                            break;
                        case -2:
                            cnt2 += 1;
                            break;
                        case -3:
                            cnt3 += 1;
                            break;
                        case -4:
                            cnt4 += 1;
                            break;
                        default:
                            break;
                    }
                }
                rictxt.AppendText(n++ +"\tp1:"+cnt1+ "\tp2:" + cnt2 + "\tp3:" + cnt3 + "\tp4:" + cnt4 + "\trate:" + item.fitness + "\r\n");
                p1Num.Add(cnt1);
                p2Num.Add(cnt2);
                p3Num.Add(cnt3);
                p4Num.Add(cnt4);
            }
            rictxt.AppendText("\r\n\r\n");
            ssss(p1Num);
            ssss(p2Num);
            ssss(p3Num);
            ssss(p4Num);
            void ssss(List<int> p)
            {
                foreach (var item in p)
                {
                    rictxt.AppendText(item + ",");
                }
                rictxt.AppendText("\r\n");
            }
        }

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
        /// <summary>
        /// 检测基因，更改/添加
        /// </summary>
        /// <param name="gene"></param>
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
                    library_num.Remove(gene[i]);
                    if (library_num.Count == 0)
                    {
                        //尝试所有类型方块都不行,开始删除后面的基因点
                        gene.RemoveRange(i, gene.Count - i);
                        return;
                    }
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
        /// <summary>
        /// 生成染色体
        /// </summary>
        /// <param name="gene"></param>
        List<int> StartPopulation_new()//初始种群
        {
            List<int> gene = new List<int>();
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

            return gene;
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
                    //Graphics graphics = pictureBox1.CreateGraphics();
                    //graphics.DrawRectangle(Pens.Black, Xy.X / scale + moveX, (3000 - Xy.Y - rect.Height) / scale, Xy.Width / scale, Xy.Height / scale);
                    return true;
                }
            } while (true);
            return false;

            bool CheckLowLine(Point point1, Point point2, Point point3, Point point4, bool[,] pic1)
            {

                if (point1.X >= 0&& point1.Y >= 0)
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
                if (point1.X == 0)//接触到方框左壁
                {
                    return true;
                }
                if (point1.Y <= 3000 && point2.Y <= 3000 && point3.Y <= 3000 && point4.Y <= 3000)
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

        private void btnTry_Click(object sender, EventArgs e)
        {
            showPicture form2 = new showPicture();
            form2.Show();

            List<int> record = new List<int>();
            Rectangle rec = new Rectangle() { Width = 3000, Height = 1500 };
           
            foreach (var item in record)
            {
                rictxt.AppendText(item + " ");
            }

        }
    }
}
