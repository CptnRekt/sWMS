SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kamil Sikora
-- Create date: 19.05.2024
-- Description:	Dodawanie pustego dokumentu
-- =============================================
CREATE OR ALTER PROCEDURE sWMS.CreateNewDocument
	@Doc_Type int
	,@Doc_Number int = null
	,@Doc_Month int = null
	,@Doc_Year int = null
	,@Doc_Series varchar(100) = null
	,@Doc_CompletionDate datetime = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @Doc_NumberString varchar(100) = sWMS.GenerateDocumentNumber(@Doc_Number, @Doc_Month, @Doc_Year, @Doc_Series)

    insert into sWMS.Documents 
	(
		Doc_Type
		,Doc_NumberString
		,Doc_Number
		,Doc_Month
		,Doc_Year
		,Doc_Series
		,Doc_CreationDate
		,Doc_CompletionDate
	) values 
	(
		@Doc_Type
		,@Doc_NumberString
		,@Doc_Number
		,@Doc_Month
		,@Doc_Year
		,@Doc_Series
		,GETDATE()
		,@Doc_CompletionDate
	)

	select SCOPE_IDENTITY() Doc_Id, @Doc_Type Doc_Type
END
GO

-- =============================================
-- Author:		Kamil Sikora
-- Create date: 19.05.2024
-- Description:	Dodawanie nowego elementu do dokumentu
-- =============================================
CREATE OR ALTER PROCEDURE sWMS.AddItemToDocument
	@Doc_Id int
	,@Doc_Type int
	,@It_No int = null
	,@It_Code varchar(100)
	,@It_Name varchar(100)
	,@It_Quantity decimal(14,9)
	,@It_Description varchar(255) = null
	,@It_CompletionDate datetime = null
	,@It_Unit_Id int
	,@It_Unit_Type int
	,@It_Unit_No int
	,@It_Secondary_Unit_Id int
	,@It_Secondary_Unit_Type int
	,@It_Secondary_Unit_No int
	,@It_Code_Cun_Id int = null
	,@It_Code_Cun_Type int = null
	,@It_Code_Cun_No int = null
	,@It_Name_Cun_Id int = null
	,@It_Name_Cun_Type int = null
	,@It_Name_Cun_No int = null
	,@It_Src_Wh_Id int
	,@It_Current_Wh_Id int
	,@It_Dst_Wh_Id int
	,@It_Art_Id int
	,@It_Art_Type int
	,@It_Art_No int
	,@It_ArB_Id int
	,@It_Arb_Type int
	,@It_Arb_No int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if (coalesce(@It_No, 0) = 0)
		select @It_No = max(coalesce(It_No, 0)) + 1 from sWMS.Documents
		left join sWMS.Items on Doc_Id = It_Doc_Id
		and Doc_Type = It_Doc_Type
		where Doc_Id = @Doc_Id
			and Doc_Type = @Doc_Type
	else
		set @It_No = @It_No + 1

    insert into sWMS.Items
	(
		It_Doc_Id
		,It_Doc_Type
		,It_No
		,It_Code
		,It_Name
		,It_Quantity
		,It_Description
		,It_CompletionDate
		,It_Unit_Id
		,It_Unit_Type
		,It_Unit_No
		,It_Secondary_Unit_Id
		,It_Secondary_Unit_Type
		,It_Secondary_Unit_No
		,It_Code_Cun_Id
		,It_Code_Cun_Type 
		,It_Code_Cun_No 
		,It_Name_Cun_Id 
		,It_Name_Cun_Type 
		,It_Name_Cun_No 
		,It_Src_Wh_Id 
		,It_Current_Wh_Id 
		,It_Dst_Wh_Id 
		,It_Art_Id 
		,It_Art_Type 
		,It_Art_No 
		,It_ArB_Id 
		,It_Arb_Type 
		,It_Arb_No
	) values 
	(
		@Doc_Id
		,@Doc_Type
		,@It_No
		,@It_Code
		,@It_Name
		,@It_Quantity
		,@It_Description
		,@It_CompletionDate
		,@It_Unit_Id
		,@It_Unit_Type
		,@It_Unit_No
		,@It_Secondary_Unit_Id
		,@It_Secondary_Unit_Type
		,@It_Secondary_Unit_No
		,@It_Code_Cun_Id
		,@It_Code_Cun_Type 
		,@It_Code_Cun_No 
		,@It_Name_Cun_Id 
		,@It_Name_Cun_Type 
		,@It_Name_Cun_No 
		,@It_Src_Wh_Id 
		,@It_Current_Wh_Id 
		,@It_Dst_Wh_Id 
		,@It_Art_Id 
		,@It_Art_Type 
		,@It_Art_No 
		,@It_ArB_Id 
		,@It_Arb_Type 
		,@It_Arb_No
	)

	select @Doc_Id Doc_Id, @Doc_Type Doc_Type, @It_No It_No
