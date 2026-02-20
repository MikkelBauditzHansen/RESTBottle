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
        [HttpGet("{id}")]
        public Bottle? Get(int id)
        {
            return _repo.GetById(id);
        }

        // POST api/<BottlesController>
        [HttpPost]
        public Bottle Post([FromBody] Bottle newBottle)
        {
            return _repo.AddBottle(newBottle);
        }

        // PUT api/<BottlesController>/5
        [HttpPut("{id}")]
        public Bottle? Put(int id, [FromBody] Bottle updatedBottle)
        {
            return _repo.Update(id, updatedBottle);
        }

        // DELETE api/<BottlesController>/5
        [HttpDelete("{id}")]
        public Bottle? Delete(int id)
        {
            return _repo.Remove(id);
        }
    }
}
