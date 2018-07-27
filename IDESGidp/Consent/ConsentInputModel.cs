// Copyright (c) tom jones

using System.Collections.Generic;

namespace IDESGidp.Models
{
    public class ConsentInputModel
    {
        public string Button { get; set; }
        public IEnumerable<string> ScopesConsented { get; set; }
        public bool RememberConsent { get; set; }
        public bool GetReceipt { get; set; }
        public string ReturnUrl { get; set; }
    }
}