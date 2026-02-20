namespace RESTBottle.Apples
{
    public class Apple
    {
        public int Id { get; set; }
        public string? Variety { get; set; }
        public string? Description { get; set; }
        public override string ToString()
        {
            return $"Apple ID: {Id}, Variety: {Variety}, Description: {Description}";
        }
    }
}
