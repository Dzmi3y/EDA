using System.ComponentModel.DataAnnotations;

namespace EDA.Gateway.Contracts.Requests
{
    public class SignInRequest
    {
        [Required(ErrorMessageResourceName = "FieldRequiredError", ErrorMessageResourceType = typeof(Resource))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Resource))]
        [MinLength(1, ErrorMessageResourceName = "FieldEmptyError", ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "FieldRequiredError", ErrorMessageResourceType = typeof(Resource))]
        [MinLength(1, ErrorMessageResourceName = "FieldEmptyError", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }
    }
}
