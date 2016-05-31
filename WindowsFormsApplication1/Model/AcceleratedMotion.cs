using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// равноускоренное движение
    /// </summary>
    public class AcceleratedMotion : IMotion
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
        public int _acceleration;

        /// <summary>
        /// ускорение
        /// </summary>
        public int Acceleration
        {
            get
            {
                return _acceleration;
            }
            set
            {
                _acceleration = value;
            }
        }
        /// <summary>
        /// расчет конечной координаты
        /// </summary>
        public int CalculationFinishCoordinate()
        {
            return StartingCoordinate + StartingSpeed * Time + Acceleration * Time * Time / 2;
        }

        public string Type
        {
            get
            {
                return "Accelerated";
            }
        }

        /// <summary>
        /// значение переменных для равноускоренного движения
        /// </summary>
        /// <param name="x">начальная координата</param>
        /// <param name="v">начальная скорость</param>
        /// <param name="t">время</param>
        /// <param name="a">ускорение</param>
        public AcceleratedMotion(int x, int v, int t, int a)
        {
            StartingCoordinate = x;
            StartingSpeed = v;
            Time = t;
            Acceleration = a;
        }
    }
}

