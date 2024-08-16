using Application.ServicesInterfaces;
using Domain.DapperRepositoriesInterfaces;
using Domain.Entities;

namespace Application.ServicesImplementation
{
    public class CartItemService : ICartItemService
    {
        private readonly IDapperCartItemRepository _dapperCartItemRepo;

        public CartItemService(IDapperCartItemRepository dapperCartItemRepo) 
        {
            _dapperCartItemRepo = dapperCartItemRepo;
        }

        public async Task<List<CartItem>?> GetCartItemListByOrderIdAsync(int id,
           CancellationToken cancellationToken)
        {
            if (id == 0)
            {
                return null;
            }
            var cartItems = await _dapperCartItemRepo
               .GetCartItemListByOrderIdAsync(id, cancellationToken);

            if (cartItems?.Count() == 0)
            {
                return null;
            }
            return cartItems!.ToList();
        }
    }
}