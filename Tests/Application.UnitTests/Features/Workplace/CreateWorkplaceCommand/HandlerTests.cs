using Application.Features.WorkplaceFeature.Commands;
using Application.Interfaces;
using AutoMapper;
using Domain.Enums;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Features.Workplace.CommandTests
{
    public class HandlerTests : IClassFixture<BaseTestFixture>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _automapper;
        private readonly Validator _validator;
        public HandlerTests(BaseTestFixture baseFixture)
        {
            _context = baseFixture.Context;
            _automapper = baseFixture.Mapper;
            _validator = new Validator();
        }

        public static IEnumerable<object[]> ValidData
        {
            get
            {
                yield return new object[] { WorkplaceType.ShortTerm, 100, 75 };
                yield return new object[] { WorkplaceType.LongTerm, 75, 100 };
            }
        }

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task ShouldCreateWorkplace(WorkplaceType workplaceType, int workplaceNumber, int mapId)
        {
            var createCommand = new CreateWorkplaceCommandRequest
            {
                WorkplaceNumber = workplaceNumber,
                WorkplaceType = workplaceType,
                MapId = mapId
            };

            var handler = new CreateWorkplaceCommandHandler(_context, _automapper);

            var response = await handler.Handle(createCommand, CancellationToken.None);
            response.Id.Should().Be(_context.Workplaces.Where(x => x.MapId == mapId && x.WorkplaceNumber == workplaceNumber).First().Id);
        }

        
    }
}
