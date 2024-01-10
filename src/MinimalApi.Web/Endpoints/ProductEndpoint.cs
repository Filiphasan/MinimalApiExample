namespace MinimalApi.Web.Endpoints;

public static class ProductEndpoint
{
    private static readonly List<Product> ProductList = [];

    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var productGroup = app.MapGroup("/api/v1/products")
            .WithTags("Products v1");

        productGroup.MapGet("", HandleGetProductList);
        productGroup.MapGet("{id}", HandleGetProduct);
        productGroup.MapPost("", HandlePostProduct);
        productGroup.MapDelete("{id}", HandleDeleteProduct);

        return app;
    }

    private static IResult HandleGetProductList()
    {
        return Results.Ok(ProductList);
    }

    private static IResult HandleGetProduct(int id)
    {
        var product = ProductList.Find(p => p.Id == id);
        if (product is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(product);
    }

    private static IResult HandlePostProduct(Product product)
    {
        if (product is { Id: > 0 })
        {
            ProductList.Add(product);
            return Results.Ok(product);
        }
        else
        {
            return Results.BadRequest();
        }
    }

    private static IResult HandleDeleteProduct(int id)
    {
        var product = ProductList.Find(p => p.Id == id);
        if (product is null)
        {
            return Results.NotFound();
        }

        ProductList.Remove(product);
        return Results.Ok();
    }
}

public record Product(int Id, string Name, decimal Price);