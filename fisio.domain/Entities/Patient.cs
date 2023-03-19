
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fisio.domain.Entities
{
    public class Patient : Base
    {
        public Patient(string name, string document, string cellphone, DateTime dateBirth, bool active, string userId, User user)
        {
            Name = name;
            Document = document;
            Cellphone = cellphone;
            DateBirth = dateBirth;
            Active = active;
            UserId = userId;
            User = user;
        }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(255, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [Column("name")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(255, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [Column("document")]
        public string Document { get; private set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(255, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [Column("cellphone")]
        public string Cellphone { get; private set; }

        [Column("datebirth")]
        public DateTime DateBirth { get; private set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue(true)]
        [Column("active")]
        public bool Active { get; private set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Column("user_id")]
        [ForeignKey("User")]
        public string UserId { get; private set; }
        public User User { get; private set; }
    }
}
