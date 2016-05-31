using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Model
{
    [TestFixture]
    public class AccelerationTests
    {
        /// <summary>
        /// Тестирование свойства StartCoordinate 
        /// </summary>
        /// <param name="startingcoordinata">начальная координата</param>
        [Test]
        [TestCase(0, TestName = "Тестирование StartingCoordinate при присваивании 0.")]
        [TestCase(int.MinValue, TestName = "Тестирование StartingCoordinate при присваивании int.MinValue.")]
        [TestCase(int.MaxValue, TestName = "Тестирование StartingCoordinate при присваивании int.MaxValue.")]
        public void StartingCoordinateTest(int startingcoordinata)
        {
            var accelerated = new AcceleratedMotion(0, 0, 0, 0);
            accelerated.StartingCoordinate = startingcoordinata;
        }

        /// <summary>
        /// Тестирование свойства StartingSpeed
        /// </summary>
        /// <param name="startingspeed">начальная скорость</param>
        [Test]
        [TestCase(0, TestName = "Тестирование StartingSpeed при присваивании 0.")]
        [TestCase(int.MinValue, TestName = "Тестирование StartingSpeed при присваивании int.MinValue.")]
        [TestCase(int.MaxValue, TestName = "Тестирование StartingSpeed при присваивании int.MaxValue.")]
        public void StartingSpeedTest(int startingspeed)
        {
            var accelerated = new AcceleratedMotion(0, 0, 0, 0);
            accelerated.StartingCoordinate = startingspeed;
        }

        /// <summary>
        /// Тестирование свойства Acceleration
        /// </summary>
        /// <param name="time">время движения</param>
        [Test]
        [TestCase(0, TestName = "Тестирование Time при присваивании 0.")]
        [TestCase(int.MaxValue, TestName = "Тестирование Time при присваивании int.MaxValue.")]
        public void TimeTest(int time)
        {
            var accelerated = new AcceleratedMotion(0, 0, 0, 0);
            accelerated.Time = time;
        }

        /// <summary>
        /// Негативный тест Time
        /// </summary>
        /// <param name="time">время движения</param>
        [Test]
        [TestCase(int.MinValue, TestName = "Тестирование Time при присваивании int.minValue.")]
        [TestCase(-10, TestName = "Тестирование Time при присваивании -10.")]
        [TestCase(-1, TestName = "Тестирование Time при присваивании -1.")]
        public void TimeTestNegative(int time)
        {
            var accelerated = new AcceleratedMotion(0, 0, 0, 0);
            Assert.Throws(typeof(ArgumentException), () => { accelerated.Time = time; });   //!!!//
        }

        /// <summary>
        /// Тестирование свойства acceleration
        /// </summary>
        /// <param name="time">ускорение</param>
        [Test]
        [TestCase(0, TestName = "Тестирование acceleration при присваивании 0.")]
        [TestCase(int.MaxValue, TestName = "Тестирование acceleration при присваивании int.MaxValue.")]
        [TestCase(int.MinValue, TestName = "Тестирование acceleration при присваивании int.MinValue.")]
        public void accelerationTest(int acceleration)
        {
            var accelerated = new AcceleratedMotion(0, 0, 0, 0);
            accelerated.Acceleration = acceleration;
        }

        /// <summary>
        /// Тестирование метода CalculationFinishCoordinate
        /// </summary>
        /// <param name="StartingCoordinate">начальная координата  </param>
        /// <param name="StartingSpeed">начальная скорость</param>
        /// <param name="time">время движения</param>
        [Test]
        [TestCase(1, 2, 3, 4, TestName = "Тестирование метода CalculationFinishCoordinate при присваивании 1, 2, 3 в аргументы")]
        [TestCase(-1, 2, int.MaxValue, 4, TestName = "Тестирование метода CalculationFinishCoordinate при присваивании -1, 2, int.MaxValue в аргументы")]
        [TestCase(int.MaxValue, 2, int.MaxValue, int.MaxValue, TestName = "Тестирование метода CalculationFinishCoordinate при присваивании int.MaxValue, 2, int.MaxValue в аргументы")]
        [TestCase(1, int.MinValue, 3, int.MaxValue, TestName = "Тестирование метода CalculationFinishCoordinate при присваивании 1, int.MinValue, 3 в аргументы")]
        [TestCase(int.MaxValue, -2, 0, int.MinValue, TestName = "Тестирование метода CalculationFinishCoordinate при присваивании int.MaxValue, -2, 0 в аргументы")]
        public void CalculationFinishCoordinateTest(int StartingCoordinate, int StartingSpeed, int Time, int Acceleration)
        {
            var accelerated = new AcceleratedMotion(StartingCoordinate, StartingSpeed, Time, Acceleration);
            accelerated.CalculationFinishCoordinate();
        }

        /// <summary>
        /// Негативный тест метода CalculationFinishCoordinate
        /// </summary>
        /// <param name="startCoordinate">начальная координата</param>
        /// <param name="startSpeed">начальная скорость</param>
        /// <param name="time">время</param>
        [Test]
        [TestCase(1, 2, -3, 0, TestName = "Тестирование метода CalculationFinishCoordinate при присваивании 1, 2, -3 в аргументы")]
        [TestCase(-1, -2, -3, int.MinValue, TestName = "Тестирование метода CalculationFinishCoordinate при присваивании -1, -2, -3 в аргументы")]
        [TestCase(int.MinValue, -2, -3, -1, TestName = "Тестирование метода CalculationFinishCoordinate при присваивании int.MinValue, -2, -3 в аргументы")]
        public void CalculationFinishCoordinateTestNegative(int startCoordinate, int startSpeed, int time, int acceleration)
        {
            NUnit.Framework.Assert.Throws(typeof(ArgumentException), () =>
            {
                var accelerated = new AcceleratedMotion(startCoordinate, startSpeed, time, acceleration);
                accelerated.CalculationFinishCoordinate();
            });

        }
    }
}
