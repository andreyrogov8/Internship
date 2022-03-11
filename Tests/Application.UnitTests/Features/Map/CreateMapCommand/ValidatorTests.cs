using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator = Application.Features.MapFeature.Commands.CreateMapCommandValidator;
using Request = Application.Features.MapFeature.Commands.CreateMapCommandRequest;
using Xunit;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Features.Map.CreateMapCommand
{
    public class ValidatorTests
    {
        private readonly Validator _validator;

        public ValidatorTests()
        {
            _validator = new Validator();
        }

        [Fact]
        public void RuleForFloorNumber_WhenFloorNumberIsNullOrEmpty_ShouldHaveValidationError()
        {
            var model = new Request
            {
                FloorNumber = 0
            };
            var result = _validator.TestValidate(model, opt => opt.IncludeProperties(x => x.FloorNumber));
            result.ShouldHaveValidationErrorFor(x => x.FloorNumber);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void RuleForFloorNumber_FloorNumberIsNotEmpty_ShouldNotHaveValidationError(int value)
        {
            var model = new Request
            {
                FloorNumber = value
            };
            var result = _validator.TestValidate(model, opt => opt.IncludeProperties(x => x.FloorNumber));
            result.ShouldNotHaveValidationErrorFor(x => x.FloorNumber);
        }

        [Fact]
        public void RuleForOfficeId_WhenOfficeIdIsNullOrEmpty_ShouldHaveValidationError()
        {
            var model = new Request
            {
                OfficeId = 0
            };
            var result = _validator.TestValidate(model, opt => opt.IncludeProperties(x => x.OfficeId));
            result.ShouldHaveValidationErrorFor(x => x.OfficeId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void RuleForOfficeId_OfficeIdIsNotEmpty_ShouldNotHaveValidationError(int value)
        {
            var model = new Request
            {
                OfficeId = value
            };
            var result = _validator.TestValidate(model, opt => opt.IncludeProperties(x => x.OfficeId));
            result.ShouldNotHaveValidationErrorFor(x => x.OfficeId);
        }
    }
}
