using bikes.data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bikes.models.Tables;

namespace bikes.data.ADO
{
    public class ColorRepoADO : IColorRepo

    {
        public List<BikeColorTable> GetAll()
        {
            List<BikeColorTable> Models = new List<BikeColorTable>();

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

                        Models.Add(currentRow);
                    }
                }
            }

            return Models;
        }

        public void Insert(BikeColorTable NewModel)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ModelId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

 //               cmd.Parameters.AddWithValue("@ModelId", NewModel.BikeColorId);
                cmd.Parameters.AddWithValue("@BikeColor", NewModel.BikeColorName);

                cn.Open();

                cmd.ExecuteNonQuery();

                //. = (int)param.Value;
                NewModel.BikeColorId = (int)param.Value;
            }
        }
    }
}
