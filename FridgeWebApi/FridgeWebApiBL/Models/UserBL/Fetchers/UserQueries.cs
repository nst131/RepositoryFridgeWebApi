using FridgeWebApiBL.Models.UserBL.Interfaces;
using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;

namespace FridgeWebApiBL.Models.UserBL.Fetchers
{
    public class UserQueries : IUserQueries
    {
        public string QueryCheckUniqueEmail(string email, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select Top 1 {nameof(Entity.Id)} From [{nameof(User)}]
                        Where {nameof(User.UserEmail)} = '{email}'
                    ";
        }

        public string QueryCheckUniqueName(string name, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select Top 1 {nameof(Entity.Id)} From [{nameof(User)}]
                        Where {nameof(User.UserName)} = '{name}'
                    ";
        }

        public string QueryGetUserId(int userId, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select {nameof(User.Id)} From [{nameof(User)}]
                        Where {nameof(User.Id)} = {userId}
                    ";
        }
    }
}
