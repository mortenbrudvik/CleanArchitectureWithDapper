using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;

namespace Features;

public class FeaturesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var configuration = MediatRConfigurationBuilder
            .Create(typeof(FeaturesModule).Assembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .Build();
        
       builder.RegisterMediatR(configuration);

    }
}