using System;
using System.Collections.Generic;

namespace NewYearLanding.Model {
    public class DataViewModel {
        public int CompanyId { get; set; }
        public Guid PublicGuid { get; set; }
        public Guid FullAccessGuid { get; set; }
        public string Hostname { get; set; }
        public string Logo { get; set; }
        public List<string> EventsCovers { get; set; }
        public StatisticsYear Year { get; set; }
        public string BestMonth { get; set; }
        public string BestTime { get; set; }
        public string BestDay { get; set; }
        public string BestChannel { get; set; }
    }
}