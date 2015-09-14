namespace HybridTjek.Model
{
    public class OrderLine
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public override string ToString()
        {
            return $"{Quantity} x {ItemName}";
        }
    }
}