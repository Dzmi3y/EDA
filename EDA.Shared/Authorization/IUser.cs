namespace EDA.Shared.Authorization
{
    public interface IUser
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
    }
}
