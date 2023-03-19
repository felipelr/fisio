using Flunt.Notifications;
using fisio.domain.Commands.Interfaces;
using fisio.domain.Commands.Users;
using fisio.domain.Entities;
using fisio.domain.Handlers.Interfaces;
using fisio.domain.Repositories;
using fisio.domain.Commands.Common;
using fisio.domain.Enums.User;

namespace fisio.domain.Handlers.Users
{
    public class CreateUserHandler : Notifiable<Notification>, IHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly string[] roles = { "patient", "professional", "adm" };

        public CreateUserHandler(IUserRepository userRepository, IPatientRepository patientRepository)
        {
            _userRepository = userRepository;
            _patientRepository = patientRepository;
        }

        public async Task<ICommandResult> Handle(CreateUserCommand command)
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
            await _userRepository.Create(user);

            switch (command.UserType)
            {
                case EUserType.Patient:
                    Patient patient = new Patient(
                        command.Name,
                        command.Document,
                        command.Cellphone,
                        command.DateBirth,
                        true,
                        user.Id,
                        user
                    );
                    await _patientRepository.Create(patient);
                    break;
            }

            return new GenericCommandResult(
                true,
                "Usuário criado com sucesso!",
                user
            );
        }
    }
}