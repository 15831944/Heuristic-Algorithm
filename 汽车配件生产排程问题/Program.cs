using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 汽车配件生产排程问题
{
    class Program
    {
        static 零件类 底漆件 = new 零件类() { 颜色 = "无", 需求量 = 0, 支架数量 = 0, 项目编号 = -1, 所属产品 = "底漆件" };//序号0
        static 零件类 上格栅A = new 零件类() { 颜色 = "铱银", 需求量 = 135, 支架数量 = 34, 项目编号 = 0, 所属产品 = "上格栅A" };//序号1
        static 零件类 上格栅B = new 零件类() { 颜色 = "铱银", 需求量 = 150, 支架数量 = 39, 所属产品 = "上格栅B", 项目编号 = 0 };
        static 零件类 中间扰流板A_光耀蓝 = new 零件类() { 颜色 = "光耀蓝", 需求量 = 39, 支架数量 = 95, 所属产品 = "中间扰流板A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 中间扰流板A_曜岩黑 = new 零件类() { 颜色 = "曜岩黑", 需求量 = 72, 支架数量 = 95, 所属产品 = "中间扰流板A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 中间扰流板A_极地白 = new 零件类() { 颜色 = "极地白", 需求量 = 149, 支架数量 = 95, 所属产品 = "中间扰流板A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 中间扰流板A_米兰银 = new 零件类() { 颜色 = "米兰银", 需求量 = 76, 支架数量 = 95, 所属产品 = "中间扰流板A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保A_光耀蓝 = new 零件类() { 颜色 = "光耀蓝", 需求量 = 249, 支架数量 = 400, 所属产品 = "前保A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保A_宝石蓝 = new 零件类() { 颜色 = "宝石蓝", 需求量 = 80, 支架数量 = 400, 所属产品 = "前保A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保A_曜岩黑 = new 零件类() { 颜色 = "曜岩黑", 需求量 = 212, 支架数量 = 400, 所属产品 = "前保A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保A_极地白 = new 零件类() { 颜色 = "极地白", 需求量 = 885, 支架数量 = 400, 所属产品 = "前保A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保A_米兰银 = new 零件类() { 颜色 = "米兰银", 需求量 = 84, 支架数量 = 400, 所属产品 = "前保A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保A_铱银 = new 零件类() { 颜色 = "铱银", 需求量 = 40, 支架数量 = 400, 所属产品 = "前保A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保B_宝石蓝 = new 零件类() { 颜色 = "宝石蓝", 需求量 = 27, 支架数量 = 28, 所属产品 = "前保B", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保B_极地白 = new 零件类() { 颜色 = "极地白", 需求量 = 28, 支架数量 = 28, 所属产品 = "前保B", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保B_铱银 = new 零件类() { 颜色 = "铱银", 需求量 = 24, 支架数量 = 28, 所属产品 = "前保B", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保C_光耀蓝 = new 零件类() { 颜色 = "光耀蓝", 需求量 = 12, 支架数量 = 141, 所属产品 = "前保C", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保C_宝石蓝 = new 零件类() { 颜色 = "宝石蓝", 需求量 = 3, 支架数量 = 141, 所属产品 = "前保C", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保C_曜岩黑 = new 零件类() { 颜色 = "曜岩黑", 需求量 = 424, 支架数量 = 141, 所属产品 = "前保C", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保C_钻石白 = new 零件类() { 颜色 = "钻石白", 需求量 = 78, 支架数量 = 141, 所属产品 = "前保C", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保D_宝石蓝 = new 零件类() { 颜色 = "宝石蓝", 需求量 = 15, 支架数量 = 255, 所属产品 = "前保D", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保D_曜岩黑 = new 零件类() { 颜色 = "曜岩黑", 需求量 = 992, 支架数量 = 255, 所属产品 = "前保D", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保E_光耀蓝 = new 零件类() { 颜色 = "光耀蓝", 需求量 = 115, 支架数量 = 297, 所属产品 = "前保E", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保E_宝石红 = new 零件类() { 颜色 = "宝石红", 需求量 = 99, 支架数量 = 297, 所属产品 = "前保E", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保E_曜岩黑 = new 零件类() { 颜色 = "曜岩黑", 需求量 = 373, 支架数量 = 297, 所属产品 = "前保E", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保E_米兰银 = new 零件类() { 颜色 = "米兰银", 需求量 = 75, 支架数量 = 297, 所属产品 = "前保E", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保E_钻石白 = new 零件类() { 颜色 = "钻石白", 需求量 = 475, 支架数量 = 297, 所属产品 = "前保E", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保F_光耀蓝 = new 零件类() { 颜色 = "光耀蓝", 需求量 = 128, 支架数量 = 280, 所属产品 = "前保F", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保F_极地白 = new 零件类() { 颜色 = "极地白", 需求量 = 961, 支架数量 = 280, 所属产品 = "前保F", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保G_宇宙黑 = new 零件类() { 颜色 = "宇宙黑", 需求量 = 43, 支架数量 = 51, 所属产品 = "前保G", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保G_米兰银 = new 零件类() { 颜色 = "米兰银", 需求量 = 20, 支架数量 = 51, 所属产品 = "前保G", 项目编号 = 0 };//和支架数量！！！
        static 零件类 前保G_钻石白 = new 零件类() { 颜色 = "钻石白", 需求量 = 121, 支架数量 = 51, 所属产品 = "前保G", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保A_光耀蓝 = new 零件类() { 颜色 = "光耀蓝", 需求量 = 174, 支架数量 = 304, 所属产品 = "后保A", 项目编号 = 3 };//和支架数量！！！
        static 零件类 后保A_宝石红 = new 零件类() { 颜色 = "宝石红", 需求量 = 372, 支架数量 = 304, 所属产品 = "后保A", 项目编号 = 3 };//和支架数量！！！
        static 零件类 后保A_宝石蓝 = new 零件类() { 颜色 = "宝石蓝", 需求量 = 3, 支架数量 = 304, 所属产品 = "后保A", 项目编号 = 3 };//和支架数量！！！
        static 零件类 后保A_极地白 = new 零件类() { 颜色 = "极地白", 需求量 = 565, 支架数量 = 304, 所属产品 = "后保A", 项目编号 = 3 };//和支架数量！！！
        static 零件类 后保A_铱银 = new 零件类() { 颜色 = "铱银", 需求量 = 35, 支架数量 = 304, 所属产品 = "后保A", 项目编号 = 3 };//和支架数量！！！
        static 零件类 后保B_米兰银 = new 零件类() { 颜色 = "米兰银", 需求量 = 75, 支架数量 = 21, 所属产品 = "后保B", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保C_宝石蓝 = new 零件类() { 颜色 = "宝石蓝", 需求量 = 85, 支架数量 = 251, 所属产品 = "后保C", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保C_曜岩黑 = new 零件类() { 颜色 = "曜岩黑", 需求量 = 808, 支架数量 = 251, 所属产品 = "后保C", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保C_钻石白 = new 零件类() { 颜色 = "钻石白", 需求量 = 87, 支架数量 = 251, 所属产品 = "后保C", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保D_曜岩黑 = new 零件类() { 颜色 = "曜岩黑", 需求量 = 3, 支架数量 = 11, 所属产品 = "后保D", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保D_铱银 = new 零件类() { 颜色 = "铱银", 需求量 = 11, 支架数量 = 11, 所属产品 = "后保D", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保E_极地白 = new 零件类() { 颜色 = "极地白", 需求量 = 6, 支架数量 = 3, 所属产品 = "后保E", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保F_光耀蓝 = new 零件类() { 颜色 = "光耀蓝", 需求量 = 115, 支架数量 = 225, 所属产品 = "后保F", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保F_宝石红 = new 零件类() { 颜色 = "宝石红", 需求量 = 101, 支架数量 = 225, 所属产品 = "后保F", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保F_曜岩黑 = new 零件类() { 颜色 = "曜岩黑", 需求量 = 278, 支架数量 = 225, 所属产品 = "后保F", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保F_米兰银 = new 零件类() { 颜色 = "米兰银", 需求量 = 41, 支架数量 = 225, 所属产品 = "后保F", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保F_钻石白 = new 零件类() { 颜色 = "钻石白", 需求量 = 323, 支架数量 = 225, 所属产品 = "后保F", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保G_米兰银 = new 零件类() { 颜色 = "米兰银", 需求量 = 79, 支架数量 = 131, 所属产品 = "后保G", 项目编号 = 0 };//和支架数量！！！
        static 零件类 后保G_钻石白 = new 零件类() { 颜色 = "钻石白", 需求量 = 427, 支架数量 = 131, 所属产品 = "后保G", 项目编号 = 0 };//和支架数量！！！
        static 零件类 外壳A_光耀蓝 = new 零件类() { 颜色 = "光耀蓝", 需求量 = 13, 支架数量 = 52, 所属产品 = "外壳A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 外壳A_宝石红 = new 零件类() { 颜色 = "宝石红", 需求量 = 7, 支架数量 = 52, 所属产品 = "外壳A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 外壳A_宝石蓝 = new 零件类() { 颜色 = "宝石蓝", 需求量 = 18, 支架数量 = 52, 所属产品 = "外壳A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 外壳A_极地白 = new 零件类() { 颜色 = "极地白", 需求量 = 87, 支架数量 = 52, 所属产品 = "外壳A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 外壳A_牛仔蓝 = new 零件类() { 颜色 = "牛仔蓝", 需求量 = 6, 支架数量 = 52, 所属产品 = "外壳A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 外壳A_米兰银 = new 零件类() { 颜色 = "米兰银", 需求量 = 3, 支架数量 = 52, 所属产品 = "外壳A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 轮口装饰件A_光耀蓝 = new 零件类() { 颜色 = "光耀蓝", 需求量 = 51, 支架数量 = 63, 所属产品 = "轮口装饰件A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 轮口装饰件A_宝石蓝 = new 零件类() { 颜色 = "宝石蓝", 需求量 = 6, 支架数量 = 63, 所属产品 = "轮口装饰件A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 轮口装饰件A_极地白 = new 零件类() { 颜色 = "极地白", 需求量 = 168, 支架数量 = 63, 所属产品 = "轮口装饰件A", 项目编号 = 0 };//和支架数量！！！
        static 零件类 轮口装饰件B_牛仔蓝 = new 零件类() { 颜色 = "牛仔蓝", 需求量 = 4, 支架数量 = 15, 所属产品 = "轮口装饰件B", 项目编号 = 0 };//和支架数量！！！
        static 零件类 轮口装饰件B_钻石白 = new 零件类() { 颜色 = "钻石白", 需求量 = 26, 支架数量 = 15, 所属产品 = "轮口装饰件B", 项目编号 = 0 };//和支架数量！！！
        static 零件类 门槛A_光耀蓝 = new 零件类() { 颜色 = "光耀蓝", 需求量 = 204, 支架数量 = 341, 所属产品 = "门槛A", 项目编号 = 3 };//和支架数量！！！
        static 零件类 门槛A_宝石红 = new 零件类() { 颜色 = "宝石红", 需求量 = 351, 支架数量 = 341, 所属产品 = "门槛A", 项目编号 = 3 };//和支架数量！！！
        static 零件类 门槛A_宝石蓝 = new 零件类() { 颜色 = "宝石蓝", 需求量 = 86, 支架数量 = 341, 所属产品 = "门槛A", 项目编号 = 3 };//和支架数量！！！
        static 零件类 门槛A_曜岩黑 = new 零件类() { 颜色 = "曜岩黑", 需求量 = 47, 支架数量 = 341, 所属产品 = "门槛A", 项目编号 = 3 };//和支架数量！！！
        static 零件类 门槛A_极地白 = new 零件类() { 颜色 = "极地白", 需求量 = 505, 支架数量 = 341, 所属产品 = "门槛A", 项目编号 = 3 };//和支架数量！！！
        static 零件类 门槛A_米兰银 = new 零件类() { 颜色 = "米兰银", 需求量 = 83, 支架数量 = 341, 所属产品 = "门槛A", 项目编号 = 3 };//和支架数量！！！
        static 零件类 门槛A_铱银 = new 零件类() { 颜色 = "铱银", 需求量 = 10, 支架数量 = 341, 所属产品 = "门槛A", 项目编号 = 3 };//和支架数量！！！
        static 零件类 门槛B_宝石蓝 = new 零件类() { 颜色 = "宝石蓝", 需求量 = 94, 支架数量 = 219, 项目编号 = 1, 所属产品 = "门槛B" };//和支架数量！！！
        static 零件类 门槛B_曜岩黑 = new 零件类() { 颜色 = "曜岩黑", 需求量 = 579, 支架数量 = 219, 项目编号 = 1, 所属产品 = "门槛B" };//和支架数量！！！
        static 零件类 门槛B_钻石白 = new 零件类() { 颜色 = "钻石白", 需求量 = 177, 支架数量 = 219, 项目编号 = 1, 所属产品 = "门槛B" };//和支架数量！！！
        static 零件类 门槛C_曜岩黑 = new 零件类() { 颜色 = "曜岩黑", 需求量 = 276, 支架数量 = 69, 项目编号 = 2, 所属产品 = "门槛C", };//和支架数量！！！
        static 零件类 门槛D_极地白 = new 零件类() { 颜色 = "极地白", 需求量 = 468, 支架数量 = 118, 项目编号 = 3, 所属产品 = "门槛" };//和支架数量！！！
        static 零件类 门槛E_米兰银 = new 零件类() { 颜色 = "米兰银", 需求量 = 12, 支架数量 = 3, 项目编号 = 0, 所属产品 = "门槛E" };//和支架数量！！！
        static 零件类 门槛装饰条A = new 零件类() { 颜色 = "铱银", 需求量 = 299, 支架数量 = 76, 项目编号 = 3, 所属产品 = "门槛装饰条A" };//和支架数量！！！
        static 零件类 雷达支架A_宝石红 = new 零件类() { 颜色 = "宝石红", 需求量 = 5, 支架数量 = 5, 项目编号 = 4, 所属产品 = "雷达支架A" };//和支架数量！！！
        static 零件类 雷达支架B_宝石蓝 = new 零件类() { 颜色 = "宝石蓝", 需求量 = 12, 支架数量 = 16, 项目编号 = 4, 所属产品 = "雷达支架B" };//和支架数量！！！
        static 零件类 雷达支架B_极地白 = new 零件类() { 颜色 = "极地白", 需求量 = 32, 支架数量 = 16, 项目编号 = 4, 所属产品 = "雷达支架B" };//和支架数量！！！
        static 零件类 雷达支架C_钻石白 = new 零件类() { 颜色 = "钻石白", 需求量 = 26, 支架数量 = 9, 项目编号 = 4, 所属产品 = "雷达支架C" };//和支架数量！！！
        static 零件类 雷达支架D_曜岩黑 = new 零件类() { 颜色 = "曜岩黑", 需求量 = 4, 支架数量 = 10, 项目编号 = 4, 所属产品 = "雷达支架D" };//和支架数量！！！
        static 零件类 雷达支架D_米兰银 = new 零件类() { 颜色 = "米兰银", 需求量 = 3, 支架数量 = 10, 项目编号 = 4, 所属产品 = "雷达支架D" };//和支架数量！！！
        static 零件类 雷达支架E_牛仔蓝 = new 零件类() { 颜色 = "牛仔蓝", 需求量 = 5, 支架数量 = 11, 项目编号 = 4, 所属产品 = "雷达支架E" };//和支架数量！！！
        static 零件类 雷达支架E_钻石白 = new 零件类() { 颜色 = "钻石白", 需求量 = 6, 支架数量 = 11, 项目编号 = 4, 所属产品 = "雷达支架E" };//和支架数量！！！

        
        static List<int> cells_list = new List<int>();
        static List<int>[] cells_list_last = new List<int>[2424];
        static Random rand = new Random();
        static int f_value = 587;//极地白阀值
        static List<零件类> 零件字典 = new List<零件类>()
        {
            底漆件,上格栅A,上格栅B,中间扰流板A_光耀蓝,中间扰流板A_曜岩黑,中间扰流板A_极地白,中间扰流板A_米兰银,前保A_光耀蓝,前保A_宝石蓝,前保A_曜岩黑,前保A_极地白,前保A_米兰银,前保A_铱银,前保B_宝石蓝,前保B_极地白, 前保B_铱银,前保C_光耀蓝 ,
            前保C_宝石蓝 ,前保C_曜岩黑 , 前保C_钻石白 ,前保D_宝石蓝 , 前保D_曜岩黑 ,前保E_光耀蓝 ,前保E_宝石红 ,前保E_曜岩黑 ,前保E_米兰银 ,前保E_钻石白 ,前保F_光耀蓝 , 前保F_极地白 ,前保G_宇宙黑 ,
            前保G_米兰银 ,前保G_钻石白 ,后保A_光耀蓝 ,后保A_宝石红 ,后保A_宝石蓝 ,后保A_极地白 ,后保A_铱银 ,后保B_米兰银 ,后保C_宝石蓝 ,后保C_曜岩黑 ,后保C_钻石白 ,后保D_曜岩黑 , 后保D_铱银 ,
            后保E_极地白 ,后保F_光耀蓝 ,后保F_宝石红 ,后保F_曜岩黑 , 后保F_米兰银 ,后保F_钻石白 ,后保G_米兰银 ,后保G_钻石白 ,外壳A_光耀蓝,外壳A_宝石红 ,外壳A_宝石蓝 ,外壳A_极地白 ,外壳A_牛仔蓝 ,
            外壳A_米兰银 ,轮口装饰件A_光耀蓝 ,轮口装饰件A_宝石蓝 ,轮口装饰件A_极地白 ,轮口装饰件B_牛仔蓝 ,轮口装饰件B_钻石白 ,门槛A_光耀蓝 , 门槛A_宝石红 ,门槛A_宝石蓝 , 门槛A_曜岩黑 ,门槛A_极地白 ,
            门槛A_米兰银 ,门槛A_铱银 ,门槛B_宝石蓝 ,门槛B_曜岩黑 ,门槛B_钻石白 ,门槛C_曜岩黑 ,门槛D_极地白 ,门槛E_米兰银 ,门槛装饰条A ,雷达支架A_宝石红 ,雷达支架B_宝石蓝 ,雷达支架B_极地白 , 雷达支架C_钻石白 ,
            雷达支架D_曜岩黑 ,雷达支架D_米兰银 ,雷达支架E_牛仔蓝 ,雷达支架E_钻石白
        };
        static void Main(string[] args)
        {
            Set();
            Console.Read();
        }
        private static void Set()
        {
            for (int n = 0; n < cells_list_last.Length; n++)
            {
                cells_list_last[n] = new List<int>();
            }
            for (int n = 0; n < cells_list_last.Length; n++)
            {
                cells_list_last[n] = new List<int>();
            }
            cells_list.Add(70);
            //Console.Write(cells_list[0] + " ");
            do
            {
                //cells_list_last.Count(有数字的) - 1= cells_list.Count 
                if (cells_list_last[cells_list.Count].Count > 0)//库中是否有解
                {
                    int index;
                    //从库中选取最优颜色
                    index = Better_color_index(cells_list_last[cells_list.Count], 零件字典[cells_list[cells_list.Count - 1]].颜色);
                    int right = cells_list_last[cells_list.Count][index];
                    //Console.Write("\n换色 {0}->{1} ", 零件字典[cells_list[cells_list.Count - 1]].颜色, 零件字典[right].颜色);
                    Print_list_little(cells_list_last[cells_list.Count]);
                    Console.Write("  选1择{0} ", index);
                    cells_list_last[cells_list.Count].RemoveAt(index);//移除最优颜色元素 此时元素还没有添加，是下一个滑撬的所有可行解
                    Print_list_little(cells_list_last[cells_list.Count]);
                    Console.Write("  ({0})\n", cells_list.Count);
                    List<int> temp_1 = cells_list_last[cells_list.Count].ToList();
                    cells_list_last[cells_list.Count] = new List<int>();
                    if (零件字典[cells_list[cells_list.Count - 1 - 1]].颜色 != 零件字典[cells_list[cells_list.Count - 1]].颜色)
                    {
                        //插入底漆件滑撬
                        cells_list.Add(right);
                        cells_list.RemoveAt(cells_list.Count - 1);//移除最后一个元素
                        cells_list.Add(0);
                        cells_list_last[cells_list.Count - 1] = new List<int>();
                        //Console.Write(cells_list[cells_list.Count - 1] + " ");
                        cells_list.Add(right);
                    }
                    else
                    {
                        cells_list.Add(right);
                    }
                    if (cells_list.Count > 2424)
                    {
                        break;
                    }
                    else
                    {
                        cells_list_last[cells_list.Count - 1] = temp_1;
                    }

                }
                else//库中无可行解 查找合适解
                {
                    //遍历合适结果
                    List<int> temp_suitale = new List<int>();
                    for (int j = 1; j < 零件字典.Count; j++)
                    {
                        cells_list.Add(j);
                        if (Constraint(cells_list) && Same_supports_all(cells_list) && Demand_count(cells_list))//局部 <颜色 零件 支架 需求量> 约束
                        {
                            temp_suitale.Add(j);
                        }
                        cells_list.RemoveAt(cells_list.Count - 1);//移除最后一个元素
                    }
                    if (temp_suitale.Count > 0)
                    {
                        int index;
                        //从合适结果中选取最优颜色
                        index = Better_color_index(temp_suitale, 零件字典[cells_list[cells_list.Count - 1]].颜色);
                        int right = temp_suitale[index];
                        //Console.Write("\n换色 {0}->{1} ", 零件字典[cells_list[cells_list.Count - 1]].颜色, 零件字典[right].颜色);
                        Print_list_little(temp_suitale);
                        Console.Write("  选2择{0} ", index);
                        temp_suitale.RemoveAt(index);//移除最优颜色元素 此时元素还没有添加，是下一个滑撬的所有可行解
                        Print_list_little(temp_suitale);
                        Console.Write("  ({0})\n", cells_list.Count);
                        cells_list.Add(right);
                        //Console.Write("\n添加");
                        if (零件字典[cells_list[cells_list.Count - 1 - 1]].颜色 != 零件字典[cells_list[cells_list.Count - 1]].颜色)
                        {
                            //插入底漆件滑撬
                            cells_list.RemoveAt(cells_list.Count - 1);//移除最后一个元素
                            cells_list.Add(0);
                            //Console.Write("0->");
                            cells_list.Add(right);
                        }
                        if (cells_list.Count > 2424)
                        {
                            break;
                        }
                        else
                            cells_list_last[cells_list.Count - 1] = temp_suitale.ToList();//此处list处库有可能为0
                    }
                    else
                    {
                        //达到无解，回调      这里有问题！！
                        Console.Write("\n<达到无解，\n\n\n\n\n\n\n\n");
                        //Console.Write("移除{0} ", cells_list[cells_list.Count - 1] + " ");
                        int k = cells_list.Count - 1;
                        do
                        {
                            if (cells_list_last[k].Count == 0)
                            {
                                cells_list.RemoveAt(k);
                            }
                            else
                            {
                                break;
                            }
                            k--;
                        } while (true);
                        cells_list.RemoveAt(cells_list.Count - 1);
                        if (cells_list[cells_list.Count - 1] == 0)//移除底漆件
                        {
                            cells_list_last[cells_list.Count - 1] = cells_list_last[cells_list.Count].ToList();
                            cells_list_last[cells_list.Count] = new List<int>();
                            cells_list.RemoveAt(cells_list.Count - 1);
                        }
                        Console.Write("，回调到索引{0}处> 此时{1}:{2} {3}:{4}\n", cells_list.Count-1, cells_list.Count - 1, cells_list[cells_list.Count - 1], cells_list.Count - 2, cells_list[cells_list.Count - 2]);
                    }
                }
                int j_count = 0;
                int z_count = 0;
                for (int m = 0; m < cells_list.Count; m++)
                {
                    if (零件字典[cells_list[m]].颜色 == "极地白")
                        j_count++;
                    if (零件字典[cells_list[m]].颜色 == "钻石白")
                        z_count++;
                }
                Console.Write("极地白{0},钻石白{1},剩余滑撬{2}\n", j_count, z_count, 2424 - cells_list.Count);
            } while (cells_list.Count < 2424);
            if (cells_list.Count == 2425)
                cells_list.RemoveAt(cells_list.Count - 1);
            Print_list(cells_list);
            FileStream fs = new FileStream("信息.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("平均换色{0}圈 f_value={1} 第一个滑撬为{2}", Change_color_count(cells_list), f_value, cells_list[0]);
            sw.Flush();
            sw.Close();
            fs.Close();
            Print_excel(cells_list);
            No_satisfycount(cells_list);
            //cells_list.Clear();
        }
        //计算所有零件未满足需求量数
        private static void No_satisfycount(List<int> cells_list)//L为零件编号
        {
            for(int j=1;j< 零件字典.Count;j++)
            {
                int count = 0;//滑橇数
                for (int i = 0; i < cells_list.Count; i++)
                {
                    if (cells_list[i] == j)
                        count++;
                }
                if (count * 6 < 零件字典[j].需求量)
                {
                    Console.Write("\n零件编号{0} {1} {2} 未满足数{3}", j, 零件字典[j].所属产品, 零件字典[j].颜色, 零件字典[j].需求量 - count * 6);
                }
            }
        }
        //输出excel
        private static void Print_excel(List<int> cells_list)
        {
            FileStream fs = new FileStream("EXCEL信息.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("滑橇编号 颜色 产品名称");
            for (int i = 0; i < 2424; i ++)
            {
                sw.WriteLine("{0} {1} {2}", i+1,零件字典[cells_list[i]].颜色, 零件字典[cells_list[i]].所属产品);
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        //打印数组元素
        private static void Print_list(List<int> cells_list)
        {
            int z_count = 0;
            int j_count = 0;
            for (int i = 0; i < 2424; i += 303)
            {
                Console.WriteLine("\n以下是第{0}圈-------------", i / 303 + 1);
                for (int j = i; j < i + 303; j++)
                {
                    Console.Write(cells_list[j] + " ");
                    if (零件字典[cells_list[j]].颜色 == "钻石白")
                        z_count++;
                    if (零件字典[cells_list[j]].颜色 == "极地白")
                        j_count++;
                }
            }
            Console.WriteLine("\n-------------共{0}元素--------钻石白{1}个,极地白{2}个-------------", cells_list.Count, z_count, j_count);
        }
        //小打印数组元素
        private static void Print_list_little(List<int> cells_list)
        {
            for (int i = 0; i < cells_list.Count; i++)
            {
                Console.Write("{0} ", cells_list[i]);
            }
        }
        //从库中选取最优颜色,返回选取索引值
        private static int Better_color_index(List<int> cells_list_last, string color)
        {
            //if (color == "极地白")
            //{
            //    int j_count = 0;
            //    int z_count = 0;
            //    for(int i=0;i< cells_list.Count;i++)
            //    {
            //        if (零件字典[cells_list[i]].颜色 == "极地白")
            //            j_count++;
            //        if (零件字典[cells_list[i]].颜色 == "钻石白")
            //            z_count++;
            //    }
            //    if(f_value - j_count > Math.Min(291 - z_count, (2424 - cells_list.Count) / 3))//为真声明还有"极地白"可用 643- j_count>Math.Min(291- z_count,(2424- cells_list.Count)/3)
            //    {
            //        Console.Write("\n还有极地白可用");
            //        for (int i = 0; i < cells_list_last.Count; i++)
            //        {
            //            if (零件字典[cells_list_last[i]].颜色 == "极地白" || 零件字典[cells_list_last[i]].颜色 != "钻石白")//库中不选"钻石白"
            //            {
            //                return i;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < cells_list_last.Count; i++)
            //        {
            //            if (零件字典[cells_list_last[i]].颜色 == "钻石白")
            //            {
            //                return i;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < cells_list_last.Count; i++)
            //    {
            //        if (零件字典[cells_list_last[i]].颜色 == color)
            //        {
            //            return i;
            //        }
            //    }
            //    for (int i = 0; i < cells_list_last.Count; i++)
            //    {
            //        //优先不排"极地白"，免得没得东西消耗"钻石白"
            //        if (零件字典[cells_list_last[i]].颜色 != "极地白")
            //        {
            //            return i;
            //        }
            //    }
            //}
            for (int i = 0; i < cells_list_last.Count; i++)
            {
                if (零件字典[cells_list_last[i]].颜色 == color)
                {
                    return i;
                }
            }
            return 0;
        }

        //局部需求量约束
        private static bool Demand_count(List<int> cells_list)//为真就通过约束
        {
            int count = 0;//滑撬数
            int demand = 0;//需求量
            for (int i = 0; i < cells_list.Count; i++)
            {
                if (cells_list[i] == cells_list[cells_list.Count - 1])
                {
                    demand = 零件字典[cells_list[i]].需求量;
                    count++;
                }
            }
            if (demand % 6 == 0)
            {
                if (count > demand / 6)
                {
                    return false;
                }
            }
            else
            {
                if (count > demand / 6 + 1)
                {
                    return false;
                }
            }
            return true;
        }
        //从局部查询<颜色 零件>约束(算法使得此处可以无视底漆件)
        private static bool Constraint(List<int> cells_list)//为真就通过约束
        {
            //颜色顺序约束
            if (零件字典[cells_list[cells_list.Count - 1 - 1]].颜色 == "光耀蓝" || 零件字典[cells_list[cells_list.Count - 1 - 1]].颜色 == "宝石蓝" || 零件字典[cells_list[cells_list.Count - 1 - 1]].颜色 == "牛仔蓝" || 零件字典[cells_list[cells_list.Count - 1 - 1]].颜色 == "宝石红")
            {
                if (零件字典[cells_list[cells_list.Count - 1]].颜色 == "极地白" || 零件字典[cells_list[cells_list.Count - 1]].颜色 == "钻石白")
                {
                    return false;
                }
            }
            if (零件字典[cells_list[cells_list.Count - 1 - 1]].颜色 == "极地白")
            {
                if (零件字典[cells_list[cells_list.Count - 1]].颜色 == "曜岩黑" || 零件字典[cells_list[cells_list.Count - 1]].颜色 == "宇宙黑")
                {
                    return false;
                }
            }
            if (零件字典[cells_list[cells_list.Count - 1]].颜色 == "钻石白" && cells_list.Count >= 2)
            {
                if (零件字典[cells_list[cells_list.Count - 1 - 1]].颜色 != "极地白" || 零件字典[cells_list[cells_list.Count - 1 - 1]].颜色 != "钻石白")
                {
                    return false;
                }
            }
            //零件不能相连约束
            if (零件字典[cells_list[cells_list.Count - 1 - 1]].项目编号 * 零件字典[cells_list[cells_list.Count - 1]].项目编号 == 2)//门槛B C
            {
                return false;
            }
            if (零件字典[cells_list[cells_list.Count - 1 - 1]].项目编号 * 零件字典[cells_list[cells_list.Count - 1]].项目编号 == 3)// 门槛B ADAA
            {
                return false;
            }
            if (零件字典[cells_list[cells_list.Count - 1 - 1]].项目编号 * 零件字典[cells_list[cells_list.Count - 1]].项目编号 == 6)// 门槛C ADAA
            {
                return false;
            }
            if (零件字典[cells_list[cells_list.Count - 1 - 1]].项目编号 * 零件字典[cells_list[cells_list.Count - 1]].项目编号 == 4 || 零件字典[cells_list[cells_list.Count - 1 - 1]].项目编号 * 零件字典[cells_list[cells_list.Count - 1]].项目编号 == 8)// 门槛BC 雷达
            {
                return false;
            }
            return true;
        }
        //查询303前范围内上相同所有所属产品和滑撬数是否合适（支架约束）
        private static bool Same_supports_all(List<int> cells_list)//返回真通过测试
        {
            List<string> temp = new List<string>();//所属产品集合
            //找出所有所属产品集合
            for (int k = 0; k < 零件字典.Count; k++)
            {
                if (!temp.Contains(零件字典[k].所属产品))
                {
                    temp.Add(零件字典[k].所属产品);
                }
            }
            temp.RemoveAt(0);
            foreach (var temp_child in temp)
            {
                if (!Same_supports(cells_list, temp_child))
                {
                    return false;
                }
            }
            return true;
        }
        //查询303前范围内上一个所属产品滑撬数是否合适（支架约束）//可优化
        private static bool Same_supports(List<int> cells_list, string name)//返回真通过测试
        {
            int count = 0;//滑撬数
            int supports = 0;//支架数
            int temp = 0;
            if (cells_list.Count - 303 >= 0)
            {
                temp = cells_list.Count - 303;
            }
            for (int i = temp; i < cells_list.Count; i++)
            {
                if (零件字典[cells_list[i]].所属产品 == name)
                {
                    count++;
                    supports = 零件字典[cells_list[i]].支架数量;
                }
            }
            if (count <= supports / 6 + 1)
                return true;
            else
                return false;
        }
        //返回平均每圈换色次数（全局查找）
        private static double Change_color_count(List<int> cells_list)
        {
            double count_all = 0;
            for (int i = 0; i < 2424; i += 303)
            {
                int count = 0;
                for (int j = i; j < i + 303; j++)
                {
                    if (cells_list[j] == 0 && j != 2423)//最后一位不算换色
                        count++;
                }
                count_all += count;
                Console.WriteLine("第{0}圈换色{1}次", i / 303 + 1, count);
            }
            Console.WriteLine("平均每圈换色{0}次", count_all / 8.00);
            return count_all / 8.00;
        }
    }
}
