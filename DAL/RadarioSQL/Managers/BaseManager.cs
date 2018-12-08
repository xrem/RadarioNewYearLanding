namespace NewYearLanding.DAL.RadarioSQL.Managers {
    public abstract class BaseManager {
        protected readonly SqlDbContext Requester;
        protected BaseManager(SqlDbContext requester) {
            Requester = requester;
        }
    }
}