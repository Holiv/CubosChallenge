using Core.Entities;

namespace CubosChallenge.DTOs
{
    public class TransactionForCreationDTO
    {
        public double Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;
        public Guid AccountId { get; set; }

        public void AddAccountId(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
