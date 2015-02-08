namespace WebTorrent.Domain.Services.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, Email: {2}, Password: {3}, Role: {4}", Id, Name, Email, Password, Role);
        }
    }
}