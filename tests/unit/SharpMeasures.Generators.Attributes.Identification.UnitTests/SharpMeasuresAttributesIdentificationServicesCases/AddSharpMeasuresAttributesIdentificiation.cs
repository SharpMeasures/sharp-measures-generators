namespace SharpMeasures.Generators.Attributes.Identification.SharpMeasuresAttributesIdentificationServicesCases;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Moq;

using System;

using Xunit;

public sealed class AddSharpMeasuresAttributesIdentificiation
{
    private static IServiceCollection Target(IServiceCollection services) => SharpMeasuresAttributesIdentificationServices.AddSharpMeasuresAttributesIdentification(services);

    private IServiceProvider ServiceProvider { get; }

    public AddSharpMeasuresAttributesIdentificiation()
    {
        HostBuilder host = new();

        host.ConfigureServices(static (services) => Target(services));

        ServiceProvider = host.Build().Services;
    }

    [Fact]
    public void NullServiceCollection_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidServiceCollection_ReturnsSameServiceCollection()
    {
        var serviceCollection = Mock.Of<IServiceCollection>();

        var actual = Target(serviceCollection);

        Assert.Same(serviceCollection, actual);
    }

    [Fact]
    public void IAttributeFilter_ServiceCanBeResolved() => ServiceCanBeResolved<IAttributeFilter>();

    [Fact]
    public void IAttributeIdentifier_ServiceCanBeResolved() => ServiceCanBeResolved<IAttributeIdentifier>();

    [AssertionMethod]
    private void ServiceCanBeResolved<TService>() where TService : notnull
    {
        var service = ServiceProvider.GetRequiredService<TService>();

        Assert.NotNull(service);
    }
}
