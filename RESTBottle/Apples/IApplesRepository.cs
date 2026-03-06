
namespace RESTBottle.Apples
{
    public interface IApplesRepository
    {
        Apple AddApple(Apple apple);
        IEnumerable<Apple> Get(int? minimumweight, int? maximumweight);
        Apple? GetById(int id);
        Apple? Update(int id, Apple updatedApple);
    }
}