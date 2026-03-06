using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RESTBottle.Apples
{
    public class ApplesRepositoryList : IApplesRepository
    {
        private List<Apple> apples = new List<Apple>();
        private int nextId = 1;
        public ApplesRepositoryList(bool includeData = true)
        {
            if (includeData)
            {
                AddApple(new Apple { Variety = "Granny Smith", Description = "Green and tart", Weight = 150 });
                AddApple(new Apple { Variety = "Red Delicious", Description = "Red and sweet", Weight = 200 });
                AddApple(new Apple { Variety = "Fuji", Description = "Crisp and sweet", Weight = 180 });
            }
        }
        public IEnumerable<Apple> Get(int? minimumweight, int? maximumweight)
        {
            IEnumerable<Apple> result = apples.AsReadOnly();
            if (minimumweight != null)
            {
                result = result.Where(a => a.Weight >= minimumweight);
            }
            if (maximumweight != null)
            {
                result = result.Where(a => a.Weight <= maximumweight);
            }
                return result;
        }
        public Apple? GetById(int id)
        {
            return apples.FirstOrDefault(a => a.Id == id);
        }
        public Apple AddApple(Apple apple)
        {
            apple.Id = nextId++;
            apples.Add(apple);
            return apple;
        }
        public Apple? Update(int id, Apple updatedApple)
        {
            var apple = GetById(id);
            if (apple != null)
            {
                apple.Variety = updatedApple.Variety;
                apple.Description = updatedApple.Description;
                return apple;
            }
            return null;

        }
        public Apple? Remove(int id)
        {
            var apple = GetById(id);
            if (apple != null)
            {
                apples.Remove(apple);
                return apple;
            }
            return null;
        }
    }
}
