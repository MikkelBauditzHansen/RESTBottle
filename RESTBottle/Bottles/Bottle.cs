namespace RESTBottle.Bottles
{
    public class Bottle
    {
        public int Id { get; set; }
        public int Volume { get; set; }
        public string? Description { get; set; }
        //'?' fortæller at string kan være null. 

        public override string ToString()
        {
            return $"Bottle ID: {Id}, Volume: {Volume}, Description: {Description}";
        }

    }
}
