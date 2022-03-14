using Validator = Application.Features.OfficeFeature.Commands.CreateOfficeValidator;
using Request = Application.Features.OfficeFeature.Commands.CreateOfficeCommandRequest;
using Xunit;
using FluentValidation.TestHelper;

namespace Application.UnitTests.Features.Office.CreateOfficeCommand
{
    public class ValidatorTests
    {
        private readonly Validator _validator;

        public ValidatorTests()
        {
            _validator = new Validator();
        }

        [Fact]
        public void RuleForName__WhenNameIsNullOrEmpty_ShouldHaveValidator()
        {
            var model = new Request
            {
                Name = string.Empty
            };
            var result = _validator.TestValidate(model, opt => opt.IncludeProperties(x => x.Name));
            result.ShouldHaveValidationErrorFor(o => o.Name);
        }

        [Fact]
        public void RuleForCountry__WhenCountryIsNullOrEmpty_ShouldHaveValidator()
        {
            var model = new Request
            {
                Country = string.Empty
            };
            var result = _validator.TestValidate(model, opt => opt.IncludeProperties(x => x.Country));
            result.ShouldHaveValidationErrorFor(o => o.Country);
        }

        [Fact]
        public void RuleForCity__WhenCityIsNullOrEmpty_ShouldHaveValidator()
        {
            var model = new Request
            {
                City = string.Empty
            };
            var result = _validator.TestValidate(model, opt => opt.IncludeProperties(x => x.City));
            result.ShouldHaveValidationErrorFor(o => o.City);
        }

        [Fact]
        public void RuleForAddress__WhenAddressIsNullOrEmpty_ShouldHaveValidator()
        {
            var model = new Request
            {
                Address = string.Empty
            };
            var result = _validator.TestValidate(model, opt => opt.IncludeProperties(x => x.Address));
            result.ShouldHaveValidationErrorFor(o => o.Address);
        }

    }
}
