using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Diseases;

namespace Talabat.Core.Sepecifications.PlantDiseases_Specs
{
    public class PlantDiseasesSpecifications : BaseSepecifications<PlantDiseases>
    {
        public PlantDiseasesSpecifications(string plantName , string diseasesName)
            :base(p => string.IsNullOrEmpty(plantName) || string.IsNullOrEmpty(diseasesName) || 
                  (p.plant_name.ToLower().Contains(plantName) && p.disease_name.ToLower().Contains(diseasesName)))
        {
            
        }
    }
}
