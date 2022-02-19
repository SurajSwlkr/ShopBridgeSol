using Dapper;
using Microsoft.AspNetCore.Hosting;
using ShopBridgeSol.Application_start;
using ShopBridgeSol.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace ShopBridgeSol.Repo
{
    public class ItemRepo
    {
        private bool disposed = false;
        GetConnection _GetConnection;

        public ItemRepo()
        {
            _GetConnection = GetConnection.Instance;
        }

        public async Task<IEnumerable<Inventory>> GetData()
        {
            try
            {
                StringBuilder sQuery = new StringBuilder("select ItemName,Description,ImageName,Price,ItemCategoryID AS ItemCategory from InventoryItemDetails ");
                using (IDbConnection connection = new SqlConnection(_GetConnection.GetConnectionString(DBtype.SqlServerDB.ToString())))
                {
                    var result = await connection.QueryAsync<Inventory>(sQuery.ToString(), null, null, null, CommandType.Text);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Inventory>> GetDatabyID(int ID)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder("select ItemName,Description,ImageName,Price,ItemCategoryID AS ItemCategory from InventoryItemDetails where ItemID=@ID");
                using (IDbConnection connection = new SqlConnection(_GetConnection.GetConnectionString(DBtype.SqlServerDB.ToString())))
                {
                 
                    var result = await connection.QueryAsync<Inventory>(sQuery.ToString(), new { ID = ID }, null, null, CommandType.Text);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResultOut> AddData(Inventory obj)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder("InsertInventoryData");
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@ItemName", obj.ItemName);
                dp.Add("@Description", obj.ItemDescription);
                dp.Add("@ImageName", obj.ItemImageName);
                dp.Add("@Price", obj.Price);
                dp.Add("@ItemCategoryID", obj.ItemCategory);
                dp.Add("@Createdby",obj.UserID);
               
                using (IDbConnection connection = new SqlConnection(_GetConnection.GetConnectionString(DBtype.SqlServerDB.ToString())))
                {
                    
                    var result = await connection.QueryAsync<ResultOut>(sQuery.ToString(), dp, null, null, CommandType.StoredProcedure);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<ResultOut> UpdateData(Inventory obj)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder("UpdateInventoryData");
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@ItemName", obj.ItemName);
                dp.Add("@Description", obj.ItemDescription);
                dp.Add("@ImageName", obj.ItemImageName);
                dp.Add("@Price", obj.Price);
                dp.Add("@ItemCategoryID", obj.ItemCategory);
                dp.Add("@Createdby", obj.UserID);
                dp.Add("@ItemID", obj.ItemID);

                using (IDbConnection connection = new SqlConnection(_GetConnection.GetConnectionString(DBtype.SqlServerDB.ToString())))
                {
                    var result = await connection.QueryAsync<ResultOut>(sQuery.ToString(), dp,null, null, CommandType.StoredProcedure);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> Delete(int id)
        {
            try
            {
                string sQuery = "Delete From InventoryItemDetails where ItemID=@ItemID";
                DynamicParameters objParameter = new DynamicParameters();
                objParameter.Add("@ItemID", id);

                using (IDbConnection connection = new SqlConnection(_GetConnection.GetConnectionString(DBtype.SqlServerDB.ToString())))
                {
                    var result = await connection.ExecuteAsync(sQuery, objParameter, commandType: CommandType.Text);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
