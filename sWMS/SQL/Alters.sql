---------------------------------PK

ALTER TABLE sWMS.Articles
ADD CONSTRAINT PK_Art_Id PRIMARY KEY (Art_Id, Art_Type)
GO

ALTER TABLE sWMS.AttrClasses
ADD CONSTRAINT PK_AtC_Id PRIMARY KEY (AtC_Id, AtC_Type)
GO

ALTER TABLE sWMS.Attributes
ADD CONSTRAINT PK_Attr_Id PRIMARY KEY (Attr_Id, Attr_Type)
GO

ALTER TABLE sWMS.BinaryData
ADD CONSTRAINT PK_BinD_Id PRIMARY KEY (BinD_Id, BinD_Type)
GO

ALTER TABLE sWMS.Config
ADD CONSTRAINT PK_Conf_Id PRIMARY KEY (Conf_Id, Conf_Type)
GO

ALTER TABLE sWMS.Contractors
ADD CONSTRAINT PK_Con_Id PRIMARY KEY (Con_Id, Con_Type)
GO

ALTER TABLE sWMS.CustomNames
ADD CONSTRAINT PK_Cun_Id PRIMARY KEY (Cun_Id, Cun_Type)
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
ADD CONSTRAINT PK_Unit_Id PRIMARY KEY (Unit_Id, Unit_Type)
GO

ALTER TABLE sWMS.Users 
ADD CONSTRAINT PK_Usr_Id PRIMARY KEY (Usr_Id, Usr_Type)
GO

ALTER TABLE sWMS.Warehouses
ADD CONSTRAINT PK_Wh_Id PRIMARY KEY (Wh_Id, Wh_Type)
GO

ALTER TABLE sWMS.Resources
ADD CONSTRAINT PK_Res_Id PRIMARY KEY (Res_Id, Res_Type)
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
FOREIGN KEY (Attr_AtC_Id, Attr_AtC_Type) 
REFERENCES sWMS.AttrClasses(AtC_Id, AtC_Type)
ON DELETE CASCADE
ON UPDATE CASCADE
GO

---------------------------------- NON-CLUSTERED INDEXES

CREATE INDEX Art_Primary_Unit_Index ON sWMS.Articles (Art_Default_Primary_Unit_Id, Art_Default_Primary_Unit_Type)
CREATE INDEX Art_Secondary_Unit_Index ON sWMS.Articles (Art_Default_Secondary_Unit_Id, Art_Default_Secondary_Unit_Type)

CREATE INDEX ArU_Art_Index ON sWMS.ArticlesUnits (ArU_Art_Id, ArU_Art_Type)
CREATE INDEX ArU_Unit_Index ON sWMS.ArticlesUnits (ArU_Unit_Id, ArU_Unit_Type)

CREATE INDEX Attr_Document_Index ON sWMS.Attributes (Attr_ObjectId, Attr_ObjectType)
CREATE INDEX Attr_Item_Index ON sWMS.Attributes (Attr_ObjectId, Attr_ObjectType, Attr_ObjectItem)
CREATE INDEX Attr_Subitem_Index ON sWMS.Attributes (Attr_ObjectId, Attr_ObjectType, Attr_ObjectItem, Attr_ObjectSubitem)
CREATE INDEX Attr_AtC_Index ON sWMS.Attributes (Attr_AtC_Id, Attr_AtC_Type)

CREATE INDEX Cun_Art_Index ON sWMS.CustomNames (Cun_Art_Id, Cun_Art_Type)
CREATE INDEX Cun_Con_Index ON sWMS.CustomNames (Cun_Con_Id, Cun_Con_Type)

CREATE INDEX Doc_Source_Wh_Index ON sWMS.Documents (Doc_Source_Wh_Id)
CREATE INDEX Doc_Destination_Wh_Index ON sWMS.Documents (Doc_Destination_Wh_Id)

CREATE INDEX It_Unit_Index ON sWMS.Items (It_Unit_Id, It_Unit_Type)
CREATE INDEX It_Art_Index ON sWMS.Items (It_Art_Id, It_Art_Type)

CREATE INDEX Res_Wh_Index ON sWMS.Resources (Res_Wh_Id)
CREATE INDEX Res_Art_Index ON sWMS.Resources (Res_Art_Id, Res_Art_Type)
CREATE INDEX Res_Unit_Index on sWMS.Resources (Res_Unit_Id, Res_Unit_Type)
CREATE INDEX Res_Secondary_Unit_Index on sWMS.Resources (Res_Secondary_Unit_Id, Res_Secondary_Unit_Type)

CREATE INDEX Sit_Unit_Index ON sWMS.Subitems (Sit_Unit_Id, Sit_Unit_Type)
CREATE INDEX Sit_Secondary_Unit_Index ON sWMS.Subitems (Sit_Secondary_Unit_Id, Sit_Secondary_Unit_Type)
CREATE INDEX Sit_Art_Index ON sWMS.Subitems (Sit_Art_Id, Sit_Art_Type)
CREATE INDEX Sit_Res_Index ON sWMS.Subitems (Sit_Res_Id, Sit_Res_Type)