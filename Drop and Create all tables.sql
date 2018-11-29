use dmai0917_1067303;

--Clear Database
IF OBJECT_ID('dbo.OrderHistory', 'U') IS NOT NULL
DROP TABLE dbo.OrderHistory;
IF Object_ID('dbo.Customer', 'U') IS NOT NULL
DROP TABLE dbo.Customer;
IF Object_ID('dbo.Users', 'U') IS NOT NULL
DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.OrderLineItem', 'U') IS NOT NULL
DROP TABLE dbo.OrderLineItem;
IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL
DROP TABLE dbo.Orders;
IF OBJECT_ID('dbo.Price', 'U') IS NOT NULL
DROP TABLE dbo.Price;
IF OBJECT_ID('dbo.Item', 'U') IS NOT NULL
DROP TABLE dbo.Item;
IF OBJECT_ID('dbo.ItemCat', 'U') IS NOT NULL
DROP TABLE dbo.ItemCat;
IF OBJECT_ID('dbo.Menu', 'U') IS NOT NULL
DROP TABLE dbo.Menu;
IF OBJECT_ID('dbo.ResTable', 'U') IS NOT NULL
DROP TABLE dbo.ResTable;
IF OBJECT_ID('dbo.Restaurant', 'U') IS NOT NULL
DROP TABLE dbo.Restaurant;
IF OBJECT_ID('dbo.ResCat', 'U') IS NOT NULL
DROP TABLE dbo.ResCat;

--Restaurant Category Table
create table ResCat
(id int not null IDENTITY(100,1),
name nvarchar(30) not null,
primary key (id));

--Restaurant Table
create table Restaurant
(id int not null IDENTITY(1000000,1),
name nvarchar(30) not null,
address nvarchar(50) not null,
zipcode int not null,
phoneNo int not null,
email nvarchar(50) not null,
verified bit not null,
resCatId int,
discontinued bit not null,
primary key (id),
constraint fkResCatId foreign key (resCatId) references ResCat (id) on delete cascade on update cascade);

--Table Table
create table ResTable
(id int not null IDENTITY(1000000,1),
restaurantId int not null,
noSeats int not null,
oId int not null,
primary key (id),
foreign key (restaurantId) references Restaurant(id) on delete cascade on update cascade);

--Menu Table
create table Menu
(id int not null IDENTITY(1000000,1),
restaurantId int not null,
name nvarchar(50) not null,
active bit not null,
primary key (id),
foreign key (restaurantId) references Restaurant(id) on delete cascade on update cascade);

--Item Category Table
create table ItemCat
(id int not null IDENTITY(1000000,1),
name nvarchar(30),
primary key (id));

--Item Table
create table Item
(id int not null IDENTITY(1000000,1),
menuId int not null,
name nvarchar(30),
description nvarchar(max),
itemCatId int,
primary key (id),
constraint fkMenuId foreign key (menuId) references Menu(id) on delete cascade on update cascade,
constraint fkItemCatId foreign key (itemCatId) references ItemCat(id) on delete cascade on update cascade);

--Price Table
create table Price
(itemId int not null,
price float not null,
startDate Date not null,
endDate Date not null,
constraint pkIdTime primary key (itemId, startDate, endDate),
foreign key (itemId) references Item(id) on delete cascade on update cascade);

--Order Table
create table Orders
(id int not null IDENTITY(1000000,1),
restaurantId int not null,
dateTime DateTime,
reservation DateTime,
noSeats int not null,
accepted bit not null,
primary key (id),
foreign key (restaurantId) references Restaurant(id) on delete cascade on update cascade);

--OrderLineItem
create table OrderLineItem
(orderId int not null,
itemId int not null,
quantity int not null,
constraint pkOrderItem primary key (orderId, itemId),
constraint fkOrder foreign key (orderId) references Orders(id) on delete cascade on update cascade,
constraint fkItem foreign key (itemId) references Item(id));

--Customer Table
create table Customer
(id int not null IDENTITY(1000000,1),
name nvarchar(50) not null,
address nvarchar(50) not null,
phoneNo int not null,
email nvarchar(50),
password varbinary(max),
restaurantId int default 0,
primary key (id));

--User Table
create table Users
(customerId int not null,
password varbinary(max),
primary key (customerId),
foreign key (customerId) references Customer(id));

--Order History Table
create table OrderHistory
(orderId int not null,
customerId int not null,
payment float not null,
primary key (orderId),
foreign key (orderId) references Orders(id) on delete cascade on update cascade,
foreign key (customerId) references Customer(id));