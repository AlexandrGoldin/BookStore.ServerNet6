using Application.ApplicationDTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common.Exceptions;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Commands.DeleteOrder;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Queries.GetOrderById;
using WebApi.CqrsMediatrFeatures.CqrsOrders.Queries.GetOrderList;
using WebApi.CqrsMediatrFeatures.CqrsProducts.Queries.GetProductById;
using WebApi.CqrsMediatrFeatures.CqrsProducts.Queries.GetProductList;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class AdminController : BaseController
    {

        /// <summary>
        /// Gets the list of orders
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  GET /api/Admin
        /// 
        /// </remarks>
        /// <returns>Returns List OrderReadDto(get all orders)</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If the request is bad</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet, Route("Index")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ResponseCache(Duration = 300)]
        public async Task<IActionResult> Index()
        {
            var orderListReadDto = await Mediator.Send(new GetOrderListQuery());
          
            return Ok(orderListReadDto);
        }

        /// <summary>
        /// Gets the order by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  GET /api/Admin/GetOrderAsync/5
        /// 
        /// </remarks>
        /// <param name="id">Id of the OrderReadDto (int)</param>
        /// <returns>Returns OrderVm</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If Order Id is zero</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If Order Id is out of range</response>
        [HttpGet, Route("GetOrderAsync/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// Gets the list of all products(books)
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  GET /api/Admin
        /// 
        /// </remarks>
        /// <returns>Returns list ProductReadDto</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If the request is bad</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllProductsAsync()
        {

            var productListVm = await Mediator.Send(new GetProductListQuery());
           
            List<ProductReadDto> productListDto = productListVm.Products!;
            return Ok(productListDto);
        }

        /// <summary>
        /// Gets the product by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  GET /api/Admin/5
        /// 
        /// </remarks>
        /// <param name="id">Id of the Product (int)</param>
        /// <returns>Returns ProductReadDto</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If Product Id is out of range (or zero)</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProductAsync(int id)
        {          
            var productReadDto = await Mediator.Send(new GetProductByIdQuery { ProductId = id });
            if (productReadDto == null)
            {
                throw new NotFoundException(nameof(productReadDto), id);
            }
            return Ok(productReadDto);
        }

        /// <summary>
        /// Deletes the order by id
        /// </summary>
        /// <remarks>
        /// Semple request:
        /// 
        ///  DELETE /api/Admin/DeleteOrder/5
        ///  
        /// </remarks>
        /// <param name="id">Id of the Order (int)</param>
        /// <returns>NoContent</returns>
        /// <response code="204">NoContent</response>
        ///<response code="404">If Product Id is out of range or zero</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="400">If the request is bad</response>
        [HttpDelete, Route("DeleteOrder/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(int id)
        {            
            var deleteOrderDto = await Mediator.Send(new DeleteOrderCommand { OrderId = id });

            if (deleteOrderDto == null)
            {
                throw new NotFoundException(nameof(deleteOrderDto), id);
            }
            
            return NoContent();
        }
    }
}
