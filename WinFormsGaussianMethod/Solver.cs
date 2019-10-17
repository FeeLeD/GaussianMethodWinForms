using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsGaussianMethod
{
    class Solver
    {
        private double[,] Matrix;
        public Solver(double[,] matrix)
        {
            Matrix = matrix;
        }

        public double[] GetSolution()
        {
            double currentElement = 0.0;
            double nextElement = 0.0;

            for (int column = 0, rowValue = 0; column < 3; column++, rowValue++)
            {
                for (int row = rowValue; row < 4; row++)
                {
                    currentElement = Matrix[row, column];
                    if (currentElement == 0)
                        continue;

                    if (row == 3)
                        break;

                    for (int nextRow = row + 1; nextRow < 4; nextRow++)
                    {
                        nextElement = Matrix[nextRow, column];
                        if (nextElement == 0)
                            continue;

                        var temp = new double[5];
                        for (var i = 0; i < 5; i++)
                        {
                            temp[i] = Matrix[row, i];
                        }

                        for (var i = 0; i < 5; i++)
                        {
                            Matrix[row, i] *= nextElement;
                            Matrix[nextRow, i] *= currentElement;

                            Matrix[nextRow, i] -= Matrix[row, i];

                            Matrix[row, i] = temp[i];
                        }
                    }
                }
            }

            var numerator = 0.0;
            var denominator = 1.0;
            var answers = new double[4];
            var currentAnswer = 0.0;

            for (int i = 3; i >= 0; i--)
            {
                for (var j = 4; j >= 0; j--)
                {
                    if (j == i)
                        continue;

                    if (j == 4)
                        numerator += Matrix[i, j];
                    else
                        numerator -= Matrix[i, j];
                }
                denominator = Matrix[i, i];
                currentAnswer = numerator / denominator;

                if (i > 0)
                {
                    for (var previousRow = i - 1; previousRow >= 0; previousRow--)
                        Matrix[previousRow, i] *= currentAnswer;
                }

                answers[i] = currentAnswer;

                numerator = 0.0;
            }

            return answers;
        }
    }
}
