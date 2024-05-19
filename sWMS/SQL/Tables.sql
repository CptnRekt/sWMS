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
	Wh_Id int primary key identity(1,1),
	Wh_Code varchar(100) not null,
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
	Doc_Id int identity(1,1),
	Doc_Type int,
	Doc_NumberString varchar(100),
	Doc_Number int,
	Doc_Month int,
	Doc_Year int,
	Doc_Series varchar(5),
	Doc_CreationDate datetime,
	Doc_CompletionDate datetime
)

CREATE TABLE sWMS.Items
(
	It_Id int identity(1,1),
	It_Type int,
	It_No int,
	It_Code varchar(100),
	It_Name varchar(100),
	It_Quantity decimal(14,9),
	It_Description varchar(255),
	It_CompletionDate datetime,
	It_Unit_Id int,
	It_Unit_Type int,
	It_Unit_No int,
	It_Code_Cun_Id int,
	It_Code_Cun_Type int,
	It_Code_Cun_No int,
	It_Name_Cun_Id int,
	It_Name_Cun_Type int,
	It_Name_Cun_No int,
	It_Src_Wh_Id int,
	It_Current_Wh_Id int,
	It_Dst_Wh_Id int,
	It_Art_Id int,
	It_Art_Type int,
	It_Art_No int,
	It_Doc_Id int,
	It_Doc_Type int,
	It_ArB_Id int,
	It_Arb_Type int,
	It_Arb_No int
)

CREATE TABLE sWMS.CustomNames
(
	Cun_Id int identity(1,1),
	Cun_Type int,
	Cun_No int,
	Cun_Name varchar(100),
)

CREATE TABLE sWMS.Articles
(
	Art_Id int identity(1,1),
	Art_Type int,
	Art_No int,
	Art_Code varchar(100),
	Art_Name varchar(100),
	Art_Default_UnitId int,
	Art_CreationDate datetime
)

CREATE TABLE sWMS.Attributes
(
	Attr_Id int identity(1,1),
	Attr_Type int,
	Attr_No int,
	Attr_AtC_Id int,
	Attr_AtC_Type int,
	Attr_AtC_No int,
	Attr_Value varchar(255)
)

CREATE TABLE sWMS.AttrClasses
(
	AtC_Id int identity(1,1),
	AtC_Type int,
	AtC_No int,
	AtC_Name varchar(100),
	AtC_DataType varchar(100)
)

CREATE TABLE sWMS.Units
(
	Unit_Id int identity(1,1),
	Unit_Type int,
	Unit_No int,
	Unit_Name varchar(100),
	Unit_Dividend decimal(14,9),
	Unit_Divisor decimal(14,9),
	Unit_AttachedTo_Art_Id int,
	Unit_AttachedTo_Art_Type int,
	Unit_AttachedTo_Art_No int
)

CREATE TABLE sWMS.Contractors
(
	Con_Id int identity(1,1),
	Con_Type int,
	Con_No int,
	Con_Code varchar(100),
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

CREATE TABLE sWMS.ArticlesBatches
(
	ArB_Id int identity(1,1),
	ArB_Type int,
	ArB_No int,
	ArB_Code varchar(100),
	ArB_Name varchar(100),
	ArB_AttachedTo_Art_Id int,
	ArB_AttachedTo_Art_Type int,
	ArB_AttachedTo_Art_No int
	--ArB_Description varchar(255),
	--ArB_CreationDate datetime,
	--ArB_Quantity decimal(14,9),
	--ArB_Unit_Id int,
	--ArB_Src_Wh_Id int,
	--ArB_Current_Wh_Id int,
	--ArB_Dst_Wh_Id int
)

CREATE TABLE sWMS.Users
(
	Usr_Id int identity(1,1),
	Usr_Login varchar(50),
	Usr_Password varchar(50),
	Usr_Autologin bit
)

CREATE TABLE sWMS.BinaryData
(
	BinD_Id int identity(1,1),
	BinD_Type int,
	BinD_No int,
	BinD_Content varbinary(max)
)

CREATE TABLE sWMS.Config
(
	Conf_Id int identity(1,1),
	Conf_CodeName varchar(100),
	Conf_Name varchar(100),
	Conf_Value varchar(100)
)