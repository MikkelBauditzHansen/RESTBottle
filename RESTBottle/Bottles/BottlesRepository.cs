using System.Collections.Generic;
using System.Linq;

namespace RESTBottle.Bottles
{
    public class BottlesRepository
    {
        private List<Bottle> bottles = new List<Bottle>();
        private int nextId = 1;

        public BottlesRepository()
        {
            AddBottle(new Bottle { Volume = 500, Description = "Water Bottle"});
            AddBottle(new Bottle { Volume = 600, Description = "Wine Bottle" });
            AddBottle(new Bottle { Volume = 700, Description = "Soda Bottle" });
        }

        public IEnumerable<Bottle> Get(int? volumeAtLeast = null,
            string? descriptionStartsWith = null,
            string? sortOrder = null
            )
        {
            IEnumerable<Bottle> result = new List<Bottle>();
            if (volumeAtLeast != null)
            {
                result = result.Where(b => b.Volume >= volumeAtLeast);
            }
            if (descriptionStartsWith != null)
            {
                result = result.Where(b => b.Description != null &&
                 b.Description.StartsWith(descriptionStartsWith));
            }
            if (sortOrder != null)
            {
                switch (sortOrder)
                {
                    case "Volume":
                        return bottles.OrderBy(b => b.Volume);
                    case "volumeDesc":
                        return result.OrderByDescending(b => b.Volume);
                    case "desc":
                        return result.OrderBy(b => b.Description);
                }
            }
            return result;
        }

        public Bottle AddBottle(Bottle bottle)
        {
            bottle.Id = nextId++;
            bottles.Add(bottle);
            return bottle;
        }

        public Bottle? GetById(int id)
        {
            return bottles.FirstOrDefault(b => b.Id == id);
        }

        public Bottle? Remove(int id)
        {
            var bottle = GetById(id);
            if (bottle != null)
            {
                bottles.Remove(bottle);
                return bottle;
            }
            return null;
        }

        public Bottle? Update(int id, Bottle data)
        {
            var bottle = GetById(id);
            if (bottle != null)
            {
                bottle.Volume = data.Volume;
                bottle.Description = data.Description;
                return bottle;
            }
            return null;
        }
    }
}
