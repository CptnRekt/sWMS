CREATE DATABASE sWMS;
GO

USE sWMS
GO

CREATE SCHEMA sWMS;
GO

/*
DROP TABLE sWMS.Warehouses
DROP TABLE sWMS.Documents
DROP TABLE sWMS.Items
DROP TABLE sWMS.Subitems
DROP TABLE sWMS.CustomNames
DROP TABLE sWMS.Articles
DROP TABLE sWMS.Attributes
DROP TABLE sWMS.AttrClasses
DROP TABLE sWMS.Units
DROP TABLE sWMS.Contractors
DROP TABLE sWMS.Users
DROP TABLE sWMS.BinaryData
DROP TABLE sWMS.Config
DROP TABLE sWMS.Resources
DROP SCHEMA sWMS
DROP DATABASE sWMS
*/

--TODO: dodac clustered indexy, foreign keye, zmienic kolejnosc tworzenia tabel

CREATE TABLE sWMS.Warehouses
(
	Wh_Id int not null identity(1,1),
	Wh_Code varchar(100) not null unique,
	Wh_Name varchar(100) not null,
	Wh_Country varchar(100),
	Wh_City varchar(100),
	Wh_Street varchar(100),
	Wh_Postal varchar(100)
)

CREATE TABLE sWMS.Documents
(
	Doc_ObjectId int not null identity(1,1),
	Doc_ObjectType int not null,
	Doc_NumberString varchar(100) not null unique,
	Doc_Number int not null,
	Doc_Month int not null,
	Doc_Year int not null,
	Doc_Series varchar(5) not null,
	Doc_CreationDate datetime not null,
	Doc_CompletionDate datetime,
	Doc_Source_Wh_Id int,
	Doc_Destination_Wh_Id int,
)

CREATE TABLE sWMS.Items
(
	It_ObjectId int not null,
	It_ObjectType int not null,
	It_ObjectItem int not null,
	It_Code varchar(100) not null,
	It_Name varchar(100),
	It_Quantity decimal(14,9) not null,
	It_RealizedQuantity decimal(14,9) not null,
	It_Description varchar(255),
	It_CompletionDate datetime,
	It_Unit_Id int,
	It_Unit_Type int,
	It_Unit_No int,
	It_Art_Id int,
	It_Art_Type int,
	It_Art_No int,
)

CREATE TABLE sWMS.Subitems
(
	Sit_ObjectId int not null,
	Sit_ObjectType int not null,
	Sit_ObjectItem int not null,
	Sit_ObjectSubitem int not null,
	Sit_Quantity decimal(14,9) not null,
	Sit_RealizedQuantity decimal(14,9) not null,
	Sit_Unit_Id int,
	Sit_Unit_Type int,
	Sit_Unit_No int,
	Sit_Secondary_Unit_Id int,
	Sit_Secondary_Unit_Type int,
	Sit_Secondary_Unit_No int,
	Sit_Art_Id int,
	Sit_Art_Type int,
	Sit_Art_No int,
	Sit_Res_Id int,
	Sit_Res_Type int,
	Sit_Res_No int
)

CREATE TABLE sWMS.CustomNames
(
	Cun_Id int not null identity(1,1),
	Cun_Type int not null,
	Cun_No int not null,
	Cun_Name varchar(100) not null,
	Cun_Art_Id int,
	Cun_Art_Type int,
	Cun_Art_No int,
	Cun_Con_Id int,
	Cun_Con_Type int,
	Cun_Con_No int
)

CREATE TABLE sWMS.Articles
(
	Art_Id int not null identity(1,1),
	Art_Type int not null,
	Art_No int not null,
	Art_Code varchar(100) not null unique,
	Art_Name varchar(100),
	Art_Default_UnitId int,
	Art_Default_UnitType int,
	Art_Default_UnitNo int,
	Art_CreationDate datetime
)

CREATE TABLE sWMS.Attributes
(
	Attr_Id int not null identity(1,1),
	Attr_Type int not null,
	Attr_No int not null,
	Attr_ObjectId int not null,
	Attr_ObjectType int not null,
	Attr_ObjectItem int not null,
	Attr_ObjectSubitem int not null,
	Attr_AtC_Id int not null,
	Attr_AtC_Type int not null,
	Attr_AtC_No int not null,
	Attr_Value varchar(255) not null
)

CREATE TABLE sWMS.AttrClasses
(
	AtC_Id int not null identity(1,1),
	AtC_Type int not null,
	AtC_No int not null,
	AtC_Name varchar(100) not null,
	AtC_DataType varchar(100) not null
)

CREATE TABLE sWMS.Units
(
	Unit_Id int not null identity(1,1),
	Unit_Type int not null,
	Unit_No int not null,
	Unit_Name varchar(100) not null,
	Unit_Dividend decimal(14,9) not null,
	Unit_Divisor decimal(14,9) not null
)

CREATE TABLE sWMS.Contractors
(
	Con_Id int not null identity(1,1),
	Con_Type int not null,
	Con_No int not null,
	Con_Code varchar(100) not null unique,
	Con_FullName varchar(200),
	Con_Country varchar(100),
	Con_City varchar(100),
	Con_Street varchar(100),
	Con_Postal varchar(100),
	Con_Logo_BinD_Id int,
	Con_Logo_BinD_Type int,
	Con_Logo_BinD_No int
)

CREATE TABLE sWMS.Users
(
	Usr_Id int identity(1,1),
	Usr_Login varchar(50) not null unique,
	Usr_Password varchar(50),
	Usr_Autologin bit
)

CREATE TABLE sWMS.BinaryData
(
	BinD_Id int not null identity(1,1),
	BinD_Type int not null,
	BinD_No int not null,
	BinD_Content varbinary(max) not null
)

CREATE TABLE sWMS.Config
(
	Conf_Id int not null identity(1,1),
	Conf_CodeName varchar(100) not null,
	Conf_Name varchar(100) not null,
	Conf_Value varchar(100) not null
)

CREATE TABLE sWMS.Resources
(
	Res_Id int not null identity(1,1),
	Res_Wh_Id int,
	Res_BatchCode varchar(100) not null unique,
	Res_BatchName varchar(100) not null,
	Res_Art_Id int,
	Res_Art_Type int,
	Res_Art_No int,
	Res_Unit_Id int,
	Res_Unit_Type int,
	Res_Unit_No int,
	Res_Secondary_Unit_Id int,
	Res_Secondary_Unit_Type int,
	Res_Secondary_Unit_No int,
	Res_Quantity decimal(14,9) 
)