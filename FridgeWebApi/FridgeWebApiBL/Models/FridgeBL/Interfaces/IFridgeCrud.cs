using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FridgeWebApiBL.Models.FridgeBL.Dto;

namespace FridgeWebApiBL.Models.FridgeBL.Interfaces
{
    public interface IFridgeCrud
    {
        Task<ICollection<ResponseFridgeDtoBL>> GetAllFridges(CancellationToken token = default);
        Task Create(AcceptCreateFridgeDtoBL createFridge, CancellationToken token = default);
        Task Update(AcceptUpdateFridgeDtoBL updateFridge, CancellationToken token = default);
        Task<ResponseFridgeDtoBL> Get(AcceptGetFridgeDtoBL getFridgeDto, CancellationToken token = default);
        Task Delete(AcceptDeleteFridgeDtoBL deleteFridge, CancellationToken token = default);
    }
}