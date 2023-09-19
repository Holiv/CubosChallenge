namespace CubosChallenge.DTOs
{
    public class AccountForCreationDTO
    {
        public string Branch { get; private set; }
        public string AccountNumber { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public AccountForCreationDTO(string branch, string accountNumber)
        {
            Branch = branch;
            AccountNumber = accountNumber;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
