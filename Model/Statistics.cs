using System.Collections.Generic;

namespace NewYearLanding.Model {
    public class StatisticsYear {
        /// <summary>
        /// Cобытий проведено за год
        /// </summary>
        public int Events { get; set; }

        /// <summary>
        /// Билетов продано за год
        /// </summary>
        public int Tickets { get; set; }

        /// <summary>
        /// Денег заработано за год
        /// </summary>
        public double Money { get; set; }
    }

    public class Statistics {
        /// <summary>
        /// Именование хоста
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// Путь до логотипа хоста
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// Нужно 8 разных обложек событий у хоста (лучше всего топовых событий)
        /// </summary>
        public List<string> EventsCovers { get; set; }

        public StatisticsYear Year { get; set; }

        /// <summary>
        /// Cамый прибыльный месяц в году
        /// </summary>
        public string BestMonth { get; set; }

        /// <summary>
        /// Самое продаваемое время в году
        /// </summary>
        public string BestTime { get; set; }

        /// <summary>
        /// Самый продаваемый день в году
        /// </summary>
        public string BestDay { get; set; }

        /// <summary>
        /// Самый популярный канал продаж
        /// </summary>
        public string BestChannel { get; set; }
    }
}