---------------------------------PK

ALTER TABLE sWMS.Articles
ADD CONSTRAINT PK_Art_Id PRIMARY KEY (Art_Id, Art_Type, Art_No)
GO

ALTER TABLE sWMS.ArticlesBatches
ADD CONSTRAINT PK_ArB_Id PRIMARY KEY (ArB_Id, ArB_Type, ArB_No)
GO

ALTER TABLE sWMS.AttrClasses
ADD CONSTRAINT PK_AtC_Id PRIMARY KEY (AtC_Id, AtC_Type, AtC_No)
GO

ALTER TABLE sWMS.Attributes
ADD CONSTRAINT PK_Attr_Id PRIMARY KEY (Attr_Id, Attr_Type, Attr_No)
GO

ALTER TABLE sWMS.BinaryData
ADD CONSTRAINT PK_BinD_Id PRIMARY KEY (BinD_Id, BinD_Type, BinD_No)
GO

ALTER TABLE sWMS.Config
ADD CONSTRAINT PK_Conf_Id PRIMARY KEY (Conf_Id)
GO

ALTER TABLE sWMS.Contractors
ADD CONSTRAINT PK_Con_Id PRIMARY KEY (Con_Id, Con_Type, Con_No)
GO

ALTER TABLE sWMS.CustomNames
ADD CONSTRAINT PK_Cun_Id PRIMARY KEY (Cun_Id, Cun_Type, Cun_No)
GO

ALTER TABLE sWMS.Documents 
ADD CONSTRAINT PK_Doc_Id PRIMARY KEY (Doc_ObjectId, Doc_ObjectType)
GO

ALTER TABLE sWMS.Items 
ADD CONSTRAINT PK_It_Id PRIMARY KEY (It_ObjectId, It_ObjectType, It_ObjectItem)
GO

ALTER TABLE sWMS.Subitems 
ADD CONSTRAINT PK_Sit_Id PRIMARY KEY (Sit_ObjectId, Sit_ObjectType, Sit_ObjectItem, Sit_ObjectSubitem)
GO

ALTER TABLE sWMS.Units 
ADD CONSTRAINT PK_Unit_Id PRIMARY KEY (Unit_Id, Unit_Type, Unit_No)
GO

ALTER TABLE sWMS.Users 
ADD CONSTRAINT PK_Usr_Id PRIMARY KEY (Usr_Id)
GO

ALTER TABLE sWMS.Warehouses
ADD CONSTRAINT PK_Wh_Id PRIMARY KEY (Wh_Id)
GO

----------------------------------FK

ALTER TABLE sWMS.Attributes 
ADD CONSTRAINT FK_AttrDoc 
FOREIGN KEY (Attr_ObjectId, Attr_ObjectType) 
REFERENCES sWMS.Documents(Doc_ObjectId, Doc_ObjectType)
ON DELETE CASCADE
ON UPDATE CASCADE
GO

ALTER TABLE sWMS.Attributes 
ADD CONSTRAINT FK_AttrIt 
FOREIGN KEY (Attr_ObjectId, Attr_ObjectType, Attr_ObjectItem) 
REFERENCES sWMS.Items(It_ObjectId, It_ObjectType, It_ObjectItem)
ON DELETE CASCADE
ON UPDATE CASCADE
GO

ALTER TABLE sWMS.Attributes 
ADD CONSTRAINT FK_AttrSit 
FOREIGN KEY (Attr_ObjectId, Attr_ObjectType, Attr_ObjectItem, Attr_ObjectSubitem) 
REFERENCES sWMS.Subitems(Sit_ObjectId, Sit_ObjectType, Sit_ObjectItem, Sit_ObjectSubitem)
ON DELETE CASCADE
ON UPDATE CASCADE
GO

ALTER TABLE sWMS.Attributes 
ADD CONSTRAINT FK_AttrAtC 
FOREIGN KEY (Attr_AtC_Id, Attr_AtC_Type, Attr_AtC_No) 
REFERENCES sWMS.AttrClasses(AtC_Id, AtC_Type, AtC_No)
ON DELETE CASCADE
ON UPDATE CASCADE
GO

----------------------------------

ALTER TABLE sWMS.ArticlesBatches
ADD CONSTRAINT FK_ArBPrimaryUnit 
FOREIGN KEY (ArB_Unit_Id, ArB_Unit_Type, ArB_Unit_No) 
REFERENCES sWMS.Units(Unit_Id, Unit_Type, Unit_No)
ON DELETE NO ACTION
ON UPDATE NO ACTION
GO

ALTER TABLE sWMS.ArticlesBatches
ADD CONSTRAINT FK_ArBSecondaryUnit 
FOREIGN KEY (ArB_Secondary_Unit_Id, ArB_Secondary_Unit_Type, ArB_Secondary_Unit_No) 
REFERENCES sWMS.Units(Unit_Id, Unit_Type, Unit_No)
ON DELETE SET NULL
ON UPDATE SET NULL
GO

