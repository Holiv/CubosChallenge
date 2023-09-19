namespace CubosChallenge.DTOs
{
    public class TransactionToReturnDTO
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
