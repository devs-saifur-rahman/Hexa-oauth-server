namespace Hexa.Web.Models.oatuh
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public List<User> Userlist { get; } = new();

    }
}
                                                                                                                                                                                               