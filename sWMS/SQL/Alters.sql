---------------------------------PK

ALTER TABLE sWMS.Articles
ADD CONSTRAINT PK_Art_Id PRIMARY KEY (Art_Id, Art_Type, Art_No)
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

ALTER TABLE sWMS.WarehousesResources
ADD CONSTRAINT PK_Wr_Id PRIMARY KEY (Wr_Id)
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

---------------------------------- NON-CLUSTERED INDEXES

