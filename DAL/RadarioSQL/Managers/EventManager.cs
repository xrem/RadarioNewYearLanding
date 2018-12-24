using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewYearLanding.DAL.RadarioSQL.DTO;

namespace NewYearLanding.DAL.RadarioSQL.Managers {
    public class EventManager : BaseManager {
        public EventManager(SqlDbContext requester) : base(requester) { }

        public List<ImagesByTitle> GetImagesByEventIds(IEnumerable<int> eventIds) {
            var sb = new StringBuilder();
            sb.AppendLine("SELECT Title, Images FROM [Event]");
            sb.AppendLine("WHERE Id IN @ids");
            sb.AppendLine("AND Images IS NOT NULL AND Len(Images)>4");
            return Requester.Query<ImagesByTitle>(sb.ToString(), new {
                ids = eventIds
            }).ToList();
        }
    }
}