namespace CubosChallenge.DTOs
{
    public class AccountToReturnDTO
    {
        public Guid Id { get; private set; }
        public string Branch { get; private set; }
        public string AccountNumber { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public AccountToReturnDTO(Guid id, string branch, string accountNumber, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Branch = branch;
            AccountNumber = accountNumber;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

    }
}
