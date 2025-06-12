using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Sepecifications.PlantDiseases_Specs;
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
        public async Task<PlantDiseasesToReturnObject> PredictDiseaseAsync(IFormFile imageFile)
        {
            try
            {
                var requestUrl = "https://2871-34-82-202-79.ngrok-free.app/predict"; // URL For Flask Api for detection

                using var content = new MultipartFormDataContent();
                using var memoryStream = new MemoryStream();

                await imageFile.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                string base64String = Convert.ToBase64String(fileBytes);
                string imageDataUrl = $"data:{imageFile.ContentType};base64,{base64String}";


                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(imageFile.ContentType);
                content.Add(fileContent, "file", imageFile.FileName);
                var response = await _httpClient.PostAsync(requestUrl, content);
                response.EnsureSuccessStatusCode();

                var returnData = new PlantDiseasesToReturnObject
                {
                    Response = await response.Content.ReadAsStringAsync(),
                    Image = imageDataUrl
                };

                return returnData;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error calling prediction service", ex);
            }
        }
    }
}
