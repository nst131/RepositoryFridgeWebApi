using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FridgeWebApiBL.Models.ProductsBL.Dto.CrudDto;

namespace FridgeWebApiBL.Models.ProductsBL.Interfaces
{
    public interface IProductsCrud
    {
        Task<ICollection<ResponseProductDtoBL>> GetAll(CancellationToken token = default);
        Task Create(AcceptCreateProductDtoBL createProduct, CancellationToken token = default);
        Task Update(AcceptUpdateProductDtoBL updateProduct, CancellationToken token = default);
        Task<ResponseProductDtoBL> Get(AcceptGetProductDtoBL getProductDto, CancellationToken token = default);
        Task Delete(AcceptDeleteProductDtoBL deleteProductDto, CancellationToken token = default);
    }
}