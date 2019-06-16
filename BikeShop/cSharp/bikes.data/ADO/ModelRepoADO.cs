using bikes.data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bikes.models.Tables;
using bikes.models.Queries;
using bikes.data.ADO.AdoUtils;

namespace bikes.data.ADO
{
    public class ModelRepoADO : IModelRepo

    {
        public List<BikeModelTable> GetAll()
        {
            List<BikeModelTable> Models = new List<BikeModelTable>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {
                SqlCommand cmd = new SqlCommand("ModelsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BikeModelTable currentRow = new BikeModelTable();
                        currentRow.BikeModelId = (int) dr["BikeModelId"];
                        currentRow.BikeModelName = dr["BikeModelName"].ToString();

                        Models.Add(currentRow);
                    }
                }
            }

            return Models;
        }

        public void Insert(BikeModelTable NewModel)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ModelId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

 //               cmd.Parameters.AddWithValue("@ModelId", NewModel.BikeModelId);
                cmd.Parameters.AddWithValue("@BikeModelName", NewModel.BikeModelName);

                cn.Open();

                cmd.ExecuteNonQuery();

                //. = (int)param.Value;
                NewModel.BikeModelId = (int)param.Value;
            }
        }

        public void Edit(BikeModelTable Model)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BikeModelId", Model.BikeModelId);
                cmd.Parameters.AddWithValue("@BikeModelName", Model.BikeModelName);
                //cmd.Parameters.AddWithValue("@UserId", Model.UserId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static IEnumerable<BikeShortItem> CheckIfModelIsUsed(BikeModelTable ModelToDelete)
        {

            BikeSearchParameters parameters = new BikeSearchParameters();
            parameters.MakeModelOrYr = ModelToDelete.BikeModelName;

            SearchAll BikeSearch = new SearchAll();

            IEnumerable<BikeShortItem> BikesWithModel = BikeSearch.Search2(parameters);

            return BikesWithModel;
        }

        public static void Delete(int ModelIdToDelete)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BikeModelId", ModelIdToDelete);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static BikeModelTable GetById(int modelId)
        {
            BikeModelTable Model = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelSelect", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BikeModelId", modelId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Model = new BikeModelTable();
                        Model.BikeModelId = (int)dr["BikeModelId"];
                        Model.BikeModelName = (string)dr["BikeModelName"];
                        // Model.UserId = dr["UserId"].ToString();

                    }
                }
            }

            return Model;
        }
    }
}
