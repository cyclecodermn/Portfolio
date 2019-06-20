using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bikes.data.ADO.AdoUtils;
using bikes.data.Interfaces;
using bikes.models.Queries;
using bikes.models.Tables;

namespace bikes.data.ADO
{
    public class BikeRepoADO : IBikesRepo

    {
        public BikeTable GetById(int BikeId)
        {
            BikeTable bike = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {
                SqlCommand cmd = new SqlCommand("OneBikeDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BikeId", BikeId);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        bike = new BikeTable();
                        //bike.BikeId = (int)dr["BikeId"];

                        bike.BikeId = BikeId;
                        bike.BikeMsrp = (decimal)dr["BikeMsrp"];
                        bike.BikeMsrp= Math.Round(bike.BikeMsrp, 2);

                        bike.BikeListPrice = (decimal)dr["BikeListPrice"];
                        bike.BikeListPrice = Math.Round(bike.BikeListPrice, 2);
                        bike.BikeYear = (int)dr["BikeYear"];

                        var intToBool = dr["BikeisNew"];
                        //bike.BikeisNew = (intToBool==1);
                        bike.BikeIsNew = (bool)dr["BikeisNew"];
                        bike.BikeCondition = (int)dr["BikeCondition"];
                        bike.BikeNumGears = (int)dr["BikeNumGears"];
                        bike.BikeSerialNum = dr["BikeSerialNum"].ToString();
                        bike.BikeDescription = dr["BikeDescription"].ToString();

                        if (dr["BikePictName"] != DBNull.Value)
                            bike.BikePictName = dr["BikePictName"].ToString();

                        //InvDetailedItem currentRow = new InvDetailedItem();
                        //currentRow.BikeFrame = (string)dr["BikeFrame"];
                        //currentRow.BikeFrame = dr["BikeFrame"].ToString();

                    }
                }
            }

