using System.Collections.Generic;
using System.Threading.Tasks;
using NewYearLanding.Model;

namespace NewYearLanding.DAL.Mongo.Abstractions {
    public interface IStatisticsRepository {
        Task<List<Statistics>> GetAllCompaniesStatistics();
        Task<Statistics> GetStatisticsByCompanyId(int companyId);
        Task<bool> Update(Statistics stats);
        Task Insert(Statistics stats);
    }
}