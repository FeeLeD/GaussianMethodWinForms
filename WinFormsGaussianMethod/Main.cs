using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsGaussianMethod
{
    public partial class Main : Form
    {
        private double[,] Matrix;
        public Main()
        {
            Matrix = new double[4, 5];
            InitializeComponent();
        }

        private void solveBtn_Click(object sender, EventArgs e)
        {
            FillTheMatrix();
            var solver = new Solver(Matrix);
            var answers = solver.GetSolution();
            x1.Text = answers[0].ToString();
            x2.Text = answers[1].ToString();
            x3.Text = answers[2].ToString();
            x4.Text = answers[3].ToString();
        }

        private void FillTheMatrix()
        {
            var coefficient = new StringBuilder();
            var number = 0.0;
            for (var i = 0; i < 4; i++)
            {
                coefficient.Append("x");
                coefficient.Append(i.ToString());
                for (var j = 0; j < 5; j++)
                {
                    coefficient.Append(j.ToString());
                    if (!double.TryParse(this.Controls[coefficient.ToString()].Text.Replace('.', ','), out number))
                        number = 0.0;
                    Matrix[i, j] = number;
                    coefficient.Remove(2, 1);
                }
                coefficient.Clear();
            }
        }
    }
}
