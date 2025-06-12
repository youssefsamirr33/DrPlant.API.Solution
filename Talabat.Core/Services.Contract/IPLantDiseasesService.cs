using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Sepecifications.PlantDiseases_Specs;


namespace Talabat.Core.Services.Contract
{
    public interface IPLantDiseasesService
    {
        Task<PlantDiseasesToReturnObject> PredictDiseaseAsync(IFormFile imageFile);
    }
}
