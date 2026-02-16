using System;
using System.Diagnostics.Metrics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using RESTBottle.Bottles;
using Xunit;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnitTestProject
{
    public class BottlesTest
    {
        bool useDatabase = true;
        IBottlesRepository repo;
        public BottlesTest()
        {
            if (useDatabase)
            {
                var optionsBuilder = new DbContextOptionsBuilder<BottlesDBContext>();
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Bottles;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                BottlesDBContext _dbContext = new(optionsBuilder.Options);
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Bottles");
                repo = new BottlesRepositoryDatabase(_dbContext);
            }
            else
            {
                repo = new BottlesRepositoryList(includeData: false);

            }
        }
        [Fact]
        public void GetAll_Returns_Initial_Three_Bottles()
        {
            IBottlesRepository repo = new BottlesRepositoryList();

            var all = repo.Get(sortOrder: "Description").ToList();

            Assert.Equal(3, all.Count);
        }

        [Fact]
        public void AddBottle_Assigns_Id_And_Adds_To_Repository()
        {
            IBottlesRepository repo = new BottlesRepositoryList();
            var newBottle = new Bottle { Volume = 250, Description = "Test Bottle" };

            var added = repo.AddBottle(newBottle);

            Assert.Equal(4, added.Id); // initial repo seeds 3 bottles
            Assert.Equal(250, added.Volume);
            Assert.Contains(added, repo.Get());
        }

        [Fact]
        public void GetById_Returns_Bottle_When_Found_And_Null_When_NotFound()
        {
            IBottlesRepository repo = new BottlesRepositoryList();

            var found = repo.GetById(1);
            var notFound = repo.GetById(999);

            Assert.NotNull(found);
            Assert.Equal(1, found!.Id);
            Assert.Null(notFound);
        }

        [Fact]
        public void Remove_Removes_And_Returns_Bottle_When_Exists()
        {
            IBottlesRepository repo = new BottlesRepositoryList();

            var removed = repo.Remove(2);

            Assert.NotNull(removed);
            Assert.Equal(2, removed!.Id);
            Assert.Null(repo.GetById(2));
        }

        [Fact]
        public void Update_Updates_And_Returns_Bottle_When_Exists_And_Null_When_Not()
        {
            IBottlesRepository repo = new BottlesRepositoryList();

            var updated = repo.Update(3, new Bottle { Volume = 999, Description = "Updated" });

            Assert.NotNull(updated);
            Assert.Equal(999, updated!.Volume);
            Assert.Equal("Updated", updated.Description);

            var updateNonExisting = repo.Update(999, new Bottle { Volume = 1, Description = "No" });
            Assert.Null(updateNonExisting);
        }
    }
}
