using System.ComponentModel.DataAnnotations;

namespace Cameleon.Model
{
    public class Configuration
    {
        [Required(ErrorMessage = "The path field is required.")]
        public string Path { get; set; }

        [Required(ErrorMessage = "The method field is required.")]
        public string Method { get; set; }

        public string Body { get; set; }

        [Required(ErrorMessage = "The httpCode field is required.")]
        public int HttpCode { get; set; }
    }
}
