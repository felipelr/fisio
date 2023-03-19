
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fisio.domain.Entities
{
    public class User : Base
    {
        public User(string email, string password, bool active, string role)
        {
            Email = email;
            Password = password;
            Active = active;
            Role = role;
        }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(255, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [Column("email")]
        public string Email { get; private set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(255, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [Column("password")]
        public string Password { get; private set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue(true)]
        [Column("active")]
        public bool Active { get; private set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(255, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [Column("role")]
        public string Role { get; private set; }

        public void SetHashedPassword(string passwordHashed)
        {
            Password = passwordHashed;
        }
    }
}
