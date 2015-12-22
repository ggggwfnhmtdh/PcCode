#include<stdio.h>

void Display(double *dat, double *Answer, double *SquarePoor, int rows, int cols);
int LinearRegression(double *data, int rows, double *a, double *b, double *SquarePoor);

double data1[51][2] = {
//    X      Y
    {-250,0.263},
{-240,0.317},
{-230,0.377},
{-220,0.432},
{-210,0.485},
{-200,0.543},
{-190,0.599},
{-180,0.656},
{-170,0.714},
{-160,0.770},
{-150,0.825},
{-140,0.882},
{-130,0.938},
{-120,0.995},
{-110,1.050},
{-100,1.107},
{-90,1.163},
{-80,1.216},
{-70,1.274},
{-60,1.325},
{-50,1.386},
{-40,1.440},
{-30,1.497},
{-20,1.552},
{-10,1.605},
{0	,1.656},
{10	,1.720},
{20	,1.778},
{30	,1.828},
{40	,1.890},
{50	,1.940},
{60	,1.990},
{70	,2.050},
{80	,2.100},
{90	,2.160},
{100,2.220},
{110,2.270},
{120,2.330},
{130,2.380},
{140,2.440},
{150,2.500},
{160,2.550},
{170,2.610},
{180,2.670},
{190,2.720},
{200,2.780},
{210,2.840},
{220,2.890},
{230,2.940},
{240,3.000},
{250,3.050}
};

main()
{
    double Answer[2], SquarePoor[4];
    if (LinearRegression((double*)data1, 12, &Answer[0], &Answer[1], SquarePoor) == 0)
        Display((double*)data1, Answer, SquarePoor, 12, 2);
 
}

int LinearRegression(double *data, int rows, double *a, double *b, double *SquarePoor)
{
    int m;
    double *p, Lxx = 0.0, Lxy = 0.0, xa = 0.0, ya = 0.0;
    if (data == 0 || a == 0 || b == 0 || rows < 1)
        return -1;
    for (p = data, m = 0; m < rows; m ++)
    {
        xa += *p ++;
        ya += *p ++;
    }
    xa /= rows;                                     // X平均值
    ya /= rows;                                     // Y平均值
    for (p = data, m = 0; m < rows; m ++, p += 2)
    {
        Lxx += ((*p - xa) * (*p - xa));             // Lxx = Sum((X - Xa)平方)
        Lxy += ((*p - xa) * (*(p + 1) - ya));       // Lxy = Sum((X - Xa)(Y - Ya))
    }
    *b = Lxy / Lxx;                                 // b = Lxy / Lxx
    *a = ya - *b * xa;                              // a = Ya - b*Xa
    if (SquarePoor == 0)
        return 0;
    // 方差分析
    SquarePoor[0] = SquarePoor[1] = 0.0;
    for (p = data, m = 0; m < rows; m ++, p ++)
    {
        Lxy = *a + *b * *p ++;
        SquarePoor[0] += ((Lxy - ya) * (Lxy - ya)); // U(回归平方和)
        SquarePoor[1] += ((*p - Lxy) * (*p - Lxy)); // Q(剩余平方和)
    }
    SquarePoor[2] = SquarePoor[0];                  // 回归方差
    SquarePoor[3] = SquarePoor[1] / (rows - 2);     // 剩余方差
    return 0;
}

void Display(double *dat, double *Answer, double *SquarePoor, int rows, int cols)
{
    double v, *p;
    int i, j;
    printf("回归方程式:    Y = %.5lf", Answer[0]);
    for (i = 1; i < cols; i ++)
        printf(" + %.5lf*X%d", Answer[i], i);
    printf(" ");
    printf("回归显著性检验: ");
    printf("回归平方和：%12.4lf  回归方差：%12.4lf ", SquarePoor[0], SquarePoor[2]);
    printf("剩余平方和：%12.4lf  剩余方差：%12.4lf ", SquarePoor[1], SquarePoor[3]);
    printf("离差平方和：%12.4lf  标准误差：%12.4lf ", SquarePoor[0] + SquarePoor[1], sqrt(SquarePoor[3]));
    printf("F   检  验：%12.4lf  相关系数：%12.4lf ", SquarePoor[2] /SquarePoor[3],
           sqrt(SquarePoor[0] / (SquarePoor[0] + SquarePoor[1])));
    printf("剩余分析: ");
    printf("      观察值      估计值      剩余值    剩余平方 ");
    for (i = 0, p = dat; i < rows; i ++, p ++)
    {
        v = Answer[0];
        for (j = 1; j < cols; j ++, p ++)
            v += *p * Answer[j];
        printf("%12.2lf%12.2lf%12.2lf%12.2lf ", *p, v, *p - v, (*p - v) * (*p - v));
    }
    system("pause");
}