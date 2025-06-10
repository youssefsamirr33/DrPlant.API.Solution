using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Diseases
{
    public class PlantDiseases : BaseEntity
    {
        public string plant_name { get; set; } = null!;
        public string disease_name { get; set; } = null!;
        public string description { get; set; } = null!;
        public string prevention_tips { get; set; } = null!;
        public string treatment_methods { get; set; } = null!;
        public string organic_options { get; set; } = null!;
        public string chemical_options { get; set; } = null!;
        public string diagnostic_images_url { get; set; } = null!;
    }
}
