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
            ShowAnswers(answers);
        }

        private void FillTheMatrix()
        {
            var coefficient = new StringBuilder();
            var number = 0.0;
            for (var i = 0; i < 4; i++) //4 строки
            {
                coefficient.Append("x");
                coefficient.Append(i.ToString());
                for (var j = 0; j < 5; j++) //5 столбцов
                {
                    coefficient.Append(j.ToString());
                    if (!double.TryParse(this.Controls[coefficient.ToString()].Text.Replace('.', ','), out number))
                        number = 0.0;
                    Matrix[i, j] = number;
                    coefficient.Remove(2, 1); //очистка номера столбца
                }
                coefficient.Clear();
            }
        }

        private void ShowAnswers(double[] answers)
        {
            x1.Text = Math.Round(answers[0], 5).ToString();
            x2.Text = Math.Round(answers[1], 5).ToString();
            x3.Text = Math.Round(answers[2], 5).ToString();
            x4.Text = Math.Round(answers[3], 5).ToString();
        }
    }
}
