using FridgeWebApiBL.Models.FridgeBL.Interfaces;
using FridgeWebApiDL.Entity;

namespace FridgeWebApiBL.Models.FridgeBL.Fetchers
{
    public class FridgeQueries : IFridgeQueries
    {
        public string QueryGetFridgeId(int fridgeId, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select {nameof(Fridge.Id)} From {nameof(Fridge)} Where {nameof(Fridge.Id)} = {fridgeId}
                    ";
        }

        public string QueryGetFridgeModelId(int fridgeModelId, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select {nameof(FridgeModel.Id)} From {nameof(FridgeModel)} Where {nameof(FridgeModel.Id)} = {fridgeModelId}
                    ";
        }

        public string QueryGetUserId(int userId, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select {nameof(User.Id)} From [{nameof(User)}] Where {nameof(User.Id)} = {userId}
                    ";
        }
    }
}
