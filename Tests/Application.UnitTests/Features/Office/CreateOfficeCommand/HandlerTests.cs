using Xunit;
using Request = Application.Features.OfficeFeature.Commands.CreateOfficeCommandRequest;
using Response = Application.Features.OfficeFeature.Commands.CreateOfficeCommandResponse;
using Handler = Application.Features.OfficeFeature.Commands.CreateCommandHandler;
using Repository;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;
using System.Linq;

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
            var office = _context.Offices.FirstOrDefault(o =>
            o.Name == "Toshkent Office" &&
            o.Country == "O'zbekiston" &&
            o.City == "Toshkent" &&
            o.Address == "73 Mirzo Ulugbek Avenue" &&
            o.HasFreeParking == true
            );
            Assert.Null(office);

            var request = new Request
            {
                Name = "Toshkent Office",
                Country = "O'zbekiston",
                City = "Toshkent",
                Address = "73 Mirzo Ulugbek Avenue",
                HasFreeParking = true
            };
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);
            office = _context.Offices.FirstOrDefault(o =>
            o.Name == "Toshkent Office" &&
            o.Country == "O'zbekiston" &&
            o.City == "Toshkent" &&
            o.Address == "73 Mirzo Ulugbek Avenue" &&
            o.HasFreeParking == true
            );
            // Assert

            result.Id.Should().Be(office.Id);
            request.Name.Should().Be(office.Name);  
            request.City.Should().Be(office.City);
            request.Country.Should().Be(office.Country);
            request.Address.Should().Be(office.Address);
            request.HasFreeParking.Should().BeTrue();
        }



    }
}
