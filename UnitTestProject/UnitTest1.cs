using System.Linq;
using RESTBottle.Bottles;
using Xunit;

namespace UnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void GetAll_Returns_Initial_Three_Bottles()
        {
            var repo = new BottlesRepository();

            var all = repo.Get(sortOrder: "Description").ToList();

            Assert.Equal(3, all.Count);
        }

        [Fact]
        public void AddBottle_Assigns_Id_And_Adds_To_Repository()
        {
            var repo = new BottlesRepository();
            var newBottle = new Bottle { Volume = 250, Description = "Test Bottle" };

            var added = repo.AddBottle(newBottle);

            Assert.Equal(4, added.Id); // initial repo seeds 3 bottles
            Assert.Equal(250, added.Volume);
            Assert.Contains(added, repo.Get());
        }

        [Fact]
        public void GetById_Returns_Bottle_When_Found_And_Null_When_NotFound()
        {
            var repo = new BottlesRepository();

            var found = repo.GetById(1);
            var notFound = repo.GetById(999);

            Assert.NotNull(found);
            Assert.Equal(1, found!.Id);
            Assert.Null(notFound);
        }

        [Fact]
        public void Remove_Removes_And_Returns_Bottle_When_Exists()
        {
            var repo = new BottlesRepository();

            var removed = repo.Remove(2);

            Assert.NotNull(removed);
            Assert.Equal(2, removed!.Id);
            Assert.Null(repo.GetById(2));
        }

        [Fact]
        public void Update_Updates_And_Returns_Bottle_When_Exists_And_Null_When_Not()
        {
            var repo = new BottlesRepository();

            var updated = repo.Update(3, new Bottle { Volume = 999, Description = "Updated" });

            Assert.NotNull(updated);
            Assert.Equal(999, updated!.Volume);
            Assert.Equal("Updated", updated.Description);

            var updateNonExisting = repo.Update(999, new Bottle { Volume = 1, Description = "No" });
            Assert.Null(updateNonExisting);
        }
    }
}
