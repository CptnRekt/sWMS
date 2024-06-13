--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO

CREATE OR ALTER PROCEDURE sWMS.GetDocuments
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * from sWMS.Documents
END
GO

CREATE OR ALTER PROCEDURE sWMS.GetWarehouses
AS
BEGIN
	SET NOCOUNT ON;

	;with Warehouses as
	(
		select * from sWMS.Warehouses
	),
	Acceptances as 
	(
		select Wh_Id, count(*) Number from sWMS.Documents
		join Warehouses on Wh_Id = Doc_Destination_Wh_Id
		group by Wh_Id
	),
	Issues as 
	(
		select Wh_Id, count(*) Number from sWMS.Documents
		join Warehouses on Wh_Id = Doc_Source_Wh_Id
		group by Wh_Id
	)
	SELECT w.*
	,isnull(a.Number, 0) Wh_AcceptancesNumber
	,isnull(i.Number, 0) Wh_IssuesNumber
	from Warehouses w
	left join Acceptances a on a.Wh_Id = w.Wh_Id
	left join Issues i on i.Wh_Id = w.Wh_Id
END
GO

CREATE PROCEDURE sWMS.GetContractors
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * from sWMS.Contractors
END
GO

CREATE PROCEDURE sWMS.GetArticles
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * from sWMS.Articles
END
GO

CREATE PROCEDURE sWMS.GetUnits
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * from sWMS.Units
END
GO

CREATE PROCEDURE sWMS.GetAttrClasses
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * from sWMS.AttrClasses
END
GO

CREATE OR ALTER PROCEDURE sWMS.GetConfig
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * from sWMS.Config
END
GO

CREATE OR ALTER PROCEDURE [sWMS].[AddWarehouse]
	@Wh_Type int = 99
	,@Wh_Code varchar(100)
	,@Wh_Name varchar(100)
	,@Wh_Country varchar(100) = null
	,@Wh_City varchar(100) = null
	,@Wh_Street varchar(100) = null
	,@Wh_Postal varchar(100) = null
AS
BEGIN
	SET NOCOUNT ON;
	insert into sWMS.Warehouses (
		Wh_Type
		,Wh_Code
		,Wh_Name
		,Wh_Country
		,Wh_City
		,Wh_Street
		,Wh_Postal
	) values (
		@Wh_Type
		,@Wh_Code
		,@Wh_Name
		,@Wh_Country
		,@Wh_City
		,@Wh_Street
		,@Wh_Postal
	)
	exec sWMS.GetWarehouses
END
GO

CREATE OR ALTER PROCEDURE sWMS.EditWarehouse
	@Wh_Id int
	,@Wh_Type int = 99
	,@Wh_Code varchar(100)
	,@Wh_Name varchar(100)
	,@Wh_Country varchar(100) = null
	,@Wh_City varchar(100) = null
	,@Wh_Street varchar(100) = null
	,@Wh_Postal varchar(100) = null
AS
BEGIN
	SET NOCOUNT ON;
	update sWMS.Warehouses set 
	Wh_Type = @Wh_Type
	,Wh_Code = @Wh_Code
	,Wh_Name = @Wh_Name
	,Wh_Country = @Wh_Country
	,Wh_City = @Wh_City
	,Wh_Street = @Wh_Street
	,Wh_Postal = @Wh_Postal
	where Wh_Id = @Wh_Id
	exec sWMS.GetWarehouses
END
GO

CREATE OR ALTER PROCEDURE sWMS.RemoveWarehouse
	@Wh_Id int
AS
BEGIN
	SET NOCOUNT ON;
	delete from sWMS.Warehouses where Wh_Id = @Wh_Id
END

----OLD

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 19.05.2024
---- Description:	Dodawanie pustego dokumentu
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.CreateNewDocument
--	@Doc_Type int
--	,@Doc_Number int = null
--	,@Doc_Month int = null
--	,@Doc_Year int = null
--	,@Doc_Series varchar(100) = null
--	,@Doc_CompletionDate datetime = null
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--	declare @Doc_NumberString varchar(100) = sWMS.GenerateDocumentNumber(@Doc_Number, @Doc_Month, @Doc_Year, @Doc_Series)

--    insert into sWMS.Documents 
--	(
--		Doc_Type
--		,Doc_NumberString
--		,Doc_Number
--		,Doc_Month
--		,Doc_Year
--		,Doc_Series
--		,Doc_CreationDate
--		,Doc_CompletionDate
--	) values 
--	(
--		@Doc_Type
--		,@Doc_NumberString
--		,@Doc_Number
--		,@Doc_Month
--		,@Doc_Year
--		,@Doc_Series
--		,GETDATE()
--		,@Doc_CompletionDate
--	)

