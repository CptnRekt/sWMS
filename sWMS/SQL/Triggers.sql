--CREATE TRIGGER sWMS.Units_OnAfterDeleteHandler
--   ON sWMS.Units
--   AFTER DELETE
--AS 
--BEGIN
--	declare @Unit_Id int,
--	@Unit_Type int

--	select @Unit_Id = Unit_Id,
--	@Unit_Type = Unit_Type from DELETED

--	update sWMS.ArticlesUnits
--	set ArU_Unit_Id = null,
--		ArU_Unit_Type = null
--	where ArU_Unit_Id  = @Unit_Id
--		and ArU_Unit_Type = @Unit_Type
--END
--GO

----nie mozemy zmieniac jednostek na istniejacych dokumentach, bo sie ilosci zepsuja
--CREATE TRIGGER sWMS.ArticlesUnits_OnAfterDeleteHandler
--   ON sWMS.ArticlesUnits
--   AFTER DELETE
--AS 
--BEGIN
--	declare @Unit_Id int,
--	@Unit_Type int

--	select @Unit_Id = ArU_Unit_Id,
--	@Unit_Type = ArU_Unit_Type from DELETED

--	update sWMS.Articles 
--	set Art_Default_Primary_Unit_Id = 1,
--		Art_Default_Primary_Unit_Type = 100
--	where Art_Default_Primary_Unit_Id = @Unit_Id
--		and Art_Default_Primary_Unit_Type = @Unit_Type

--	update sWMS.Articles 
--	set Art_Default_Secondary_Unit_Id = 1,
--		Art_Default_Secondary_Unit_Type = 100
--	where Art_Default_Secondary_Unit_Id = @Unit_Id
--		and Art_Default_Secondary_Unit_Type = @Unit_Type

--	update sWMS.Items
--	set It_Unit_Id = null,
--		It_Unit_Type = null
--	where It_Unit_Id = @Unit_Id
--		and It_Unit_Type = @Unit_Type

--	update sWMS.Subitems
--	set Sit_Unit_Id = null,
--		Sit_Unit_Type = null
--	where Sit_Unit_Id = @Unit_Id
--		and Sit_Unit_Id = @Unit_Type

--	update sWMS.Subitems
--	set Sit_Secondary_Unit_Id = null,
--		Sit_Secondary_Unit_Type = null
--	where Sit_Secondary_Unit_Id = @Unit_Id
--		and Sit_Secondary_Unit_Id = @Unit_Type

--	update sWMS.Resources
--	set Res_Unit_Id = null,
--		Res_Unit_Type = null
--	where Res_Unit_Id = @Unit_Id
--		and Res_Unit_Type = @Unit_Type

--	update sWMS.Resources
--	set Res_Secondary_Unit_Id = null,
--		Res_Secondary_Unit_Type = null
--	where Res_Secondary_Unit_Id = @Unit_Id
--		and Res_Secondary_Unit_Type = @Unit_Type
--END
--GO