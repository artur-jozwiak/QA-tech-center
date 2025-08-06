using Microsoft.EntityFrameworkCore;
using QA.Domain.Models.Helicheck.Models;

namespace QA.DataAccess;

public partial class HelicheckContext : DbContext
{
    public HelicheckContext()
    {
    }

    public HelicheckContext(DbContextOptions<HelicheckContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Program> HelicheckPrograms { get; set; }
    public virtual DbSet<HelicheckParameter> HelicheckParameters { get; set; }
    public virtual DbSet<HelicheckMeasurements> HelicheckMeasurements { get; set; }
    public virtual DbSet<HelicheckResult> HelicheckResults { get; set; }
    public virtual DbSet<HelicheckInfo> HelicheckInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("ConnectionStrings:QAConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Program>(entity =>
        {
            entity.HasKey(e => e.Ix).HasName("Pr_pk");

            entity.ToTable("Programme");
            entity.HasIndex(e => e.GrIx, "Pr_GrIx");
            entity.HasIndex(e => e.HeadStrIx, "Pr_HeadStrIx");
            entity.HasIndex(e => e.HeadValIx, "Pr_HeadValIx");
            entity.HasIndex(e => e.KundeIx, "Pr_KundeIx");
            entity.HasIndex(e => e.RowGuid, "UQ__Programm__B975DD83060DEAE8").IsUnique();
            entity.Property(e => e.Autopilot)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.Datum1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.Datum2)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.Filter).HasDefaultValue("");
            entity.Property(e => e.GrIx).HasDefaultValueSql("('')");
            entity.Property(e => e.HeadStrIx).HasDefaultValueSql("('')");
            entity.Property(e => e.HeadValIx).HasDefaultValueSql("('')");
            entity.Property(e => e.Icon).HasDefaultValueSql("('0')");
            entity.Property(e => e.IncToolNum).HasDefaultValueSql("('1')");
            entity.Property(e => e.KundeIx).HasDefaultValueSql("('')");
            entity.Property(e => e.LastToolNum).HasDefaultValueSql("('0')");
            entity.Property(e => e.MessLp)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("MessLP");
            entity.Property(e => e.MessPrint)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.MessSpc)
                .IsRequired()
                .HasDefaultValueSql("('0')")
                .HasColumnName("MessSPC");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .HasDefaultValue("noname");
            entity.Property(e => e.PicPath)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.ProgComment).HasDefaultValue("");
            entity.Property(e => e.RowGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Spctrans)
                .HasDefaultValueSql("('0')")
                .HasColumnName("SPCTrans");
            entity.Property(e => e.Text1)
                .HasMaxLength(500)
                .HasDefaultValue("");
            entity.Property(e => e.Text2)
                .HasMaxLength(500)
                .HasDefaultValue("");

            entity.HasMany(p => p.Messungs).WithOne(m => m.Programme).HasForeignKey(m => m.PrgIx);
            entity.HasMany(p => p.Kriteriens).WithOne(k => k.HelicheckProgram).HasForeignKey(k => k.PrgIx);
        });


        modelBuilder.Entity<HelicheckMeasurements>(entity =>
        {
            entity.HasKey(e => e.Ix).HasName("Me_pk");
            entity.ToTable("Messung");
            entity.HasIndex(e => e.KundeIx, "Me_KundeIx");
            entity.HasIndex(e => e.PrgIx, "Me_PrgIx");
            entity.HasIndex(e => e.ToolStrIx, "Me_ToolStrIx");
            entity.HasIndex(e => e.ReportGuid, "UQ__Messung__2E3CA74A2F10007B").IsUnique();
            entity.Property(e => e.Datum)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.Kunde).HasDefaultValue("");
            entity.Property(e => e.KundeIx).HasDefaultValueSql("('')");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.PrgIx).HasDefaultValueSql("('')");
            entity.Property(e => e.ReportGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ToolNum).HasDefaultValueSql("('0')");
            entity.Property(e => e.ToolStrIx).HasDefaultValueSql("('')");

            entity.HasOne(m => m.InfoDat2).WithOne(id => id.Messung).HasForeignKey<HelicheckMeasurements>(id => id.ToolStrIx);

            entity.HasMany(m => m.Parameters)
                    .WithMany(k => k.HelicheckMeasurements)
                    .UsingEntity<HelicheckResult>(
                        j => j
                            .HasOne(hr => hr.HelicheckParameter)
                            .WithMany()
                            .HasForeignKey(hr => hr.KritIx),
                        j => j
                            .HasOne(hr => hr.HelicheckMeasurements)
                            .WithMany()
                            .HasForeignKey(hr => hr.MessIx),
                        j =>
                        {
                            j.HasKey(hr => new { hr.MessIx, hr.KritIx });
                            j.ToTable("Results");
                        });
        });


        modelBuilder.Entity<HelicheckParameter>(entity =>
        {
            entity.HasKey(e => e.Ix).HasName("Kr_pk");
            entity.ToTable("Kriterien");
            entity.HasIndex(e => e.PrgIx, "Kr_PrgIx");
            entity.Property(e => e.Dim).HasDefaultValueSql("('')");
            entity.Property(e => e.Ids)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("IDS");
            entity.Property(e => e.LfdNr).HasDefaultValueSql("('0')");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValue("noname");
            entity.Property(e => e.Nom)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.Otol)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("OTol");
            entity.Property(e => e.PrgIx).HasDefaultValueSql("('')");
            entity.Property(e => e.Utol)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("UTol");
        });

        modelBuilder.Entity<HelicheckResult>(entity =>
        {

            entity.HasKey(hr => new { hr.MessIx, hr.KritIx });

            entity.ToTable("Results");
            //entity.HasKey(e => e.KritIx);
            entity.HasIndex(e => new { e.MessIx, e.KritIx }, "Re_MessIxKritIx").IsUnique();
            entity.Property(e => e.KritIx).HasDefaultValueSql("('0')");
            entity.Property(e => e.MessIx).HasDefaultValueSql("('0')");
            entity.Property(e => e.Valid).HasDefaultValueSql("('0')");
            entity.Property(e => e.ValueF)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasDefaultValue("0");

            entity
            .HasOne(hr => hr.HelicheckParameter)
            .WithMany(k => k.HelicheckResults)
            .HasForeignKey(hr => hr.KritIx);

            entity
            .HasOne(hr => hr.HelicheckMeasurements)
            .WithMany(m => m.Results)
            .HasForeignKey(hr => hr.MessIx);
        });


        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<HelicheckInfo>(entity =>
        {
            entity.HasKey(e => e.IdIx);
            entity.ToTable("InfoDat2");
            entity.HasIndex(e => e.IdC, "Id2_IdC");
            entity.HasIndex(e => e.IdIx, "Id2_IdIx");
            entity.HasIndex(e => new { e.IdIx, e.IdC }, "Id2_IdIxIdC").IsUnique();
            entity.Property(e => e.IdC).HasDefaultValueSql("('0')");
            entity.Property(e => e.IdIx).HasDefaultValueSql("('0')");
            entity.Property(e => e.Str)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.Vflag)
                .HasDefaultValueSql("('1')")
                .HasColumnName("VFlag");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
