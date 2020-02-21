using System.ComponentModel.DataAnnotations;

namespace Cameleon.Model
{
    public class Configuration
    {
        [Required(ErrorMessage = "The Data field is required.")]
        public string Data { get; set; }


        [Required(ErrorMessage = "The Num field is required.")]
        public int Num { get; set; }
    }
}
