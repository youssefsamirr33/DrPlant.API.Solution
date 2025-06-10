using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Talabat.Core.Services.Contract
{
    public interface IPLantDiseasesService
    {
        Task<string> PredictDiseaseAsync(IFormFile imageFile);
    }
}
