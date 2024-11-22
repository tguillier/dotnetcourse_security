using ApiApplication1.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace ApiApplication1.Queries
{
    public class GetWeatherByDateQuery
    {
        [Required]
        [WeatherDateValidation]
        public DateOnly Date { get; set; }
    }
}
