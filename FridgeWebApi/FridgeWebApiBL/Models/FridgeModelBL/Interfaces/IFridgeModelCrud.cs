using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FridgeWebApiBL.Models.FridgeModelBL.Dto;

namespace FridgeWebApiBL.Models.FridgeModelBL.Interfaces
{
    public interface IFridgeModelCrud
    {
        Task<ICollection<ResponseGetFridgeModelDtoBL>> GetAllFridges(CancellationToken token = default);
        Task<ResponseGetFridgeModelDtoBL> Get(AcceptGetFridgeModelDtoBL getFridgeModelDto, CancellationToken token = default);
    }
}