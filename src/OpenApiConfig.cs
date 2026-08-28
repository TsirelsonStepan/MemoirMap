using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;

public static class OpenApiConfig
{
    public static IServiceCollection AddAuthorizationToOpenApiEndpoints(this IServiceCollection services)
    {
        services.AddOpenApi(options => {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Components ??= new OpenApiComponents();

                document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                document.Components.SecuritySchemes["Bearer"] =
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT"
                    };

                return Task.CompletedTask;
            });

            options.AddOperationTransformer((operation, context, cancellationToken) =>
            {
                var requiresAuthorization = context.Description.ActionDescriptor.EndpointMetadata.OfType<IAuthorizeData>().Any();

                if (requiresAuthorization)
                {
                    operation.Security ??= [];
                    
                    operation.Security.Add(
                        new OpenApiSecurityRequirement
                        {
                            [new OpenApiSecuritySchemeReference("Bearer", context.Document)] = []
                        });
                }

                return Task.CompletedTask;
            });
        });
        return services;
    }
}