using fisio.domain.Commands.Interfaces;
using fisio.domain.Enums.User;
using Flunt.Notifications;
using Flunt.Validations;

namespace fisio.domain.Commands.Users
{
    public class CreateUserCommand : Notifiable<Notification>, ICommand
    {
        public CreateUserCommand(string name, string document, string document2, string gender, string cellphone, string photo, DateTime dateBirth, string workplace, string spouseName, string fatherName, string motherName, string maritalStatus, string responsibleName1, string responsibleName2, string street, string streetNumber, string neighborhood, string city, string state, string zipCode, string email, string password, EUserType userType)
        {
            Name = name;
            Document = document;
            Document2 = document2;
            Gender = gender;
            Cellphone = cellphone;
            Photo = photo;
            DateBirth = dateBirth;
            Workplace = workplace;
            SpouseName = spouseName;
            FatherName = fatherName;
            MotherName = motherName;
            MaritalStatus = maritalStatus;
            ResponsibleName1 = responsibleName1;
            ResponsibleName2 = responsibleName2;
            Street = street;
            StreetNumber = streetNumber;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ZipCode = zipCode;
            Email = email;
            Password = password;
            UserType = userType;
        }

        public string Name { get; set; }
        public string Document { get; set; }
        public string Document2 { get; set; }
        public string Gender { get; set; }
        public string Cellphone { get; set; }
        public string Photo { get; private set; }
        public DateTime DateBirth { get; set; }
        public string Workplace { get; set; }
        public string SpouseName { get; private set; }
        public string FatherName { get; private set; }
        public string MotherName { get; private set; }
        public string MaritalStatus { get; private set; }
        public string ResponsibleName1 { get; private set; }
        public string ResponsibleName2 { get; private set; }
        public string Street { get; private set; }
        public string StreetNumber { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
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