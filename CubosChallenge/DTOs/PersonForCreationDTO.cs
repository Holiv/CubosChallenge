namespace CubosChallenge.DTOs
{
    public class PersonForCreationDTO
    {
        public string Name { get; private set; }
        public string Document { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public PersonForCreationDTO(string name, string document, string password)
        {
            Name = name;
            Document = new String(document.Where(Char.IsDigit).ToArray());
            Password = password;
            CreatedAt = DateTime.Now; 
            UpdatedAt = DateTime.Now;
        }

    }
}