ALTER TABLE sWMS.ArticlesBatches
ADD CONSTRAINT FK_ArBArt
FOREIGN KEY (ArB_Art_Id, ArB_Art_Type, ArB_Art_No) 
REFERENCES sWMS.Articles(Art_Id, Art_Type, Art_No)
ON DELETE CASCADE
ON UPDATE CASCADE
GO

---------------------------------- DODAC TRIGGERY ZAMIAST ON DELETE SET NULL !!!!!!!

ALTER TABLE sWMS.Documents
ADD CONSTRAINT FK_DocSrcWh
FOREIGN KEY (Doc_Src_Wh_Id) 
REFERENCES sWMS.Warehouses(Wh_Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
GO

ALTER TABLE sWMS.Documents
ADD CONSTRAINT FK_DocDstWh
FOREIGN KEY (Doc_Dst_Wh_Id) 
REFERENCES sWMS.Warehouses(Wh_Id)
ON DELETE NO ACTION
ON UPDATE NO ACTION
GO

----------------------------------

---TU TRIGGER!!!
ALTER TABLE sWMS.Items
ADD CONSTRAINT FK_ItDoc
FOREIGN KEY (It_ObjectId, It_ObjectType) 
REFERENCES sWMS.Documents(Doc_ObjectId, Doc_ObjectType)
ON DELETE NO ACTION
ON UPDATE NO ACTION
GO

ALTER TABLE sWMS.Items
ADD CONSTRAINT FK_ItPrimaryUnit 
FOREIGN KEY (It_Unit_Id, It_Unit_Type, It_Unit_No) 
REFERENCES sWMS.Units(Unit_Id, Unit_Type, Unit_No)
ON DELETE NO ACTION
ON UPDATE NO ACTION
GO

ALTER TABLE sWMS.Items
ADD CONSTRAINT FK_ItSecondaryUnit 
FOREIGN KEY (It_Secondary_Unit_Id, It_Secondary_Unit_Type, It_Secondary_Unit_No) 
REFERENCES sWMS.Units(Unit_Id, Unit_Type, Unit_No)
ON DELETE SET NULL
ON UPDATE SET NULL
GO

ALTER TABLE sWMS.Items
ADD CONSTRAINT FK_ItCunCode
FOREIGN KEY (It_Code_Cun_Id, It_Code_Cun_Type, It_Code_Cun_No) 
REFERENCES sWMS.CustomNames(Cun_Id, Cun_Type, Cun_No)
ON DELETE NO ACTION
ON UPDATE NO ACTION
GO

ALTER TABLE sWMS.Items
ADD CONSTRAINT FK_ItCunName
FOREIGN KEY (It_Name_Cun_Id, It_Name_Cun_Type, It_Name_Cun_No) 
REFERENCES sWMS.CustomNames(Cun_Id, Cun_Type, Cun_No)
ON DELETE NO ACTION
ON UPDATE NO ACTION
GO

--Nie dziala usunac not null
ALTER TABLE sWMS.Items
ADD CONSTRAINT FK_ItArt
FOREIGN KEY (It_Art_Id, It_Art_Type, It_Art_No) 
REFERENCES sWMS.Articles(Art_Id, Art_Type, Art_No)
ON DELETE SET NULL
ON UPDATE SET NULL
GO

----------------------------------

--Nie dziala usunac not null
ALTER TABLE sWMS.Subitems
ADD CONSTRAINT FK_SitArt
FOREIGN KEY (Sit_Art_Id, Sit_Art_Type, Sit_Art_No) 
REFERENCES sWMS.Articles(Art_Id, Art_Type, Art_No)
ON DELETE SET NULL
ON UPDATE SET NULL
GO

--Nie dziala zle nazwy kolumn
ALTER TABLE sWMS.Subitems
ADD CONSTRAINT FK_SitArB
FOREIGN KEY (Sit_ArB_Id, Sit_ArB_Type, Sit_ArB_No) 
REFERENCES sWMS.ArticlesBatches(ArB_Id, ArB_Type, ArB_No)
ON DELETE SET NULL
ON UPDATE SET NULL
GO

----------------------------------

--Dorobic trigger!!!!
ALTER TABLE sWMS.Units
ADD CONSTRAINT FK_UnitArt
FOREIGN KEY (Unit_AttachedTo_Art_Id, Unit_AttachedTo_Art_Type, Unit_AttachedTo_Art_No) 
REFERENCES sWMS.Articles(Art_Id, Art_Type, Art_No)
ON DELETE NO ACTION
ON UPDATE NO ACTION
GO

----------------------------------

ALTER TABLE sWMS.WarehouseResources
ADD CONSTRAINT FK_WrWh
FOREIGN KEY (Wr_Wh_Id) 
REFERENCES sWMS.Warehouses(Wh_Id)
ON DELETE CASCADE
ON UPDATE CASCADE
GO

ALTER TABLE sWMS.WarehouseResources
ADD CONSTRAINT FK_WrArB
FOREIGN KEY (Wr_ArB_Id, Wr_ArB_Type, Wr_ArB_No) 
REFERENCES sWMS.ArticlesBatches(ArB_Id, ArB_Type, ArB_No)
ON DELETE NO ACTION
ON UPDATE NO ACTION
GO