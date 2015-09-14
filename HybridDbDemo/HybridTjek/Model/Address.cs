namespace HybridTjek.Model
{
    public class Address
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public override string ToString()
        {
            return $"{Street} {HouseNumber}, {PostalCode} {City}";
        }
    }
}