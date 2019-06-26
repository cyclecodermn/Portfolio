using bikes.data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bikes.models.Queries;
using bikes.models.Tables;
using bikes.data.ADO.AdoUtils;

namespace bikes.data.ADO
{
    /// <summary>
    /// GetAll Returns  List<BikeFrameTable> 
    /// </summary>
    public class FrameRepoADO : IFrameRepo

    {
        public List<BikeFrameTable> GetAll()
        {
            List<BikeFrameTable> frames = new List<BikeFrameTable>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {
                SqlCommand cmd = new SqlCommand("FramesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BikeFrameTable currentRow = new BikeFrameTable();
                        currentRow.BikeFrameId = (int)dr["BikeFrameId"];
                        currentRow.BikeFrame = dr["BikeFrameName"].ToString();

                        frames.Add(currentRow);
                    }
                }
            }
            return frames;
        }

        public void Insert(BikeFrameTable Frame)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {

                SqlCommand cmd = new SqlCommand("FrameInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                //3 lines below are from BikeAdd
                SqlParameter param = new SqlParameter("@FrameId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                //cmd.Parameters.AddWithValue("@BikeFrameId", Frame.BikeFrameId);
                cmd.Parameters.AddWithValue("@BikeFrame", Frame.BikeFrame);
                //cmd.Parameters.AddWithValue("@UserId", Frame.UserId);

                cn.Open();
                cmd.ExecuteNonQuery();
                Frame.BikeFrameId = (int)param.Value;
            }
        }

        public void Edit(BikeFrameTable Frame)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("FrameUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BikeFrameId", Frame.BikeFrameId);
                cmd.Parameters.AddWithValue("@BikeFrameName", Frame.BikeFrame);
                //cmd.Parameters.AddWithValue("@UserId", Frame.UserId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static IEnumerable<BikeShortItem> CheckIfFrameIsUsed(BikeFrameTable FrameToDelete)
        {

            BikeSearchParameters parameters = new BikeSearchParameters();
            parameters.MakeModelOrYr = FrameToDelete.BikeFrame;

            SearchAll BikeSearch = new SearchAll();
            
            IEnumerable<BikeShortItem> BikesWithFrame= BikeSearch.Search2(parameters);

            return BikesWithFrame;
        }

        public static void Delete(int FrameIdToDelete)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("FrameDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BikeFrameId", FrameIdToDelete);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static BikeFrameTable GetById(int frameId)
        {
            BikeFrameTable Frame = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("FrameSelect", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BikeFrameId", frameId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Frame = new BikeFrameTable();
                        Frame.BikeFrameId = (int)dr["BikeFrameId"];
                        Frame.BikeFrame = (string)dr["BikeFrameName"];
                        // Frame.UserId = dr["UserId"].ToString();

                    }
                }
            }

            return Frame;
        }
    }
}
