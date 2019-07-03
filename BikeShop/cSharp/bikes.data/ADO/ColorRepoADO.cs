using bikes.data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bikes.models.Tables;
using bikes.data.ADO.AdoUtils;
using bikes.models.Queries;

namespace bikes.data.ADO
{
    public class ColorRepoADO : IColorRepo

    {
        public List<BikeColorTable> GetAll()
        {
            List<BikeColorTable> Colors = new List<BikeColorTable>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {
                SqlCommand cmd = new SqlCommand("ColorsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BikeColorTable currentRow = new BikeColorTable();
                        currentRow.BikeColorId = (int) dr["BikeColorId"];
                        currentRow.BikeColorName = dr["BikeColor"].ToString();

                        Colors.Add(currentRow);
                    }
                }
            }

            return Colors;
        }

        public void Insert(BikeColorTable NewColor)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ColorInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ColorId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

 //               cmd.Parameters.AddWithValue("@ColorId", NewColor.BikeColorId);
                cmd.Parameters.AddWithValue("@BikeColorName", NewColor.BikeColorName);

                cn.Open();

                cmd.ExecuteNonQuery();

                //. = (int)param.Value;
                NewColor.BikeColorId = (int)param.Value;
            }
        }

        public void Edit(BikeColorTable BikeColor)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ColorUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BikeColorId", BikeColor.BikeColorId);
                cmd.Parameters.AddWithValue("@BikeColorName", BikeColor.BikeColorName);
                //cmd.Parameters.AddWithValue("@UserId", BikeColor.UserId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static IEnumerable<BikeShortItem> CheckIfColorIsUsed(BikeColorTable ColorToDelete)
        {

            BikeSearchParameters parameters = new BikeSearchParameters();
            parameters.MakeModelOrYr = ColorToDelete.BikeColorName;

            SearchAll BikeSearch = new SearchAll();

            IEnumerable<BikeShortItem> BikesWithColor = BikeSearch.Search2(parameters);

            return BikesWithColor;
        }

        public static void Delete(int ColorIdToDelete)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ColorDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BikeColorId", ColorIdToDelete);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static BikeColorTable GetById(int frameId)
        {
            BikeColorTable BikeColor = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ColorSelect", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BikeColorId", frameId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        BikeColor = new BikeColorTable();
                        BikeColor.BikeColorId = (int)dr["BikeColorId"];
                        BikeColor.BikeColorName = (string)dr["BikeColor"];
                        // BikeColor.UserId = dr["UserId"].ToString();

                    }
                }
            }

            return BikeColor;
        }


    }
}
