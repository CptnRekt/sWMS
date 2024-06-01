using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace sWMS.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<ArticlesUnit> ArticlesUnits { get; set; } = null!;
        public virtual DbSet<AttrClass> AttrClasses { get; set; } = null!;
        public virtual DbSet<Attribute> Attributes { get; set; } = null!;
        public virtual DbSet<Config> Configs { get; set; } = null!;
        public virtual DbSet<Contractor> Contractors { get; set; } = null!;
        public virtual DbSet<CustomName> CustomNames { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Resource> Resources { get; set; } = null!;
        public virtual DbSet<Subitem> Subitems { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Warehouse> Warehouses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=sWMS;Persist Security Info=True;User ID=sa;Password=Rambo846303");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Articles", "sWMS");

                entity.HasIndex(e => e.ArtCode, "UQ__Articles__CFBEB6B76C9C6AE9")
                    .IsUnique();

                entity.Property(e => e.ArtCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Art_Code");

                entity.Property(e => e.ArtCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Art_CreationDate");

                entity.Property(e => e.ArtDefaultPrimaryUnitId).HasColumnName("Art_Default_Primary_Unit_Id");

                entity.Property(e => e.ArtDefaultPrimaryUnitType).HasColumnName("Art_Default_Primary_Unit_Type");

                entity.Property(e => e.ArtDefaultSecondaryUnitId).HasColumnName("Art_Default_Secondary_Unit_Id");

                entity.Property(e => e.ArtDefaultSecondaryUnitType).HasColumnName("Art_Default_Secondary_Unit_Type");

                entity.Property(e => e.ArtId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Art_Id");

                entity.Property(e => e.ArtName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Art_Name");

                entity.Property(e => e.ArtType).HasColumnName("Art_Type");
            });

            modelBuilder.Entity<ArticlesUnit>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ArticlesUnits", "sWMS");

                entity.Property(e => e.ArUArtId).HasColumnName("ArU_Art_Id");

                entity.Property(e => e.ArUArtType).HasColumnName("ArU_Art_Type");

                entity.Property(e => e.ArUDividend)
                    .HasColumnType("decimal(14, 9)")
                    .HasColumnName("ArU_Dividend");

                entity.Property(e => e.ArUDivisor)
                    .HasColumnType("decimal(14, 9)")
                    .HasColumnName("ArU_Divisor");

                entity.Property(e => e.ArUId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ArU_Id");

                entity.Property(e => e.ArUName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ArU_Name");

                entity.Property(e => e.ArUType).HasColumnName("ArU_Type");

                entity.Property(e => e.ArUUnitId).HasColumnName("ArU_Unit_Id");

                entity.Property(e => e.ArUUnitType).HasColumnName("ArU_Unit_Type");
            });

            modelBuilder.Entity<AttrClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AttrClasses", "sWMS");

                entity.Property(e => e.AtCDataType)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("AtC_DataType");

                entity.Property(e => e.AtCDefaultValue)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("AtC_DefaultValue");

                entity.Property(e => e.AtCId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("AtC_Id");

                entity.Property(e => e.AtCName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("AtC_Name");

                entity.Property(e => e.AtCType).HasColumnName("AtC_Type");
            });

            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Attributes", "sWMS");

                entity.Property(e => e.AttrAtCId).HasColumnName("Attr_AtC_Id");

                entity.Property(e => e.AttrAtCType).HasColumnName("Attr_AtC_Type");

                entity.Property(e => e.AttrId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Attr_Id");

                entity.Property(e => e.AttrObjectId).HasColumnName("Attr_ObjectId");

                entity.Property(e => e.AttrObjectItem).HasColumnName("Attr_ObjectItem");

                entity.Property(e => e.AttrObjectSubitem).HasColumnName("Attr_ObjectSubitem");

                entity.Property(e => e.AttrObjectType).HasColumnName("Attr_ObjectType");

                entity.Property(e => e.AttrType).HasColumnName("Attr_Type");

                entity.Property(e => e.AttrValue)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Attr_Value");
            });

            modelBuilder.Entity<Config>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Config", "sWMS");

                entity.Property(e => e.ConfCodeName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Conf_CodeName");

                entity.Property(e => e.ConfId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Conf_Id");

                entity.Property(e => e.ConfName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Conf_Name");

                entity.Property(e => e.ConfType).HasColumnName("Conf_Type");

                entity.Property(e => e.ConfValue)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Conf_Value");
            });

            modelBuilder.Entity<Contractor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Contractors", "sWMS");

                entity.HasIndex(e => e.ConCode, "UQ__Contract__3FEAFCBF0595EF8C")
                    .IsUnique();

                entity.Property(e => e.ConCity)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Con_City");

                entity.Property(e => e.ConCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Con_Code");

                entity.Property(e => e.ConCountry)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Con_Country");

                entity.Property(e => e.ConFullName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Con_FullName");

                entity.Property(e => e.ConId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Con_Id");

                entity.Property(e => e.ConLogoBinDId).HasColumnName("Con_Logo_BinD_Id");

                entity.Property(e => e.ConLogoBinDType).HasColumnName("Con_Logo_BinD_Type");

                entity.Property(e => e.ConPostal)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Con_Postal");

                entity.Property(e => e.ConStreet)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Con_Street");

                entity.Property(e => e.ConType).HasColumnName("Con_Type");
            });

            modelBuilder.Entity<CustomName>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CustomNames", "sWMS");

                entity.Property(e => e.CunArtId).HasColumnName("Cun_Art_Id");

                entity.Property(e => e.CunArtType).HasColumnName("Cun_Art_Type");

                entity.Property(e => e.CunConId).HasColumnName("Cun_Con_Id");

                entity.Property(e => e.CunConType).HasColumnName("Cun_Con_Type");

                entity.Property(e => e.CunId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Cun_Id");

                entity.Property(e => e.CunName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Cun_Name");

                entity.Property(e => e.CunType).HasColumnName("Cun_Type");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Documents", "sWMS");

                entity.HasIndex(e => e.DocNumberString, "UQ__Document__C2EA6B71EB67BCBE")
                    .IsUnique();

                entity.Property(e => e.DocCompletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Doc_CompletionDate");

                entity.Property(e => e.DocCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Doc_CreationDate");

                entity.Property(e => e.DocDestinationWhId).HasColumnName("Doc_Destination_Wh_Id");

                entity.Property(e => e.DocMonth).HasColumnName("Doc_Month");

                entity.Property(e => e.DocNumber).HasColumnName("Doc_Number");

                entity.Property(e => e.DocNumberString)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Doc_NumberString");

                entity.Property(e => e.DocObjectId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Doc_ObjectId");

                entity.Property(e => e.DocObjectType).HasColumnName("Doc_ObjectType");

                entity.Property(e => e.DocRealizationStatus)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Doc_RealizationStatus");

                entity.Property(e => e.DocSeries)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Doc_Series");

                entity.Property(e => e.DocSourceWhId).HasColumnName("Doc_Source_Wh_Id");

                entity.Property(e => e.DocYear).HasColumnName("Doc_Year");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Items", "sWMS");

                entity.Property(e => e.ItArtId).HasColumnName("It_Art_Id");

                entity.Property(e => e.ItArtType).HasColumnName("It_Art_Type");

                entity.Property(e => e.ItCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("It_Code");

                entity.Property(e => e.ItCompletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("It_CompletionDate");

                entity.Property(e => e.ItDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("It_Description");

                entity.Property(e => e.ItName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("It_Name");

                entity.Property(e => e.ItObjectId).HasColumnName("It_ObjectId");

                entity.Property(e => e.ItObjectItem).HasColumnName("It_ObjectItem");

                entity.Property(e => e.ItObjectType).HasColumnName("It_ObjectType");

                entity.Property(e => e.ItQuantity)
                    .HasColumnType("decimal(14, 9)")
                    .HasColumnName("It_Quantity");

                entity.Property(e => e.ItRealizedQuantity)
                    .HasColumnType("decimal(14, 9)")
                    .HasColumnName("It_RealizedQuantity");

                entity.Property(e => e.ItUnitId).HasColumnName("It_Unit_Id");

                entity.Property(e => e.ItUnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("It_Unit_Name");

                entity.Property(e => e.ItUnitType).HasColumnName("It_Unit_Type");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Resources", "sWMS");

                entity.HasIndex(e => e.ResBatchCode, "UQ__Resource__6895E47902149973")
                    .IsUnique();

                entity.Property(e => e.ResArtId).HasColumnName("Res_Art_Id");

                entity.Property(e => e.ResArtType).HasColumnName("Res_Art_Type");

                entity.Property(e => e.ResBatchCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Res_BatchCode");

                entity.Property(e => e.ResBatchName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Res_BatchName");

                entity.Property(e => e.ResId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Res_Id");

                entity.Property(e => e.ResQuantity)
                    .HasColumnType("decimal(14, 9)")
                    .HasColumnName("Res_Quantity");

                entity.Property(e => e.ResSecondaryUnitId).HasColumnName("Res_Secondary_Unit_Id");

                entity.Property(e => e.ResSecondaryUnitType).HasColumnName("Res_Secondary_Unit_Type");

                entity.Property(e => e.ResType).HasColumnName("Res_Type");

                entity.Property(e => e.ResUnitId).HasColumnName("Res_Unit_Id");

                entity.Property(e => e.ResUnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Res_Unit_Name");

                entity.Property(e => e.ResUnitType).HasColumnName("Res_Unit_Type");

                entity.Property(e => e.ResWhId).HasColumnName("Res_Wh_Id");
            });

            modelBuilder.Entity<Subitem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Subitems", "sWMS");

                entity.Property(e => e.SitArtId).HasColumnName("Sit_Art_Id");

                entity.Property(e => e.SitArtType).HasColumnName("Sit_Art_Type");

                entity.Property(e => e.SitObjectId).HasColumnName("Sit_ObjectId");

                entity.Property(e => e.SitObjectItem).HasColumnName("Sit_ObjectItem");

                entity.Property(e => e.SitObjectSubitem).HasColumnName("Sit_ObjectSubitem");

                entity.Property(e => e.SitObjectType).HasColumnName("Sit_ObjectType");

                entity.Property(e => e.SitQuantity)
                    .HasColumnType("decimal(14, 9)")
                    .HasColumnName("Sit_Quantity");

                entity.Property(e => e.SitRealizedQuantity)
                    .HasColumnType("decimal(14, 9)")
                    .HasColumnName("Sit_RealizedQuantity");

                entity.Property(e => e.SitResId).HasColumnName("Sit_Res_Id");

                entity.Property(e => e.SitResType).HasColumnName("Sit_Res_Type");

                entity.Property(e => e.SitSecondaryUnitId).HasColumnName("Sit_Secondary_Unit_Id");

                entity.Property(e => e.SitSecondaryUnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Sit_Secondary_Unit_Name");

                entity.Property(e => e.SitSecondaryUnitType).HasColumnName("Sit_Secondary_Unit_Type");

                entity.Property(e => e.SitUnitId).HasColumnName("Sit_Unit_Id");

                entity.Property(e => e.SitUnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Sit_Unit_Name");

                entity.Property(e => e.SitUnitType).HasColumnName("Sit_Unit_Type");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Units", "sWMS");

                entity.Property(e => e.UnitDividend)
                    .HasColumnType("decimal(14, 9)")
                    .HasColumnName("Unit_Dividend");

                entity.Property(e => e.UnitDivisor)
                    .HasColumnType("decimal(14, 9)")
                    .HasColumnName("Unit_Divisor");

                entity.Property(e => e.UnitId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Unit_Id");

                entity.Property(e => e.UnitName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Unit_Name");

                entity.Property(e => e.UnitType).HasColumnName("Unit_Type");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Users", "sWMS");

                entity.HasIndex(e => e.UsrLogin, "UQ__Users__CAED5D68C9ED1787")
                    .IsUnique();

                entity.Property(e => e.UsrAutologin).HasColumnName("Usr_Autologin");

                entity.Property(e => e.UsrId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Usr_Id");

                entity.Property(e => e.UsrLogin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Usr_Login");

                entity.Property(e => e.UsrPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Usr_Password");

                entity.Property(e => e.UsrType).HasColumnName("Usr_Type");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Warehouses", "sWMS");

                entity.HasIndex(e => e.WhCode, "UQ__Warehous__C85406D6AB7277ED")
                    .IsUnique();

                entity.Property(e => e.WhAcceptancesNumber).HasColumnName("Wh_AcceptancesNumber");

                entity.Property(e => e.WhCity)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Wh_City");

                entity.Property(e => e.WhCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Wh_Code");

                entity.Property(e => e.WhCountry)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Wh_Country");

                entity.Property(e => e.WhId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Wh_Id");

                entity.Property(e => e.WhIssuesNumber).HasColumnName("Wh_IssuesNumber");

                entity.Property(e => e.WhName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Wh_Name");

                entity.Property(e => e.WhPostal)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Wh_Postal");

                entity.Property(e => e.WhStreet)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Wh_Street");

                entity.Property(e => e.WhType).HasColumnName("Wh_Type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
