using NewYearLanding.DAL.RadarioSQL.Managers;

namespace NewYearLanding.DAL.RadarioSQL {
    public interface IModelFacade {
        CompanyManager CompanyManager { get; }
        EventManager EventManager { get; }
    }
}