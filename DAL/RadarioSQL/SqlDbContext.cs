using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace NewYearLanding.DAL.RadarioSQL {
    public class SqlDbContext {
        private readonly IDbConnection _db;
        public SqlDbContext(string connectionString) {
            _db = new SqlConnection(connectionString);
        }

        public IEnumerable<T> Query<T>(string query, object parameters = null) {
            return _db.Query<T>(query, parameters);
        }

        public void Query(string query, object parameters = null) {
            _db.Query(query, parameters);
        }
    }
}