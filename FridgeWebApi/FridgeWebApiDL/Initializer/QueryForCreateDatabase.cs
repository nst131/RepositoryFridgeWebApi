using FridgeWebApiDL.Entity;
using FridgeWebApiDL.Helper;
using FridgeWebApiDL.Interfaces;

namespace FridgeWebApiDL.Initializer
{
    internal class QueryForCreateDatabase : IQueryForCreateDatabase
    {
        public string QueryCreateAllTables(string name)
        {
            return $@"
                        Use {name}

                        Begin Try
                        Begin Transaction
                        
                            {this.QueryCreateFridgeModelTable()}
                            {this.QueryCreateUserTable()}
                            {this.QueryCreateFridgeTable()}
                            {this.QueryCreateProductsTable()}
                            {this.QueryCreateFridgeProductsTable()}

                        End Try
                        Begin Catch
                            
                            Rollback Transaction
                            Use {Database.master}
                            {this.QueryDropDatabase(name)}

                        Return
                        End Catch
                        Commit Transaction
                    ";
        }

        public string QueryCreateDatabase(string name)
        {
            return @$"
                       {DDL.Create} Database {name}
                    ";
        }

        private string QueryDropDatabase(string name)
        {
            return $@"
                        Use master
                        {DDL.Drop} Database {name}
                    ";
        }

        private string QueryCreateFridgeModelTable() // FridgeModel --
        {
            return $@"
                        {DDL.Create} Table {nameof(FridgeModel)}
                        (
                            {nameof(FridgeModel.Id)} {DataType.Int} {Constraint.NotNull} {Constraint.PrimaryKey} {Constraint.Identity},
                            {nameof(FridgeModel.Name)} {DataType.Nvarchar(20)} {Constraint.NotNull},
                            {nameof(FridgeModel.Year)} {DataType.Int} {Constraint.Null}
                        );
                    ";
        }

        private string QueryCreateUserTable() // User --
        {
            return $@"
                        {DDL.Create} Table [{nameof(User)}]
                        (
                            {nameof(User.Id)} {DataType.Int} {Constraint.NotNull} {Constraint.PrimaryKey} {Constraint.Identity},
                            {nameof(User.UserEmail)} {DataType.Nvarchar(20)} {Constraint.NotNull},
                            {nameof(User.UserName)} {DataType.Nvarchar(20)} {Constraint.NotNull}
                        );
                    ";
        }

        private string QueryCreateFridgeTable() // Fridge --
        {
            return $@"
                        {DDL.Create} Table {nameof(Fridge)}
                        (
                            {nameof(Fridge.Id)} {DataType.Int} {Constraint.NotNull} {Constraint.PrimaryKey} {Constraint.Identity},
                            {nameof(Fridge.Name)} {DataType.Nvarchar(20)} {Constraint.NotNull},
                            {nameof(Fridge.FridgeModelId)} {DataType.Int} {Constraint.ForeignKey(nameof(FridgeModel), nameof(FridgeModel.Id))} On Delete No Action,
                            {nameof(Fridge.UserId)} {DataType.Int} {Constraint.ForeignKey("[" + nameof(User) + "]", nameof(User.Id))} On Delete Cascade
                            
                        );
                    ";
        }

        private string QueryCreateProductsTable() // Products --
        {
            return $@"
                        {DDL.Create} Table {nameof(Products)}
                        (
                            {nameof(Products.Id)} {DataType.Int} {Constraint.NotNull} {Constraint.PrimaryKey} {Constraint.Identity},
                            {nameof(Products.Name)} {DataType.Nvarchar(20)} {Constraint.NotNull},
                            {nameof(Products.DefaultQuantity)} {DataType.Int} {Constraint.NotNull}
                        );
                    ";
        }

        private string QueryCreateFridgeProductsTable() // FridgeProducts --
        {
            return $@"
                        {DDL.Create} Table {nameof(FridgeProducts)}
                        (
                            {nameof(FridgeProducts.Id)} {DataType.Int} {Constraint.NotNull} {Constraint.PrimaryKey} {Constraint.Identity},
                            {nameof(FridgeProducts.Quantity)} {DataType.Int} {Constraint.NotNull},
                            {nameof(FridgeProducts.FridgeId)} {DataType.Int} {Constraint.ForeignKey(nameof(Fridge), nameof(Fridge.Id))} On Delete Cascade,
                            {nameof(FridgeProducts.ProductId)} {DataType.Int} {Constraint.ForeignKey(nameof(Products), nameof(Products.Id))} On Delete No Action
                        );
                    ";
        }

        public string QueryFirstInitializer(string name)
        {
            var additionalInit = @$"
                        {DML.Insert} Into [{nameof(User)}]
                        ({nameof(User.UserName)}, {nameof(User.UserEmail)})
                        Values
                        ('Alexander', 'alexander@mail.ru'),
                        ('Maria', 'maria@mail.ru'),
                        ('Vitaliy', 'vitaliy@mail.ru')

                        {DML.Insert} Into {nameof(Fridge)}
                        ({nameof(Fridge.Name)}, {nameof(Fridge.UserId)}, {nameof(Fridge.FridgeModelId)})
                        Values
                        ('HisFridge', 1, 1),
                        ('HerFridge', 2, 2),
                        ('FreeFridge', 3, 2)
                        
                        {DML.Insert} Into {nameof(FridgeProducts)}
                        ({nameof(FridgeProducts.Quantity)}, {nameof(FridgeProducts.FridgeId)}, {nameof(FridgeProducts.ProductId)})
                        Values
                        (5, 1, 1),
                        (5, 1, 2),
                        (5, 1, 3),
                        (6, 2, 3),
                        (6, 2, 4)
                                    ";

            return $@"
                        Use {name}        
    
                        {DML.Insert} Into {nameof(Products)}
                        ({nameof(Products.Name)}, {nameof(Products.DefaultQuantity)})
                        Values
                        ('Apple',3),
                        ('Orange',5),
                        ('Peach',5),
                        ('Watermelon',1),
                        ('Apricot',4)

                        {DML.Insert} Into {nameof(FridgeModel)}
                        ({nameof(FridgeModel.Name)}, {nameof(FridgeModel.Year)})
                        Values
                        ('Samsung',4),
                        ('LG',3),
                        ('Atlant',1),
                        ('Indesit',2),
                        ('Bosch',5)
                    ";
        }
    }
}
