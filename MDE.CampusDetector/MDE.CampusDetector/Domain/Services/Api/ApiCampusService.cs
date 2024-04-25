using MDE.CampusDetector.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MDE.CampusDetector.Domain.Services.Api
{
    public class ApiCampusService: ICampusService
    {
        private const string dataUrl = "https://raw.githubusercontent.com/howest-gp-mde/cu-xamarin-campusdetector/master/data";
        private readonly HttpClient httpClient;
        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public ApiCampusService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Campus>> GetAllCampuses()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{dataUrl}/campuses.json");
            if (response.IsSuccessStatusCode)
            {
                //get and parse json
                string jsonContent = await response.Content.ReadAsStringAsync();
                List<CampusDto> campusDtos =
                        JsonSerializer.Deserialize<List<CampusDto>>(jsonContent, jsonSerializerOptions);

                // map CampusDto objects to Campus objects
                return campusDtos.Select(dto => new Campus
                {
                    Latitude = dto.Coordinates[0],
                    Longitude = dto.Coordinates[1],
                    Name = dto.Description,
                    PhotoUrl = $"{dataUrl}/images/{dto.Image}"
                });
            }
            else
            {
                throw new Exception($"Failed to fetch data. Status code: {response.StatusCode}");
            }
        }
    }
}
