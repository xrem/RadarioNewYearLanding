using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NewYearLanding.DAL.Mongo.Abstractions;
using NewYearLanding.Model;

namespace NewYearLanding.DAL.Mongo.Implementation {
    public class StatisticsRepository : IStatisticsRepository {
        private readonly MongoDbContext _context = null;

        public StatisticsRepository(IOptions<MongoDbConnectionSettings> settings) {
            _context = new MongoDbContext(settings);
        }

        public async Task<List<Statistics>> GetAllCompaniesStatistics() {
            return await _context.Statistics.Find(_ => true)
                                            .ToListAsync();
        }

        public async Task<Statistics> GetStatisticsByCompanyId(int companyId) {
            return await _context.Statistics.Find(f => f.CompanyId == companyId)
                                            .SingleOrDefaultAsync();
        }

        public async Task<bool> Update(Statistics stats) {
            var actionResult = await _context.Statistics
                .ReplaceOneAsync(f => f.InternalId.Equals(stats.InternalId),
                    stats, new UpdateOptions {
                        IsUpsert = true
                    });
            return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        }

        public async Task Insert(Statistics stats) {
            await _context.Statistics.InsertOneAsync(stats);
        }
    }
}