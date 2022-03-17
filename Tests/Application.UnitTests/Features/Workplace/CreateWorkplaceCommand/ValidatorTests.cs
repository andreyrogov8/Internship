using Application.Features.WorkplaceFeature.Commands;
using Application.Interfaces;
using AutoMapper;
using Domain.Enums;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Features.Workplace.CreateWorkplaceCommand
{
    public class ValidatorTests : IClassFixture<BaseTestFixture>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _automapper;
        private readonly Validator _validator;

        public ValidatorTests(BaseTestFixture baseFixture)
        {
            _context = baseFixture.Context;
            _automapper = baseFixture.Mapper;
            _validator = new Validator();
        }

        public static IEnumerable<object[]> InvalidData
        {
            get
            {
                yield return new object[] { null, 100, 75 };
                yield return new object[] { WorkplaceType.ShortTerm, 0, 100 };
                yield return new object[] { WorkplaceType.LongTerm, 100, 0 };
            }
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async void ShouldRequireMinimumFields(WorkplaceType workplaceType, int workplaceNumber, int mapId)
        {
            var createCommand = new CreateWorkplaceCommandRequest
            {
                WorkplaceNumber = workplaceNumber,
                WorkplaceType = workplaceType,
                MapId = mapId
            };

            var result = _validator.TestValidate(createCommand);
            result.ShouldHaveAnyValidationError();
        }
    }
}