--	select SCOPE_IDENTITY() Doc_Id, @Doc_Type Doc_Type
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 19.05.2024
---- Description:	Dodawanie nowego elementu do dokumentu
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.AddItemToDocument
--	@Doc_Id int
--	,@Doc_Type int
--	,@It_No int = null
--	,@It_Code varchar(100)
--	,@It_Name varchar(100)
--	,@It_Quantity decimal(14,9)
--	,@It_Description varchar(255) = null
--	,@It_CompletionDate datetime = null
--	,@It_Unit_Id int
--	,@It_Unit_Type int
--	,@It_Unit_No int
--	,@It_Secondary_Unit_Id int
--	,@It_Secondary_Unit_Type int
--	,@It_Secondary_Unit_No int
--	,@It_Code_Cun_Id int = null
--	,@It_Code_Cun_Type int = null
--	,@It_Code_Cun_No int = null
--	,@It_Name_Cun_Id int = null
--	,@It_Name_Cun_Type int = null
--	,@It_Name_Cun_No int = null
--	,@It_Src_Wh_Id int
--	,@It_Current_Wh_Id int
--	,@It_Dst_Wh_Id int
--	,@It_Art_Id int
--	,@It_Art_Type int
--	,@It_Art_No int
--	,@It_ArB_Id int
--	,@It_Arb_Type int
--	,@It_Arb_No int
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--	if (coalesce(@It_No, 0) = 0)
--		select @It_No = max(coalesce(It_No, 0)) + 1 from sWMS.Documents
--		left join sWMS.Items on Doc_Id = It_Doc_Id
--		and Doc_Type = It_Doc_Type
--		where Doc_Id = @Doc_Id
--			and Doc_Type = @Doc_Type
--	else
--		set @It_No = @It_No + 1

--    insert into sWMS.Items
--	(
--		It_Doc_Id
--		,It_Doc_Type
--		,It_No
--		,It_Code
--		,It_Name
--		,It_Quantity
--		,It_Description
--		,It_CompletionDate
--		,It_Unit_Id
--		,It_Unit_Type
--		,It_Unit_No
--		,It_Secondary_Unit_Id
--		,It_Secondary_Unit_Type
--		,It_Secondary_Unit_No
--		,It_Code_Cun_Id
--		,It_Code_Cun_Type 
--		,It_Code_Cun_No 
--		,It_Name_Cun_Id 
--		,It_Name_Cun_Type 
--		,It_Name_Cun_No 
--		,It_Src_Wh_Id 
--		,It_Current_Wh_Id 
--		,It_Dst_Wh_Id 
--		,It_Art_Id 
--		,It_Art_Type 
--		,It_Art_No 
--		,It_ArB_Id 
--		,It_Arb_Type 
--		,It_Arb_No
--	) values 
--	(
--		@Doc_Id
--		,@Doc_Type
--		,@It_No
--		,@It_Code
--		,@It_Name
--		,@It_Quantity
--		,@It_Description
--		,@It_CompletionDate
--		,@It_Unit_Id
--		,@It_Unit_Type
--		,@It_Unit_No
--		,@It_Secondary_Unit_Id
--		,@It_Secondary_Unit_Type
--		,@It_Secondary_Unit_No
--		,@It_Code_Cun_Id
--		,@It_Code_Cun_Type 
--		,@It_Code_Cun_No 
--		,@It_Name_Cun_Id 
--		,@It_Name_Cun_Type 
--		,@It_Name_Cun_No 
--		,@It_Src_Wh_Id 
--		,@It_Current_Wh_Id 
--		,@It_Dst_Wh_Id 
--		,@It_Art_Id 
--		,@It_Art_Type 
--		,@It_Art_No 
--		,@It_ArB_Id 
--		,@It_Arb_Type 
--		,@It_Arb_No
--	)

--	select @Doc_Id Doc_Id, @Doc_Type Doc_Type, @It_No It_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 19.05.2024
---- Description:	Tworzenie partii
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.CreateNewArticleBatch 
--	@ArB_Type int
--	,@ArB_No int
--	,@ArB_Code varchar(100)
--	,@ArB_Name varchar(100)
--	,@ArB_Art_Id int
--	,@ArB_Art_Type int
--	,@ArB_Art_No int
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--   insert into sWMS.ArticlesBatches
--	(
--		ArB_Type
--		,ArB_No
--		,ArB_Code
--		,ArB_Name
--		,ArB_Art_Id
--		,ArB_Art_Type
--		,ArB_Art_No
--	) values 
--	(
--		@ArB_Type
--		,@ArB_No
--		,@ArB_Code
--		,@ArB_Name
--		,@ArB_Art_Id
--		,@ArB_Art_Type
--		,@ArB_Art_No
--	)

--	select SCOPE_IDENTITY() ArB_Id, @ArB_Type ArB_Type, @ArB_No ArB_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 19.05.2024
---- Description:	Tworzenie nowej kartoteki towarowej
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.CreateNewArticle
--	@Art_Type int,
--	@Art_No int,
--	@Art_Code varchar(100),
--	@Art_Name varchar(100),
--	@Art_Default_UnitId int = null
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;



--   insert into sWMS.Articles
--	(
--		Art_Type,
--		Art_No,
--		Art_Code,
--		Art_Name,
--		Art_Default_UnitId,
--		Art_CreationDate
--	) values 
--	(
--		@Art_Type,
--		@Art_No,
--		@Art_Code,
--		@Art_Name,
--		@Art_Default_UnitId,
--		GETDATE()
--	)

