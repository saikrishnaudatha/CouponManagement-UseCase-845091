
create database CouponManagement

create table UserDetails(UserId int IDENTITY(1,1) primary key,UserName varchar(50) unique not null,FirstName varchar(50) not null,
LastName varchar(50),EmailAddress varchar(50)  not null,PhoneNumber varchar(50),UserPassword varchar(30) not null,
UserAddress varchar(500),UpdatedDate Datetime,CreatedDate Datetime not null)


create table CouponDetails(CouponId int IDENTITY(1,1) primary key,CouponNumber varchar(20) not null,CouponStatus varchar(20) not null,
   CouponStartDate datetime not null,CouponExpiredDate datetime not null,CreateDate datetime not null, 
   UpdatedDate datetime,UserId int foreign key references UserDetails(UserId));