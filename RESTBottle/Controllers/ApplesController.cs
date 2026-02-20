using Microsoft.AspNetCore.Mvc;
using RESTBottle.Apples;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTBottle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplesController : ControllerBase
    {
        private ApplesRepositoryList _repo;
        public ApplesController(ApplesRepositoryList repo)
        {
            _repo = repo;
        }
        // GET: api/<ApplesController>
        [HttpGet]
        public IEnumerable<Apple> Get()
        {
            return _repo.Get();
        }

        // GET api/<ApplesController>/5
        [HttpGet("{id}")]
        public Apple? Get(int id)
        {
            return _repo.GetById(id);
        }

        // POST api/<ApplesController>
        [HttpPost]
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
