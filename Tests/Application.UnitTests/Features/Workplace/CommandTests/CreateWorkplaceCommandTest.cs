using Application.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Features.Workplace.CommandTests
{
    public class CreateWorkplaceCommandTest : IClassFixture<BaseTestFixture>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _automapper;
        public CreateWorkplaceCommandTest(BaseTestFixture baseFixture)
        {
            _context = baseFixture.Context;
            _automapper = baseFixture.Mapper;
        }

        [Fact]
        public async Task CreateWorkplace()
        {
            // Arrange
            // Act
            // Assert
        }
    }
}
