using Dapper;
using ShopBridgeSol.Application_start;
using ShopBridgeSol.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeSol.Repo
{
    public class LoginRepo
    {
        private bool disposed = false;
        GetConnection _GetConnection;
        JwtTokanAuth objJwtTokanAuth;
        public LoginRepo()
        {
            _GetConnection = GetConnection.Instance;
            objJwtTokanAuth = JwtTokanAuth.Instance;
        }

        public async Task<IEnumerable<Login>> GetuserDetails(Login obj)
        {
            try
            {
                string jwtTokan = objJwtTokanAuth.GenerateJSONWebToken(obj);
                StringBuilder sQuery = new StringBuilder("select *,'"+jwtTokan+ "' as jwtTokan from usermaster");
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@UserName", obj.UserName);
                dp.Add("@Password", obj.Password);
                using (IDbConnection connection = new SqlConnection(_GetConnection.GetConnectionString(DBtype.SqlServerDB.ToString())))
                {
                    
                    var result = await connection.QueryAsync<Login>(sQuery.ToString(), dp, null, null, CommandType.Text);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         
    }
}
