using MediatR;
using SmartStore.Application.Common.Interfaces; // الاعتماد على الـ Interface الجديد
using SmartStore.Domain.Entities;

namespace SmartStore.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string SKU,
    string? Description,
    decimal Price,
    int StockQuantity,
    Guid CategoryId) : IRequest<Guid>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    // استبدال الـ DbContext بالـ Interface النظيف
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            SKU = request.SKU,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            CategoryId = request.CategoryId
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
