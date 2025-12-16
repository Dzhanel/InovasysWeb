namespace Inovasys.Data.Dto
{
    public class ApiUserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public ApiAddressDto Address { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public ApiCompanyDto Company { get; set; }
    }
}
