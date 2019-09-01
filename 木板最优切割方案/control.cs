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

        int geneLength = 200;
        int populationNum = 100;//种群数量
        int interationTime = 20;//迭代次数
        int selectNum = 5;
        List<int> genes = new List<int>();
        List<Chromosome> population = new List<Chromosome>();
        List<Chromosome> crossChro = new List<Chromosome>();

        private void btnGo_Click(object sender, EventArgs e)
        {
            prbInitial.Maximum = populationNum;
            prbCross.Maximum =0;
            prbIteration.Maximum = interationTime;

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            Initial();
            //cross(new List<int>() { 1, 2, 3, 4, 5 }, new List<int>() { 99, 88, 77, 66, 55 });
            for (int i = 0; i < interationTime; i++)
            {
                SelectChro(SelectType.ratio);
                //cross();

            }

            stopwatch.Stop();
            rictxt.AppendText("\r\n\t" + stopwatch.Elapsed.TotalSeconds.ToString());
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
        void GetFitness(Chromosome chromosome)
        {
            int cnt1 = 0, cnt2 = 0, cnt3 = 0, cnt4 = 0; ;
            foreach (var item in chromosome.genes)
            {
                switch (item)
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
                    default:
                        break;
                }
            }
            double area = cnt1 * p1.width * p1.height
                + cnt2 * p2.width * p2.height
                + cnt3 * p3.width * p3.height
                + cnt4 * p4.width * p4.height;
            chromosome.fitness = area / (s1.height * s1.width);
            double profit = cnt1 * p1.profit
                            + cnt2 * p2.profit
                            + cnt3 * p3.profit
                            + cnt4 * p4.profit;
            chromosome.profit = profit;
        }
        /// <summary>
        /// 选出最优的一批
        /// </summary>
        /// <param name="population"></param>
        void SelectChro(SelectType type)
        {
            switch (type)
            {
                case SelectType.ratio:
                    population.OrderByDescending(s => s.fitness);
                    population = population.Take(selectNum).ToList();
                    break;
                case SelectType.profit:
                    population.OrderByDescending(s => s.profit);
                    population = population.Take(selectNum).ToList();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 交叉
        /// </summary>
        /// <param name="genes1"></param>
        /// <param name="genes2"></param>
        void cross(List<int> genes1, List<int> genes2)
        {
            int minLength = 0;
            int maxLength = Math.Min(genes1.Count, genes2.Count);
            int crossLength = new Random(new Guid().GetHashCode()).Next(minLength, maxLength);
            //交叉点
            int index1 = genes1.Count - crossLength;
            int index2 = genes2.Count - crossLength;
            //取出交叉段
            List<int> temp1 = genes1.Skip(index1 ).Take(crossLength).ToList();
            List<int> temp2 = genes2.Skip(index2 ).Take(crossLength).ToList();

            List<int> newChro1 = genes1.Take(index1).ToList();
            newChro1.AddRange(temp2);
            List<int> newChro2 = genes2.Take(index2).ToList();
            newChro2.AddRange(temp1);

            crossChro.Add(new Chromosome() { genes = newChro1 });
            crossChro.Add(new Chromosome() { genes = newChro2 });

        }



        private void btnTry_Click(object sender, EventArgs e)
        {
            showPicture form2 = new showPicture();
            form2.Show();

            List<int> record = new List<int>();
            Rectangle rec = new Rectangle() { Width = 3000, Height = 1500 };
            cut();
            foreach (var item in record)
            {
                rictxt.AppendText(item + " ");
            }
            void cut()
            {
                if (rec.Width >= p1.width && rec.Height >= p1.height)
                {
                    List<int> cnt = new List<int>() { 1000, 1000, 1000, 1000 };
                    for (int i = 0; i < 4; i++)
                    {
                        cnt[i] = 10000;
                    }
                    if (rec.Width >= p1.width && rec.Height >= p1.height)
                    {
                        cnt[0] = (rec.Width % p1.width) * p1.height;//hh
                        cnt[2] = (rec.Height % p1.height) * p1.width;//sh
                    }
                    if (rec.Width >= p1.height && rec.Height >= p1.width)
                    {
                        cnt[1] = (rec.Width % p1.height) * p1.width;//hs
                        cnt[3] = (rec.Height % p1.width) * p1.height;//ss
                    }
                    int num = cnt.IndexOf(cnt.Min());
                    switch (num)
                    {
                        case 0:
                            rec.Height -= p1.height;
                            break;
                        case 1:
                            rec.Height -= p1.width;
                            break;
                        case 2:
                            rec.Width -= p1.width;
                            break;
                        case 3:
                            rec.Width -= p1.height;
                            break;
                        default:
                            break;
                    }
                    record.Add(num);
                    cut();
                }
                else
                {
                    return;
                }
            }
            void randomCut()
            {
                if (rec.Width >= p1.width && rec.Height >= p1.height)
                {
                    List<int> cnt = new List<int>() { 1000, 1000, 1000, 1000 };
                    for (int i = 0; i < 4; i++)
                    {
                        cnt[i] = 10000;
                    }
                    if (rec.Width >= p1.width && rec.Height >= p1.height)
                    {
                        cnt[0] = (rec.Width % p1.width) * p1.height;//hh
                        cnt[2] = (rec.Height % p1.height) * p1.width;//sh
                    }
                    if (rec.Width >= p1.height && rec.Height >= p1.width)
                    {
                        cnt[1] = (rec.Width % p1.height) * p1.width;//hs
                        cnt[3] = (rec.Height % p1.width) * p1.height;//ss
                    }
                    int num = cnt.IndexOf(cnt.Min());
                    switch (num)
                    {
                        case 0:
                            rec.Height -= p1.height;
                            break;
                        case 1:
                            rec.Height -= p1.width;
                            break;
                        case 2:
                            rec.Width -= p1.width;
                            break;
                        case 3:
                            rec.Width -= p1.height;
                            break;
                        default:
                            break;
                    }
                    record.Add(num);
                    cut();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
