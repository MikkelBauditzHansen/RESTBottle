using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RESTBottle.Apples;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTBottle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ApplesController : ControllerBase
    {
        private ApplesRepositoryList _repo;
        public ApplesController(ApplesRepositoryList repo)
        {
            _repo = repo;
        }
        // GET: api/<ApplesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]

        public ActionResult<IEnumerable<Apple>> Get([FromQuery] int? minimumweight, [FromQuery] int? maximumweight)
        {
            IEnumerable<Apple> result = _repo.Get(minimumweight, maximumweight);
            if (result == null || result.Count() == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }

        // GET api/<ApplesController>/5
        [HttpGet("{id}")]
        public Apple? Get(int id)
        {
            return _repo.GetById(id);
        }

        // POST api/<ApplesController>
        [HttpPost]
        [Authorize]

        public Apple? Post([FromBody] Apple newApple)
        {
            return _repo.AddApple(newApple);
        }

        // PUT api/<ApplesController>/5
        [HttpPut("{id}")]
        public Apple? Put(int id, [FromBody] Apple updatedApple)
        {
            return _repo.Update(id, updatedApple);
        }

        // DELETE api/<ApplesController>/5
        [HttpDelete("{id}")]
        public Apple? Delete(int id)
        {
            return _repo.Remove(id);
        }
    }
}
