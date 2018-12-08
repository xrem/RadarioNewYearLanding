using Microsoft.Extensions.Configuration;
using NewYearLanding.DAL.RadarioSQL.Managers;

namespace NewYearLanding.DAL.RadarioSQL {
    public class ModelFacade : IModelFacade {
        private readonly SqlDbContext _db;
        private readonly object _synch = new object();

        public ModelFacade(IConfiguration config) {
            _db = new SqlDbContext(config["RadarioDb:ConnectionString"]);
        }

        private CompanyManager _companyManager;
        private EventManager _eventManager;

        public CompanyManager CompanyManager {
            get {
                lock (_synch) {
                    if (_companyManager == null) {
                        _companyManager = new CompanyManager(_db);
                    }
                }
                return _companyManager;
            }
        }

        public EventManager EventManager {
            get {
                lock (_synch) {
                    if (_eventManager == null) {
                        _eventManager = new EventManager(_db);
                    }
                }
                return _eventManager;
            }
        }
    }
}