using Application.UnitTests.Infrastructure;
using AutoMapper;
using Repository;
using System;
using Xunit;

namespace Application.UnitTests;

public class BaseTestFixture : IDisposable
{
    public ApplicationDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public BaseTestFixture()
    {
        Context = AppDbContextFactory.Create();
        Mapper = AutoMapperFactory.Create();
        AppDbContextSeeds.SeedDataAsync(Context);
    }

    public void Dispose()
    {
        AppDbContextFactory.Destroy(Context);
    }
}