--	select SCOPE_IDENTITY() Art_Id, @Art_Type Art_Type, @Art_No Art_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 19.05.2024
---- Description:	Tworzenie nowej klasy atrybutu
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.CreateNewClassOfAttribute
--	@AtC_Type int,
--	@AtC_No int,
--	@AtC_Name varchar(100),
--	@AtC_DataType varchar(100)
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--   insert into sWMS.AttrClasses
--	(
--		AtC_Type,
--		AtC_No,
--		AtC_Name,
--		AtC_DataType
--	) values 
--	(
--		@AtC_Type,
--		@AtC_No,
--		@AtC_Name,
--		@AtC_DataType
--	)

--	select SCOPE_IDENTITY() AtC_Id, @AtC_Type AtC_Type, @AtC_No AtC_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 19.05.2024
---- Description:	Tworzenie nowego atrybutu
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.CreateNewAttribute
--	@Attr_Type int,
--	@Attr_No int,
--	@Attr_Object_Id int,
--	@Attr_Object_Type int,
--	@Attr_Object_No int,
--	@Attr_AtC_Id int,
--	@Attr_AtC_Type int,
--	@Attr_AtC_No int,
--	@Attr_Value varchar(255)
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--   insert into sWMS.Attributes
--	(
--		Attr_Type,
--		Attr_No,
--		Attr_Object_Id,
--		Attr_Object_Type,
--		Attr_Object_No,
--		Attr_AtC_Id,
--		Attr_AtC_Type,
--		Attr_AtC_No,
--		Attr_Value
--	) values 
--	(
--		@Attr_Type,
--		@Attr_No,
--		@Attr_Object_Id,
--		@Attr_Object_Type,
--		@Attr_Object_No,
--		@Attr_AtC_Id,
--		@Attr_AtC_Type,
--		@Attr_AtC_No,
--		@Attr_Value
--	)

--	select SCOPE_IDENTITY() Attr_Id, @Attr_Type Attr_Type, @Attr_No Attr_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 19.05.2024
---- Description:	Modyfikacja konfiguracji
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.UpdateConfig
--	@Conf_Id int,
--	@Conf_Value varchar(100)
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--	update sWMS.Config set Conf_Value = @Conf_Value
--	where Conf_Id = @Conf_Id

--	select SCOPE_IDENTITY() Conf_Id, @Conf_Value Conf_Value
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 19.05.2024
---- Description:	Dodawanie nowego kontrahenta
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.CreateNewContractor
--	@Con_Type int,
--	@Con_No int,
--	@Con_Code varchar(100),
--	@Con_FullName varchar(200),
--	@Con_Country varchar(100) = null,
--	@Con_City varchar(100) = null,
--	@Con_Street varchar(100) = null,
--	@Con_Postal varchar(100) = null,
--	@Con_Code_Cun_Id int = null,
--	@Con_Code_Cun_Type int = null,
--	@Con_Code_Cun_No int = null,
--	@Con_Name_Cun_Id int = null,
--	@Con_Name_Cun_Type int = null,
--	@Con_Name_Cun_No int = null,
--	@Con_Logo_BinD_Id int = null,
--	@Con_Logo_BinD_Type int = null,
--	@Con_Logo_BinD_No int = null
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--	insert into sWMS.Contractors 
--	(
--		Con_Type, 
--		Con_No, 
--		Con_Code,
--		Con_FullName,
--		Con_Country,
--		Con_City,
--		Con_Street,
--		Con_Postal,
--		Con_Code_Cun_Id, 
--		Con_Code_Cun_Type, 
--		Con_Code_Cun_No, 
--		Con_Name_Cun_Id, 
--		Con_Name_Cun_Type, 
--		Con_Name_Cun_No, 
--		Con_Logo_BinD_Id, 
--		Con_Logo_BinD_Type, 
--		Con_Logo_BinD_No
--	)
--	values
--	(
--		@Con_Type, 
--		@Con_No, 
--		@Con_Code,
--		@Con_FullName,
--		@Con_Country,
--		@Con_City,
--		@Con_Street,
--		@Con_Postal,
--		@Con_Code_Cun_Id, 
--		@Con_Code_Cun_Type, 
--		@Con_Code_Cun_No, 
--		@Con_Name_Cun_Id, 
--		@Con_Name_Cun_Type, 
--		@Con_Name_Cun_No, 
--		@Con_Logo_BinD_Id, 
--		@Con_Logo_BinD_Type, 
--		@Con_Logo_BinD_No
--	)

--	select SCOPE_IDENTITY() Con_Id, @Con_Type Con_Type, @Con_No Con_No
--END
--GO

