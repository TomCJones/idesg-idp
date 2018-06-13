using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IDESGidp.Models.ManageViewModels
{
    public class ConsentReceiptViewModel
    {
        public string Username { get; set; }

        public string CrGuid { get; set; }

        public string Insert { get; set; }

        public string Json { get; set; }
    }
}