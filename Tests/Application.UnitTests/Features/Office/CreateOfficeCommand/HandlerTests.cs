using Xunit;
using Request = Application.Features.OfficeFeature.Commands.CreateOfficeCommandRequest;
using Response = Application.Features.OfficeFeature.Commands.CreateOfficeCommandResponse;
using Handler = Application.Features.OfficeFeature.Commands.CreateCommandHandler;
using Repository;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;

namespace Application.UnitTests.Features.Office.CreateOfficeCommand
{
    public class HandlerTests : IClassFixture<BaseTestFixture>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly Handler _handler;

        public HandlerTests(BaseTestFixture baseTestFixture)
        {
            _context = baseTestFixture.Context;
            _mapper = baseTestFixture.Mapper;
            _handler = new Handler(_context, _mapper);
        }

        [Fact]
        public async Task CreateOffice_WhenModelIsValid_ReturnNewOfficeId()
        {
            // Arrange
            var request = new Request
            {
                Name = "Tashkent Office",
                Country = "Uzbekistan",
                City = "Tashkent",
                Address = "73 Mirzo Ulugbek Avenue",
                HasFreeParking = true
            };
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Id.Should().BeGreaterThan(0);
        }


        
    }
}
