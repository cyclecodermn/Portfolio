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
    public class MakeRepoADO : IMakeRepo

    {
        public List<BikeMakeTable> GetAll()
        {
            List<BikeMakeTable> Makes = new List<BikeMakeTable>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {
                SqlCommand cmd = new SqlCommand("MakesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
	
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        BikeMakeTable currentRow = new BikeMakeTable();
                        currentRow.BikeMakeId = (int)dr["BikeMakeId"];
                        currentRow.BikeMakeName = dr["BikeMakeName"].ToString();

                        Makes.Add(currentRow);
                    }
                }
            }
            return Makes;
        }


        public void Insert(BikeMakeTable NewMake)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakeInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@MakeId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                //               cmd.Parameters.AddWithValue("@MakeId", NewMake.BikeMakeId);
                //cmd.Parameters.AddWithValue("@BikeMakeId", NewMake.BikeMakeId);
                cmd.Parameters.AddWithValue("@BikeMakeName", NewMake.BikeMakeName);

                cn.Open();

                cmd.ExecuteNonQuery();

                //. = (int)param.Value;
                NewMake.BikeMakeId = (int)param.Value;
            }
        }


        public void Edit(BikeMakeTable Make)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakeUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BikeMakeId", Make.BikeMakeId);
                cmd.Parameters.AddWithValue("@BikeMakeName", Make.BikeMakeName);
                //cmd.Parameters.AddWithValue("@UserId", Make.UserId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static IEnumerable<BikeShortItem> CheckIfMakeIsUsed(BikeMakeTable MakeToDelete)
        {

            BikeSearchParameters parameters = new BikeSearchParameters();
            parameters.MakeModelOrYr = MakeToDelete.BikeMakeName;

            SearchAll BikeSearch = new SearchAll();

            IEnumerable<BikeShortItem> BikesWithMake = BikeSearch.Search2(parameters);

            return BikesWithMake;
        }

        public static void Delete(int MakeIdToDelete)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakeDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BikeMakeId", MakeIdToDelete);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static BikeMakeTable GetById(int frameId)
        {
            BikeMakeTable Make = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakeSelect", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BikeMakeId", frameId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Make = new BikeMakeTable();
                        Make.BikeMakeId = (int)dr["BikeMakeId"];
                        Make.BikeMakeName = (string)dr["BikeMakeName"];
                        Make.BikeMakeName = Make.BikeMakeName.TrimEnd();
                        // Make.UserId = dr["UserId"].ToString();

                    }
                }
            }

            return Make;
        }

    }
}
