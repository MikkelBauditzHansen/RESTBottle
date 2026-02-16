
namespace RESTBottle.Bottles
{
    public interface IBottlesRepository
    {
        Bottle AddBottle(Bottle bottle);
        IEnumerable<Bottle> Get(int? volumeAtLeast = null, string? descriptionStartsWith = null, string? sortOrder = null);
        Bottle? GetById(int id);
        Bottle? Remove(int id);
        Bottle? Update(int id, Bottle data);
    }
}