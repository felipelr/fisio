using fisio.domain.Commands.Interfaces;
using fisio.domain.Enums.User;
using Flunt.Notifications;
using Flunt.Validations;

namespace fisio.domain.Commands.Users
{
    public class CreateUserCommand : Notifiable<Notification>, ICommand
    {
        public CreateUserCommand(string name, string document, DateTime dateBirth, string gender, string photo, string cellphone, string email, string password, EUserType userType)
        {
            Name = name;
            Document = document;
            DateBirth = dateBirth;
            Gender = gender;
            Photo = photo;
            Cellphone = cellphone;
            Email = email;
            Password = password;
            UserType = userType;
        }

        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime DateBirth { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EUserType UserType { get; set; }

        public void Validate()
        {
            AddNotifications(new CreateUserCommandContract(this));
        }
    }

    public class CreateUserCommandContract : Contract<CreateUserCommand>
    {
        public CreateUserCommandContract(CreateUserCommand command)
        {
            DateTime minDateBirth = DateTime.Now.AddYears(-18);
            Requires()
                .IsEmail(command.Email, "Email")
                .IsGreaterThan(command.Password, 8, "Password")
                .IsNotNullOrEmpty(command.Name, "Name")
                .IsNotNullOrEmpty(command.Document, "Document")
                .IsNotNullOrEmpty(command.Cellphone, "Cellphone")
                .IsLowerOrEqualsThan(command.DateBirth, minDateBirth, "DateBirth");
        }
    }
}