using Carter;
using MinimalApi.Web.Endpoints;

namespace MinimalApi.Web.Modules;

public class ProductModule : ICarterModule
{
    private readonly List<Product> _productList = [];

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var productGroup = app.MapGroup("/api/v2/products")
            .WithTags("Products v2")
            .RequireAuthorization();

        productGroup
            .MapGet("", HandleGetProductList)
            .AllowAnonymous()
            .Produces<List<Product>>(200);
        productGroup
            .MapGet("{id}", HandleGetProduct)
            .AllowAnonymous()
            .Produces<Product>(200);
        productGroup.MapPost("", HandlePostProduct);
        productGroup.MapDelete("{id}", HandleDeleteProduct);
    }
    
    private IResult HandleGetProductList()
    {
        return Results.Ok(_productList);
    }

    private IResult HandleGetProduct(int id)
    {
        var product = _productList.Find(p => p.Id == id);
        if (product is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(product);
    }

    private IResult HandlePostProduct(Product product)
    {
        if (product is { Id: > 0 })
        {
            _productList.Add(product);
            return Results.Ok(product);
        }
        else
        {
            return Results.BadRequest();
        }
    }

    private IResult HandleDeleteProduct(int id)
    {
        var product = _productList.Find(p => p.Id == id);
        if (product is null)
        {
            return Results.NotFound();
        }

        _productList.Remove(product);
        return Results.Ok();
    }
}