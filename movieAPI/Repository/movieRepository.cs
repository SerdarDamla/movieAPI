using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DataAccessLayer;
using movieAPI.Models;

namespace movieAPI.Repository
{
    public class movieRepository
    {
        private DBFacade dBFacade;
        public movieRepository()
        {
            dBFacade = new DBFacade("server=(localdb)\\Mssqllocaldb;database=bookStoreDb;integrated security=true");
        }
        ~movieRepository()
        {
            dBFacade = null;
        }
        public List<movieEntitiy> GetMovies()
        {
            DataTable table = dBFacade.GetTable("Select * from movieDB",false);

            List<movieEntitiy> list = new List<movieEntitiy>(table.Rows.Count);
            
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(
                new movieEntitiy()
                {
                    MovieID = Convert.ToInt32(table.Rows[i]["MovieID"]),
                    MovieName = table.Rows[i]["MovieName"].ToString(),
                    DirectorName = table.Rows[i]["DirectorName"].ToString(),
                    DurationTime = Convert.ToInt32(table.Rows[i]["DurationTime"])
                });
            }
            return list;
        }

        public movieEntitiy GetMovieById(int id)
        {
            DataTable table = dBFacade.GetTable("Select * from movieDB Where MovieID= @id", false,
                new SqlParameter()
                {
                    ParameterName = "@id", DbType = DbType.Int32,Value=id
                }
                );

            if (table.Rows.Count == 1)
            {
               return new movieEntitiy()
                {
                   MovieID = Convert.ToInt32(table.Rows[0]["MovieID"]),
                   MovieName = table.Rows[0]["MovieName"].ToString(),
                   DirectorName = table.Rows[0]["DirectorName"].ToString(),
                   DurationTime = Convert.ToInt32(table.Rows[0]["DurationTime"])
               };
            }
            else
            {
                return new movieEntitiy()
                {
                    MovieID = -1,
                    MovieName = "",
                    DirectorName = "",
                    DurationTime =0
                };
            }
        }

        public bool AddMovie(movieEntitiy movie)
        {
            return dBFacade.Execute("insert MovieDB (MovieName,DirectorName,DurationTime) values (@movName,@dirName,@time)", false,


                new SqlParameter()
                { ParameterName = "@movName", SqlDbType = SqlDbType.NVarChar, Value = movie.MovieName }
                ,new SqlParameter()
                { ParameterName = "@dirName", SqlDbType = SqlDbType.NVarChar, Value = movie.DirectorName }
                , new SqlParameter()
                { ParameterName = "@time", SqlDbType = SqlDbType.Int, Value = movie.DurationTime }
                );
        }

        public bool UpdateMovie(movieEntitiy movie)
        {
            return dBFacade.Execute("update MovieDB set MovieName=@movName,DirectorName=@dirName,DurationTime=@time) Where MovieID=@id", false,

                new SqlParameter()
                { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = movie.MovieID }
                ,new SqlParameter()
                { ParameterName = "@movName", SqlDbType = SqlDbType.NVarChar, Value = movie.MovieName }
                , new SqlParameter()
                { ParameterName = "@dirName", SqlDbType = SqlDbType.NVarChar, Value = movie.DirectorName }
                , new SqlParameter()
                { ParameterName = "@time", SqlDbType = SqlDbType.Int, Value = movie.DurationTime }
                );
        }
        public bool DeleteMovie(movieEntitiy movie)
        {
            return dBFacade.Execute("Delete from MovieDB Where MovieID=@id", false,
                new SqlParameter()
                { ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = movie.MovieID });
        }
    }
}