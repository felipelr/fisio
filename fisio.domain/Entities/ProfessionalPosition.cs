
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fisio.domain.Entities
{
    public class ProfessionalPosition : Base
    {
        public ProfessionalPosition(string description)
        {
            Description = description;
        }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(255, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [Column("description")]
        public string Description { get; private set; }
    }
}
