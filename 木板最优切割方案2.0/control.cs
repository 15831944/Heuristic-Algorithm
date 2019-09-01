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
using System.Windows.Forms.DataVisualization.Charting;

namespace 木板最优切割方案
{
    public partial class control : Form
    {
        public control()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
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
        int populationNum = 200;//种群数量
        int interationTime = 10;//迭代次数
        int selectNum = 5;
        int crossNum = 6;
        double MutationProbability = 0.5;//变异概率
        double SelectFitness = 0.85;
        List<int> genes = new List<int>();
        List<Chromosome> population = new List<Chromosome>();//种群集合
                                                             // List<Chromosome> crossChro = new List<Chromosome>();//交叉后的种群
        List<Chromosome> allChro = new List<Chromosome>();//总的记录
        List<int> library_allnum= new List<int> { 1, -1,2,-2, 3, -3 ,4,-4};
        Thread thread = null;  //声明一个线程
        public static List<int> theBest = new List<int>();

        private void btnGo_Click(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(Go));
            thread.Start();   //启动线程

        }

        private void Go()
        {
            toolStripStatusLabel1.Text = "正在迭代";
            chart1.Series.Clear();
            //把series添加到chart上
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    library_allnum = new List<int> { 1, -1 };
                    chart1.Series.Add(P1);
                    break;
                case 1:
                    library_allnum = new List<int> { 1, -1, 3, -3 };
                    chart1.Series.Add(P1);
                    chart1.Series.Add(P3);
                    break;
                case 2:
                    library_allnum = new List<int> { 1, -1, 3, -3 };
                    chart1.Series.Add(P1);
                    chart1.Series.Add(P3);
                    break;
                case 3:
                    library_allnum = new List<int> { 1, -1, 2, -2, 3, -3, 4, -4 };
                    chart1.Series.Add(P1);
                    chart1.Series.Add(P2);
                    chart1.Series.Add(P3);
                    chart1.Series.Add(P4);
                    break;
                case 4:
                    library_allnum = new List<int> { 1, -1, 2, -2, 3, -3, 4, -4 };
                    chart1.Series.Add(P1);
                    chart1.Series.Add(P2);
                    chart1.Series.Add(P3);
                    chart1.Series.Add(P4);
                    break;
                default:
                    MessageBox.Show("未选择问题！");
                    break;
            }
            //rictxt.Clear();
            interationTime = Convert.ToInt32(comboBox1.SelectedItem);
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
                Record(population, allChro);
                //allChro.AddRange(population.ToList());
                if (comboBox2.SelectedIndex == 4)
                    SelectChro(SelectType.profit);
                else
                    SelectChro(SelectType.ratio);

                population.Add(new Chromosome() { genes = StartPopulation_new() });
                showMessage(population, i);
                int num = 0;
                for (int j = 0; j < crossNum; j++)
                {
                    for (int k = j + 1; k < crossNum; k++)
                    {
                        cross(population[j].genes, population[k].genes);
                        cross(population[k].genes, population[j].genes);
                        prbCross.Value = num++;
                    }
                }
                Variation(population);

