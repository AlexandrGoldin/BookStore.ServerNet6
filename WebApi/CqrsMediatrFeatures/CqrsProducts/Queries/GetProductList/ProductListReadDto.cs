using Application.ApplicationDTOs;

namespace WebApi.CqrsMediatrFeatures.CqrsProducts.Queries.GetProductList
{
    public class ProductListReadDto
    {
        public List<ProductReadDto>? Products { get; set; }
            
    }
}
