namespace DoAnCuoiKy.Model
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string id { get; set; } = Guid.NewGuid().ToString();
    }
}
