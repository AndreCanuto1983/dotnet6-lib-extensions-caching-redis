namespace Application.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        

        public bool IsValid()
        {
            return Id != Guid.Empty && string.IsNullOrEmpty(Name);
        }
    }
}
