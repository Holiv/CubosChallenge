using System.Reflection.Metadata;

namespace CubosChallenge.DTOs
{
    public class CardForCreationDTO
    {
        public string Type { get; private set; }
        public string Number { get; private set; }
        public string Cvv { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public Guid PersonId { get; private set; }
        public Guid AccountId { get; private set; }

        public CardForCreationDTO(string type, string number, string cvv)
        {
            Type = type.ToLower();
            Number = new String(number.Where(Char.IsDigit).ToArray());
            Cvv = cvv;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public void AddOwnerId(Guid personId, Guid accountId)
        {
            PersonId = personId;
            AccountId = accountId;
        }

       
    }
}
