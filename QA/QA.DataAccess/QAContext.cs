using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QA.Domain.Models;
using QA.Domain.Models.CoatingModels;
using QA.Domain.Models.Keyence;
using QA.Domain.Models.SinteringModels;
using QA.Domain.Models.ToolTests;

namespace QA.DataAccess
{
    public class QAContext : IdentityDbContext
    {
        public QAContext()
        {

        }

        public QAContext(DbContextOptions<QAContext> options) : base(options)
        {

        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<MeasuringDevice> MeasuringDevices { get; set; }
        public virtual DbSet<DevicePort> DevicePorts { get; set; }
        public virtual DbSet<VisualInspectionForm> VisualInspectionForms { get; set; }
        public virtual DbSet<Sample> Samples { get; set; }
        public virtual DbSet<SampleDefect> SampleDefects { get; set; }
        public virtual DbSet<Defect> Defects { get; set; }
        public virtual DbSet<DefectCategory> DefectCategories { get; set; }
        public virtual DbSet<Marker> Markers { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ChildParametersAssignement> ChildParametersAssignement { get; set; }
        public virtual DbSet<MeasurementsSeries> MeasurementsSeries { get; set; }
        public virtual DbSet<OperationDetails> OperationDetails { get; set; }
        public virtual DbSet<KeyenceParameter> KeyenceParameters { get; set; }
        public virtual DbSet<KeyenceMeasurement> KeyenceMeasurements { get; set; }
        public virtual DbSet<PowderSpecification> PowdersSpecifications { get; set; }
        public virtual DbSet<MRB> MRB { get; set; }
        public virtual DbSet<MRBCorrectiveAction> MRBCorrectiveActions { get; set; }
        public virtual DbSet<MRBInstruction> MRBInstructions { get; set; }
        public virtual DbSet<MRBMemberSummary> MRBMemberSummaries { get; set; }
        public virtual DbSet<DescriptiveParameter> DescriptiveParameter { get; set; }
        public virtual DbSet<Result> Results { get; set; }//zmienic nazwe???
        public virtual DbSet<Pressing> Pressing { get; set; }

        //Sintering
        public virtual DbSet<SinteringBatch> Sinterings { get; set; }
        public virtual DbSet<TrayLocation> TrayLocations { get; set; }
        public virtual DbSet<FurnaceLocalization> FuranceLocalizations { get; set; }

        public virtual DbSet<CoatingProcess> CoatingProcess { get; set; }
        public virtual DbSet<CoatingMeasurementSeries> CoatingMeasurementSeriess { get; set; }
        public virtual DbSet<Coating> Coatings { get; set; }
        public virtual DbSet<ComparisonPoint> ComparePoints { get; set; }

        public virtual DbSet<ToolTest> ToolTests { get; set; }
        public virtual DbSet<ToolTestComparison> ToolTestComparisons { get; set; }
        public virtual DbSet<Tool> Tool { get; set; }

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
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.OrderKey).HasMaxLength(30).IsRequired();
                entity.Property(o => o.ShortenedKey).HasMaxLength(30).IsRequired(false);
                entity.Property(o => o.RowDatetime).HasColumnType("datetime");
                entity.HasOne(o => o.Product).WithMany(p => p.Orders).HasForeignKey(o => o.ProductId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Symbol).HasMaxLength(150).IsRequired();
                entity.Property(p => p.PdmNo).HasMaxLength(20).IsRequired(false);
                entity.Property(p => p.Description).HasMaxLength(150).IsRequired(false);
                entity.Property(kp => kp.SpacerHeight).HasPrecision(4, 2);
                entity.Property(kp => kp.Weight).HasPrecision(5, 3);


                entity.Property(kp => kp.NarrowingLTol).HasPrecision(5, 3);
                entity.Property(kp => kp.NarrowingUTol).HasPrecision(5, 3);
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.Property(o => o.Name).HasMaxLength(150).IsRequired();
                entity.Property(o => o.Symbol).HasMaxLength(16).IsRequired(false);
                entity.Property(o => o.Comment).IsRequired(false);
                entity.HasOne(o => o.Product).WithMany(p => p.Operations).HasForeignKey(o => o.ProductId);
                entity.HasMany(e => e.MeasurementsSeries)
                    .WithOne(ms => ms.Operation)
                    .HasForeignKey(ms => ms.OperationId);
            });

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.Property(p => p.Name).HasMaxLength(50).IsRequired();
                entity.Property(p => p.Unit).HasMaxLength(20).IsRequired(false);
                entity.Property(p => p.LowerTolerance).HasPrecision(10, 3);
                entity.Property(p => p.LSL).HasPrecision(10, 3);
                entity.Property(p => p.NominalValue).HasPrecision(10, 3);
                entity.Property(p => p.USL).HasPrecision(10, 3);
                entity.Property(p => p.UpperTolerance).HasPrecision(10, 3);
                entity.Property(p => p.Comment).HasMaxLength(250).IsRequired(false);
                entity.Property(p => p.SampleClass).HasMaxLength(1).IsRequired(false);
                entity.Property(p => p.CreationDate).HasColumnType("datetime");
                entity.HasOne(p => p.Operation).WithMany(o => o.Parameters).HasForeignKey(o => o.OperationId);
                entity.HasOne(p => p.DevicePort).WithMany(dp => dp.Parameters).HasForeignKey(p => p.DevicePortId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<ChildParametersAssignement>(entity =>
            {
                entity.HasOne(cp => cp.ChildParameter)
                  .WithMany()
                  .HasForeignKey(cp => cp.ChildParameterId)
                  .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(cp => cp.ParentParameter)
                       .WithMany(p => p.ChildParametersAssignements)
                       .HasForeignKey(cp => cp.ParentParameterId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.Property(m => m.Value).HasPrecision(10, 3);
                entity.Property(m => m.Operator).HasMaxLength(50).IsRequired(false);
                entity.Property(m => m.Date).HasColumnType("datetime");
                entity.Property(m => m.OrderKey).HasMaxLength(30).IsRequired(false);
                entity.HasOne(m => m.Parameter).WithMany(p => p.Measurements).HasForeignKey(m => m.ParameterId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(m => m.Order).WithMany(o => o.Measurements).HasForeignKey(m => m.OrderId).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DevicePort>(entity =>
            {
                entity.Property(dp => dp.Name).HasMaxLength(30).IsRequired();
                entity.HasOne(dp => dp.MeasuringDevice).WithMany(md => md.Ports).HasForeignKey(dp => dp.MeasuringDeviceId);
            });

            modelBuilder.Entity<MeasuringDevice>(entity =>
            {
                entity.Property(dp => dp.Name).HasMaxLength(50).IsRequired();
                entity.Property(dp => dp.SerialNo).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<VisualInspectionForm>(entity =>
            {
                entity.Property(vis => vis.MRBNumber).HasMaxLength(30).IsRequired(false);
                entity.Property(vis => vis.InstructionNumber).HasMaxLength(30).IsRequired(false);
                entity.Property(vis => vis.Comments).IsRequired(false);
                entity.HasOne(vis => vis.Order).WithOne(o => o.VisualInspectionForm).HasForeignKey<Order>(o => o.VisualInspectionFormId).IsRequired(false);
            });

            modelBuilder.Entity<Sample>(entity =>
            {
                entity.Property(s => s.Operator).HasMaxLength(50).IsRequired(false);
                entity.Property(s => s.Date).HasColumnType("datetime");
                entity.HasOne(s => s.VisualInspectionForm).WithMany(vis => vis.Samples).HasForeignKey(o => o.VisualInspectionFormId);
            });

            modelBuilder.Entity<SampleDefect>(entity =>
            {
                entity.Property(sd => sd.DefectSymbol).HasMaxLength(20);
                entity.HasOne(sd => sd.Sample).WithMany(s => s.SampleDefects).HasForeignKey(sd => sd.SampleId);
                entity.HasOne(sd => sd.Defect);
            });

            modelBuilder.Entity<Defect>(entity =>
            {
                entity.Property(dc => dc.Name).HasMaxLength(50);
                entity.Property(dc => dc.Symbol).HasMaxLength(20);
                entity.HasOne(d => d.DefectCategory).WithMany(dc => dc.Defects);
            });

            modelBuilder.Entity<DefectCategory>(entity =>
            {
                entity.Property(dc => dc.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(dc => dc.Name).HasMaxLength(150);
                entity.Property(dc => dc.Destination);
                entity.HasOne(i => i.Product).WithOne(p => p.Image).HasForeignKey<Image>(i => i.ProductId).IsRequired(false);
                entity.HasOne(i => i.Parameter).WithOne(p => p.Image).HasForeignKey<Image>(i => i.ParameterId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(i => i.VisualInspectionForms).WithOne(vis => vis.Image).HasForeignKey(vis => vis.ImageId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(i => i.MRB).WithMany(mrb => mrb.Images).HasForeignKey(i => i.MRBId).IsRequired(false);
                entity.HasOne(i => i.Marker).WithOne(p => p.Image).HasForeignKey<Image>(i => i.MarkerId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

                //Parametr opisowy
                //entity.HasOne(i => i.DescriptiveParameter).WithOne(p => p.Image).HasForeignKey<Image>(i => i.DescriptiveParameterId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Marker>(entity =>
            {
                entity.Property(m => m.Label).HasMaxLength(10);
                entity.Property(m => m.CreationDate).HasColumnType("datetime");
                entity.Property(m => m.Department).HasMaxLength(10);
                entity.Property(m => m.Remark).HasMaxLength(200);

                entity.HasOne(m => m.VisualInspectionForm).WithMany(i => i.Markers);
                //entity.HasOne(m => m.Image).WithMany(i => i.Markers).HasForeignKey(i => i.ImageId);
            });

            modelBuilder.Entity<KeyenceMeasurement>(entity =>
            {
                entity.Property(km => km.Value).HasPrecision(10, 3);
                entity.Property(km => km.FileName).HasMaxLength(100);
                entity.Property(km => km.OrderNo).HasMaxLength(50);
                entity.Property(km => km.FileModificationDate).HasColumnType("datetime");
                entity.Property(km => km.Date).HasColumnType("datetime");
                entity.HasOne(km => km.Parameter).WithMany(kp => kp.Measurements).HasForeignKey(km => km.ParameterId).IsRequired();
            });

            modelBuilder.Entity<KeyenceParameter>(entity =>
            {
                entity.Property(kp => kp.FileName).HasMaxLength(20);
                entity.Property(kp => kp.Unit).HasMaxLength(20);
                entity.Property(kp => kp.LSL).HasPrecision(10, 3);
                entity.Property(kp => kp.Nominal).HasPrecision(10, 3);
                entity.Property(kp => kp.USL).HasPrecision(10, 3);
                entity.Property(kp => kp.UpperTollerance).HasPrecision(5, 3);
                entity.Property(kp => kp.LowerTollerance).HasPrecision(5, 3);
                entity.Property(kp => kp.FileName).HasMaxLength(100);
                entity.Property(kp => kp.ModificationDate).HasColumnType("datetime");
                entity.Property(kp => kp.Number).HasPrecision(3, 1);
            });

            //Piaskowanie
            modelBuilder.Entity<MeasurementsSeries>(entity =>
            {
                entity.Property(e => e.TrayNo)
                    .HasMaxLength(50);
                entity.Property(e => e.PositionOnTray)
                    .HasMaxLength(20);
                entity.Property(e => e.Comment)
                    .HasMaxLength(200);
                entity.HasMany(e => e.Measurements)
                    .WithOne(m => m.MeasurementsSeries)
                    .HasForeignKey(m => m.MeasurementSeriesId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(m => m.Order).WithMany(o => o.MeasurementsSeries).HasForeignKey(m => m.OrderId).OnDelete(DeleteBehavior.NoAction);
                entity.Property(e => e.PorosityClass)
                   .HasMaxLength(20);
                entity.Property(e => e.GrainSize)
                      .HasMaxLength(20);
                entity.Property(e => e.StorageLocation)
                      .HasMaxLength(20);
            });
            //Piaskowanie

            modelBuilder.Entity<OperationDetails>(entity =>
            {
                entity.Property(kp => kp.ModificationDate).HasColumnType("datetime");
                entity.Property(od => od.Program).HasMaxLength(200).IsRequired(false);
                entity.Property(od => od.PressureLeft).HasPrecision(5,2).IsRequired(false);
                entity.Property(od => od.PressureRight).HasPrecision(5,2).IsRequired(false);
                entity.Property(od => od.Feed).IsRequired(false);
                entity.Property(od => od.SandblastingHeight).IsRequired(false);
                entity.Property(od => od.BurrRate).IsRequired(false);
                entity.Property(od => od.ProcessTray).HasMaxLength(150).IsRequired(false);
                entity.Property(od => od.HeadsQty).HasMaxLength(150).IsRequired(false);
                entity.Property(od => od.ScanningMode).HasMaxLength(150).IsRequired(false);
                entity.Property(od => od.NoOfPasses).IsRequired(false);
                entity.Property(od => od.GunsPitch).HasPrecision(3, 1).IsRequired(false);
                entity.Property(od => od.NoBlastingBetweenRows).IsRequired(false);

                entity.Property(od => od.Cassette).HasMaxLength(30).IsRequired(false);
                entity.Property(od => od.CassetteInsertsQty).IsRequired(false);
                entity.Property(od => od.UpperRPM).HasPrecision(6, 2).IsRequired(false);
                entity.Property(od => od.UpperDirection).HasMaxLength(30).IsRequired(false);
                entity.Property(od => od.LowerRPM).HasPrecision(6, 2).IsRequired(false);
                entity.Property(od => od.LowerDirection).HasMaxLength(30).IsRequired(false);
                entity.Property(od => od.CentralTableRPM).HasPrecision(6, 2).IsRequired(false);
                entity.Property(od => od.CentralTableDirection).HasMaxLength(30).IsRequired(false);
                entity.Property(od => od.OrderKey).HasMaxLength(30).IsRequired(false);

                entity.HasOne(d => d.Operation)
                      .WithMany(o => o.OperationDetails)
                      .HasForeignKey(d => d.OperationId);
            });

            modelBuilder.Entity<PowderSpecification>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.PowderType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.HCJMin)
         .HasColumnType("decimal(7,3)");

                entity.Property(p => p.HCJNominal)
                    .HasColumnType("decimal(7,3)");

                entity.Property(p => p.HCJMax)
                    .HasColumnType("decimal(7,3)");

                entity.Property(p => p.COMMin)
                    .HasColumnType("decimal(7,3)");

                entity.Property(p => p.COMNominal)
                    .HasColumnType("decimal(7,3)");

                entity.Property(p => p.COMMax)
                    .HasColumnType("decimal(7,3)");

                entity.Property(p => p.DensityMin)
                    .HasColumnType("decimal(7,3)");

                entity.Property(p => p.DensityNominal)
                    .HasColumnType("decimal(7,3)");

                entity.Property(p => p.DensityMax)
                    .HasColumnType("decimal(7,3)");

                entity.Property(e => e.K1C)
                    .HasMaxLength(50)
                    .IsRequired(false);

                entity.Property(e => e.Porosity)
                    .HasMaxLength(50)
                    .IsRequired(false);

                entity.Property(e => e.ReleaseRules)
                    .HasMaxLength(200)
                    .IsRequired(false);

                entity.Property(e => e.ReleaseRulesForSamples)
                    .HasMaxLength(200)
                    .IsRequired(false);
            });

            modelBuilder.Entity<MRB>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Symbol).HasMaxLength(50);
                entity.Property(e => e.NonConformanceDescription).HasMaxLength(1000);
                entity.Property(e => e.RootCause).HasMaxLength(1000);
                entity.Property(e => e.Comment).HasMaxLength(1000);
                entity.Property(e => e.CreationDate).HasColumnType("datetime").IsRequired();
                entity.Property(e => e.ModificationDate).HasColumnType("datetime").IsRequired(false);
                entity.HasMany(e => e.CorrectiveActions)
                      .WithOne(e => e.MRB)
                      .HasForeignKey(e => e.MRBId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.MemberSummary)
                      .WithOne(e => e.MRB)
                      .HasForeignKey(e => e.MRBId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Instruction)
                      .WithOne(e => e.MRB)
                      .HasForeignKey<MRBInstruction>(e => e.MRBId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Order)
                      .WithOne(o => o.MRB)
                      .HasForeignKey<Order>(o => o.MRBId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<MRBMemberSummary>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Member).IsRequired(false).HasMaxLength(100);
                entity.Property(e => e.Summary).IsRequired(false).HasMaxLength(1000);
                entity.Property(e => e.ModificationDate).HasColumnType("datetime").IsRequired(false);
                entity.Property(e => e.MRBDipositions)
             .HasConversion(
                 v => string.Join(',', v), 
                 v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()) 
             .HasMaxLength(500);
            });

            modelBuilder.Entity<MRBCorrectiveAction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Creator).IsRequired(false).HasMaxLength(100);
                entity.Property(e => e.Action).IsRequired(false).HasMaxLength(1000);
                entity.Property(e => e.StaffResponsible).IsRequired(false).HasMaxLength(100);
                entity.Property(e => e.DueDate).HasColumnType("datetime").IsRequired(false);
            });

            modelBuilder.Entity<MRBInstruction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Creator).IsRequired(false).HasMaxLength(100);
                entity.Property(e => e.Instruction).IsRequired(false).HasMaxLength(1000);
                entity.Property(e => e.StaffResponsible).IsRequired(false).HasMaxLength(100);
                entity.Property(e => e.DueDate).HasColumnType("datetime").IsRequired(false);
            });
            
            modelBuilder.Entity<DescriptiveParameter>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.TestingInstrument).IsRequired(false).HasMaxLength(200);
                entity.Property(e => e.FillingMethod).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Comment).IsRequired(false).HasMaxLength(250);
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
                entity.HasOne(p => p.Operation).WithMany(o => o.DescriptiveParameters).HasForeignKey(o => o.OperationId);
                entity.HasOne(i => i.Image).WithOne(p => p.DescriptiveParameter).HasForeignKey<Image>(i => i.DescriptiveParameterId).IsRequired(false);
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.Property(m => m.Value).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Operator).HasMaxLength(50).IsRequired(false);
                entity.Property(m => m.Date).HasColumnType("datetime");
                entity.Property(m => m.OrderKey).HasMaxLength(30).IsRequired(false);
                entity.HasOne(m => m.Parameter).WithMany(p => p.Values).HasForeignKey(m => m.ParameterId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(m => m.Order).WithMany(o => o.Results).HasForeignKey(m => m.OrderId).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Pressing>(entity =>
            {
                entity.Property(m => m.RowDateTime).HasColumnType("datetime");
                entity.Property(m => m.TrialNo).HasMaxLength(10).IsRequired(false);
                entity.Property(m => m.OrderKey).HasMaxLength(50).IsRequired(false);
                entity.Property(m => m.PDMNo).HasMaxLength(30).IsRequired(false);
                entity.Property(m => m.Height1).HasPrecision(10, 3);
                entity.Property(m => m.Height2).HasPrecision(10, 3);
                entity.Property(m => m.Height3).HasPrecision(10, 3);
                entity.Property(m => m.Height4).HasPrecision(10, 3);
                entity.Property(m => m.Weight).HasPrecision(10, 3);
                entity.Property(m => m.Force).HasPrecision(10, 3);
                entity.Property(m => m.UCSB).HasPrecision(7, 3);
                entity.Property(m => m.UPS).HasPrecision(7, 3);
                entity.Property(m => m.PrecompactingA).HasPrecision(7, 3);
                entity.Property(m => m.PrecompactingB).HasPrecision(7, 3);
                entity.Property(m => m.PressStrokeRelation).HasMaxLength(50).IsRequired(false);

                entity.Property(m => m.Decopression1A).HasPrecision(7, 3);
                entity.Property(m => m.Decopression1B).HasPrecision(7, 3);
                entity.Property(m => m.DecopressionV1).HasPrecision(7, 3);
                entity.Property(m => m.Decopression2A).HasPrecision(7, 3);
                entity.Property(m => m.Decopression2B).HasPrecision(7, 3);
                entity.Property(m => m.DecopressionV2).HasPrecision(7, 3);
                entity.Property(m => m.UnderfillStrokeB).HasPrecision(7, 3);

                entity.Property(m => m.TrayQty).HasMaxLength(20).IsRequired(false);
                entity.Property(m => m.BaloonNo).HasMaxLength(50).IsRequired(false);
                entity.Property(m => m.RobotProgam).HasMaxLength(50).IsRequired(false);
                entity.Property(m => m.BurringPrassuereCloseValve).HasMaxLength(20).IsRequired(false);
                entity.Property(m => m.BurringPrassuereCloseValve).HasMaxLength(20).IsRequired(false);
                entity.Property(m => m.Comment).HasMaxLength(500).IsRequired(false);
                entity.Property(m => m.Powder).HasMaxLength(30).IsRequired(false);

                entity.HasOne(p => p.Order).WithMany(o => o.Pressings).HasForeignKey(p => p.OrderId);
            });

            modelBuilder.Entity<SinteringBatch>(entity =>
            {
                entity.Property(s => s.CreationDate).HasColumnType("datetime");
                entity.Property(s => s.CompletionDate).HasColumnType("datetime");
                entity.HasMany(s => s.TrayLocations).WithOne(ol => ol.Sintering).HasForeignKey(y => y.SinteringId);
            });

            modelBuilder.Entity<TrayLocation>(entity =>
            {
                entity.Property(ol => ol.Comment).HasMaxLength(250).IsRequired(false); ;
                entity.Property(ol => ol.TrayCoating).HasMaxLength(10).IsRequired(false); ;
                entity.HasOne(ol => ol.Order).WithMany(o => o.TrayLocations).HasForeignKey(ol => ol.OrderId);
                entity.HasOne(ol => ol.FuranceLocalization).WithMany(fl => fl.TrayLocations).HasForeignKey(sl => sl.FuranceLocalizationId);
                entity.HasMany(fl => fl.MeasurementsSeries).WithOne(ms => ms.TrayLocation).HasForeignKey(ms => ms.TrayLocationId);
            });

            modelBuilder.Entity<CoatingProcess>(entity =>
            {
                entity.Property(cp => cp.ProcessId).HasMaxLength(50).IsRequired(false);
                entity.Property(cp => cp.ProcessId).HasMaxLength(50).IsRequired(false);
                entity.Property(cp => cp.Coating).HasMaxLength(50).IsRequired(false);
                entity.HasMany(cp => cp.Measurements).WithOne(ms => ms.CoatingProcess).HasForeignKey(ms => ms.CoatingProcessId);
            });

            modelBuilder.Entity<Coating>(entity =>
            {
                entity.Property(cs => cs.CoatingSymbol).HasMaxLength(30).IsRequired(false);
                entity.Property(cs => cs.ProcessId).HasMaxLength(50).IsRequired(false);
                entity.Property(cs => cs.Type).HasMaxLength(50).IsRequired(false);
                entity.Property(cs => cs.CoatingName).HasMaxLength(30).IsRequired(false);
                entity.Property(cs => cs.InternalName).HasMaxLength(30).IsRequired(false);
                entity.Property(cs => cs.LSL).HasPrecision(4, 2);
                entity.Property(cs => cs.USL).HasPrecision(4, 2);
            });

            modelBuilder.Entity<CoatingMeasurementSeries>(entity =>
            {
                entity.Property(cp => cp.Date).HasColumnType("datetime");
                entity.Property(m => m.Thickness1).HasPrecision(5, 3);
                entity.Property(m => m.Thickness2).HasPrecision(5, 3);
                entity.Property(m => m.Thickness3).HasPrecision(5, 3);
                entity.Property(m => m.Thickness4).HasPrecision(5, 3);
                entity.Property(cp => cp.Comment).HasMaxLength(150);
            });

            modelBuilder.Entity<ToolTestComparison>(entity =>
            {
                entity.Property(ttc => ttc.TypeOfMachinning).HasMaxLength(50).IsRequired(false);
                entity.Property(ttc => ttc.WorpieceDescription).HasMaxLength(100).IsRequired(false);
                entity.Property(ttc => ttc.WorpieceHardness).HasMaxLength(100).IsRequired(false);
                entity.Property(ttc => ttc.Machine).HasMaxLength(100).IsRequired(false);
                entity.Property(ttc => ttc.Remarks).HasMaxLength(350).IsRequired(false);
                entity.Property(ttc => ttc.Conclusion).IsRequired(false);

                entity.HasMany(ttc => ttc.ToolTests).WithOne(tt => tt.Comparison).HasForeignKey(tt => tt.ComparisonId).OnDelete(DeleteBehavior.Cascade); ;

                //czy usunać relacje pomiędzy ComparisonPoint a ToolTestComparison
                entity.HasMany(ttc => ttc.ComparisonPoints).WithOne(tt => tt.ToolTestComparison).HasForeignKey(tt => tt.ToolTestComparisonId);



            });

            modelBuilder.Entity<ToolTest>(entity =>
            {
                entity.Property(tt => tt.HolderType).HasMaxLength(50).IsRequired(false);
                entity.Property(tt => tt.Manufacturer).HasMaxLength(30).IsRequired(false);
                entity.Property(tt => tt.ProductSymbol).HasMaxLength(30).IsRequired(false);
                entity.Property(tt => tt.Application).HasMaxLength(50).IsRequired(false);
                entity.Property(tt => tt.Substrate).HasMaxLength(50).IsRequired(false);
                entity.Property(tt => tt.Coating).HasMaxLength(50).IsRequired(false);
                entity.Property(tt => tt.ae).HasMaxLength(30).IsRequired(false);
                entity.Property(tt => tt.CoatingThickness).HasMaxLength(10).IsRequired(false);
                entity.Property(tt => tt.BatchNo).HasMaxLength(30).IsRequired(false);
                entity.Property(tt => tt.VisualInspection).HasMaxLength(10).IsRequired(false);
                entity.Property(tt => tt.Other).HasMaxLength(30).IsRequired(false);
                entity.Property(tt => tt.Feedf).HasMaxLength(10).IsRequired(false);
                entity.Property(tt => tt.FeedVf).HasMaxLength(10).IsRequired(false);
                entity.Property(tt => tt.SpeedVc).HasMaxLength(10).IsRequired(false);
                entity.Property(tt => tt.CuttingDepth).HasMaxLength(10).IsRequired(false);
                entity.Property(tt => tt.NumberOfPasses).HasMaxLength(10).IsRequired(false);
                entity.Property(tt => tt.Time).HasMaxLength(10).IsRequired(false);
                entity.Property(tt => tt.Cooling).HasMaxLength(10).IsRequired(false);
                entity.Property(tt => tt.ChipShape).HasMaxLength(30).IsRequired(false);
                entity.Property(tt => tt.WorkpieceRoughness).HasMaxLength(50).IsRequired(false);
                entity.Property(tt => tt.VisualDamageDescription).HasMaxLength(50).IsRequired(false);
                entity.Property(tt => tt.EdgeWear).HasMaxLength(30).IsRequired(false);
                entity.Property(tt => tt.Chipping).HasMaxLength(30).IsRequired(false);
                entity.Property(tt => tt.PlasticDeformation).HasMaxLength(30).IsRequired(false);
                entity.HasMany(tt => tt.ComparisonPoints).WithOne(cp => cp.ToolTest).HasForeignKey(tt => tt.ToolTestId);

            });

            modelBuilder.Entity<ComparisonPoint>(entity =>
            {
                entity.Property(cp => cp.Name).HasMaxLength(100).IsRequired(false);
                entity.Property(cp => cp.ControlPointValue).HasMaxLength(100).IsRequired(false);
                entity.Property(cp => cp.Parameter).HasMaxLength(30).IsRequired(false);
                entity.Property(cp => cp.Remarks).HasMaxLength(100).IsRequired(false);
                entity.HasMany(cp => cp.Images).WithOne(i => i.ComparisonPoint).HasForeignKey(i => i.ComparisonPointId)
                .OnDelete(DeleteBehavior.Cascade); 
            });

            modelBuilder.Entity<Tool>(entity =>
            {
                entity.Property(cp => cp.Name).HasMaxLength(100).IsRequired(false);
                entity.HasMany(cp => cp.ToolTestComparisons).WithOne(i => i.Tool).HasForeignKey(i => i.ToolId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
