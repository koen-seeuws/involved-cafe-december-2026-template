using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace MyToDoList.Presentation.WebApi.Security;

/// <summary>
/// Documents the <c>X-User</c> header on the endpoints that act on the current user's to-do list,
/// so it can be filled in from Swagger UI.
/// </summary>
public sealed class UserHeaderOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken ct)
    {
        var path = context.Description.RelativePath ?? string.Empty;

        if (!path.StartsWith("api/me", StringComparison.OrdinalIgnoreCase))
            return Task.CompletedTask;

        operation.Parameters ??= [];
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = HttpUserContext.UserHeader,
            In = ParameterLocation.Header,
            Required = true,
            Description = "Name of the current user (stand-in until real authentication is added).",
            Schema = new OpenApiSchema { Type = JsonSchemaType.String }
        });

        return Task.CompletedTask;
    }
}
