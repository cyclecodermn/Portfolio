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

        public static void Insert(BikeFrameTable Frame)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {

                SqlCommand cmd = new SqlCommand("FrameAdd", cn);
                cmd.CommandType = CommandType.StoredProcedure;


                //3 lines below are from BikeAdd
                SqlParameter param = new SqlParameter("@BikeId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);




               //cmd.Parameters.AddWithValue("@BikeFrameId", Frame.BikeFrameId);
                cmd.Parameters.AddWithValue("@BikeFrameName", Frame.BikeFrame);
                //cmd.Parameters.AddWithValue("@UserId", Frame.UserId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static void Edit(BikeFrameTable Frame)
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

        public static IEnumerable<InvDetailedItem> Delete(BikeFrameTable FrameToDelete)
        {
            List<BikeFrameTable> allFrames = new List<BikeFrameTable>();
            FrameRepoADO FrameRepo = new FrameRepoADO();

            allFrames = FrameRepo.GetAll();

            //BikeFrameTable FrameToDelete = new BikeFrameTable();
            FrameToDelete = allFrames.FirstOrDefault(f => f.BikeFrameId == FrameToDelete.BikeFrameId);

            //Note: Change the reference to FrameRepoADO to the factory when I get this working

            List<InvDetailedItem> allBikes = new List<InvDetailedItem>();
            BikeRepoADO BikeRepo = new BikeRepoADO();
            allBikes = BikeRepo.GetAll();

            List<InvDetailedItem> FramesFound = new List<InvDetailedItem>();

            string oneFrame = "";
            foreach (InvDetailedItem Bike in allBikes)
            {
                Bike.BikeFrame = Bike.BikeFrame.TrimEnd();
                FrameToDelete.BikeFrame = FrameToDelete.BikeFrame.TrimEnd();
                //TODO: Remove the lines above after I get extra spaces removed from db

                oneFrame = Bike.BikeFrame;
                if (oneFrame == FrameToDelete.BikeFrame)
                    FramesFound.Add(Bike);

            }

            //FramesFound = allBikes.Where(b => b.BikeFrame == FrameToDelete.BikeFrame);

            if (FramesFound.Count() == 0)
            //The frame is not used by any bikes, so it can be deleted.
            {
                using (var cn = new SqlConnection(Settings.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("FrameDelete", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BikeFrameId", FrameToDelete.BikeFrameId);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }

            return FramesFound;
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
