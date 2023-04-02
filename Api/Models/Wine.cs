using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("Wine")]
    public class Wine
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? CountryCode { get; set; }

        public int Type { get; set; }

        public DateTime? Year { get; set; }
    }
}
