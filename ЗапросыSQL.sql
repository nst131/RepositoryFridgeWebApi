Use master
Create Database FridgeDatabase
Go

Use FridgeDatabase    
Create Table FridgeModel
(
    Id Int Not Null Primary Key Identity,
    Name Nvarchar(20) Not Null,
    Year Int Null
);
    
Create Table Fridge
(
    Id Int Not Null Primary Key Identity,
    Name Nvarchar(20) Not Null,
    OwnerName Nvarchar(20) Null,
    FridgeModelId Int Foreign Key References FridgeModel(Id) On Delete No Action
);
    
Create Table Products
(
    Id Int Not Null Primary Key Identity,
    Name Nvarchar(20) Not Null,
    DefaultQuantity Int Null
);
    
Create Table FridgeProducts
(
    Id Int Not Null Primary Key Identity,
	Quantity Int Not Null,
    FridgeId Int Foreign Key References Fridge(Id) On Delete Cascade,
    ProductId Int Foreign Key References Products(Id) On Delete No Action
);
Go

Use FridgeDatabase
Insert Into Products
(Name, DefaultQuantity)
Values
('Apple',3),
('Orange',5),
('Peach',5),
('Watermelon',1),
('Apricot',4)

Insert Into FridgeModel
(Name, Year)
Values
('Samsung',1999),
('LG',2000),
('Atlant',1996),
('Indesit',2010),
('Bosch',2019)

Insert Into Fridge
(Name, OwnerName, FridgeModelId)
Values
('HisFridge','Alexander',1),
('HerFridge','Maria',2),
('FreeFridge','Own',2),
('MyFridge', 'Dmitriy',3),
('JustFridge', 'Nastya', 4)

Insert Into FridgeProducts
(Quantity ,FridgeId, ProductId)
Values
(5, 1, 1),
(5, 1, 2),
(5, 1, 3),
(6, 2, 3),
(6, 2, 4),
(3, 3, 1),
(2, 3, 2),
(15,3, 3),
(1, 4, 2),
(5, 4, 3),
(6, 4, 4)
Go

--1.—делать выборку продуктов по холодильникам, модель которой начинаетьс€ на 'A'.

Select Distinct P.Name From Fridge as F
Join FridgeModel as FM
On F.FridgeModelId = FM.Id
Join FridgeProducts as FP
On FP.FridgeId = F.Id
Join Products as P
On P.Id = FP.ProductId
Where FM.Name Like 'A%'
Go

--2.—делать выборку холодильников, в которых есть продукты в количестве, меньше чем в количестве по умолчанию.

Select Distinct F.Name From Fridge as F -- ’от€ бы 1 продукт, он попадает в список.
Join FridgeProducts as FP
On FP.FridgeId = F.Id
Join Products as P
On P.Id = FP.ProductId
Where FP.Quantity < P.DefaultQuantity
Go

--3.¬ каком году выпустили с наибольшей вместимостью (сумма всех продуктов больше всего).

Select Year From Fridge as F -- ¬ыбор неоходимых полей
Join FridgeModel as FM
On FM.Id = F.FridgeModelId
Where F.Id = ( 
				Select F.Id From Fridge as F -- ¬ыбор необходимой Id
					Join FridgeProducts as FP
					On FP.FridgeId = F.Id
					Group By F.Id
					Having Sum(FP.Quantity) = (
												Select MAX(tb.SumProducts) From -- ћаксимальное количесво продуктов в холодильнике
												  (
													Select Sum(FP.Quantity) as SumProducts From Fridge as F
													Join FridgeProducts as FP
													On FP.FridgeId = F.Id
													Group By F.Id
												  ) as tb
											  )
			  )
Go

--4.¬ыбрать все продукты и им€ владельца из холодильника, в котором больше всего наименований продуктов. »менно наименнований, не количество.

Select F.OwnerName, P.Name From Fridge as F -- ¬ыбор необходимых полей
Join FridgeProducts as FP
On FP.FridgeId = F.Id
Join Products as P
On P.Id = FP.ProductId
Where F.Id = (
				Select F.Id From Fridge as F -- ¬ыбор необходимой Id
				Join FridgeProducts as FP
				On FP.FridgeId = F.Id
				Join Products as P
				On P.Id = FP.ProductId
				Group by F.Id
				Having Count(P.Id) =(
										Select Max(tb.CountName) From -- Ќаибольше количества наименнований
											(
												Select Count(P.Name) as CountName From Products as P
												Join FridgeProducts as FP
												On FP.ProductId = P.Id
												Join Fridge as F
												On F.Id = FP.FridgeId
												Group By F.Id
											) as tb
									)
			 )
Go

--5.¬ывести все продукты дл€ холодильника в Id=2.

Select P.Name From Fridge as F
Join FridgeProducts as FP
On FP.FridgeId = F.Id
Join Products as P
On P.Id = FP.ProductId
Where F.Id = 2
Go

--6.¬ывести все продукты дл€ всех холодильников.

Select P.Name, F.Name From Fridge as F
Join FridgeProducts as FP
On FP.FridgeId = F.Id
Join Products as P
On P.Id = FP.ProductId
Go

--7.¬ывести список холодильников и сумму всех продуктов дл€ каждого из них.

Select Sum(FP.Quantity) as SumProducts, F.Name From Fridge as F
Join FridgeProducts as FP
On FP.FridgeId = F.Id
Group By F.Name
Go

--8.¬ывести им€ холодильника, название и год модели, а также количество продуктов дл€ каждого из них

Select Distinct F.Name as FridgeName, FM.Name as Model, FM.Year as ModelYear, tb.SumProducts From Fridge as F
Join FridgeModel as FM
On FM.Id = F.FridgeModelId
Join FridgeProducts as FP
On FP.FridgeId = F.Id
Join Products as P
On P.Id = FP.ProductId
Join    (
			Select Sum(FP.Quantity) as SumProducts, F.Id From Fridge as F
			Join FridgeProducts as FP
			On FP.FridgeId = F.Id
			Group By F.Id
		) as tb
On tb.Id = F.Id
Go

--9.¬ывести список холодильников, где содержатьс€ продукты, количество которых больше, чем кол-во по умолчанию.

Select Distinct F.Name From Fridge as F   -- ’от€ бы 1 продукт, он попадает в список.
Join FridgeProducts as FP
On FP.FridgeId = F.Id
Join Products as P
On P.Id = FP.ProductId
Where FP.Quantity > P.DefaultQuantity
Go

--10.¬ывести список холодильников и дл€ каждого холодильника кол-во наименований продуктов, кол-во которых больше, чем количество по умолчанию.
Select F.Name, tb.NumberOfProducts From Fridge as F
Join (
		Select Count(F.Id) as NumberOfProducts, F.Id From FridgeProducts as FP
		Join Products as P
		On P.Id = FP.ProductId
		Join Fridge as F
		On F.Id = FP.FridgeId
		Where FP.Quantity > P.DefaultQuantity
		Group by F.Id
	) as tb
On tb.Id = F.Id
Go