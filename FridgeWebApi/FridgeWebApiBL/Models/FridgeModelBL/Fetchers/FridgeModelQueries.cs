using FridgeWebApiBL.Models.FridgeModelBL.Interfaces;
using FridgeWebApiDL.Entity;

namespace FridgeWebApiBL.Models.FridgeModelBL.Fetchers
{
    public class FridgeModelQueries : IFridgeModelQueries
    {
        public string QueryGetFridgeModelId(int fridgeModelId, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select {nameof(FridgeModel.Id)} From {nameof(FridgeModel)} Where {nameof(FridgeModel.Id)} = {fridgeModelId}
                    ";
        }
    }
}
