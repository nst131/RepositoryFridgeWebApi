using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;

namespace FridgeWebApiDL.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(string connectionString) : base(connectionString) { }

        public override string QueryGetAll(string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select * From [{nameof(User)}]
                    ";
        }

        public override string QueryGet(int id, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        Select * From [{nameof(User)}]
                        Where {nameof(User.Id)} = {id}
                    ";
        }

        public override string QueryCreate(User item, string nameDatabase)
        {
            return $@"
                        Use {nameDatabase}
                        {DML.Insert} Into [{nameof(User)}]
                        ({nameof(User.UserName)}, {nameof(User.UserEmail)})
                        Values
                        ('{item.UserName}', '{item.UserEmail}')
                    ";
        }

        public override string QueryUpdate(User item, string nameDatabase)
        {
            throw new System.NotImplementedException();
        }

        public override string QueryDelete(int id, string nameDatabase)
        {
            throw new System.NotImplementedException();
        }
    }
}