---- =============================================
---- Author: Kamil Sikora
---- Create date: 19.05.2024
---- Description:	Dodawanie nowej jednostki
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.CreateNewUnit
--	@Unit_Type int,
--	@Unit_No int,
--	@Unit_Name varchar(100),
--	@Unit_Dividend decimal(14,9),
--	@Unit_Divisor decimal(14,9),
--	@Unit_AttachedTo_Art_Id int = null,
--	@Unit_AttachedTo_Art_Type int = null,
--	@Unit_AttachedTo_Art_No int = null
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--	insert into sWMS.Units 
--	(
--		Unit_Type,
--		Unit_No,
--		Unit_Name,
--		Unit_Dividend,
--		Unit_Divisor,
--		Unit_AttachedTo_Art_Id,
--		Unit_AttachedTo_Art_Type,
--		Unit_AttachedTo_Art_No
--	)
--	values
--	(
--		@Unit_Type,
--		@Unit_No,
--		@Unit_Name,
--		@Unit_Dividend,
--		@Unit_Divisor,
--		@Unit_AttachedTo_Art_Id,
--		@Unit_AttachedTo_Art_Type,
--		@Unit_AttachedTo_Art_No
--	)

--	SELECT SCOPE_IDENTITY() Unit_Id, @Unit_Type Unit_Type, @Unit_No Unit_No
--END
--GO

---- =============================================
---- Author: Kamil Sikora
---- Create date: 19.05.2024
---- Description:	Dodawanie nowej nazwy niestandardowej
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.CreateNewCustomName
--	@Cun_Type int,
--	@Cun_No int,
--	@Cun_Name varchar(100)
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--	insert into sWMS.CustomNames
--	(
--		Cun_Type,
--		Cun_No,
--		Cun_Name
--	)
--	values
--	(
--		@Cun_Type,
--		@Cun_No,
--		@Cun_Name
--	)

--	SELECT SCOPE_IDENTITY() Cun_Id, @Cun_Type Cun_Type, @Cun_No Cun_No
--END
--GO

---- =============================================
---- Author: Kamil Sikora
---- Create date: 19.05.2024
---- Description:	Dodawanie nowego magazynu
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.CreateNewWarehouse
--	@Wh_Code varchar(100),
--	@Wh_Name varchar(100),
--	@Wh_Country varchar(100) = null,
--	@Wh_City varchar(100) = null,
--	@Wh_Street varchar(100) = null,
--	@Wh_Postal varchar(100) = null
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--	insert into sWMS.Warehouses
--	(
--		Wh_Code,
--		Wh_Name,
--		Wh_Country,
--		Wh_City,
--		Wh_Street,
--		Wh_Postal
--	)
--	values
--	(
--		@Wh_Code,
--		@Wh_Name,
--		@Wh_Country,
--		@Wh_City,
--		@Wh_Street,
--		@Wh_Postal
--	)

--	SELECT SCOPE_IDENTITY() Wh_Id
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Usuwanie pozycji z dokumentu
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.RemoveItemFromDocument
--	@Doc_Id int,
--	@Doc_Type int,
--	@It_No int
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    delete from sWMS.Items 
--	where It_Doc_Id = @Doc_Id
--		and It_Doc_Type = @Doc_Type
--		and It_No = @It_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Usuwanie kartoteki towarowej
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.RemoveArticle
--	@Art_Id int,
--	@Art_Type int,
--	@Art_No int
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    delete from sWMS.Articles 
--	where Art_Id = @Art_Id
--		and Art_Type = @Art_Type
--		and Art_No = @Art_No

