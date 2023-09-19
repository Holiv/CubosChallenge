namespace CubosChallenge.DTOs
{
    public class PersonToReturnDTO
    {
        public PersonToReturnDTO(Guid id, string name, string document, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Document = document;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;

        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
