using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Services.Contract;

namespace Talabat.Application.PlantDiseasesService
{
    public class PlantDiseasesServices : IPLantDiseasesService
    {
        private readonly HttpClient _httpClient;

        public PlantDiseasesServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> PredictDiseaseAsync(IFormFile imageFile)
        {
            try
            {
                var requestUrl = "http://localhost:8000/predict"; // URL For Flask Api for detection

                using var content = new MultipartFormDataContent();
                using var memoryStream = new MemoryStream();

                await imageFile.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(imageFile.ContentType);

                content.Add(fileContent, "file", imageFile.FileName);

                var response = await _httpClient.PostAsync(requestUrl, content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error calling prediction service", ex);
            }
        }
    }
}