--	--update It set It_Art_Id = -1
--	--	,It_Art_Type = -1
--	--	,It_Art_No = -1
--	--	,It_Code = It_Code + ' (Kartoteka towarowa usunięta) '
--	--	,It_Name = It_Name + ' (Kartoteka towarowa usunięta) '
--	--from
--	--	sWMS.Items It where It_Art_Id = @Art_Id
--	--	and It_Art_Type = @Art_Type
--	--	and It_Art_No = @Art_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Usuwanie partii
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.RemoveArticlesBatches
--	@ArB_Id int,
--	@ArB_Type int,
--	@ArB_No int
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    delete from sWMS.ArticlesBatches 
--	where ArB_Id = @ArB_Id
--		and ArB_Type = @ArB_Type
--		and ArB_No = @ArB_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Usuwanie kontrahentów
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.RemoveContractors
--	@Con_Id int,
--	@Con_Type int,
--	@Con_No int
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    delete from sWMS.Contractors
--	where Con_Id = @Con_Id
--		and Con_Type = @Con_Type
--		and Con_No = @Con_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Usuwanie kontrahentów
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.RemoveCustomName
--	@Cun_Id int,
--	@Cun_Type int,
--	@Cun_No int
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    delete from sWMS.CustomNames
--	where Cun_Id = @Cun_Id
--		and Cun_Type = @Cun_Type
--		and Cun_No = @Cun_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Usuwanie dokumentów
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.RemoveDocument
--	@Doc_Id int,
--	@Doc_Type int
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    delete from sWMS.Documents
--	where Doc_Id = @Doc_Id
--		and Doc_Type = @Doc_Type
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Usuwanie magazynów / lokalizacji
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.RemoveWarehouse
--	@Wh_Id int
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    delete from sWMS.Warehouses
--	where Wh_Id = @Wh_Id
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Usuwanie jednostek
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.RemoveUnit
--	@Unit_Id int
--	,@Unit_Type int
--	,@Unit_No int
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    delete from sWMS.Units
--	where Unit_Id = @Unit_Id
--		and Unit_Type = @Unit_Type
--		and Unit_No = @Unit_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Modyfikacja towaru
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.UpdateArticle
--	@Art_Id int
--	,@Art_Type int
--	,@Art_No int
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    delete from sWMS.Articles
--	where Art_Id = @Art_Id
--		and Art_Type = @Art_Type
--		and Art_No = @Art_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Modyfikacja towaru
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.UpdateArticle
--	@Art_Id int
--	,@Art_Type int
--	,@Art_No int
--	,@Art_Code varchar(100)
--	,@Art_Name varchar(100)
--	,@Art_Default_UnitId int
--	,@Art_CreationDate datetime
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    update sWMS.Articles
--	set	Art_Code = @Art_Code,
--	Art_Name = @Art_Name,
--	Art_Default_UnitId = @Art_Default_UnitId,
--	Art_CreationDate = @Art_CreationDate
--	where Art_Id = @Art_Id
--		and Art_Type = @Art_Type
--		and Art_No = @Art_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Modyfikacja kontrahenta
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.UpdateContractor
--	@Con_Id int
--	,@Con_Type int
--	,@Con_No int
--	,@Con_Code varchar(100)
--	,@Con_FullName varchar(100)
--	,@Con_Country varchar(100) = null
--	,@Con_City varchar(100) = null
--	,@Con_Street varchar(100) = null
--	,@Con_Postal varchar(100) = null
--	,@Con_Code_Cun_Id int = null
--	,@Con_Code_Cun_Type int = null
--	,@Con_Code_Cun_No int = null
--	,@Con_Name_Cun_Id int = null
--	,@Con_Name_Cun_Type int = null
--	,@Con_Name_Cun_No int = null
--	,@Con_Logo_BinD_Id int = null
--	,@Con_Logo_BinD_Type int = null
--	,@Con_Logo_BinD_No int = null
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    update sWMS.Contractors
--	set	Con_Code = @Con_Code
--	,Con_FullName = @Con_FullName
--	,Con_Country = @Con_Country 
--	,Con_City = @Con_City 
--	,Con_Street = @Con_Street 
--	,Con_Postal = @Con_Postal 
--	,Con_Code_Cun_Id = @Con_Code_Cun_Id 
--	,Con_Code_Cun_Type = @Con_Code_Cun_Type 
--	,Con_Code_Cun_No = @Con_Code_Cun_No 
--	,Con_Name_Cun_Id = @Con_Name_Cun_Id 
--	,Con_Name_Cun_Type = @Con_Name_Cun_Type 
--	,Con_Name_Cun_No = @Con_Name_Cun_No 
--	,Con_Logo_BinD_Id = @Con_Logo_BinD_Id 
--	,Con_Logo_BinD_Type = @Con_Logo_BinD_Type 
--	,Con_Logo_BinD_No = @Con_Logo_BinD_No 
--	where Con_Id = @Con_Id
--		and Con_Type = @Con_Type
--		and Con_No = @Con_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Modyfikacja magazynu
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.UpdateWarehouse
--	@Wh_Id int,
--	@Wh_Code varchar(100),
--	@Wh_Name varchar(100),
--	@Wh_Country varchar(100) = null,
--	@Wh_City varchar(100) = null,
--	@Wh_Street varchar(100) = null,
--	@Wh_Postal varchar(100) = null
--AS
--BEGIN
--	SET NOCOUNT ON;

--    update sWMS.Warehouses
--	set Wh_Code = @Wh_Code,
--	Wh_Name = @Wh_Name,
--	Wh_Country = @Wh_Country,
--	Wh_City = @Wh_City,
--	Wh_Street = @Wh_Street,
--	Wh_Postal = @Wh_Postal
--	where Wh_Id = @Wh_Id 
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Modyfikacja partii
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.UpdateArticlesBatches
--	@ArB_Id int,
--	@ArB_Type int,
--	@ArB_No int,
--	@ArB_Code varchar(100),
--	@ArB_Name varchar(100),
--	@ArB_Quantity decimal(14,9),
--	@ArB_Unit_Id int,
--	@ArB_Unit_Type int,
--	@ArB_Unit_No int,
--	@ArB_Secondary_Unit_Id int,
--	@ArB_Secondary_Unit_Type int,
--	@ArB_Secondary_Unit_No int,
--	@ArB_Art_Id int,
--	@ArB_Art_Type int,
--	@ArB_Art_No int
--AS
--BEGIN
--	SET NOCOUNT ON;

