using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IDESGidp.Models.ManageViewModels
{
    public class EnableWebAuthNViewModel
    {
        public string UserName { get; set; }
        public string AppId { get; set; }
        public string Challenge { get; set; }
        public string Version { get; set; }

        [Required]
        [Display(Name = "JSON Data Request")]
        public string jsonData { get; set; }

        [Display(Name = "Token Response")]
        public string TokenResponse { get; set; }
    }
}
