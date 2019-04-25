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
    /// <summary>
    /// GetAll Returns  List<BikeFrameTable> 
    /// </summary>
    public class FrameRepoADO : IFrameRepo

    {
        private static List<BikeFrameTable> _frames;

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
                    while(dr.Read())
                    {
                        BikeFrameTable currentRow = new BikeFrameTable();
                        currentRow.BikeFrameId = (int)dr["BikeFrameId"];
                        currentRow.BikeFrame = dr["BikeFrame"].ToString();

                        frames.Add(currentRow);
                    }
                }
            }
            return frames;
        }

        public static void Edit(BikeFrameTable frame)
        {
            var selectedFrame = _frames.First(f => f.BikeFrameId == frame.BikeFrameId);

            selectedFrame.BikeFrame = frame.BikeFrame;
        }

        public static void Delete(int frameId)
        {
            _frames.RemoveAll(f => f.BikeFrameId == frameId);
        }

        public static BikeFrameTable Get(int frameId)
        {
            return _frames.FirstOrDefault(f => f.BikeFrameId == frameId);
        }
    }
}