            return bike;
        }

        public void Insert(BikeTable bike)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {
                SqlCommand cmd = new SqlCommand("BikeInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@BikeId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                //cmd.Parameters.AddWithValue("@BikeId", bike.BikeId);
                cmd.Parameters.AddWithValue("@BikeMakeNameId", bike.BikeMakeNameId);
                cmd.Parameters.AddWithValue("@BikeModelId", bike.BikeModelId);
                cmd.Parameters.AddWithValue("@BikeFrameColorId", bike.BikeFrameColorId);
                cmd.Parameters.AddWithValue("@BikeTrimColorId", bike.BikeTrimColorId);
                cmd.Parameters.AddWithValue("@BikeFrameId", bike.BikeFrameId);
                cmd.Parameters.AddWithValue("@BikeMsrp", bike.BikeMsrp);
                cmd.Parameters.AddWithValue("@BikeListPrice", bike.BikeListPrice);
                cmd.Parameters.AddWithValue("@BikeYear", bike.BikeYear);
                cmd.Parameters.AddWithValue("@BikeIsNew", bike.BikeIsNew);
                cmd.Parameters.AddWithValue("@BikeDateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@BikeCondition", bike.BikeCondition);
                cmd.Parameters.AddWithValue("@BikeNumGears", bike.BikeNumGears);
                cmd.Parameters.AddWithValue("@BikeSerialNum", bike.BikeSerialNum);
                cmd.Parameters.AddWithValue("@BikeDescription", bike.BikeDescription);
                cmd.Parameters.AddWithValue("@BikePictName", bike.BikePictName);

                cn.Open();
                cmd.ExecuteNonQuery();
                bike.BikeId = (int)param.Value;
            }
        }

        public void Update(BikeTable bike)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {
                SqlCommand cmd = new SqlCommand("BikeUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BikeId", bike.BikeId);
                cmd.Parameters.AddWithValue("@BikeMakeNameId", bike.BikeMakeNameId);
                cmd.Parameters.AddWithValue("@BikeModelId", bike.BikeModelId);
                cmd.Parameters.AddWithValue("@BikeFrameColorId", bike.BikeFrameColorId);
                cmd.Parameters.AddWithValue("@BikeTrimColorId", bike.BikeTrimColorId);
                cmd.Parameters.AddWithValue("@BikeFrameId", bike.BikeFrameId);
                cmd.Parameters.AddWithValue("@BikeMsrp", bike.BikeMsrp);
                cmd.Parameters.AddWithValue("@BikeListPrice", bike.BikeListPrice);
                cmd.Parameters.AddWithValue("@BikeYear", bike.BikeYear);
                cmd.Parameters.AddWithValue("@BikeIsNew", bike.BikeIsNew);
                cmd.Parameters.AddWithValue("@BikeDateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@BikeCondition", bike.BikeCondition);
                cmd.Parameters.AddWithValue("@BikeNumGears", bike.BikeNumGears);
                cmd.Parameters.AddWithValue("@BikeSerialNum", bike.BikeSerialNum);
                cmd.Parameters.AddWithValue("@BikeDescription", bike.BikeDescription);
                cmd.Parameters.AddWithValue("@BikePictName", bike.BikePictName);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int BikeId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {
                SqlCommand cmd = new SqlCommand("BikeDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BikeId", BikeId);

                cn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public IEnumerable<FeaturedItem> GetFeatured()
        {
            List<FeaturedItem> FeaturedBikes = new List<FeaturedItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {
                SqlCommand cmd = new SqlCommand("GetFeaturedBikes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@BikeId", BikeId);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        FeaturedItem row = new FeaturedItem();

                        row.FeatureId = (int)dr["FeatureId"];
                        row.BikeId = (int)dr["BikeId"];
                        row.BikeMake = (string)dr["BikeMakeName"];
                        row.BikeModel = (string)dr["BikeModelName"];
                        row.BikeYear = (int)dr["BikeYear"];
                        row.BikeListPrice = (decimal)dr["BikeListPrice"];
                        row.BikeListPrice = Math.Round(row.BikeListPrice, 2);

                        if (dr["BikePictName"] != DBNull.Value)
                            row.BikePictName = dr["BikePictName"].ToString();
                        FeaturedBikes.Add(row);
                    }
                }
            }

            return FeaturedBikes;

        }

        public InvDetailedItem GetBikeDetails(int BikeId)
        {
            InvDetailedItem bike = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {
                SqlCommand cmd = new SqlCommand("OneBikeDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BikeId", BikeId);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        bike = new InvDetailedItem();
                        //                        bike.BikeId = (int)dr["BikeId"];
                        //                        bike.BikeFrameColorId = (int)dr["BikeFrameColorId"];
                        //                        bike.BikeTrimColorId = (int)dr["BikeTrimColorId"];
                        bike.BikeMsrp = (decimal)dr["BikeMsrp"];
                        bike.BikeListPrice = (decimal)dr["BikeListPrice"];
                        bike.BikeYear = (int)dr["BikeYear"];

                        var intToBool = dr["BikeisNew"];
                        //bike.BikeisNew = (intToBool==1);
                        bike.BikeIsNew = (bool)dr["BikeisNew"];
                        bike.BikeCondition = (int)dr["BikeCondition"];
                        bike.BikeNumGears = (int)dr["BikeNumGears"];
                        bike.BikeSerialNum = dr["BikeSerialNum"].ToString();
                        bike.BikeDescription = dr["BikeDescription"].ToString();

                        bike.BikeMake = dr["BikeMakeName"].ToString();
                        bike.BikeModel = dr["BikeModelName"].ToString();
                        bike.BikeFrame = dr["BikeFrameName"].ToString();
                        bike.FrameColor = dr["FrameColor"].ToString();
                        bike.TrimColor = dr["TrimColor"].ToString();

                        if (dr["BikePictName"] != DBNull.Value)
                            bike.BikePictName = dr["BikePictName"].ToString();

                        BikeFrameTable currentRow = new BikeFrameTable();
                        //     currentRow.BikeFrameId = (int)dr["BikeFrameId"];
                        //currentRow.BikeFrame = dr["BikeFrame"].ToString();

                    }
                }
            }
            return bike;
        }

        public List<InvDetailedItem> GetAll()
        {
            List<InvDetailedItem> Bikes = new List<InvDetailedItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))

            {
                SqlCommand cmd = new SqlCommand("BikeSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InvDetailedItem currentRow = new InvDetailedItem();

                        currentRow.BikeId = (int)dr["BikeId"];
                        currentRow.BikeMake = (string)dr["BikeMakeName"];
                        currentRow.BikeModel = (string)dr["BikeModelName"];
                        currentRow.FrameColor = (string)dr["FrameColor"];
                        currentRow.TrimColor = (string)dr["TrimColor"];
                        currentRow.BikeFrame = (string)dr["BikeFrameName"];
                        currentRow.BikeMsrp = (decimal)dr["BikeMsrp"];
                        currentRow.BikeListPrice = (decimal)dr["BikeListPrice"];
                        currentRow.BikeYear = (int)dr["BikeYear"];

                        var intToBool = dr["BikeisNew"];
                        //currentRow.BikeisNew = (intToBool==1);
                        currentRow.BikeIsNew = (bool)dr["BikeisNew"];
                        currentRow.BikeCondition = (int)dr["BikeCondition"];
                        currentRow.BikeNumGears = (int)dr["BikeNumGears"];
                        currentRow.BikeSerialNum = dr["BikeSerialNum"].ToString();
                        currentRow.BikeDescription = dr["BikeDescription"].ToString();

                        if (dr["BikePictName"] != DBNull.Value)
                            currentRow.BikePictName = dr["BikePictName"].ToString();

                        Bikes.Add(currentRow);
                    }
                }
            }

            return Bikes;
        }

        public IEnumerable<BikeShortItem> Search(BikeSearchParameters parameters)
        {
            SearchAll BikeSearch = new SearchAll();
            return BikeSearch.Search2(parameters);

        //    List<BikeShortItem> Bikes = new List<BikeShortItem>();

        //    FrameRepoADO FrameRepo = new FrameRepoADO();
        //    List<BikeFrameTable> AllFrames = FrameRepo.GetAll();

        //    ModelRepoADO ModelRepo = new ModelRepoADO();
        //    List<BikeModelTable> AllModels = ModelRepo.GetAll();

        //    MakeRepoADO MakeRepo = new MakeRepoADO();
        //    List<BikeMakeTable> AllMakes = MakeRepo.GetAll();

        //    List<int> AllYears = new List<int>();
        //    for (int i = 2000; i <= DateTime.Now.Year; i++)
        //        AllYears.Add(i);

        //    using (var cn = new SqlConnection(Settings.GetConnectionString()))
        //    {
        //        //string query = "SELECT TOP 12 BikeId, BikeMsrp, BikeListPrice, BikePictName FROM BikeTable bt ";
        //        string query = GetAllBikeSQL();

        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = cn;

        //        if (parameters.IsNew.HasValue)
        //        {
        //            query += "AND BikeIsNew = @BikeIsNew ";
        //            cmd.Parameters.AddWithValue("@BikeIsNew", parameters.IsNew);
        //        }


        //        if (parameters.MinPrice.HasValue)
        //        {
        //            query += "AND BikeListPrice >= @MinPrice ";
        //            cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
        //        }

        //        if (parameters.MaxPrice.HasValue)
        //        {
        //            query += "AND BikeListPrice <= @MaxPrice ";
        //            cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
        //        }

        //        if (parameters.MinYear.HasValue)
        //        {
        //            query += "AND BikeYear >= @MinYear ";
        //            cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
        //        }
        //        if (parameters.MaxYear.HasValue)
        //        {
        //            query += "AND BikeYear <= @MaxYear ";
        //            cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
        //        }

        //        //parameters.MakeModelOrYr = parameters.MakeModelOrYr.TrimStart();
        //        //parameters.MakeModelOrYr = parameters.MakeModelOrYr.TrimEnd();

        //        if (!string.IsNullOrEmpty(parameters.MakeModelOrYr))
        //        {
        //            bool isFrame = AllFrames.Any(p => p.BikeFrame == parameters.MakeModelOrYr);

        //            if (isFrame)
        //            {
        //                query += "AND BikeFrameName LIKE @MakeModelOrYr ";
        //                cmd.Parameters.AddWithValue("@MakeModelOrYr", parameters.MakeModelOrYr);
        //            }

        //            bool isModel = AllModels.Any(p => p.BikeModel == parameters.MakeModelOrYr);

        //            if (isModel)
        //            {
        //                query += "AND BikeModel LIKE @MakeModelOrYr ";
        //                cmd.Parameters.AddWithValue("@MakeModelOrYr", parameters.MakeModelOrYr + '%');
        //            }

        //            bool isMake = AllMakes.Any(p => p.BikeMake == parameters.MakeModelOrYr);

        //            if (isMake)
        //            {
        //                query += "AND BikeMake LIKE @MakeModelOrYr ";
        //                cmd.Parameters.AddWithValue("@MakeModelOrYr", parameters.MakeModelOrYr + '%');
        //            }

        //            int searchNum;
        //            bool isNum = Int32.TryParse(parameters.MakeModelOrYr, out searchNum);

        //            if (isNum && searchNum > 1999 && searchNum < DateTime.Now.Year + 1)
        //            {
        //                query += "AND BikeYear LIKE @MakeModelOrYr ";
        //                cmd.Parameters.AddWithValue("@MakeModelOrYr", parameters.MakeModelOrYr + '%');
        //            }

        //        }

        //        cmd.CommandText = query;

        //        cn.Open();

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                BikeShortItem row = new BikeShortItem();
        //                //BikeId, BikeMsrp, BikeListPrice, ImageFileName
        //                row.BikeId = (int)dr["BikeId"];
        //                row.BikeIsNew = (bool)dr["BikeIsNew"];
        //                row.BikeYear = (int)dr["BikeYear"];
        //                row.BikeMake = (string)dr["BikeMake"];
        //                row.BikeModel = (string)dr["BikeModel"];
        //                row.BikeFrame = (string)dr["BikeFrameName"];
        //                row.BikeNumGears = (int)dr["BikeNumGears"];
        //                row.BikeCondition = (int)dr["BikeCondition"];
        //                row.BikeSerialNum = (string)dr["BikeSerialNum"];
        //                row.BikeTrimColor = (string)dr["trimColor"];
        //                row.BikeFrameColor = (string)dr["frameColor"];

        //                row.BikeMsrp = (decimal)dr["BikeMsrp"];
        //               // row.BikeMsrp = Math.Round(row.BikeMsrp, 2);
        //               // Math.round dind't add .00, pity.

        //                row.BikeListPrice = (decimal)dr["BikeListPrice"];
        //                row.BikePictName = dr["BikePictName"].ToString();

        //                Bikes.Add(row);
        //            }
        //        }
        //    }

        //    return Bikes;

        //}

        //private string GetAllBikeSQL()
        //{
        //    string query = "SELECT TOP 12 BikeId, BikeMake, BikeModel, c.BikeColor AS frameColor, ";
        //    query += " ct.BikeColor AS trimColor, BikeFrameName,BikeMsrp,BikeListPrice, ";
        //    query += " BikeYear,BikeIsNew,BikeCondition,BikeNumGears,BikeSerialNum,BikeDescription,BikePictName";

        //    query += " FROM BikeTable bt ";
        //    query += " INNER JOIN BikeMakeTable mk ON mk.BikeMakeNameId = bt.BikeMakeNameId ";
        //    query += " INNER JOIN BikeModelTable md ON md.BikeModelId = bt.BikeModelId ";

        //    query += " INNER JOIN BikeFrameTable fr ON fr.BikeFrameId = bt.BikeFrameId ";
        //    query += " INNER JOIN BikeColorTable c ON c.BikeColorId = bt.BikeFrameColorId ";
        //    query += " INNER JOIN BikeColorTable ct ON ct.BikeColorId = bt.BikeTrimColorId ";

        //    query += " WHERE 1 = 1  ";
        //    return query;
        }
    }
}
