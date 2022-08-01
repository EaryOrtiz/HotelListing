using AutoMapper;
using HotelListing.API.Configurations;
using HotelListing.API.Controllers;
using HotelListing.API.Data;
using HotelListing.API.IRepository;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net.Http.Json;

namespace HotelListing.Test.Remote
{
    public class CountryTest :IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly Mock<ICountryRepository> _countryRepositoryMock;
        private readonly CountriesController _countryController;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;


        public CountryTest(WebApplicationFactory<Program> factory)
        {
            var myProfile = new MapperConfig();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            _mapper = mapper;
            _countryRepositoryMock = new Mock<ICountryRepository>();
            _countryController = new CountriesController(_countryRepositoryMock.Object, _mapper);

            _httpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll<ICountryRepository>();
                    services.TryAddTransient( m => _countryRepositoryMock.Object);
                });

            }).CreateClient();

            _httpClient.BaseAddress = new System.Uri("https://localhost:7298");
        }

        [Fact]
        public async Task GetCountries_WhenCalled_ReturnAllCountries()
        {
            //Arange

            var requestPath = "/api/Countries";
            var countries = new List<Country>() { 
                new Country() { Name = "Mexico", ShortName = "MX" }, 
                new Country() { Name = "Canada", ShortName = "CN" }, 
                new Country() { Name = "United States Of America", ShortName = "USA" } 
            };
            _countryRepositoryMock.Setup(repo => repo.GetAllAsync())
                                .ReturnsAsync(countries)
                                .Verifiable();

            //Act
            var response  = await _httpClient.GetAsync(requestPath);
            //response.StatusCode.Should().
            var countriesResponse = await response.Content.ReadFromJsonAsync<List<Country>>();

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(countriesResponse.Count, countries.Count);
        }
    }
}