END
GO

-- =============================================
-- Author:		Kamil Sikora
-- Create date: 19.05.2024
-- Description:	Tworzenie partii
-- =============================================
CREATE OR ALTER PROCEDURE sWMS.CreateNewArticleBatch 
	@ArB_Type int
	,@ArB_No int
	,@ArB_Code varchar(100)
	,@ArB_Name varchar(100)
	,@ArB_Art_Id int
	,@ArB_Art_Type int
	,@ArB_Art_No int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   insert into sWMS.ArticlesBatches
	(
		ArB_Type
		,ArB_No
		,ArB_Code
		,ArB_Name
		,ArB_Art_Id
		,ArB_Art_Type
		,ArB_Art_No
	) values 
	(
		@ArB_Type
		,@ArB_No
		,@ArB_Code
		,@ArB_Name
		,@ArB_Art_Id
		,@ArB_Art_Type
		,@ArB_Art_No
	)

	select SCOPE_IDENTITY() ArB_Id, @ArB_Type ArB_Type, @ArB_No ArB_No
END
GO

-- =============================================
-- Author:		Kamil Sikora
-- Create date: 19.05.2024
-- Description:	Tworzenie nowej kartoteki towarowej
-- =============================================
CREATE PROCEDURE sWMS.CreateNewArticle
	@Art_Type int,
	@Art_No int,
	@Art_Code varchar(100),
	@Art_Name varchar(100),
	@Art_Default_UnitId int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;



   insert into sWMS.Articles
	(
		Art_Type,
		Art_No,
		Art_Code,
		Art_Name,
		Art_Default_UnitId,
		Art_CreationDate
	) values 
	(
		@Art_Type,
		@Art_No,
		@Art_Code,
		@Art_Name,
		@Art_Default_UnitId,
		GETDATE()
	)

	select SCOPE_IDENTITY() Art_Id, @Art_Type Art_Type, @Art_No Art_No
END
GO

-- =============================================
-- Author:		Kamil Sikora
-- Create date: 19.05.2024
-- Description:	Tworzenie nowej klasy atrybutu
-- =============================================
CREATE PROCEDURE sWMS.CreateNewClassOfAttribute
	@AtC_Type int,
	@AtC_No int,
	@AtC_Name varchar(100),
	@AtC_DataType varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   insert into sWMS.AttrClasses
	(
		AtC_Type,
		AtC_No,
		AtC_Name,
		AtC_DataType
	) values 
	(
		@AtC_Type,
		@AtC_No,
		@AtC_Name,
		@AtC_DataType
	)

	select SCOPE_IDENTITY() AtC_Id, @AtC_Type AtC_Type, @AtC_No AtC_No
END
GO

-- =============================================
-- Author:		Kamil Sikora
-- Create date: 19.05.2024
-- Description:	Tworzenie nowego atrybutu
-- =============================================
CREATE PROCEDURE sWMS.CreateNewAttribute
	@Attr_Type int,
	@Attr_No int,
	@Attr_Object_Id int,
	@Attr_Object_Type int,
	@Attr_Object_No int,
	@Attr_AtC_Id int,
	@Attr_AtC_Type int,
	@Attr_AtC_No int,
	@Attr_Value varchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   insert into sWMS.Attributes
	(
		Attr_Type,
		Attr_No,
		Attr_Object_Id,
		Attr_Object_Type,
		Attr_Object_No,
		Attr_AtC_Id,
		Attr_AtC_Type,
		Attr_AtC_No,
		Attr_Value
	) values 
	(
		@Attr_Type,
		@Attr_No,
		@Attr_Object_Id,
		@Attr_Object_Type,
		@Attr_Object_No,
		@Attr_AtC_Id,
		@Attr_AtC_Type,
		@Attr_AtC_No,
		@Attr_Value
	)

	select SCOPE_IDENTITY() Attr_Id, @Attr_Type Attr_Type, @Attr_No Attr_No
END
GO

-- =============================================
-- Author:		Kamil Sikora
-- Create date: 19.05.2024
-- Description:	Modyfikacja konfiguracji
-- =============================================
CREATE PROCEDURE sWMS.UpdateConfig
	@Conf_Id int,
	@Conf_Value varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update sWMS.Config set Conf_Value = @Conf_Value
	where Conf_Id = @Conf_Id

	select SCOPE_IDENTITY() Conf_Id, @Conf_Value Conf_Value
END
GO

