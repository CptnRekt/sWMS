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
        public virtual DbSet<ArticlesBatch> ArticlesBatches { get; set; } = null!;
        public virtual DbSet<AttrClass> AttrClasses { get; set; } = null!;
        public virtual DbSet<Attribute> Attributes { get; set; } = null!;
        public virtual DbSet<BinaryDatum> BinaryData { get; set; } = null!;
        public virtual DbSet<Config> Configs { get; set; } = null!;
        public virtual DbSet<Contractor> Contractors { get; set; } = null!;
        public virtual DbSet<CustomName> CustomNames { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Warehouse> Warehouses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=sWMS;Persist Security Info=True;User ID=sa;Password=Rambo846303");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Articles", "sWMS");

                entity.Property(e => e.ArtCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Art_Code");

                entity.Property(e => e.ArtCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Art_CreationDate");

                entity.Property(e => e.ArtDefaultUnitId).HasColumnName("Art_Default_UnitId");

                entity.Property(e => e.ArtId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Art_Id");

                entity.Property(e => e.ArtName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Art_Name");

                entity.Property(e => e.ArtNo).HasColumnName("Art_No");

                entity.Property(e => e.ArtType).HasColumnName("Art_Type");
            });

            modelBuilder.Entity<ArticlesBatch>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ArticlesBatches", "sWMS");

                entity.Property(e => e.ArBArtId).HasColumnName("ArB_Art_Id");

                entity.Property(e => e.ArBArtNo).HasColumnName("ArB_Art_No");

                entity.Property(e => e.ArBArtType).HasColumnName("ArB_Art_Type");

                entity.Property(e => e.ArBCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ArB_Code");

                entity.Property(e => e.ArBId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ArB_Id");

                entity.Property(e => e.ArBName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ArB_Name");

                entity.Property(e => e.ArBNo).HasColumnName("ArB_No");

                entity.Property(e => e.ArBQuantity)
                    .HasColumnType("decimal(14, 9)")
                    .HasColumnName("ArB_Quantity");

                entity.Property(e => e.ArBSecondaryUnitId).HasColumnName("ArB_Secondary_Unit_Id");

                entity.Property(e => e.ArBSecondaryUnitNo).HasColumnName("ArB_Secondary_Unit_No");

                entity.Property(e => e.ArBSecondaryUnitType).HasColumnName("ArB_Secondary_Unit_Type");

                entity.Property(e => e.ArBType).HasColumnName("ArB_Type");

                entity.Property(e => e.ArBUnitId).HasColumnName("ArB_Unit_Id");

                entity.Property(e => e.ArBUnitNo).HasColumnName("ArB_Unit_No");

                entity.Property(e => e.ArBUnitType).HasColumnName("ArB_Unit_Type");
            });

            modelBuilder.Entity<AttrClass>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AttrClasses", "sWMS");

                entity.Property(e => e.AtCDataType)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("AtC_DataType");

                entity.Property(e => e.AtCId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("AtC_Id");

                entity.Property(e => e.AtCName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("AtC_Name");

                entity.Property(e => e.AtCNo).HasColumnName("AtC_No");

                entity.Property(e => e.AtCType).HasColumnName("AtC_Type");
            });

            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Attributes", "sWMS");

                entity.Property(e => e.AttrAtCId).HasColumnName("Attr_AtC_Id");

                entity.Property(e => e.AttrAtCNo).HasColumnName("Attr_AtC_No");

                entity.Property(e => e.AttrAtCType).HasColumnName("Attr_AtC_Type");

                entity.Property(e => e.AttrId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Attr_Id");

                entity.Property(e => e.AttrNo).HasColumnName("Attr_No");

                entity.Property(e => e.AttrObjectId).HasColumnName("Attr_Object_Id");

                entity.Property(e => e.AttrObjectNo).HasColumnName("Attr_Object_No");

                entity.Property(e => e.AttrObjectType).HasColumnName("Attr_Object_Type");

                entity.Property(e => e.AttrType).HasColumnName("Attr_Type");

                entity.Property(e => e.AttrValue)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Attr_Value");
            });

            modelBuilder.Entity<BinaryDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BinaryData", "sWMS");

                entity.Property(e => e.BinDContent).HasColumnName("BinD_Content");

                entity.Property(e => e.BinDId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BinD_Id");

                entity.Property(e => e.BinDNo).HasColumnName("BinD_No");

                entity.Property(e => e.BinDType).HasColumnName("BinD_Type");
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

                entity.Property(e => e.ConfValue)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Conf_Value");
            });

            modelBuilder.Entity<Contractor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Contractors", "sWMS");

                entity.Property(e => e.ConCity)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Con_City");

                entity.Property(e => e.ConCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Con_Code");

                entity.Property(e => e.ConCodeCunId).HasColumnName("Con_Code_Cun_Id");

                entity.Property(e => e.ConCodeCunNo).HasColumnName("Con_Code_Cun_No");

                entity.Property(e => e.ConCodeCunType).HasColumnName("Con_Code_Cun_Type");

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

                entity.Property(e => e.ConLogoBinDNo).HasColumnName("Con_Logo_BinD_No");

                entity.Property(e => e.ConLogoBinDType).HasColumnName("Con_Logo_BinD_Type");

                entity.Property(e => e.ConNameCunId).HasColumnName("Con_Name_Cun_Id");

                entity.Property(e => e.ConNameCunNo).HasColumnName("Con_Name_Cun_No");

                entity.Property(e => e.ConNameCunType).HasColumnName("Con_Name_Cun_Type");

                entity.Property(e => e.ConNo).HasColumnName("Con_No");

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

                entity.Property(e => e.CunId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Cun_Id");

                entity.Property(e => e.CunName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Cun_Name");

                entity.Property(e => e.CunNo).HasColumnName("Cun_No");

                entity.Property(e => e.CunType).HasColumnName("Cun_Type");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Documents", "sWMS");

                entity.Property(e => e.DocCompletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Doc_CompletionDate");

                entity.Property(e => e.DocCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Doc_CreationDate");

                entity.Property(e => e.DocId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Doc_Id");

                entity.Property(e => e.DocMonth).HasColumnName("Doc_Month");

                entity.Property(e => e.DocNumber).HasColumnName("Doc_Number");

                entity.Property(e => e.DocNumberString)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Doc_NumberString");

                entity.Property(e => e.DocSeries)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Doc_Series");

                entity.Property(e => e.DocType).HasColumnName("Doc_Type");

                entity.Property(e => e.DocYear).HasColumnName("Doc_Year");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Items", "sWMS");

                entity.Property(e => e.ItArBId).HasColumnName("It_ArB_Id");

                entity.Property(e => e.ItArbNo).HasColumnName("It_Arb_No");

                entity.Property(e => e.ItArbType).HasColumnName("It_Arb_Type");

                entity.Property(e => e.ItArtId).HasColumnName("It_Art_Id");

                entity.Property(e => e.ItArtNo).HasColumnName("It_Art_No");

                entity.Property(e => e.ItArtType).HasColumnName("It_Art_Type");

                entity.Property(e => e.ItCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("It_Code");

                entity.Property(e => e.ItCodeCunId).HasColumnName("It_Code_Cun_Id");

                entity.Property(e => e.ItCodeCunNo).HasColumnName("It_Code_Cun_No");

                entity.Property(e => e.ItCodeCunType).HasColumnName("It_Code_Cun_Type");

                entity.Property(e => e.ItCompletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("It_CompletionDate");

                entity.Property(e => e.ItConId).HasColumnName("It_Con_Id");

                entity.Property(e => e.ItConNo).HasColumnName("It_Con_No");

                entity.Property(e => e.ItConType).HasColumnName("It_Con_Type");

                entity.Property(e => e.ItCurrentWhId).HasColumnName("It_Current_Wh_Id");

                entity.Property(e => e.ItDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("It_Description");

                entity.Property(e => e.ItDocId).HasColumnName("It_Doc_Id");

                entity.Property(e => e.ItDocType).HasColumnName("It_Doc_Type");

                entity.Property(e => e.ItDstWhId).HasColumnName("It_Dst_Wh_Id");

                entity.Property(e => e.ItName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("It_Name");

                entity.Property(e => e.ItNameCunId).HasColumnName("It_Name_Cun_Id");

                entity.Property(e => e.ItNameCunNo).HasColumnName("It_Name_Cun_No");

                entity.Property(e => e.ItNameCunType).HasColumnName("It_Name_Cun_Type");

                entity.Property(e => e.ItNo).HasColumnName("It_No");

                entity.Property(e => e.ItQuantity)
                    .HasColumnType("decimal(14, 9)")
                    .HasColumnName("It_Quantity");

                entity.Property(e => e.ItSecondaryUnitId).HasColumnName("It_Secondary_Unit_Id");

                entity.Property(e => e.ItSecondaryUnitNo).HasColumnName("It_Secondary_Unit_No");

                entity.Property(e => e.ItSecondaryUnitType).HasColumnName("It_Secondary_Unit_Type");

                entity.Property(e => e.ItSrcWhId).HasColumnName("It_Src_Wh_Id");

                entity.Property(e => e.ItUnitId).HasColumnName("It_Unit_Id");

                entity.Property(e => e.ItUnitNo).HasColumnName("It_Unit_No");

                entity.Property(e => e.ItUnitType).HasColumnName("It_Unit_Type");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Units", "sWMS");

                entity.Property(e => e.UnitAttachedToArtId).HasColumnName("Unit_AttachedTo_Art_Id");

                entity.Property(e => e.UnitAttachedToArtNo).HasColumnName("Unit_AttachedTo_Art_No");

                entity.Property(e => e.UnitAttachedToArtType).HasColumnName("Unit_AttachedTo_Art_Type");

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

                entity.Property(e => e.UnitNo).HasColumnName("Unit_No");

                entity.Property(e => e.UnitType).HasColumnName("Unit_Type");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Users", "sWMS");

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
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.HasKey(e => e.WhId)
                    .HasName("PK__Warehous__F5A8A879F249BD75");

                entity.ToTable("Warehouses", "sWMS");

                entity.Property(e => e.WhId).HasColumnName("Wh_Id");

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
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
