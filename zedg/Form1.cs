using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace zedg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        const int _n = 30; //кол-во узлов
        double[] _node = new double[_n]; //массив узлов
        double Func(double x) //исходная функция
        {
            return (x * x) / 2 * Math.Sin(x);
        }
        void Fill(double min, double dx) //присваивает узлам значения
        {
            double x = min;
            for (int i = 0; i < _n; i++)
            {
                _node[i] = x;
                x += dx;
            }
        }
        double L1(double x, double[] Node_1) //интерполяционный полином
        {
            double Polinom = 0;
            for (int i = 0; i < _n; i++)
            {
                double p = 1;
                for (int j = 0; j < _n; j++)
                    if (i != j)
                    {
                        p = p * (x - Node_1[j]) / (Node_1[i] - Node_1[j]);
                    }

                Polinom += Func(Node_1[i]) * p; 
            }
            return Polinom;
        }

        public void drow()
        {
            GraphPane pane = zedGraphControl1.GraphPane;

             const int N = 4;

            double[] X = new double[N];
            double[] Y = new double[N];

            double a = -10;
            double b = 10;
            double h = (b - a) / (_n - 1);

            Fill(a, h);

            for (int i = 0; i < N; ++i)
            {
                X[i] = i * 0.25;
                Y[i] = Func(X[i]);
            }


            pane.XAxis.Title.Text = "x";
            pane.YAxis.Title.Text = "y";
            pane.Title.Text = "(x * x) / 2 * Sin(x)";
            pane.CurveList.Clear();
            PointPairList list = new PointPairList();
            PointPairList list2 = new PointPairList();

            for (double x = a; x <= b; x += h)
            {

                list.Add(x, Func(x));
            }

            for (double x = a; x <= b; x += h)
            {
                list2.Add(x, L1(x, _node));
            }



            LineItem myCurve = pane.AddCurve("(x * x) / 2 * Sin(x)", list, Color.Red, SymbolType.None);
            LineItem myCurve2 = pane.AddCurve("Largange(f)", list2, Color.Blue, SymbolType.Circle);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            drow();
        }

        private void zedGraph_Load(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}