namespace Model
{
    /// <summary>
    /// интерфейс для движений
    /// </summary>
    public interface IMotion
    {
        /// <summary>
        /// Для начальной координаты
        /// </summary>
        int StartingCoordinate { get; set; }

        /// <summary>
        /// Для начальной скорости
        /// </summary>
        int StartingSpeed { get; set; }

        /// <summary>
        /// Для времени
        /// </summary>
        int Time { get; set; }

        /// <summary>
        /// Тип движения
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Метод подчета 
        /// </summary>
        /// <returns></returns>
        int CalculationFinishCoordinate();



    }
}
