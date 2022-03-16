using Application.Interfaces;
using AutoMapper;
using Handler = Application.Features.MapFeature.Commands.CreateMapCommandHandler;
using Request = Application.Features.MapFeature.Commands.CreateMapCommandRequest;
using Response = Application.Features.MapFeature.Commands.CreateMapCommandResponse;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using MediatR;
using FluentAssertions;
using System;
using Application.Exceptions;
using System.Linq;

namespace Application.UnitTests.Features.Map.CreateMapCommand
{
    public class HandlerTests : IClassFixture<BaseTestFixture>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _automapper;

        private readonly IRequestHandler<Request, Response> _handler;


        public HandlerTests(BaseTestFixture baseFixture)
        {
            _context = baseFixture.Context;
            _automapper = baseFixture.Mapper;
            _handler = new Handler(_automapper, _context);
        }

        [Fact]
        public async Task CreateMap_WhenModelIsValid_ReturnsNewMapId()
        {
            // Arrange
            var map = _context.Maps.FirstOrDefault(x => x.FloorNumber.Equals(1) && x.OfficeId.Equals(1));
            Assert.Null(map);

            var request = new Request
            {
                FloorNumber =  1, 
                HasKitchen = true,
                HasMeetingRoom = true,
                OfficeId = 1
            };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);            
            map = _context.Maps.FirstOrDefault(x => x.FloorNumber.Equals(1) && x.OfficeId.Equals(1));

            // Assert
            result.Id.Should().Be(map.Id);    
            request.HasKitchen.Should().Be(map.HasKitchen);
            request.HasMeetingRoom.Should().Be(map.HasMeetingRoom);

        }

        [Fact]
        public async Task CreateMap_WhenModelIsNotValid_ReturnsNewMapId()
        {
            // Arrange
            var request = new Request
            {
                FloorNumber = 1,
                HasKitchen = true,
                HasMeetingRoom = true,
                OfficeId = 5
            };
            // Act
            Func<Task> result = async () =>
                    await _handler.Handle(request, CancellationToken.None);
            // Assert
            await result.Should().ThrowAsync<NotFoundException>();
        }
    }
}
