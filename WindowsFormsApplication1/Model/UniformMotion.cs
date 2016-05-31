using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// для равномерного движения
    /// </summary>
    public class UniformMotion : IMotion
    {
        public int _startingcoordinata;
        /// <summary>
        /// для начальной  координаты
        /// </summary>
        public int StartingCoordinate
        {
            get
            {
                return _startingcoordinata;
            }
            set
            {
                _startingcoordinata = value;
            }
        }

        public int _startingspeed;
        /// <summary>
        /// для начальной скорости
        /// </summary>
        public int StartingSpeed
        {
            get
            {
                return _startingspeed;
            }
            set
            {
                _startingspeed = value;
            }
        }
        public int _time;
        /// <summary>
        /// для времени движения
        /// </summary>
        public int Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                else _time = value;
            }
        }

        public string Type
        {
            get
            {
                return "Uniform";
            }
        }

        /// <summary>
        /// расчет конечной координаты
        /// </summary>
        public int CalculationFinishCoordinate()
        {
            return StartingCoordinate + StartingSpeed * Time;
        }
        /// <summary>
        /// значение переменных для равномерного движения
        /// </summary>
        /// <param name="x">начальная координата</param>
        /// <param name="v">начальная скорость</param>
        /// <param name="t">время</param>
        public UniformMotion(int x, int v, int t)
        {
            StartingCoordinate = x;
            StartingSpeed = v;
            Time = t;
        }
    }
}