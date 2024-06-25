using fisio.domain.Commands.Users;
using fisio.domain.Handlers.Users;
using fisio.domain.Commands.Common;
using fisio.domain.Repositories;
using fisio.test.FakeContexts;
using fisio.test.FakeRepositories;
using FluentAssertions;
using fisio.domain.Enums.User;
using fisio.domain.UnitOfWork;
using fisio.test.FakeUnitOfWorks;

namespace fisio.test.Handlers.Users
{
    public class CreateUserHandlerTest
    {
        private readonly IUserRepository _userRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserHandlerTest() {
            var context = FakeInMemoryContextFactory.Create();
            _userRepository = new FakeUserRepository(context);
            _patientRepository = new FakePatientRepository(context);
            _unitOfWork = new FakeUnitOfWork();
        }

        [Fact]
        public async Task Should_Create_User_With_Success()
        {
            var command = new CreateUserCommand("Felipe Lima", "42516423845", "", "M", "18997642032", "", DateTime.UtcNow.AddYears(-20), "Fullstack", "", "", "", "Married", "", "", "Av. Paulo Marcondes", "190", "Jd. Eldorado", "President Prud.", "SP", "19025000", "felipe.test@gmail.com", "silypassword", EUserType.Patient);
            var handler = new CreateUserHandler(_userRepository, _patientRepository, _unitOfWork);
            var result = (GenericCommandResult)await handler.Handle(command);

            result.Success.Should().BeTrue();
        }
    }
}
