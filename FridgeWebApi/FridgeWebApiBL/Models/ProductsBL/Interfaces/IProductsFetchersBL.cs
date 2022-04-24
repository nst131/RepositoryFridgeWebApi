using System.Collections.Generic;
using FridgeWebApiBL.Models.ProductsBL.Dto;
using System.Threading;
using System.Threading.Tasks;
using FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto;

namespace FridgeWebApiBL.Models.ProductsBL.Interfaces
{
    public interface IProductsFetchersBL
    {
        Task AddProductIntoFridge(AcceptAddProductIntoFridgeDtoBL addProductsIntoFridge, CancellationToken token = default);
        Task UpdateProductIntoFridge(AcceptUpdateProductIntoFridgeDtoBL updateProductsIntoFridge, CancellationToken token = default);
        Task<ResponseSearchProductsIntoFridgeDtoBL> SearchProductsIntoFridgeWhereQuantityZero();

        Task<ICollection<ResponseProductIntoFridgeDtoBL>> GetAllProductsByFridgeId(AcceptGetAllProductIntoFridgeDtoBL getProductByFridgeId,
            CancellationToken token = default);

        Task DeleteProductsIntoFridge(AcceptDeleteProductIntoFridgeDtoBL deleteProductIntoFridge, CancellationToken token = default);

        Task UpdateProductIntoFridgeById(AcceptUpdateProductIntoFridgeByIdDtoBL updateProductIntoFridge,
            CancellationToken token);

        Task DeleteProductsIntoFridgeById(AcceptDeleteProductIntoFridgeByIdDtoBL deleteProductIntoFridge,
            CancellationToken token);

        Task<ResponseGetProductIntoFridgeByIdDtoBL> GetProductIntoFridgeById(AcceptGetProductIntoFridgeByIdDtoBL getProductIntoFridge,
            CancellationToken token);
    }
}