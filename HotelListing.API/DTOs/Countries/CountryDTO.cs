using HotelListing.API.DTOs.Hotels;

namespace HotelListing.API.DTOs.Countries
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public List<HotelDTO> Hotels { get; set; }
    }
}
