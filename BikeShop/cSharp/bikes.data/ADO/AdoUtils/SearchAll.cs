using bikes.models.Queries;
using bikes.models.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.data.ADO.AdoUtils
{
    public class SearchAll
    {
        public IEnumerable<BikeShortItem> Search2(BikeSearchParameters parameters)
        {
            List<BikeShortItem> Bikes = new List<BikeShortItem>();

            FrameRepoADO FrameRepo = new FrameRepoADO();
            List<BikeFrameTable> AllFrames = FrameRepo.GetAll();

            ModelRepoADO ModelRepo = new ModelRepoADO();
            List<BikeModelTable> AllModels = ModelRepo.GetAll();

            MakeRepoADO MakeRepo = new MakeRepoADO();
            List<BikeMakeTable> AllMakes = MakeRepo.GetAll();

            List<int> AllYears = new List<int>();
            for (int i = 2000; i <= DateTime.Now.Year; i++)
                AllYears.Add(i);

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                //string query = "SELECT TOP 12 BikeId, BikeMsrp, BikeListPrice, BikePictName FROM BikeTable bt ";
                string query = GetAllBikeSQL();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.IsNew.HasValue)
                {
                    query += "AND BikeIsNew = @BikeIsNew ";
                    cmd.Parameters.AddWithValue("@BikeIsNew", parameters.IsNew);
                }


                if (parameters.MinPrice.HasValue)
                {
                    query += "AND BikeListPrice >= @MinPrice ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND BikeListPrice <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear.HasValue)
                {
                    query += "AND BikeYear >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
                }
                if (parameters.MaxYear.HasValue)
                {
                    query += "AND BikeYear <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
                }

                //parameters.MakeModelOrYr = parameters.MakeModelOrYr.TrimStart();
                //parameters.MakeModelOrYr = parameters.MakeModelOrYr.TrimEnd();

                if (!string.IsNullOrEmpty(parameters.MakeModelOrYr))
                {
                    bool isFrame = AllFrames.Any(p => p.BikeFrame == parameters.MakeModelOrYr);

                    if (isFrame)
                    {
                        query += "AND BikeFrameName LIKE @MakeModelOrYr ";
                        cmd.Parameters.AddWithValue("@MakeModelOrYr", parameters.MakeModelOrYr);
                    }

                    bool isModel = AllModels.Any(p => p.BikeModelName == parameters.MakeModelOrYr);

                    if (isModel)
                    {
                        query += "AND BikeModelName LIKE @MakeModelOrYr ";
                        cmd.Parameters.AddWithValue("@MakeModelOrYr", parameters.MakeModelOrYr + '%');
                    }

                    bool isMake = AllMakes.Any(p => p.BikeMakeName == parameters.MakeModelOrYr);

                    if (isMake)
                    {
                        query += "AND BikeMakeName LIKE @MakeModelOrYr ";
                        cmd.Parameters.AddWithValue("@MakeModelOrYr", parameters.MakeModelOrYr + '%');
                    }

                    int searchNum;
                    bool isNum = Int32.TryParse(parameters.MakeModelOrYr, out searchNum);

                    if (isNum && searchNum > 1999 && searchNum < DateTime.Now.Year + 1)
                    {
                        query += "AND BikeYear LIKE @MakeModelOrYr ";
                        cmd.Parameters.AddWithValue("@MakeModelOrYr", parameters.MakeModelOrYr + '%');
                    }

                }

                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BikeShortItem row = new BikeShortItem();
                        //BikeId, BikeMsrp, BikeListPrice, ImageFileName
                        row.BikeId = (int)dr["BikeId"];
                        row.BikeIsNew = (bool)dr["BikeIsNew"];
                        row.BikeYear = (int)dr["BikeYear"];
                        row.BikeMakeName = (string)dr["BikeMakeName"];
                        row.BikeModelName = (string)dr["BikeModelName"];
                        row.BikeFrame = (string)dr["BikeFrameName"];
                        row.BikeNumGears = (int)dr["BikeNumGears"];
                        row.BikeCondition = (int)dr["BikeCondition"];
                        row.BikeSerialNum = (string)dr["BikeSerialNum"];
                        row.BikeTrimColor = (string)dr["trimColor"];
                        row.BikeFrameColor = (string)dr["frameColor"];

                        row.BikeMsrp = (decimal)dr["BikeMsrp"];
                        // row.BikeMsrp = Math.Round(row.BikeMsrp, 2);
                        // Math.round dind't add .00, pity.

                        row.BikeListPrice = (decimal)dr["BikeListPrice"];
                        row.BikePictName = dr["BikePictName"].ToString();

                        Bikes.Add(row);
                    }
                }
            }

            return Bikes;

        }

        private string GetAllBikeSQL()
        {
            string query = "SELECT TOP 12 BikeId, BikeMakeName, BikeModelName, c.BikeColor AS frameColor, ";
            query += " ct.BikeColor AS trimColor, BikeFrameName,BikeMsrp,BikeListPrice, ";
            query += " BikeYear,BikeIsNew,BikeCondition,BikeNumGears,BikeSerialNum,BikeDescription,BikePictName";

            query += " FROM BikeTable bt ";
            query += " INNER JOIN BikeMakeTable mk ON mk.BikeMakeNameId = bt.BikeMakeNameId ";
            query += " INNER JOIN BikeModelTable md ON md.BikeModelId = bt.BikeModelId ";

            query += " INNER JOIN BikeFrameTable fr ON fr.BikeFrameId = bt.BikeFrameId ";
            query += " INNER JOIN BikeColorTable c ON c.BikeColorId = bt.BikeFrameColorId ";
            query += " INNER JOIN BikeColorTable ct ON ct.BikeColorId = bt.BikeTrimColorId ";

            query += " WHERE 1 = 1  ";
            return query;
        }
    }
}
