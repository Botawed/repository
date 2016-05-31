using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// для колебательного движения
    /// </summary>
    public class VibrationalMotion : IMotion
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
                return "Vibrational";
            }
        }

        /// <summary>
        /// расчет конечной координаты
        /// </summary>
        /// <returns></returns>
        public int CalculationFinishCoordinate()
        {
            return StartingCoordinate + ((StartingSpeed * Time) % amplitude);
        }

        public int _amplitude;
        /// <summary>
        /// амплитуада движения       
        /// </summary>
        public int amplitude
        {
            get
            {
                return _amplitude;
            }
            set
            {
                if (value < 0)
                    _amplitude = 0;
                else _amplitude = value;
            }
        }
        /// <summary>
        /// значение переменных для колебательного движения
        /// </summary>
        /// <param name="x">начальная координата</param>
        /// <param name="v">начальная скорость</param>
        /// <param name="t">время</param>
        /// <param name="a">амплитуда</param>
        public VibrationalMotion(int x, int v, int t, int a)
        {
            StartingCoordinate = x;
            StartingSpeed = v;
            Time = t;
            amplitude = a;
        }
    }
}
