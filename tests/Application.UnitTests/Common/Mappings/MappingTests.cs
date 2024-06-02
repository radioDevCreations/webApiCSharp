using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using FinalLabProject.Application.Boats.Queries.GetBoatsWithPagination;
using FinalLabProject.Application.Common.Interfaces;
using FinalLabProject.Application.Common.Models;
using FinalLabProject.Application.Harbours.Queries.GetTodos;
using FinalLabProject.Domain.Entities;
using NUnit.Framework;

namespace FinalLabProject.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(Harbour), typeof(HarbourDto))]
    [TestCase(typeof(Boat), typeof(BoatDto))]
    [TestCase(typeof(Harbour), typeof(LookupDto))]
    [TestCase(typeof(Boat), typeof(LookupDto))]
    [TestCase(typeof(Boat), typeof(BoatBriefDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
