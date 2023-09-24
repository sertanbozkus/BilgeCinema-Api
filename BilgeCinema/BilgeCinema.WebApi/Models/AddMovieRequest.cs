namespace BilgeCinema.WebApi.Models
{
    public class AddMovieRequest
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Director { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
