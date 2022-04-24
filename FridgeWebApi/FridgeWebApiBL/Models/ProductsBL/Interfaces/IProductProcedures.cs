using System.Threading.Tasks;
using FridgeWebApiBL.Models.ProductsBL.Dto.FetchersDto;
using Microsoft.Data.SqlClient;

namespace FridgeWebApiBL.Models.ProductsBL.Interfaces
{
    public interface IProductProcedures
    {
        Task CreateProcedureSearchProductsIntoFridge();
        ResponseSearchProductsIntoFridgeDtoBL GetModelFromProcedureSearchProductsIntoFridge(SqlDataReader reader);
    }
}