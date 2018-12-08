using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewYearLanding.DAL.Mongo.Abstractions;
using NewYearLanding.DAL.RadarioSQL;
using NewYearLanding.Model;

namespace NewYearLanding.Controllers {
    [Authorize]
    [Route("[controller]")]
    public class SyncController : BaseController {
        private readonly ICompaniesRepository _companiesRepository;

        public SyncController(ICompaniesRepository companiesRepository,
                              IModelFacade modelFacade) : base(modelFacade) {
            _companiesRepository = companiesRepository;
        }

        [HttpGet]
        [Route("companies")]
        public async Task<JsonResult> SyncCompanies() {
            var knownCompanies = await _companiesRepository.GetAllCompanies();
            var radarioCompanies = mf.CompanyManager.GetAllCompanies();
            var updateCount = 0;
            var insertCount = 0;
            foreach (var radarioCompany in radarioCompanies) {
                var entity = knownCompanies.SingleOrDefault(x => x.CompanyId == radarioCompany.Id);
                if (entity == null) {
                    entity = new Company() {
                        CompanyId = radarioCompany.Id,
                        CompanyTitle = radarioCompany.Title,
                        PublicGuid = Guid.NewGuid(),
                        FullAccessGuid = Guid.NewGuid()
                    };
                    await _companiesRepository.Insert(entity);
                    insertCount++;
                } else {
                    if (!radarioCompany.Title.Equals(entity.CompanyTitle,
                        StringComparison.InvariantCultureIgnoreCase)) {
                        entity.CompanyTitle = radarioCompany.Title;
                        await _companiesRepository.Update(entity);
                        updateCount++;
                    }
                }
            }
            return Json(new {
                Updated = updateCount,
                CreatedNew = insertCount
            }, JsonSerializerSettings);
        }
    }
}