-- =============================================
-- Author:		Kamil Sikora
-- Create date: 19.05.2024
-- Description:	Dodawanie nowego kontrahenta
-- =============================================
CREATE PROCEDURE sWMS.CreateNewContractor
	@Con_Type int,
	@Con_No int,
	@Con_Code varchar(100),
	@Con_FullName varchar(200),
	@Con_Country varchar(100) = null,
	@Con_City varchar(100) = null,
	@Con_Street varchar(100) = null,
	@Con_Postal varchar(100) = null,
	@Con_Code_Cun_Id int = null,
	@Con_Code_Cun_Type int = null,
	@Con_Code_Cun_No int = null,
	@Con_Name_Cun_Id int = null,
	@Con_Name_Cun_Type int = null,
	@Con_Name_Cun_No int = null,
	@Con_Logo_BinD_Id int = null,
	@Con_Logo_BinD_Type int = null,
	@Con_Logo_BinD_No int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	insert into sWMS.Contractors 
	(
		Con_Type, 
		Con_No, 
		Con_Code,
		Con_FullName,
		Con_Country,
		Con_City,
		Con_Street,
		Con_Postal,
		Con_Code_Cun_Id, 
		Con_Code_Cun_Type, 
		Con_Code_Cun_No, 
		Con_Name_Cun_Id, 
		Con_Name_Cun_Type, 
		Con_Name_Cun_No, 
		Con_Logo_BinD_Id, 
		Con_Logo_BinD_Type, 
		Con_Logo_BinD_No
	)
	values
	(
		@Con_Type, 
		@Con_No, 
		@Con_Code,
		@Con_FullName,
		@Con_Country,
		@Con_City,
		@Con_Street,
		@Con_Postal,
		@Con_Code_Cun_Id, 
		@Con_Code_Cun_Type, 
		@Con_Code_Cun_No, 
		@Con_Name_Cun_Id, 
		@Con_Name_Cun_Type, 
		@Con_Name_Cun_No, 
		@Con_Logo_BinD_Id, 
		@Con_Logo_BinD_Type, 
		@Con_Logo_BinD_No
	)

	select SCOPE_IDENTITY() Con_Id, @Con_Type Con_Type, @Con_No Con_No
END
GO

-- =============================================
-- Author: Kamil Sikora
-- Create date: 19.05.2024
-- Description:	Dodawanie nowej jednostki
-- =============================================
CREATE PROCEDURE sWMS.CreateNewUnit
	@Unit_Type int,
	@Unit_No int,
	@Unit_Name varchar(100),
	@Unit_Dividend decimal(14,9),
	@Unit_Divisor decimal(14,9),
	@Unit_AttachedTo_Art_Id int = null,
	@Unit_AttachedTo_Art_Type int = null,
	@Unit_AttachedTo_Art_No int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	insert into sWMS.Units 
	(
		Unit_Type,
		Unit_No,
		Unit_Name,
		Unit_Dividend,
		Unit_Divisor,
		Unit_AttachedTo_Art_Id,
		Unit_AttachedTo_Art_Type,
		Unit_AttachedTo_Art_No
	)
	values
	(
		@Unit_Type,
		@Unit_No,
		@Unit_Name,
		@Unit_Dividend,
		@Unit_Divisor,
		@Unit_AttachedTo_Art_Id,
		@Unit_AttachedTo_Art_Type,
		@Unit_AttachedTo_Art_No
	)

	SELECT SCOPE_IDENTITY() Unit_Id, @Unit_Type Unit_Type, @Unit_No Unit_No
END
GO

-- =============================================
-- Author: Kamil Sikora
-- Create date: 19.05.2024
-- Description:	Dodawanie nowej nazwy niestandardowej
-- =============================================
CREATE PROCEDURE sWMS.CreateNewCustomName
	@Cun_Type int,
	@Cun_No int,
	@Cun_Name varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	insert into sWMS.CustomNames
	(
		Cun_Type,
		Cun_No,
		Cun_Name
	)
	values
	(
		@Cun_Type,
		@Cun_No,
		@Cun_Name
	)

	SELECT SCOPE_IDENTITY() Cun_Id, @Cun_Type Cun_Type, @Cun_No Cun_No
END
GO

-- =============================================
-- Author: Kamil Sikora
-- Create date: 19.05.2024
-- Description:	Dodawanie nowego magazynu
-- =============================================
CREATE PROCEDURE sWMS.CreateNewWarehouse
	@Wh_Code varchar(100),
	@Wh_Name varchar(100),
	@Wh_Country varchar(100) = null,
	@Wh_City varchar(100) = null,
	@Wh_Street varchar(100) = null,
	@Wh_Postal varchar(100) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	insert into sWMS.Warehouses
	(
		Wh_Code,
		Wh_Name,
		Wh_Country,
		Wh_City,
		Wh_Street,
		Wh_Postal
	)
	values
	(
		@Wh_Code,
		@Wh_Name,
		@Wh_Country,
		@Wh_City,
		@Wh_Street,
		@Wh_Postal
	)

	SELECT SCOPE_IDENTITY() Wh_Id
END
GO