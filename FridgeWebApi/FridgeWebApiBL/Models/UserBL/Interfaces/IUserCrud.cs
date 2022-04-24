using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FridgeWebApiBL.Models.UserBL.Dto;

namespace FridgeWebApiBL.Models.UserBL.Interfaces
{
    public interface IUserCrud
    {
        Task Create(AcceptCreateUserDtoBL createUser, CancellationToken token = default);
        Task<ICollection<ResponseGetUserDtoBL>> GetAllUsers(CancellationToken token = default);
        Task<ResponseGetUserDtoBL> Get(AcceptGetUserDtoBL getUserDto, CancellationToken token = default);
    }
}