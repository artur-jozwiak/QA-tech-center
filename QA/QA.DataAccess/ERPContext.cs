using Microsoft.EntityFrameworkCore;
using QA.Domain.Models.Erp;

namespace QA.DataAccess
{
    public partial class ERPContext : DbContext
    {
        public ERPContext()
        {
        }

        public ERPContext(DbContextOptions<ERPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ErpOrder> ErpOrders { get; set; }
        public virtual DbSet<ErpOperation> ErpOperations { get; set; }
        public virtual DbSet<ErpTechPattern> ErpTechPatterns { get; set; }
        public virtual DbSet<SinteringOrder> Sintering { get; set; }
        public virtual DbSet<SinteringReport> SinteringReport { get; set; }
        public virtual DbSet<GreenStock> GreensStocks { get; set; }
        public virtual DbSet<CoatingOrder> CoatingOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Name=ConnectionStrings:HermesConnection");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErpOrder>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("QA_Zlecenia");
                entity.Property(e => e.IdTech).HasColumnName("Id_tech");
                entity.Property(e => e.Ilosc)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("ILOSC");
                entity.Property(e => e.KluczSkrocony)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("klucz_skrocony");
                entity.Property(e => e.KluczZp)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("KLUCZ_ZP");
                entity.Property(e => e.NazwaArt)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("NAZWA_ART");
                entity.Property(e => e.QasymbWnd)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("QASYMB_WND");
                entity.Property(e => e.SymbolWyr)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("SYMBOL_WYR");
                entity.Property(e => e.SymbolProszku)
                    .HasColumnName("SYMBOL_PROSZKU");
                entity.Property(e => e.NrPartiiProszku)
                      .HasColumnName("NR_PARTII_PROSZKU");
            });

            modelBuilder.Entity<ErpTechPattern>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("QA_Technologie_Wzorcowe");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nazwa)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("nazwa");
                entity.Property(e => e.SymbolTec)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("symbol_tec");
                entity.Property(e => e.SymbolWyr)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("symbol_wyr");
            });

            modelBuilder.Entity<ErpOperation>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("QA_Operacje");

                entity.Property(e => e.IdTechnolog).HasColumnName("Id_technolog");
                entity.Property(e => e.NazwaOp)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("Nazwa_Op");
                entity.Property(e => e.SymbolOp)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("symbol_op");
            });

            modelBuilder.Entity<SinteringOrder>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("QA_ZleceniaSpieku");
                    //.ToView("Q_nrSpieku");

                entity.Property(e => e.Data)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA");
                entity.Property(e => e.KluczZp)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("KLUCZ_ZP");
                entity.Property(e => e.KluczZpSkr)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("KLUCZ_ZP_SKR");
                entity.Property(e => e.MeNrprpo)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("ME_NRPRPO");
            });


            modelBuilder.Entity<SinteringReport>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("QA_MeldunkiSpieku");

                entity.Property(e => e.KluczZp)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("KLUCZ_ZP");
                entity.Property(e => e.Ilosc)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("ILOSC");
                entity.Property(e => e.MeNrprpo)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("ME_NRPRPO");
            });

            modelBuilder.Entity<GreenStock>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("QA_MagazynGreen");

                entity.Property(e => e.KluczZp)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("KLUCZ_ZP");
                entity.Property(e => e.SymbolArt)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("SYMBOL_ART");
                entity.Property(e => e.Zapas)
                    .HasColumnType("numeric(18, 8)")
                    .HasColumnName("ZAPAS");
            });


            modelBuilder.Entity<CoatingOrder>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("QA_ZleceniaPowlekania");

                entity.Property(e => e.Data)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA");
                entity.Property(e => e.KluczZp)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("KLUCZ_ZP");
                entity.Property(e => e.KluczZpSkr)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("KLUCZ_ZP_SKR");
                entity.Property(e => e.MeNrprpo)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("ME_NRPRPO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