--    update sWMS.ArticlesBatches
--	set ArB_Code = @ArB_Code,
--		ArB_Name = @ArB_Name,
--		ArB_Quantity = @ArB_Quantity,
--		ArB_Unit_Id = @ArB_Unit_Id,
--		ArB_Unit_Type = @ArB_Unit_Type,
--		ArB_Unit_No = @ArB_Unit_No,
--		ArB_Secondary_Unit_Id = @ArB_Secondary_Unit_Id,
--		ArB_Secondary_Unit_Type = @ArB_Secondary_Unit_Type,
--		ArB_Secondary_Unit_No = @ArB_Secondary_Unit_No,
--		ArB_Art_Id = @ArB_Art_Id,
--		ArB_Art_Type = @ArB_Art_Type,
--		ArB_Art_No = @ArB_Art_No
--	where ArB_Id = @ArB_Id
--		and ArB_Type = @ArB_Type
--		and ArB_No = @ArB_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Modyfikacja nazw
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.UpdateCustomName
--	@Cun_Id int,
--	@Cun_Type int,
--	@Cun_No int,
--	@Cun_Name varchar(100)
--AS
--BEGIN
--	SET NOCOUNT ON;

--    update sWMS.CustomNames
--	set Cun_Name = @Cun_Name 
--	where Cun_Id = @Cun_Id
--		and Cun_Type = @Cun_Type
--		and Cun_No = @Cun_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Modyfikacja atrybutów
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.UpdateAttributes
--	@Attr_Id int,
--	@Attr_Type int,
--	@Attr_No int,
--	@Attr_Value varchar(255)
--AS
--BEGIN
--	SET NOCOUNT ON;

--    update sWMS.Attributes
--	set Attr_Value = @Attr_Value
--	where Attr_Id = @Attr_Id
--		and Attr_Type = @Attr_Type
--		and Attr_No = @Attr_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Modyfikacja klas atrybutów
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.UpdateClassOfAttribute
--	@AtC_Id int,
--	@AtC_Type int,
--	@AtC_No int,
--	@AtC_Name varchar(100)
--AS
--BEGIN
--	SET NOCOUNT ON;

--    update sWMS.AttrClasses
--	set AtC_Name = @AtC_Name
--	where AtC_Id = @AtC_Id
--		and AtC_Type = @AtC_Type
--		and AtC_No = @AtC_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Modyfikacja dokumentów
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.UpdateDocument
--	@Doc_Id int,
--	@Doc_Type int,
--	@Doc_NumberString varchar(100),
--	@Doc_Number int,
--	@Doc_Month int,
--	@Doc_Year int,
--	@Doc_Series varchar(5),
--	@Doc_CreationDate datetime,
--	@Doc_CompletionDate datetime
--AS
--BEGIN
--	SET NOCOUNT ON;

--    update sWMS.Documents
--	set Doc_NumberString = @Doc_NumberString,
--	Doc_Number = @Doc_Number,
--	Doc_Month = @Doc_Month,
--	Doc_Year = @Doc_Year,
--	Doc_Series = @Doc_Series,
--	Doc_CreationDate = @Doc_CreationDate,
--	Doc_CompletionDate = @Doc_CompletionDate
--	where Doc_Id = @Doc_Id
--		and Doc_Type = @Doc_Type
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Modyfikacja jednostek
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.UpdateUnit
--	@Unit_Id int,
--	@Unit_Type int,
--	@Unit_No int,
--	@Unit_Name varchar(100),
--	@Unit_Dividend decimal(14,9),
--	@Unit_Divisor decimal(14,9),
--	@Unit_AttachedTo_Art_Id int,
--	@Unit_AttachedTo_Art_Type int,
--	@Unit_AttachedTo_Art_No int
--AS
--BEGIN
--	SET NOCOUNT ON;

--    update sWMS.Units
--	set Unit_Name = @Unit_Name,
--	Unit_Dividend = @Unit_Dividend,
--	Unit_Divisor = @Unit_Divisor,
--	Unit_AttachedTo_Art_Id = @Unit_AttachedTo_Art_Id,
--	Unit_AttachedTo_Art_Type = @Unit_AttachedTo_Art_Type,
--	Unit_AttachedTo_Art_No = @Unit_AttachedTo_Art_No
--	where Unit_Id = @Unit_Id
--		and Unit_Type = @Unit_Type
--		and Unit_No = @Unit_No
--END
--GO

---- =============================================
---- Author:		Kamil Sikora
---- Create date: 20.05.2024
---- Description:	Modyfikacja pozycji dokumentu
---- =============================================
--CREATE OR ALTER PROCEDURE sWMS.UpdateDocumentItem
--	@Doc_Id int,
--	@Doc_Type int,
--	@It_No int,
--	@IT_Code varchar(100),
--	@IT_Name varchar(100),
--	@IT_Quantity decimal(14,9),
--	@IT_Description varchar(255),
--	@IT_CompletionDate datetime,
--	@IT_Unit_Id int,
--	@IT_Unit_Type int,
--	@IT_Unit_No int,
--	@IT_Secondary_Unit_Id int,
--	@IT_Secondary_Unit_Type int,
--	@IT_Secondary_Unit_No int,
--	@IT_Code_Cun_Id int,
--	@IT_Code_Cun_Type int,
--	@IT_Code_Cun_No int,
--	@IT_Name_Cun_Id int,
--	@IT_Name_Cun_Type int,
--	@IT_Name_Cun_No int,
--	@IT_Src_Wh_Id int,
--	@IT_Current_Wh_Id int,
--	@IT_Dst_Wh_Id int,
--	@IT_Art_Id int,
--	@IT_Art_Type int,
--	@IT_Art_No int,
--	@IT_ArB_Id int,
--	@IT_Arb_Type int,
--	@IT_Arb_No int
--AS
--BEGIN
--	SET NOCOUNT ON;

