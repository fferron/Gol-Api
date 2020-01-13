using Gol.Entities;
using Gol.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Gol.XUnitTest.Controllers
{
    public class PassengerUnitTest 
    {
        private IConfigurationRoot _configuration;
        private DbContextOptions<GolContext> _options;
        TestServer _host;
        HttpClient _client;

        public PassengerUnitTest()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            _host = new TestServer(new WebHostBuilder()
                             .UseStartup<Startup>()
                             .UseSetting("ConnectionStrings:GolDB", _configuration.GetConnectionString("GolDB")));
            _client = _host.CreateClient();
        }

        [Fact]
        public async void ListAllPassenger_By_Airplane_Return_OkResult()
        {
            // arrange
            int idAirplane = 1;

            // Act  
            var response = await _client.GetAsync("api/Passengers/listallPassengerbyairplane/" + idAirplane);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert 
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
            
        }

        [Fact]
        public async void InsertPassenger_Return_OkResult()
        {
            // Arrange
            var PassengerToAdd = new Passenger
            {
                Name = "Passenger 1",
                AirplaneID = 1,
            };
            var content = JsonConvert.SerializeObject(PassengerToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Passengers/insertPassenger", stringContent);
            var responseString = await response.Content.ReadAsStringAsync();
            var Passenger = JsonConvert.DeserializeObject<Passenger>(responseString);

            //Assert 
            Assert.NotNull(response.Content);
            Assert.Equal(200, (int)response.StatusCode);
            Assert.NotEqual(0, Passenger.ID);
        }

        [Fact]
        public async void InsertPassengerToAirplane_Return_OkResult()
        {
            // Arrange
            var PassengerToAdd = new Passenger
            {
                ID = 1,
                AirplaneID = 1,
            };
            var content = JsonConvert.SerializeObject(PassengerToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Passengers/insertPassengertoairplane", stringContent);
            var responseString = await response.Content.ReadAsStringAsync();
            var Passenger = JsonConvert.DeserializeObject<Passenger>(responseString);

            //Assert 
            Assert.NotNull(response.Content);
            Assert.Equal(200, (int)response.StatusCode);
            Assert.NotEqual(0, Passenger.ID);
        }

        [Fact]
        public async void ChangePassenger_Return_OkResult()
        {
            // Arrange
            int idPassenger = 1;
            int NewAirplaneId = 1;

            // Act
            string uri = string.Format("/api/Passengers/changePassenger/{0}/{1}", idPassenger, NewAirplaneId);
            var response = await _client.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();
            var Passenger = JsonConvert.DeserializeObject<Passenger>(responseString);

            //Assert 
            Assert.NotNull(response.Content);
            Assert.Equal(200, (int)response.StatusCode);
            Assert.NotEqual(0, Passenger.ID);
        }
    }
}
