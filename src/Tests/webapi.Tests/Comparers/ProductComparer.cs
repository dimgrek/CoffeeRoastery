using CoffeeRoastery.BLL.Interface.Dto;
using CoffeeRoastery.DAL.Domain.Models;
using FluentAssertions;
using FluentAssertions.Equivalency;

namespace webapi.Tests.Comparers;

public class ProductComparer : IEquivalencyStep
{
    public EquivalencyResult Handle(Comparands comparands, IEquivalencyValidationContext context,
        IEquivalencyValidator nestedValidator)
    {
        var project = (Product) comparands.Subject;
        var projectResponse = (ProductResponse) comparands.Expectation;

        project.Id.Should().Be(projectResponse.Id);
        project.Name.Should().Be(projectResponse.Name);
        project.CountryOfOrigin.Should().Be(projectResponse.CountryOfOrigin);

        return EquivalencyResult.AssertionCompleted;
    }
}