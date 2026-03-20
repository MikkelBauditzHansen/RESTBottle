using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTBottle.Bottles;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTBottle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BottlesController : ControllerBase
    {
        private BottlesRepositoryList _repo;
        public BottlesController(BottlesRepositoryList repositoryList)
        {
            _repo = repositoryList;
        }
        // GET: api/<BottlesController>
        [HttpGet]
        public IEnumerable<Bottle> Get()
        {
            return _repo.Get();
        }

        // GET api/<BottlesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Bottle?> Get(int id)
        {
            Bottle? bottle = _repo.GetById(id);
            if (bottle == null)
            {
                return NotFound();
            }
            return Ok(bottle);
        }

        // POST api/<BottlesController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public ActionResult<Bottle> Post([FromBody] Bottle newBottle)
        {
            try
            {
                _repo.AddBottle(newBottle);
                return Created($"api/bottles/{newBottle.Id}", newBottle);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<BottlesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Bottle?> Put(int id, [FromBody] Bottle updatedBottle)
        {
            if (id != updatedBottle.Id)
            {
                return BadRequest("ID in URL does not match ID in body");
            }

            try
            {
                var updated = _repo.Update(id, updatedBottle);

                if (updated == null)
                {
                    return NotFound($"Bottle with id {id} not found");
                }

                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BottlesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public ActionResult<Bottle?> Delete(int id)
        {
            var deleted = _repo.Remove(id);

            if (deleted == null)
            {
                return NotFound($"Bottle with id {id} not found");
            }

            return Ok(deleted);
        }
        [HttpOptions]
        public void Options()
        {
        }
    }
}
