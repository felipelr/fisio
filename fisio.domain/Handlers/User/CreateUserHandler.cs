using Flunt.Notifications;
using fisio.domain.Commands.Interfaces;
using fisio.domain.Commands.Users;
using fisio.domain.Entities;
using fisio.domain.Handlers.Interfaces;
using fisio.domain.Repositories;
using fisio.domain.Commands.Common;
using fisio.domain.Enums.User;
using fisio.domain.UnitOfWork;

namespace fisio.domain.Handlers.Users
{
    public class CreateUserHandler : Notifiable<Notification>, IHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string[] roles = { "patient", "professional", "adm" };

        public CreateUserHandler(IUserRepository userRepository, IPatientRepository patientRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(CreateUserCommand command, CancellationToken cancellationToke = default)
        {
            command.Validate();
            if (!command.IsValid)
            {
                return new GenericCommandResult(
                    false,
                    "Não foi possível criar o usuário.",
                    command.Notifications
                );
            }

            string role = roles[(int)command.UserType];

            //criar usuario
            User user = new User(
                command.Email,
                command.Password,
                true,
                role
            );
            user.SetHashedPassword(BCrypt.Net.BCrypt.HashPassword(user.Password));
            _userRepository.Create(user);

            switch (command.UserType)
            {
                case EUserType.Patient:
                    Patient patient = new Patient(
                        command.Name,
                        command.Document,
                        command.Document2,
                        command.Gender,
                        command.Cellphone,
                        command.Photo,
                        command.DateBirth,
                        command.Workplace,
                        command.SpouseName,
                        command.FatherName,
                        command.MotherName,
                        command.MaritalStatus,
                        command.ResponsibleName1,
                        command.ResponsibleName2,
                        command.Street,
                        command.StreetNumber,
                        command.Neighborhood,
                        command.City,
                        command.State,
                        command.ZipCode,
                        true,
                        user.Id
                    );
                    _patientRepository.Create(patient);
                    break;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToke);

            return new GenericCommandResult(
                true,
                "Usuário criado com sucesso!",
                user
            );
        }
    }
}