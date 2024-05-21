CREATE SCHEMA sWMS;
GO

/*
DROP TABLE sWMS.Warehouses
DROP TABLE sWMS.Documents
DROP TABLE sWMS.Items
DROP TABLE sWMS.CustomNames
DROP TABLE sWMS.Articles
DROP TABLE sWMS.Attributes
DROP TABLE sWMS.AttrClasses
DROP TABLE sWMS.Contractors
DROP TABLE sWMS.ArticlesBatches
DROP TABLE sWMS.Users
DROP TABLE sWMS.BinaryData
DROP TABLE sWMS.Units
DROP TABLE sWMS.Config
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

-- bardziej skomplikowany kod i wszystko zapisywane do bazy, 
-- czy mniej skomplikowany kod i wszystko wyliczane we froncie? 

CREATE TABLE sWMS.Documents
(
	Doc_Id int not null identity(1,1),
	Doc_Type int not null,
	Doc_NumberString varchar(100) not null unique,
	Doc_Number int not null,
	Doc_Month int not null,
	Doc_Year int not null,
	Doc_Series varchar(5) not null,
	Doc_CreationDate datetime not null,
	Doc_CompletionDate datetime not null
)

CREATE TABLE sWMS.Items
(
	It_Doc_Id int not null identity(1,1),
	It_Doc_Type int not null,
	It_No int not null,
	It_Code varchar(100) not null,
	It_Name varchar(100),
	It_Quantity decimal(14,9) not null,
	It_Description varchar(255),
	It_CompletionDate datetime,
	It_Unit_Id int not null,
	It_Unit_Type int not null,
	It_Unit_No int not null,
	It_Secondary_Unit_Id int,
	It_Secondary_Unit_Type int,
	It_Secondary_Unit_No int,
	It_Code_Cun_Id int,
	It_Code_Cun_Type int,
	It_Code_Cun_No int,
	It_Name_Cun_Id int,
	It_Name_Cun_Type int,
	It_Name_Cun_No int,
	It_Src_Wh_Id int,
	It_Current_Wh_Id int,
	It_Dst_Wh_Id int,
	It_Art_Id int not null,
	It_Art_Type int not null,
	It_Art_No int not null,
	It_ArB_Id int,
	It_Arb_Type int,
	It_Arb_No int
)

CREATE TABLE sWMS.CustomNames
(
	Cun_Id int not null identity(1,1),
	Cun_Type int not null,
	Cun_No int not null,
	Cun_Name varchar(100) not null,
)

CREATE TABLE sWMS.Articles
(
	Art_Id int not null identity(1,1),
	Art_Type int not null,
	Art_No int not null,
	Art_Code varchar(100) not null unique,
	Art_Name varchar(100),
	Art_Default_UnitId int not null,
	Art_CreationDate datetime
)

CREATE TABLE sWMS.Attributes
(
	Attr_Id int not null identity(1,1),
	Attr_Type int not null,
	Attr_No int not null,
	Attr_Object_Id int not null,
	Attr_Object_Type int not null,
	Attr_Object_No int not null,
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
	Unit_Divisor decimal(14,9) not null,
	Unit_AttachedTo_Art_Id int,
	Unit_AttachedTo_Art_Type int,
	Unit_AttachedTo_Art_No int
)

CREATE TABLE sWMS.Contractors
(
	Con_Id int not null identity(1,1),
	Con_Type int not null,
	Con_No int not null,
	Con_Code varchar(100) not null,
	Con_FullName varchar(200),
	Con_Country varchar(100),
	Con_City varchar(100),
	Con_Street varchar(100),
	Con_Postal varchar(100),
	Con_Code_Cun_Id int,
	Con_Code_Cun_Type int,
	Con_Code_Cun_No int,
	Con_Name_Cun_Id int,
	Con_Name_Cun_Type int,
	Con_Name_Cun_No int,
	Con_Logo_BinD_Id int,
	Con_Logo_BinD_Type int,
	Con_Logo_BinD_No int
)

CREATE TABLE [sWMS].[ArticlesBatches](
	[ArB_Id] [int] IDENTITY(1,1) NOT NULL,
	[ArB_Type] [int] not null,
	[ArB_No] [int] not null,
	[ArB_Code] [varchar](100) not null unique,
	[ArB_Name] [varchar](100) NULL,
	[ArB_Quantity] decimal(14,9) not null,
	[ArB_Unit_Id] int not null,
	[ArB_Unit_Type] int not null,
	[ArB_Unit_No] int not null,
	[ArB_Secondary_Unit_Id] int,
	[ArB_Secondary_Unit_Type] int,
	[ArB_Secondary_Unit_No] int,
	[ArB_Art_Id] [int] not null,
	[ArB_Art_Type] [int] not null,
	[ArB_Art_No] [int] not null
) ON [PRIMARY]
GO

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