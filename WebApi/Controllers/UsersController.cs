using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Exceptions;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.CreateOrder;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Queries.GetOrderById;
using WebApi.CqrsMediatrFeatures.CqrsProducts.Queries.GetProductList;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class UsersController : BaseController
    {
        /// <summary>
        /// Gets the list of products(books)
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  GET /api/Users
        /// 
        /// </remarks>
        /// <returns>Returns ProductListReadDto(get all products)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If the request its bad</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ResponseCache(Duration = 300)]
        public async Task<IActionResult> Index()
        {
            var productListReadDto = await Mediator.Send(new GetProductListQuery());
          
            return Ok(productListReadDto.Products);
        }

        /// <summary>
        /// Gets the order by id
        /// </summary>
        /// <remarks>
        /// Semple request:
        /// 
        ///  GET /api/Users/GetOrderAsync/5
        /// 
        /// </remarks>
        /// <param name="id">Id of the  Order (int)</param>
        /// <returns>Returns OrderReadDto</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If the Order Id = 0 </response>
        /// <response code="404">If the Order Id is out of range</response>
        [HttpGet, Route("GetOrderAsync/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
          
            var orderReadDto = await Mediator.Send(new GetOrderByIdQuery() { OrderId = id });
            if (orderReadDto == null)
            {
                throw new NotFoundException(nameof(orderReadDto), id);
            }
            return Ok(orderReadDto);
        }

        /// <summary>
        /// Creates the purchase order a books 
        /// </summary>
        /// <param name="createOrderDto">CreateOrderDto object</param>
        /// <remarks>
        /// Semple request:
        /// 
        ///  POST /api/Users
        /// 
        /// </remarks>
        /// <returns>Returns OrderDto</returns>
        /// <response code="201">Success</response> 
        /// <response code="400">Bad Request(if model CreateOrderDto is not valid)</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrderAsync([FromBody]
        CreateOrderDto createOrderDto)
        { 
            var createOrderCommand = new CreateOrderCommand
            {
                Email = createOrderDto.Email,
                Name = createOrderDto.Name,
                Address = createOrderDto.Address,
                OrderDate = createOrderDto.OrderDate,
                Total = createOrderDto.Total,
                CartItems = createOrderDto.CartItems?.Select(co => new CartItem
                {
                    ProductId = co.ProductId,
                    Count = co.Count,
                }).ToList()
            };
            var ordrReadDto = await Mediator.Send(createOrderCommand);
          
            return new ObjectResult(ordrReadDto) { StatusCode = StatusCodes.Status201Created };
        }
    }
}
