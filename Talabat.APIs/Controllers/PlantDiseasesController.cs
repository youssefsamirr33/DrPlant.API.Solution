using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Entities.Diseases;
using Talabat.Core.Sepecifications.PlantDiseases_Specs;
using Talabat.Core.Services.Contract;
using Talabat.Infrastructure;

namespace Talabat.APIs.Controllers
{
    public class PlantDiseasesController : BaseApiController
    {
        private readonly IPLantDiseasesService _pLantDiseasesService;
        private readonly IUnitOfWork _unitOfWork;

        public PlantDiseasesController(IPLantDiseasesService pLantDiseasesService , IUnitOfWork unitOfWork  )
        {
            _pLantDiseasesService = pLantDiseasesService;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("predict")]
        public async Task<ActionResult<PlantDiseasesDto>> PredictDisease(IFormFile file)
        {
            try
            {
                string plantName = string.Empty;
                string diseaseName = string.Empty;

                if (file is null || file.Length == 0)
                    return BadRequest(new ApiResponse(400, "Please upload a valid image file."));

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extinsion = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extinsion))
                    return BadRequest(new ApiResponse(400, "Invalid file type. Only JPG, JPEG, and PNG are allowed."));

                var result = await _pLantDiseasesService.PredictDiseaseAsync(file);

                string pattern = @"""disease_name"":\s*""'(.+?)',""";
                Match match = Regex.Match(result.Response, pattern);

                if (match.Success)
                {
                    string diseaseNameRaw = match.Groups[1].Value; 

                
                    string[] parts = diseaseNameRaw.Split(" - ");
                    plantName = parts.Length > 0 ? parts[0].Trim() : "Unknown";
                    diseaseName = parts.Length > 1 ? parts[1].Trim() : "Unknown";

                }
                else
                {
                    BadRequest(new ApiResponse(400, "Failed to extract disease_name."));
                }


                var spec = new PlantDiseasesSpecifications(plantName, diseaseName.Replace(" " , "_"));
                var plant = await _unitOfWork.Repository<PlantDiseases>().GetByIdWithSpecAsync(spec);

                if (plant is null)
                    return BadRequest(new ApiResponse(400, "Null Reference Exeption"));


                return Ok(new PlantDiseasesDto
                {
                    Name = string.Concat(plant.plant_name , " - " , plant.disease_name) ,
                    Discreption = plant.description,
                    CareTips = plant.prevention_tips,
                    image = result.Image,
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Plants")]
        public async Task<IReadOnlyList<PlantDiseases>> GetAllPlants()
        {
            var plants = await _unitOfWork.Repository<PlantDiseases>().GetAllAsync();
            return (plants);
        }
    }
}
