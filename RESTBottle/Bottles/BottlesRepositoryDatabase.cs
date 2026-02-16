namespace RESTBottle.Bottles
{
    public class BottlesRepositoryDatabase : IBottlesRepository
    {
        private readonly BottlesDBContext _context;
        public BottlesRepositoryDatabase(BottlesDBContext context)
        {
            _context = context;
        }
        public Bottle AddBottle(Bottle bottle)
        {
            if (bottle == null)
            {
                throw new ArgumentNullException(nameof(bottle));
            }
            _context.Bottles.Add(bottle);
            _context.SaveChanges();
            return bottle;
        }
        public IEnumerable<Bottle> Get(int? volumeAtLeast = null, string? descriptionStartsWith = null, string? sortOrder = null)
        {
            return _context.Bottles;
        }
        public Bottle? GetById(int id)
        {
            return _context.Bottles.Find(id);
        }
        public Bottle? Remove(int id)
        {
            var bottle = _context.Bottles.Find(id);
            if (bottle != null)
            {
                _context.Bottles.Remove(bottle);
                _context.SaveChanges();
            }
            return bottle;
        }
        public Bottle? Update(int id, Bottle data)
        {
            var existingBottle = GetById(id);
            if (existingBottle != null)
            {
                existingBottle.Volume = data.Volume;
                existingBottle.Description = data.Description;
                _context.SaveChanges();
                return existingBottle;
            }
            return null;
        }
    }
}
