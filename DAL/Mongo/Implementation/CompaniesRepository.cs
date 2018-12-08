using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NewYearLanding.DAL.Mongo.Abstractions;
using NewYearLanding.Model;

namespace NewYearLanding.DAL.Mongo.Implementation {
    public class CompaniesRepository : ICompaniesRepository {
        private readonly MongoDbContext _context = null;

        public CompaniesRepository(IOptions<MongoDbConnectionSettings> settings) {
            _context = new MongoDbContext(settings);
        }

        public async Task<List<Company>> GetAllCompanies() {
            return await _context.Companies.Find(_ => true).ToListAsync();
        }

        public async Task<Company> GetCompanyByGuid(Guid guid) {
            var filters = Builders<Company>.Filter;
            var filter = filters.Or(
                filters.Eq(f => f.PublicGuid, guid),
                filters.Eq(f => f.FullAccessGuid, guid)
            );
            var companies = await _context.Companies.FindAsync(filter);
            return await companies.SingleOrDefaultAsync();
        }

        public async Task<Company> GetCompanyById(int companyId) {
            return await _context.Companies.Find(f => f.CompanyId == companyId)
                                           .SingleOrDefaultAsync();
        }

        public async Task<bool> Update(Company company) {
            var actionResult = await _context.Companies
                .ReplaceOneAsync(f => f.InternalId.Equals(company.InternalId),
                company, new UpdateOptions {
                    IsUpsert = true
                });
            return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        }

        public async Task Insert(Company company) {
            await _context.Companies.InsertOneAsync(company);
        }
    }
}