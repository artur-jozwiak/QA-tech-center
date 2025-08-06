using Microsoft.EntityFrameworkCore;
using QA.Domain.Models.OldDb;

namespace QA.DataAccess;
//Scaffold-DbContext "Name=ConnectionStrings:OldDbConnection" Microsoft.EntityFrameworkCore.SqlServer -OutputDir ScafoldModels -Tables s1_bk,press02
//najwyższe id (przed migracją) s1_bk = 10469 - dalsze rkordy pochodzą z migracji
// najwyższe id (przed migracją) press02 = 25780 , QA.Pressing = 24985

public partial class OldDbContext : DbContext
{
    public OldDbContext()
    {

    }

    public OldDbContext(DbContextOptions<OldDbContext> options)
        : base(options)
    {

    }

    public virtual DbSet<Press02> Press02s { get; set; }

    public virtual DbSet<S1Bk> S1Bks { get; set; }

    public virtual DbSet<Powder> Powders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:OldDbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Polish_CI_AS");

        modelBuilder.Entity<Press02>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("press02");

            entity.Property(e => e.F1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("f1");
            entity.Property(e => e.F10).HasColumnName("f10");
            entity.Property(e => e.F11).HasColumnName("f11");
            entity.Property(e => e.F12).HasColumnName("f12");
            entity.Property(e => e.F13).HasColumnName("f13");
            entity.Property(e => e.F14).HasColumnName("f14");
            entity.Property(e => e.F15).HasColumnName("f15");
            entity.Property(e => e.F16).HasColumnName("f16");
            entity.Property(e => e.F17)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("f17");
            entity.Property(e => e.F18).HasColumnName("f18");
            entity.Property(e => e.F19).HasColumnName("f19");
            entity.Property(e => e.F2)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("f2");
            entity.Property(e => e.F20).HasColumnName("f20");
            entity.Property(e => e.F21).HasColumnName("f21");
            entity.Property(e => e.F22).HasColumnName("f22");
            entity.Property(e => e.F23).HasColumnName("f23");
            entity.Property(e => e.F24).HasColumnName("f24");
            entity.Property(e => e.F25)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("f25");
            entity.Property(e => e.F26)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("f26");
            entity.Property(e => e.F27)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("f27");
            entity.Property(e => e.F28)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("f28");
            entity.Property(e => e.F29)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("f29");
            entity.Property(e => e.F3)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("f3");
            entity.Property(e => e.F30)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("f30");
            entity.Property(e => e.F31)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("f31");
            entity.Property(e => e.F32)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("f32");
            entity.Property(e => e.F33)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("f33");
            entity.Property(e => e.F34)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("f34");
            entity.Property(e => e.F35)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("f35");
            entity.Property(e => e.F36)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("f36");
            entity.Property(e => e.F37)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("f37");
            entity.Property(e => e.F38)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("f38");
            entity.Property(e => e.F39)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("f39");
            entity.Property(e => e.F4)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("f4");
            entity.Property(e => e.F40)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("f40");
            entity.Property(e => e.F5).HasColumnName("f5");
            entity.Property(e => e.F6)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("f6");
            entity.Property(e => e.F7).HasColumnName("f7");
            entity.Property(e => e.F8).HasColumnName("f8");
            entity.Property(e => e.F9).HasColumnName("f9");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<S1Bk>(entity =>
        {
            entity
            //    .HasNoKey()
                .ToTable("s1_bk");


            entity.Property(e => e.AvgValue).HasColumnName("avgValue");
            entity.Property(e => e.Cp).HasColumnName("cp");
            entity.Property(e => e.Cpk).HasColumnName("cpk");
            entity.Property(e => e.Delta).HasColumnName("delta");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Lsl).HasColumnName("lsl");
            entity.Property(e => e.MaxValue).HasColumnName("maxValue");
            entity.Property(e => e.MinValue).HasColumnName("minValue");
            entity.Property(e => e.Nominal).HasColumnName("nominal");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("orderNo");
            entity.Property(e => e.Parameter)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("parameter");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Ro).HasColumnName("RO");
            entity.Property(e => e.Unit)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("unit");
            entity.Property(e => e.Usl).HasColumnName("usl");
        });


        modelBuilder.Entity<Powder>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("Trigger_LogChanges_Powders"));

            //entity.HasIndex(e => e.ColorId, "IX_Powders_ColorId");

            entity.Property(e => e.Density).HasColumnName("density");
            entity.Property(e => e.F14).HasMaxLength(255);
            entity.Property(e => e.GreenDensity).HasColumnName("green density");
            entity.Property(e => e.Hs13).HasColumnName("hs1,3");
            entity.Property(e => e.Hs16).HasColumnName("hs1,6");
            entity.Property(e => e.Hs1866).HasColumnName("hs1,866");
            entity.Property(e => e.Hs21).HasColumnName("hs2,1");
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.PowderBatch)
                .HasMaxLength(255)
                .HasColumnName("Powder Batch");
            entity.Property(e => e.PowderGrade)
                .HasMaxLength(255)
                .HasColumnName("Powder grade");
            entity.Property(e => e.PowderName)
                .HasMaxLength(255)
                .HasColumnName("Powder name");
            entity.Property(e => e.Vs13).HasColumnName("vs1,3");
            entity.Property(e => e.Vs16).HasColumnName("vs1,6");
            entity.Property(e => e.Vs1866).HasColumnName("vs1,866");
            entity.Property(e => e.Vs21).HasColumnName("vs2,1");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
