using System.Collections.Generic;
using NewYearLanding.DAL.RadarioSQL.Entities;

namespace NewYearLanding.DAL.RadarioSQL.Managers {
    public class CompanyManager : BaseManager {
        public CompanyManager(SqlDbContext requester) : base(requester) { }

        public IEnumerable<Company> GetAllCompanies() {
            return Requester.Query<Company>("SELECT Id, Title FROM Company");
        }
    }
}