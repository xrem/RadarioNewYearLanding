using System.Collections.Generic;
using System.Text;

namespace NewYearLanding.DAL.RadarioSQL.Managers {
    public class EventManager : BaseManager {
        public EventManager(SqlDbContext requester) : base(requester) { }

        public class AggregatedEventCountByCompany {
            public int CompanyId { get; set; }
            public int EventCount { get; set; }
        }

        public IEnumerable<AggregatedEventCountByCompany> GetAggregatedEventCountByCompany() {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT CompanyId, Count(1) as EventCount");
            sb.AppendLine("FROM [Radario].[dbo].[EventComplexLite]");
            sb.AppendLine("WHERE DateAdd(hour, GmtOffset, BeginDate)>'2018-01-01T00:00:00.000Z'");
            sb.AppendLine("GROUP BY CompanyId");
            return Requester.Query<AggregatedEventCountByCompany>(sb.ToString());
        }
    }
}