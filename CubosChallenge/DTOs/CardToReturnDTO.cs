using Microsoft.AspNetCore.Http.HttpResults;

namespace CubosChallenge.DTOs
{
    public class CardToReturnDTO
    {
        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public string Number { get; private set; }
        public string Cvv { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public CardToReturnDTO(Guid id, string type, string number, string cvv, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Type = type;
            Number = number[^4..];
            Cvv = cvv;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
