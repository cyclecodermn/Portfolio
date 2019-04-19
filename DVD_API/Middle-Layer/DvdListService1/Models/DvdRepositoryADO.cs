using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DvdListService1.Models
{
    public class DvdRepositoryADO : IDvdRepository
    {
        IEnumerable<DVD> allDvds;
        SqlConnection conn = new SqlConnection();
        //
        // Change trustedconnecting = true to user id=DvdLibraryApp; password=testing123;


        public static string _connString = @"Server=ALEXANDRA\SQLEXPRESS;Database=DvdRepoEF;user id=DvdLibraryApp; password=Testing123;";
        // public static string _connString = @"Server=ALEXANDRA\SQLEXPRESS;Database=DvdRepoEF;Trusted_Connection=True";
        // "Server=localhost;Database=DvdLibrary;user id=DvdRepoEF; password=testing123;"; 

        //public DvdRepositoryADO()
        //{
        //    conn.ConnectionString = "Server=localhost;Database=DvdRepoEF;user id=DvdLibraryApp; password=Testing123;";
        //}

        public void Create(DVD dvd)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connString;

            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "dvdAdd";
            cmd.Parameters.AddWithValue("@dvdTitle", dvd.Title);
            cmd.Parameters.AddWithValue("@dvdYear", dvd.realeaseYear);
            cmd.Parameters.AddWithValue("@dvdDirector", dvd.Director);
            cmd.Parameters.AddWithValue("@dvdRating", dvd.Rating);
            cmd.Parameters.AddWithValue("@dvdNotes", dvd.Notes);

            cmd.Parameters.Add("@dvdId", SqlDbType.Int).Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Delete(int DvdId)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connString;

            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "dvdDelete";

            cmd.Parameters.AddWithValue("@DvdId", DvdId);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public DVD Get(int DvdId)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connString;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dvdGetById";

            cmd.Parameters.AddWithValue("@DvdId", DvdId);

            conn.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    DVD currentRow = new DVD();

                    currentRow.DvdId = (int)dr["DvdId"];
                    currentRow.Title = dr["title"].ToString();
                    currentRow.realeaseYear = (int)dr["realeaseYear"];
                    currentRow.Director = dr["Director"].ToString();
                    currentRow.Rating = dr["Rating"].ToString();

                    if (dr["Notes"] != DBNull.Value)
                        currentRow.Notes = dr["Notes"].ToString();

                    return currentRow;
                }
            }
            return null;
        }

        public IEnumerable<DVD> GetAll()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connString;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dvdGetAll";

            conn.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    DVD currentRow = new DVD();

                    currentRow.DvdId = (int)dr["DvdId"];
                    currentRow.Title = dr["title"].ToString();
                    currentRow.realeaseYear = (int)dr["realeaseYear"];
                    currentRow.Director = dr["Director"].ToString();
                    currentRow.Rating = dr["Rating"].ToString();

                    if (dr["Notes"] != DBNull.Value)
                        currentRow.Notes = dr["Notes"].ToString();

                    yield return currentRow;
                }
            }
            yield break;
        }

        public IEnumerable<DVD> GetByDirector(string term)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connString;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dvdGetByDirector";

            cmd.Parameters.AddWithValue("@term", term);

            conn.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    DVD currentRow = new DVD();

                    currentRow.DvdId = (int)dr["DvdId"];
                    currentRow.Title = dr["title"].ToString();
                    currentRow.realeaseYear = (int)dr["realeaseYear"];
                    currentRow.Director = dr["Director"].ToString();
                    currentRow.Rating = dr["Rating"].ToString();

                    if (dr["Notes"] != DBNull.Value)
                        currentRow.Notes = dr["Notes"].ToString();

                    yield return currentRow;
                }
            }
            yield break;
        }


        public IEnumerable<DVD> GetByRating(string term)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connString;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dvdGetByRating";

            cmd.Parameters.AddWithValue("@term", term);

            conn.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    DVD currentRow = new DVD();

                    currentRow.DvdId = (int)dr["DvdId"];
                    currentRow.Title = dr["title"].ToString();
                    currentRow.realeaseYear = (int)dr["realeaseYear"];
                    currentRow.Director = dr["Director"].ToString();
                    currentRow.Rating = dr["Rating"].ToString();

                    if (dr["Notes"] != DBNull.Value)
                        currentRow.Notes = dr["Notes"].ToString();

                    yield return currentRow;
                }
            }
            yield break;
        }

        public IEnumerable<DVD> GetByTitle(string term)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connString;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dvdGetByTitle";

            cmd.Parameters.AddWithValue("@term", term);

            conn.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    DVD currentRow = new DVD();

                    currentRow.DvdId = (int)dr["DvdId"];
                    currentRow.Title = dr["title"].ToString();
                    currentRow.realeaseYear = (int)dr["realeaseYear"];
                    currentRow.Director = dr["Director"].ToString();
                    currentRow.Rating = dr["Rating"].ToString();

                    if (dr["Notes"] != DBNull.Value)
                        currentRow.Notes = dr["Notes"].ToString();

                    yield return currentRow;
                }
            }
            yield break;
        }

        public IEnumerable<DVD> GetByYear(string term)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connString;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dvdGetByYear";

            cmd.Parameters.AddWithValue("@term", term);

            conn.Open();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    DVD currentRow = new DVD();

                    currentRow.DvdId = (int)dr["DvdId"];
                    currentRow.Title = dr["title"].ToString();
                    currentRow.realeaseYear = (int)dr["realeaseYear"];
                    currentRow.Director = dr["Director"].ToString();
                    currentRow.Rating = dr["Rating"].ToString();

                    if (dr["Notes"] != DBNull.Value)
                        currentRow.Notes = dr["Notes"].ToString();

                    yield return currentRow;
                }
            }
            yield break;
        }

        public void Update(DVD updatedDVD)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connString;

            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "dvdEdit";
            cmd.Parameters.AddWithValue("@dvdid", updatedDVD.DvdId);
            cmd.Parameters.AddWithValue("@dvdTitle", updatedDVD.Title);
            cmd.Parameters.AddWithValue("@dvdYear", updatedDVD.realeaseYear);
            cmd.Parameters.AddWithValue("@dvdDirector", updatedDVD.Director);
            cmd.Parameters.AddWithValue("@dvdRating", updatedDVD.Rating);
            cmd.Parameters.AddWithValue("@dvdNotes", updatedDVD.Notes);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}