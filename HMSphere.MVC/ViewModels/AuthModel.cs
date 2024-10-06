using System.Text.Json.Serialization;

namespace HMSphere.MVC.ViewModels
{

        public class AuthModel
        {

            public string? Message { get; set; }
            public bool IsAuthenticated { get; set; } = false;
            public string? UserName { get; set; }
            public string? Email { get; set; }
            public List<string>? Roles { get; set; }
            public string? Token { get; set; }

        
    }
}
