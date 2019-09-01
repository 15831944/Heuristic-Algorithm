using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 木板最优切割方案
{
   public  class Board
    {
        public int width;
        public int height;
        public int needNum;
        public double profit;
    }
    public class Chromosome
    {
        public List<int> genes;
        public double fitness;
        public double profit;
    }
}
