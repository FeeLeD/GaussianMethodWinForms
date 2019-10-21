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
            SortMatrix();
        }

        private void SortMatrix()
        {
            var tempMatrix = new double[4, 5]; //временная матрица
            var tempRow = new double[5]; //временная строка
            for (int j = 0, rowValue = 0, end = 3, start = 0; j < 3; j++, rowValue++, start++)
            {
                for (var i = rowValue; i < 4; i++)
                {
                    if (Matrix[i, j] == 0)
                    {
                        for (var elem = 0; elem < 5; elem++)
                            tempMatrix[end, elem] = Matrix[i, elem];
                        end--;
                    }
                    else
                    {
                        for (var elem = 0; elem < 5; elem++)
                            tempMatrix[start, elem] = Matrix[i, elem];
                        start++;
                    }
                }
                start = j;
                end = 3;
                Matrix = (double[,])tempMatrix.Clone();
            }
        }

        public double[] GetSolution()
        {
            double currentElement = 0.0; //заглавный элемент текущей строки
            double nextElement = 0.0; //заглавный элемент следующей строки

            var tempCurrentRow = new double[5]; //временное хранение значений текущей строки

            /*проход по трём столбцам для получения ступенчатого вида матрицы*/
            for (int column = 0, rowValue = 0; column < 3; column++, rowValue++)
            {
                for (int row = rowValue; row < 4; row++) //поиск строки с заглавным элементом != 0
                {
                    currentElement = Matrix[row, column];
                    if (currentElement == 0)
                        continue;

                    if (row == 3)
                        break;

                    for (int nextRow = row + 1; nextRow < 4; nextRow++) //поиск следующих строк с заглавным элементом != 0
                    {
                        nextElement = Matrix[nextRow, column];
                        if (nextElement == 0)
                            continue;

                        for (var i = 0; i < 5; i++) //заполнение массива элементами текущей строки
                        {
                            tempCurrentRow[i] = Matrix[row, i];
                        }

                        for (var i = 0; i < 5; i++) //5 столбцов
                        {
                            /*уравнивание двух заглавных коэффициентов
                              путём перемножения*/
                            Matrix[row, i] *= nextElement; 
                            Matrix[nextRow, i] *= currentElement;

                            //вычитание перемноженных коэффициентов
                            Matrix[nextRow, i] -= Matrix[row, i];

                            //возвращение текущей строки к изначальному виду
                            Matrix[row, i] = tempCurrentRow[i];
                        }
                    }
                }
            }

            var numerator = 0.0; //числитель
            var denominator = 1.0; //знаменатель
            var answers = new double[4]; //массив для хранения ответов
            var currentAnswer = 0.0; //ответ для текущей строки

            /*получение числителя и знаменателя*/
            for (int i = 3; i >= 0; i--) //начало с 4 строки
            {
                for (var j = 4; j >= 0; j--) //начало с 5 столбца
                {
                    if (j == i) //коэффициент, стоящий около x, который нужно вычислить
                        continue;

                    if (j == 4) //то, что после знака равно, остаётся без изменений
                        numerator += Matrix[i, j];
                    else //то, что перед знаком равно, переносится со сменой знака
                        numerator -= Matrix[i, j];
                }
                denominator = Matrix[i, i]; //знаменатель = коэффициент, стоящий около незивестного x
                currentAnswer = numerator / denominator; //x = вычисленный числитель / вычисленный знаменатель

                if (i > 0) //если не первая строка
                {
                    /*для всех предыдущих строк умножить коэффициент по данному столбцу на найдённый x*/
                    for (var previousRow = i - 1; previousRow >= 0; previousRow--) 
                        Matrix[previousRow, i] *= currentAnswer;
                }

                answers[i] = currentAnswer; //записать найдённый x в массив ответов с соответствующим индексом

                numerator = 0.0; //обнулить числитель
            }

            return answers; 
        }
    }
}
