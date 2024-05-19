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

	select SCOPE_IDENTITY()
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

	select @It_No
END
GO