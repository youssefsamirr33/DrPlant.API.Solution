using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public PlantDiseasesController(IPLantDiseasesService pLantDiseasesService , IUnitOfWork unitOfWork )
        {
            _pLantDiseasesService = pLantDiseasesService;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("predict")]
        public async Task<IActionResult> PredictDisease(IFormFile file)
        {
            try
            {
                if (file is null || file.Length == 0)
                    return BadRequest(new ApiResponse(400, "Please upload a valid image file."));

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extinsion = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extinsion))
                    return BadRequest(new ApiResponse(400, "Invalid file type. Only JPG, JPEG, and PNG are allowed."));

                var result =await _pLantDiseasesService.PredictDiseaseAsync(file);

                var plantName = result.Split("__")[0];
                var DiseasesName = result.Split("__")[1];

                var spec = new PlantDiseasesSpecifications(plantName.ToLower(), DiseasesName.ToLower());
                var plant = await _unitOfWork.Repository<PlantDiseases>().GetByIdWithSpecAsync(spec);

                


                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
