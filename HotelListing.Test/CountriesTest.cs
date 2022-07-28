using AutoMapper;
using HotelListing.API.Configurations;
using HotelListing.API.Controllers;
using HotelListing.API.Data;
using HotelListing.API.DTOs.Countries;
using HotelListing.API.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HotelListing.Test
{
    public class CountriesTest
    {
        private readonly Mock<ICountryRepository> _countryRepositoryMock;
        private readonly CountriesController _countryController;
        private readonly IMapper _mapper;


        public CountriesTest()
        {
            var myProfile = new MapperConfig();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            _mapper = mapper;
            _countryRepositoryMock = new Mock<ICountryRepository>();
            _countryController = new CountriesController(_countryRepositoryMock.Object, _mapper);
        }


        [Fact]
        public void GetCountries_WhenCalled_ReturnOkResult()
        {
            //Act
            var okResult = _countryController.GetCountries().Result.Result;

            //Assert
            var countries = Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetCountries_WhenCalled_ReturnAllCountries()
        {
            _countryRepositoryMock.Setup(repo => repo.GetAllAsync())
                                  .ReturnsAsync(new  List<Country>() { new Country(), new Country() });

            //Act
            var okResult = _countryController.GetCountries().Result.Result as OkObjectResult;

            //Assert
            var countries = Assert.IsType<List<GetCountriesDTO>>(okResult.Value);
        }

        [Fact]
        public void Add_ValidObjectPassed_CreatedResponse()
        {
            //Arrange
            CreateCountryDTO country = new CreateCountryDTO()
            {
                Name = "Mexico",
                ShortName = "MX"
            };

            //Act
            _countryController.ModelState.AddModelError("Name", "Required");

            var createResponse = _countryController.PostCountry(country).Result.Result;

            //Assert
            Assert.IsType<CreatedAtActionResult>(createResponse);
        }

    }
}