using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartStore.Application.Features.Products.Commands.CreateProduct;

namespace SmartStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    // حقن الـ Mediator فقط لإرسال الـ Commands والـ Queries
    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var productId = await _mediator.Send(command);

        return Ok(new { Id = productId, Message = "Product created successfully via CQRS!" });
    }
}
