using System;

namespace ObrabotcaURAN
{
    public class Obrabotca
    {
        public static bool MetodYuKO(double koef, int maxTime, int maxAmp, int firstTime)
        {
            double k = (double)maxAmp / (double)(maxTime - firstTime);
            if (k <= koef)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Возвращает массывы времение максимума, амплитуду максимума для каждого канала платы.
        /// </summary>
        /// <param name="data"> двухмергый массив исходных данных</param>
        /// <param name="maxTime"></param>
        /// <param name="maxAmp"></param>
        /// <param name="NullLine"></param>
        public static void AmpAndTime(int[,] data, double[] NullLine, out int[] maxTime, out int[] maxAmp)
        {
            maxAmp = new int[12];
            maxTime = new int[12];
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < data.Length / 12; j++)
                {
                    if (data[i, j] - NullLine[i] > maxAmp[i] )
                    {
                        maxAmp[i] = data[i, j] - (int)NullLine[i];
                        maxTime[i] = j;
                    }
                }
            }
        }
        public static bool Dneutron(int[] maxAmp, int porog, out int d)
        {
            int count = 0;
            d = 1;
            for (int i = 0; i < maxAmp.Length; i++)
            {
                if (maxAmp[i] > porog)
                {
                    count++;
                    d = i + 1;
                }
            }
            if (count == 1)
            {
                return true;
            }
            return false;
        }
        public static int[] FirstTme(int[] maxTime, int[] maxAmp, int porog, int[,] data, double[] NullLine, ref int[] polMaxTime)
        {
            int[] firstTime  = new int[12];
             
            for (int i = 0; i < 12; i++)
            {
               int mTime = maxTime[i];
                for (int j = mTime - 1; j > -1; j--)
                {
                    if (data[i, j] > NullLine[i] +3)
                    {
                        firstTime[i] = j;
                    }
                    if (data[i, j] >= ((maxAmp[i]/2)+NullLine[i]))
                    {
                        polMaxTime[i] = j;
                    }
                }
            }


            return firstTime;
        }
    }
}
