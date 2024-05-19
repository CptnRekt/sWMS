SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kamil Sikora
-- Create date: 19.05.2024
-- Description:	Function to generate document numbers
-- =============================================
CREATE FUNCTION sWMS.GenerateDocumentNumber
(
	@Doc_Number int = null
	,@Doc_Month int = null
	,@Doc_Year int = null
	,@Doc_Series varchar(100) = null
)
RETURNS varchar(100)
AS
BEGIN
	if (coalesce(@Doc_Number, 0) = 0)
		select @Doc_Number = max(coalesce(Doc_Number, 0)) + 1 from sWMS.Documents
		where Doc_Month = MONTH(GETDATE()) and Doc_Year = YEAR(GETDATE())

	if (coalesce(@Doc_Month, 0) = 0)
		set @Doc_Month = CONVERT(varchar(100), Month(GetDate()))

	if (coalesce(@Doc_Year, 0) = 0)
		set @Doc_Year = CONVERT(varchar(100), Year(GetDate()))

	if (coalesce(@Doc_Series, 0) = 0)
		select @Doc_Series = Conf_Value from sWMS.Config where Conf_CodeName = 'DomyslnaSeria'

	declare @Separator varchar(100)
	declare @Czlon1 varchar(100)
	declare @Czlon2 varchar(100)
	declare @Czlon3 varchar(100)
	declare @Czlon4 varchar(100)

	--select @Doc_Number = max(coalesce(Doc_Number, 0)) + 1 from sWMS.Documents
	--where Doc_Month = MONTH(GETDATE()) and Doc_Year = YEAR(GETDATE())

	select @Separator = Conf_Value from sWMS.Config where Conf_CodeName = 'NumeracjaSeparator'

	select @Czlon1 = 
	case 
		when Conf_Value like 'Numer' then
			@Doc_Number
		when Conf_Value like 'Miesiac' then
			@Doc_Month
		when Conf_Value like 'Rok' then
			@Doc_Year 
		when Conf_Value like 'Seria' then
			@Doc_Series
	end
	from sWMS.Config where Conf_CodeName = 'NumeracjaCzlon1'

	select @Czlon2 = 
	case 
		when Conf_Value like 'Numer' then
			@Doc_Number
		when Conf_Value like 'Miesiac' then
			@Doc_Month
		when Conf_Value like 'Rok' then
			@Doc_Year
		when Conf_Value like 'Seria' then
			@Doc_Series
	end
	from sWMS.Config where Conf_CodeName = 'NumeracjaCzlon2'

	select @Czlon3 = 
	case 
		when Conf_Value like 'Numer' then
			@Doc_Number
		when Conf_Value like 'Miesiac' then
			@Doc_Month
		when Conf_Value like 'Rok' then
			@Doc_Year 
		when Conf_Value like 'Seria' then
			@Doc_Series
	end
	from sWMS.Config where Conf_CodeName = 'NumeracjaCzlon3'

	select @Czlon4 = 
	case 
		when Conf_Value like 'Numer' then
			@Doc_Number
		when Conf_Value like 'Miesiac' then
			@Doc_Month 
		when Conf_Value like 'Rok' then
			@Doc_Year 
		when Conf_Value like 'Seria' then
			@Doc_Series
	end
	from sWMS.Config where Conf_CodeName = 'NumeracjaCzlon4'

	declare @Doc_NumberString varchar(100) = @Czlon1 + @Separator 
	+ @Czlon2 + @Separator
	+ @Czlon3 + @Separator
	+ @Czlon4

	return @Doc_NumberString

END
GO

