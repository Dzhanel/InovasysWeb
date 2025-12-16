namespace Inovasys.Data.Dto
{
    public class ApiAddressDto
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public ApiCoordinatesDto Geo { get; set; }
    }
}
