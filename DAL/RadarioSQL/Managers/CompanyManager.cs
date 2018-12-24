using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewYearLanding.DAL.RadarioSQL.DTO;
using NewYearLanding.DAL.RadarioSQL.Entities;

namespace NewYearLanding.DAL.RadarioSQL.Managers {
    public class CompanyManager : BaseManager {
        public CompanyManager(SqlDbContext requester) : base(requester) { }

        public IEnumerable<Company> GetAllCompanies() {
            return Requester.Query<Company>("SELECT Id, Title, Logo FROM Company");
        }

        public List<AggregatedYearInfo> GetAggregatedYearInfoByCompany(int companyId) {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT t.EventId, count(1) AS TicketCount, SUM(t.Price - t.Discount) AS GrossWithDiscount FROM [Ticket] t");
            sb.AppendLine("JOIN dbo.UserOrder uo WITH (NOLOCK) ON t.UserOrderId = uo.Id");
            sb.AppendLine("JOIN [EventComplexLite] e WITH (NOLOCK) ON t.EventId = e.Id");
            sb.AppendLine("WHERE t.Deleted=0 AND uo.Deleted=0 AND uo.PaymentReceived=1");
            sb.AppendLine("AND DateAdd(hour, GmtOffset, PaymentDate)>'2018-01-01T00:00:00.000Z'");
            sb.AppendLine("AND DateAdd(hour, GmtOffset, BeginDate)>'2018-01-01T00:00:00.000Z'");
            sb.AppendLine("AND e.Deleted = 0 AND e.CompanyId = @companyId");
            sb.AppendLine("GROUP BY t.EventId");
            return Requester.Query<AggregatedYearInfo>(sb.ToString(), new {
                companyId = companyId
            }).ToList();
        }

        public List<string> GetTop3CompanyDistributionChannels(int companyId) {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT TOP 3 DistributionType FROM [Ticket] t");
            sb.AppendLine("JOIN dbo.UserOrder uo WITH (NOLOCK) ON t.UserOrderId = uo.Id");
            sb.AppendLine("JOIN [EventComplexLite] e WITH (NOLOCK) ON t.EventId = e.Id");
            sb.AppendLine("WHERE t.Deleted=0 AND uo.Deleted=0 AND uo.PaymentReceived=1");
            sb.AppendLine("AND DateAdd(hour, GmtOffset, PaymentDate)>'2018-01-01T00:00:00.000Z'");
            sb.AppendLine("AND DateAdd(hour, GmtOffset, BeginDate)>'2018-01-01T00:00:00.000Z'");
            sb.AppendLine("AND e.Deleted = 0 AND e.CompanyId = @companyId");
            sb.AppendLine("GROUP BY DistributionType");
            sb.AppendLine("HAVING SUM(t.Price - t.Discount) > 0");
            sb.AppendLine("ORDER BY SUM(t.Price - t.Discount) DESC");
            return Requester.Query<string>(sb.ToString(), new {
                companyId = companyId
            }).ToList();
        }

        public List<AggregatedDateStatistics> GetAggregatedDateStatistics(int companyId) {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT ");
            sb.AppendLine("    DATEPART(month, DateAdd(hour, GmtOffset, PaymentDate)) AS [Month], ");
            sb.AppendLine("    DATEPART(day, DateAdd(hour, GmtOffset, PaymentDate)) AS [Day], ");
            sb.AppendLine("    DATEPART(hour, DateAdd(hour, GmtOffset, PaymentDate)) AS [Hour],");
            sb.AppendLine("    Count(1) AS Weight");
            sb.AppendLine("FROM [Ticket] t");
            sb.AppendLine("JOIN dbo.UserOrder uo WITH (NOLOCK) ON t.UserOrderId = uo.Id");
            sb.AppendLine("JOIN [EventComplexLite] e WITH (NOLOCK) ON t.EventId = e.Id");
            sb.AppendLine("WHERE t.Deleted=0 AND uo.Deleted=0 AND uo.PaymentReceived=1");
            sb.AppendLine("AND DateAdd(hour, GmtOffset, BeginDate)>'2018-01-01T00:00:00.000Z'");
            sb.AppendLine("AND DateAdd(hour, GmtOffset, PaymentDate)>'2018-01-01T00:00:00.000Z'");
            sb.AppendLine("AND e.Deleted = 0 AND e.CompanyId = @companyId");
            sb.AppendLine("GROUP BY ");
            sb.AppendLine("    DATEPART(month, DateAdd(hour, GmtOffset, PaymentDate)),");
            sb.AppendLine("    DATEPART(day, DateAdd(hour, GmtOffset, PaymentDate)),");
            sb.AppendLine("    DATEPART(hour, DateAdd(hour, GmtOffset, PaymentDate))");
            sb.AppendLine("ORDER BY 1,2,3");
            return Requester.Query<AggregatedDateStatistics>(sb.ToString(), new {
                companyId = companyId
            }).ToList();
        }
    }
}