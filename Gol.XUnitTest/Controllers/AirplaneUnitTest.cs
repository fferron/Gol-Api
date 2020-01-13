using Gol.Entities;
using Gol.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Gol.XUnitTest
{
    public class AirplaneUnitTest
    {
        private IConfigurationRoot _configuration;
        private DbContextOptions<GolContext> _options;
        TestServer _host;
        HttpClient _client;

        public AirplaneUnitTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            
            //_options = new DbContextOptionsBuilder<GolContext>().UseSqlServer(_configuration.GetConnectionString("GolDB")).Options;
            //this.Seed(new GolContext(_configuration));

            _host = new TestServer(new WebHostBuilder()
                             .UseStartup<Startup>()
                             .UseSetting("ConnectionStrings:GolDB", _configuration.GetConnectionString("GolDB")));
            _client = _host.CreateClient();
        }

        [Fact]
        public async void GetAirplanes_Return_OkResult()
        {
            // Act  
            var response = await _client.GetAsync("api/airplanes/getallairplane");
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert 
            Assert.NotNull(response);
            Assert.Equal(200, (int)response.StatusCode);
        }

        [Fact]
        public async void InsertAirplane_Return_OkResult()
        {
            // Arrange
            var airplaneToAdd = new Airplane
            {
                AirplaneModel = "x456",
                NumberOfPassengers = 120,
            };
            var content = JsonConvert.SerializeObject(airplaneToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/airplanes/insertairplane", stringContent);
            var responseString = await response.Content.ReadAsStringAsync();
            var airplane = JsonConvert.DeserializeObject<Airplane>(responseString);

            //Assert 
            Assert.NotNull(response.Content);
            Assert.Equal(200, (int)response.StatusCode);
            Assert.NotEqual(0, airplane.ID);
        }

        [Fact]
        public async void FindAirplane_Return_OkResult()
        {
            // Arrange
            int idAirplane = 1;

            //Act  
            var response = await _client.GetAsync("api/airplanes/findairplane/" + idAirplane);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert 
            Assert.NotNull(response.Content);
            Assert.Equal(200, (int)response.StatusCode);
            var airplane = JsonConvert.DeserializeObject<Airplane>(responseString);
            Assert.Equal(idAirplane, airplane.ID);
        }

        [Fact]
        public async void DeleteAirplane_Return_OkResult()
        {
            // Arrange
            int idAirplane = 1;

            // Act
            var response = await _client.DeleteAsync("/api/airplanes/" + idAirplane);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert 
            Assert.Equal(204, (int)response.StatusCode);
            Assert.Equal("No Content", response.ReasonPhrase);
        }

        private void Seed(GolContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Airplanes.AddRange(
                new Airplane() { AirplaneModel = "Boeing 747", NumberOfPassengers = 130 },
                new Airplane() { AirplaneModel = "Embraer Legacy", NumberOfPassengers = 25 },
                new Airplane() { AirplaneModel = "Airbus 380", NumberOfPassengers = 250 },
                new Airplane() { AirplaneModel = "Boeing 777", NumberOfPassengers = 180 }
            );

            context.Passengers.AddRange(
                new Passenger() { Name = "Passenger 1", AirplaneID = 1, RegistryCreationDate = DateTime.Now },
                new Passenger() { Name = "Passenger 2", AirplaneID = 1, RegistryCreationDate = DateTime.Now },
                new Passenger() { Name = "Passenger 3", AirplaneID = 2, RegistryCreationDate = DateTime.Now },
                new Passenger() { Name = "Passenger 4", AirplaneID = 2, RegistryCreationDate = DateTime.Now },
                new Passenger() { Name = "Passenger 5", AirplaneID = 3, RegistryCreationDate = DateTime.Now },
                new Passenger() { Name = "Passenger 6", AirplaneID = 3, RegistryCreationDate = DateTime.Now }
            );
            context.SaveChanges();
        }
    }
}
