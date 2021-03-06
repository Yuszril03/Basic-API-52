using API.Repository.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BaseController<Entity,Repository,Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity,Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var get = repository.Get();
            if (get != null)
            {
                //return Ok(get);
                return Ok(get);
            }
            else
            {

                return NotFound(new { status = HttpStatusCode.NotFound, result = get, message = "Data tidak tersedia" });
            }
        }
        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var get = repository.Get(key);
            if (get != null)
            {
                //return Ok(get);
                return Ok(get);
            }
            else
            {

                return NotFound(new { status = HttpStatusCode.NotFound, result = get, message = "Data tidak tersedia" });
            }
        }
        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            var insert = repository.Insert(entity);
            if (insert > 0)
            {
                return Ok(insert);
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = insert, message = "Gagal tersimpan" });
            }
        }
        [HttpDelete("{key}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Delete(Key key)
        {

            var delete = repository.Delete(key);
            if (delete > 0)
            {
                return Ok(delete);
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = delete, message = "Gagal Delete" });
            }
        }
        [HttpPut("{key}")]
        public ActionResult Update(Entity entity, Key key)
        {

            var update = repository.Update(entity, key);
            if (update > 0)
            {
                return Ok(update);
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = update, message = "Gagal diperbarui" });
            }
        }

    }
}