--    update sWMS.Items
--	set IT_Code = @IT_Code,
--	IT_Name = @IT_Name,
--	IT_Quantity = @IT_Quantity,
--	IT_Description = @IT_Description,
--	IT_CompletionDate = @IT_CompletionDate,
--	IT_Unit_Id = @IT_Unit_Id,
--	IT_Unit_Type = @IT_Unit_Type,
--	IT_Unit_No = @IT_Unit_No,
--	IT_Secondary_Unit_Id = @IT_Secondary_Unit_Id,
--	IT_Secondary_Unit_Type = @IT_Secondary_Unit_Type,
--	IT_Secondary_Unit_No = @IT_Secondary_Unit_No,
--	IT_Code_Cun_Id = @IT_Code_Cun_Id,
--	IT_Code_Cun_Type = @IT_Code_Cun_Type,
--	IT_Code_Cun_No = @IT_Code_Cun_No,
--	IT_Name_Cun_Id = @IT_Name_Cun_Id,
--	IT_Name_Cun_Type = @IT_Name_Cun_Type,
--	IT_Name_Cun_No = @IT_Name_Cun_No,
--	IT_Src_Wh_Id = @IT_Src_Wh_Id,
--	IT_Current_Wh_Id = @IT_Current_Wh_Id,
--	IT_Dst_Wh_Id = @IT_Dst_Wh_Id,
--	IT_Art_Id = @IT_Art_Id,
--	IT_Art_Type = @IT_Art_Type,
--	IT_Art_No = @IT_Art_No,
--	IT_ArB_Id = @IT_ArB_Id,
--	IT_Arb_Type = @IT_Arb_Type,
--	IT_Arb_No = @IT_Arb_No
--	where IT_Doc_Id = @Doc_Id
--		and It_Doc_Type = @Doc_Type
--		and It_No = @It_No
--END
--GO

--CREATE OR ALTER PROCEDURE sWMS.ViewDocuments
--	@QuantityOfRows int,
--	@GenerationDateFrom varchar(100),
--	@GenerationDateTo varchar(100),
--	@CompletionDateFrom varchar(100),
--	@CompletionDateTo varchar(100),
--	@SearchQuery varchar(100),
--	@DocumentType int
--AS
--BEGIN
--	SET NOCOUNT ON;

--	select * into #tmp from sWMS.Documents d
--	where d.Doc_CreationDate between @GenerationDateFrom and @GenerationDateTo
--		and d.Doc_CompletionDate between @GenerationDateFrom and @GenerationDateTo
--		and d.Doc_NumberString like '%' + @SearchQuery + '%'
--		and d.Doc_Type = @DocumentType

--	declare @SQLString nvarchar(100) = 'select top '+ convert(nvarchar(10), @QuantityOfRows) + ' * from #tmp'
--    EXECUTE sp_executesql @SQLString
--END
--GO

--CREATE OR ALTER PROCEDURE sWMS.ViewItems
--	@It_Doc_Id int
--	,@It_Doc_Type int
--	,@It_No int
--AS
--BEGIN
--	SET NOCOUNT ON;
--	select * from sWMS.Items i
--	left join sWMS.CustomNames cun_code on cun_code.Cun_Id = i.It_Code_Cun_Id
--		and cun_code.Cun_Type = i.It_Code_Cun_Type
--		and cun_code.Cun_No = i.It_Code_Cun_No
--	left join sWMS.CustomNames cun_names on cun_names.Cun_Id = i.It_Name_Cun_Id
--		and cun_names.Cun_Type = i.It_Name_Cun_Type
--		and cun_names.Cun_No = i.It_Name_Cun_No
--	join sWMS.Units primaryUnits on primaryUnits.Unit_Id = i.It_Unit_Id
--		and primaryUnits.Unit_Type = i.It_Unit_Type
--		and primaryUnits.Unit_No = i.It_Unit_No
--	left join sWMS.Units secondaryUnits on secondaryUnits.Unit_Id = i.It_Secondary_Unit_Id
--		and secondaryUnits.Unit_Type = i.It_Secondary_Unit_Type
--		and secondaryUnits.Unit_No = i.It_Unit_No
--	join sWMS.Articles art on art.Art_Id = i.It_Art_Id
--		and art.Art_Type = i.It_Art_Type
--		and art.Art_No = i.It_Art_No
--	join sWMS.ArticlesBatches ArB on ArB.ArB_Id = i.It_ArB_Id
--		and ArB.ArB_Type = i.It_Arb_Type
--		and ArB.ArB_No = i.It_Arb_No
--	where It_Doc_Id = @It_Doc_Id
--		and It_Doc_Type = @It_Doc_Type
--		and It_No = @It_No
--END
--GO

