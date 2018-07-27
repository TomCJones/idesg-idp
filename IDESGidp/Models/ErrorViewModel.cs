using System;

namespace IDESGidp.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorID { get; set; }
        public string ErrorMessage { get; set; }
        public bool ShowErrorID => !string.IsNullOrEmpty(ErrorID);
    }
}