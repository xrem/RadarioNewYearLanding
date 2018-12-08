using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewYearLanding.Model;

namespace NewYearLanding.DAL.Mongo.Abstractions {
    public interface ICompaniesRepository {
        Task<List<Company>> GetAllCompanies();
        Task<Company> GetCompanyByGuid(Guid guid);
        Task<Company> GetCompanyById(int companyId);
        Task<bool> Update(Company company);
        Task Insert(Company company);
    }
}