                prbIteration.Value = i + 1;
                // showMessage(population, i);

            }//迭代

            GetFitness(population);
            allChro.AddRange(population.ToList());

            if (comboBox2.SelectedIndex == 4)
                SelectChro(SelectType.profit);
            else
                SelectChro(SelectType.ratio);
            showMessage(population, interationTime);

            stopwatch.Stop();
            rictxt.AppendText("\r\n\t" + "用时：" + stopwatch.Elapsed.TotalSeconds.ToString() + "秒\r\n");
            theBest = population[0].genes;
            mes(allChro);
            void Record(List<Chromosome> population, List<Chromosome> allChro)
            {
                foreach (var item in population)
                {
                    if (item.fitness > SelectFitness)
                    {
                        allChro.Add(item);
                    }
                }
            }
            toolStripStatusLabel1.Text = "完成---用时：" + stopwatch.Elapsed.TotalSeconds.ToString() + "秒";
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
                    population = population.OrderByDescending(s => s.fitness).ToList();
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
            int minLength = 1;
            int maxLength = Math.Min(genes1.Count, genes2.Count);
            int crossLength = new Random(new Guid().GetHashCode()).Next(minLength, maxLength);
            //交叉点
            //int index1 = genes1.Count - crossLength;
            //int index2 = genes2.Count - crossLength;
            int index1 = new Random(new Guid().GetHashCode()).Next((genes1.Count - crossLength) / 2);
            int index2 = new Random(new Guid().GetHashCode()).Next((genes2.Count - crossLength) / 2, genes2.Count - crossLength);
            //取出交叉段
            List<int> temp1 = genes1.Skip(index1).Take(crossLength).ToList();
            List<int> temp2 = genes2.Skip(index2).Take(crossLength).ToList();

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

            Decode_new(newChro1, pic);
            Decode_new(newChro2, pic);
            population.Add(new Chromosome() { genes = newChro1 });
            population.Add(new Chromosome() { genes = newChro2 });

        }

        void Variation(List<Chromosome> population)
        {
            List<int> library_num = library_allnum.ToList();
            //List<int> library_num = new List<int>() { 1, -1, 2, -2, 3, -3, 4, -4 };

            Random random = new Random();
            List<Chromosome> temp = new List<Chromosome>();
            for (int i = 0; i < population.Count; i++)
            {
                if (random.NextDouble() > MutationProbability)
                {
                    continue;
                }
                List<int> genes = population[i].genes.ToList();
                int index = random.Next(10);
                genes[index] = library_num[random.Next(library_num.Count)];
                Decode_new(genes, pic);
                temp.Add(new Chromosome() { genes = genes });
            }
            population.AddRange(temp);
        }

        void showMessage(List<Chromosome> population, int times)
        {
            rictxt.AppendText("第" + times + "代\r\n");

            int num = 0;
            foreach (var item in population)
            {
                rictxt.AppendText("\t" + (++num) + "\t" + item.fitness + "\t" + item.profit + "\r\n");
            }
            rictxt.AppendText("--------------------\r\n\n");
            rictxt.SelectionStart = rictxt.TextLength;
            rictxt.ScrollToCaret();
        }

        void mes(List<Chromosome> population)
        {

            List<int> p1Num = new List<int>();
            List<int> p2Num = new List<int>();
            List<int> p3Num = new List<int>();
            List<int> p4Num = new List<int>();

            GetFitness(population);
            population.RemoveAll(s => s.fitness < SelectFitness);
            population = population.OrderByDescending(s => s.fitness).ToList();
            deleteSame(population);
            rictxt.AppendText("\r\n适应度百分之88以上的所有解：\r\n");

            double d_temp = 0;
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
                rictxt.AppendText(n++ + "\tp1:" + cnt1 + "\tp2:" + cnt2 + "\tp3:" + cnt3 + "\tp4:" + cnt4 + "\trate:" + item.fitness + "\r\n");
                if(Convert.ToDouble(item.fitness.ToString("F4"))!= d_temp)
                {
                    Draw(Convert.ToDouble(item.fitness.ToString("F4")), cnt1, P1);
                    Draw(Convert.ToDouble(item.fitness.ToString("F4")), cnt2, P2);
                    Draw(Convert.ToDouble(item.fitness.ToString("F4")), cnt3, P3);
                    Draw(Convert.ToDouble(item.fitness.ToString("F4")), cnt4, P4);
                }
                d_temp = Convert.ToDouble(item.fitness.ToString("F4"));

                p1Num.Add(cnt1);
                p2Num.Add(cnt2);
                p3Num.Add(cnt3);
                p4Num.Add(cnt4);
            }
            rictxt.AppendText("\r\n线性规划用：\r\n");
            ssss(p1Num);
            ssss(p2Num);
            ssss(p3Num);
            ssss(p4Num);

            rictxt.AppendText("\r\n优秀染色体如下：\r\n");
            for (int i = 0; i < population.Count; i++)
            {
                rictxt.AppendText("\r\n切割方案" + (i + 1) + "：\t适应度：" + population[i].fitness + "\r\n");
                foreach (var item in population[i].genes)
                {
                    rictxt.AppendText(item + ",");
                }
            }
            void ssss(List<int> p)
            {
                foreach (var item in p)
                {
                    rictxt.AppendText(item + ",");
                }
                rictxt.AppendText("\r\n\r\n");
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

        bool[,] pic = new bool[1501, 3001];//记录点的坐标

        /// <summary>
        /// 检测基因，更改/添加
        /// </summary>
        /// <param name="gene"></param>
        private void Decode_new(List<int> gene, bool[,] pic)
        {
            List<int> library_num = library_allnum.ToList();
            //List<int> library_num = new List<int>() { 1, -1, 2, -2, 3, -3, 4, -4 };

            // bool[,] pic = new bool[1501, 3001];//记录点的坐标
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
                    library_num = library_allnum.ToList();
                }
            }
            //到达此处说明基因现在没问题，可尝试接着添加基因点
            List<int> library_num2 = library_allnum.ToList();
            Random rand = new Random();
            while (library_num2.Count != 0)
            {
                int index = rand.Next(0, library_num2.Count);
                if (Mapping_new(pic, library_num2[index]))
                {
                    //如果可行
                    gene.Add(library_num2[index]);
                    library_num2 = library_allnum.ToList();
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
            List<int> library_num2 = library_allnum.ToList();
            Random rand = new Random();
            while (library_num2.Count != 0)
            {
                int index = rand.Next(0, library_num2.Count);
                if (Mapping_new(pic, library_num2[index]))
                {
                    //如果可行
                    gene.Add(library_num2[index]);
                    library_num2 = library_allnum.ToList();
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

                if (point1.X >= 0 && point1.Y >= 0)
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
        #region 画数据图部分
        Series P1 = new Series("P1");
        Series P2 = new Series("P2");
        Series P3 = new Series("P3");
        Series P4 = new Series("P4");
        private void Draw_first()
        {
            chart1.Series.Clear();
            P1.ChartType = SeriesChartType.Spline;
            P1.IsValueShownAsLabel = false;
            P1.Color = System.Drawing.Color.Red;
            P2.ChartType = SeriesChartType.Spline;
            P2.IsValueShownAsLabel = false;
            P2.Color = System.Drawing.Color.Yellow;
            P3.ChartType = SeriesChartType.Spline;
            P3.IsValueShownAsLabel = false;
            P3.Color = System.Drawing.Color.Blue;
            P4.ChartType = SeriesChartType.Spline;
            P4.IsValueShownAsLabel = false;
            P4.Color = System.Drawing.Color.Black;

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0%";//格式化，为了显示百分号
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
            chart1.ChartAreas[0].AxisX.Title = "木板利用率";
            chart1.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Black;
            //chart1.ChartAreas[0].AxisX.TitleFont.Size = ;
            chart1.ChartAreas[0].AxisY.Title = "每块木板切出的产品个数";
            chart1.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Black;
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Horizontal;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            P1.LegendText = "P1木板";
            P2.LegendText = "P2木板";
            P3.LegendText = "P3木板";
            P4.LegendText = "P4木板";
            //P2.Points.AddXY("1", "44");
            //P2.Points.AddXY("2", "44");
            //P2.Points.AddXY("3", "44");
            //P2.Points.AddXY("4", "44");
            //P2.Points.AddXY("5", "44");
            //P2.Points.AddXY("6", "44");
            //P2.Points.AddXY("7", "44");
            //P2.Points.AddXY("8", "44");
            //P2.LegendText = "平均每圈换色次数";
        }
        private void Draw(double x, double y, Series ser)
        {
            ser.Points.AddXY(x, y);
        }

        private void control_Load(object sender, EventArgs e)
        {
            Draw_first();
            comboBox2.SelectedIndex=3;
        }
        #endregion

        private void control_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(thread!=null)
            {
                thread.Abort();
                thread.Join(10);
            }
            
            System.Environment.Exit(0);
        }
    }
}
