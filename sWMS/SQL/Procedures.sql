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
