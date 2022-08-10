
using System.ComponentModel.DataAnnotations;

namespace Application.Persons.Dtos
{
    public class PersonDto
    {
        //[Required]
        public int PersonId { get; set; }

        //[Required(AllowEmptyStrings = false)]
        //[MaxLength(30)]
        public string Name { get; set; } = null!;

        //[Required(AllowEmptyStrings = false)]
        //[MaxLength(30)]
        public string Family { get; set; } = null!;
    }
}
