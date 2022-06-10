using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;
using movieAPI.Models;
using movieAPI.Repository;

namespace movieAPI.Controllers
{
    
    public class movieController : ApiController
    {
        private movieRepository movieRepository;
        public movieController()
        {
            movieRepository = new movieRepository();
        }
        [HttpGet]
        [Route("api/moviedb/movies")]
        public IHttpActionResult Movies()
        {
            try
            {
                return Ok(movieRepository.GetMovies());
            }
            catch
            {
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("api/moviedb/movie/{id}")]
        public IHttpActionResult Movie(int id)
        {
            try
            {
                var ent= movieRepository.GetMovieById(id);
                if (ent.MovieID == -1)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(movieRepository.GetMovieById(id));
                }
            }
            catch
            {
                return InternalServerError();
            }

        }

        [HttpPost]
        [Route("api/moviedb/add_movie")]
        public IHttpActionResult MovieAdd([FromBody] movieEntitiy e)
        {
            try
            {
                movieRepository.AddMovie(e);
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("api/moviedb/update_movie")]
        public IHttpActionResult MovieUpdate([FromBody] movieEntitiy e)
        {
            try
            {
                movieRepository.UpdateMovie(e);
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("api/moviedb/delete_movie/{id}")]
        public IHttpActionResult DeleteMovie(int id)
        {
            try
            {
                movieRepository.DeleteMovie(new movieEntitiy() { MovieID = id });
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
