using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTBottle.Bottles;
using RESTBottle.Controllers;
using Xunit;

namespace UnitTestProject
{

    public class BottlesControllerTests
    {
        private BottlesController CreateControllerWithSeededRepo()
        {
            var repo = new BottlesRepositoryList(true);
            return new BottlesController(repo);
        }

        [Fact]
        public void Get_Returns_All_Seeded_Bottles()
        {
            var controller = CreateControllerWithSeededRepo();

            var result = controller.Get();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetById_Returns_Ok_When_Bottle_Exists()
        {
            var controller = CreateControllerWithSeededRepo();

            var actionResult = controller.Get(1);

            Assert.IsType<ActionResult<Bottle?>>(actionResult);
            var ok = Assert.IsType<OkObjectResult>(actionResult.Result);
            var bottle = Assert.IsType<Bottle>(ok.Value);
            Assert.Equal(1, bottle.Id);
        }

        [Fact]
        public void GetById_Returns_NotFound_When_Bottle_Does_Not_Exist()
        {
            var controller = CreateControllerWithSeededRepo();

            var actionResult = controller.Get(999);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void Post_Adds_Bottle_And_Returns_Created()
        {
            var controller = CreateControllerWithSeededRepo();
            var newBottle = new Bottle { Volume = 250, Description = "Test Bottle" };

            var actionResult = controller.Post(newBottle);

            var created = Assert.IsType<CreatedResult>(actionResult.Result);
            var returned = Assert.IsType<Bottle>(created.Value);
            // seeded repo has 3 initial items => next id should be 4
            Assert.Equal(4, returned.Id);
            Assert.Equal("Test Bottle", returned.Description);
        }

        [Fact]
        public void Put_Updates_Bottle_When_Id_Matches()
        {
            var controller = CreateControllerWithSeededRepo();
            var updated = new Bottle { Id = 1, Volume = 999, Description = "Updated" };

            var actionResult = controller.Put(1, updated);

            var ok = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returned = Assert.IsType<Bottle>(ok.Value);
            Assert.Equal(1, returned.Id);
            Assert.Equal(999, returned.Volume);
            Assert.Equal("Updated", returned.Description);
        }

        [Fact]
        public void Put_Returns_BadRequest_When_Id_Mismatch()
        {
            var controller = CreateControllerWithSeededRepo();
            var updated = new Bottle { Id = 2, Volume = 111, Description = "Mismatch" };

            var actionResult = controller.Put(1, updated);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public void Delete_Removes_Bottle_When_Exists()
        {
            var controller = CreateControllerWithSeededRepo();

            var deleteResult = controller.Delete(1);
            var ok = Assert.IsType<OkObjectResult>(deleteResult.Result);
            var deleted = Assert.IsType<Bottle>(ok.Value);
            Assert.Equal(1, deleted.Id);

            // subsequent GET should be NotFound
            var getAfterDelete = controller.Get(1);
            Assert.IsType<NotFoundResult>(getAfterDelete.Result);
        }

        [Fact]
        public void Delete_Returns_NotFound_When_Missing()
        {
            var controller = CreateControllerWithSeededRepo();

            var deleteResult = controller.Delete(null);

            Assert.IsType<NotFoundObjectResult>(deleteResult.Result);
        }
    }
}