--CREATE OR ALTER PROCEDURE sWMS.ViewItemDetails
--	@It_Doc_Id int
--	,@It_Doc_Type int
--	,@It_No int
--AS
--BEGIN
--	SET NOCOUNT ON;
--	select * from sWMS.Items i
--	left join sWMS.CustomNames cun_code on cun_code.Cun_Id = i.It_Code_Cun_Id
--		and cun_code.Cun_Type = i.It_Code_Cun_Type
--		and cun_code.Cun_No = i.It_Code_Cun_No
--	left join sWMS.CustomNames cun_names on cun_names.Cun_Id = i.It_Name_Cun_Id
--		and cun_names.Cun_Type = i.It_Name_Cun_Type
--		and cun_names.Cun_No = i.It_Name_Cun_No
--	join sWMS.Units primaryUnits on primaryUnits.Unit_Id = i.It_Unit_Id
--		and primaryUnits.Unit_Type = i.It_Unit_Type
--		and primaryUnits.Unit_No = i.It_Unit_No
--	left join sWMS.Units secondaryUnits on secondaryUnits.Unit_Id = i.It_Secondary_Unit_Id
--		and secondaryUnits.Unit_Type = i.It_Secondary_Unit_Type
--		and secondaryUnits.Unit_No = i.It_Unit_No
--	join sWMS.Articles art on art.Art_Id = i.It_Art_Id
--		and art.Art_Type = i.It_Art_Type
--		and art.Art_No = i.It_Art_No
--	join sWMS.ArticlesBatches ArB on ArB.ArB_Id = i.It_ArB_Id
--		and ArB.ArB_Type = i.It_Arb_Type
--		and ArB.ArB_No = i.It_Arb_No
--	join sWMS.Warehouses srcWarehouse on srcWarehouse.Wh_Id = i.It_Src_Wh_Id
--	join sWMS.Warehouses curWarehouse on curWarehouse.Wh_Id = i.It_Current_Wh_Id
--	join sWMS.Warehouses dstWarehouse on dstWarehouse.Wh_Id = i.It_Dst_Wh_Id
--	left join sWMS.Contractors con on con.Con_Id = i.It_Con_Id
--		and con.Con_Type = i.It_Con_Type
--		and con.Con_No = i.It_Con_No
--	left join sWMS.Attributes attr on attr.Attr_Object_Id = i.It_Doc_Id
--		and attr.Attr_Object_Type = i.It_Doc_Type
--		and attr.Attr_Object_No = i.It_No
--	left join sWMS.AttrClasses atc on atc.AtC_Id = attr.Attr_AtC_Id
--		and atc.AtC_Type = attr.Attr_AtC_Type
--		and atc.AtC_No = attr.Attr_AtC_No
--	where It_Doc_Id = @It_Doc_Id
--		and It_Doc_Type = @It_Doc_Type
--		and It_No = @It_No
--END
--GO

--CREATE OR ALTER PROCEDURE [sWMS].[ViewDocumentDetails]
--	@It_Doc_Id int
--	,@It_Doc_Type int
--	,@It_No int
--AS
--BEGIN
--	SET NOCOUNT ON;
--	select * from sWMS.Documents d
--	left join sWMS.Attributes attr on attr.Attr_Id = d.Doc_Id
--		and attr.Attr_Type = d.Doc_Type
--	left join sWMS.AttrClasses AtC on AtC.AtC_Id = attr.Attr_AtC_Id
--		and AtC.AtC_Type = attr.Attr_AtC_Type
--		and AtC.AtC_No = attr.Attr_No
--	left join sWMS.Items i on i.It_Doc_Id = d.Doc_Id
--	join sWMS.Units primaryUnits on primaryUnits.Unit_Id = i.It_Unit_Id
--		and primaryUnits.Unit_Type = i.It_Unit_Type
--		and primaryUnits.Unit_No = i.It_Unit_No
--	left join sWMS.Units secondaryUnits on secondaryUnits.Unit_Id = i.It_Secondary_Unit_Id
--		and secondaryUnits.Unit_Type = i.It_Secondary_Unit_Type
--		and secondaryUnits.Unit_No = i.It_Secondary_Unit_No
--	join sWMS.Articles art on art.Art_Id = i.It_Art_Id
--		and art.Art_Type = i.It_Art_Type
--		and art.Art_No = i.It_Art_No
--	join sWMS.ArticlesBatches ArB on ArB.ArB_Id = i.It_ArB_Id
--		and ArB.ArB_Type = i.It_Arb_Type
--		and ArB.ArB_No = i.It_Arb_No
--	join sWMS.Warehouses srcWarehouse on srcWarehouse.Wh_Id = i.It_Src_Wh_Id
--	join sWMS.Warehouses curWarehouse on curWarehouse.Wh_Id = i.It_Current_Wh_Id
--	join sWMS.Warehouses dstWarehouse on dstWarehouse.Wh_Id = i.It_Dst_Wh_Id
--	where It_Doc_Id = @It_Doc_Id
--		and It_Doc_Type = @It_Doc_Type
--		and It_No = @It_No
--END
--GO

