using Microsoft.EntityFrameworkCore;
using Tutorial5.Model;
using Tutorial5.Models;
using Tutorial5.Services;

namespace Tutorial5.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> Prescription_Medicaments { get; set; }

    public DbSet<WashingMachine> washingMachines { get; set; }
    public DbSet<ProgramD> programs { get; set; }
    public DbSet<AvalaibleProgram> avalaiblePrograms { get; set; }
    public DbSet<Customer> customers { get; set; }
    public DbSet<PurchaseHistory> purchaseHistories { get; set; }

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<WashingMachine>(entity =>
        {
            entity.HasKey(e => e.WashingMachineId);
            entity.Property(e => e.SerialNumber).IsRequired().HasMaxLength(100);
            entity.Property(e => e.MaxWeight).IsRequired().HasColumnType("decimal(10,2)");
            entity.HasIndex(e => e.SerialNumber).IsUnique();
        });

        modelBuilder.Entity<ProgramD>(entity =>
        {
            entity.HasKey(e => e.ProgramId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.DurationMinutes).IsRequired();
            entity.Property(e => e.TemperatureCelsius).IsRequired();
        });

        modelBuilder.Entity<AvalaibleProgram>(entity =>
        {
            entity.HasKey(e => e.AvailableProgramId);
            entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(10,2)");

            entity.HasOne(e => e.WachineMachine)
                .WithMany(w => w.AvalaiblePrograms)
                .HasForeignKey(e => e.WaschingMachineId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Program)
                .WithMany(p => p.AvalaiblePrograms)
                .HasForeignKey(e => e.ProgramId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(100);
        });

        modelBuilder.Entity<PurchaseHistory>(entity =>
        {
            //entity.HasKey(e => e.PurchaseHistoryId);
            //entity.Property(e => e.PurchaseDate).IsRequired();

            entity.HasOne(e => e.AvalaibleProgram)
                .WithMany(ap => ap.PurchaseHistories)
                .HasForeignKey(e => e.AvalaibleProgramId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Customer)
                .WithMany(c => c.PurchaseHistories)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        var baseDate2 = new DateTime(2025, 5, 13);

        modelBuilder.Entity<WashingMachine>().HasData(
           new WashingMachine() { MaxWeight = 400, SerialNumber = "test232", WashingMachineId = 1 },
           new WashingMachine() { MaxWeight = 420, SerialNumber = "test22", WashingMachineId = 2 },
           new WashingMachine() { MaxWeight = 430, SerialNumber = "test12", WashingMachineId = 3 }
       );

        modelBuilder.Entity<ProgramD>().HasData(
            new ProgramD()
            {
                Name = "test23",
                DurationMinutes = 23,
                TemperatureCelsius = 30,
                ProgramId = 1
            },
            new ProgramD()
            {
                Name = "test21323",
                DurationMinutes = 232,
                TemperatureCelsius = 40,
                ProgramId = 2
            },
            new ProgramD()
            {
                Name = "sdiuadsaoda",
                DurationMinutes = 232,
                TemperatureCelsius = 20,
                ProgramId = 3
            }
        );

        modelBuilder.Entity<AvalaibleProgram>().HasData(
            new AvalaibleProgram()
            {
                AvailableProgramId = 1,
                WaschingMachineId = 1,
                ProgramId = 1,
                Price = new decimal(12.23),
            },
            new AvalaibleProgram()
            {
                AvailableProgramId = 2,
                WaschingMachineId = 2,
                ProgramId = 2,
                Price = new decimal(10.23)
            },
            new AvalaibleProgram()
            {
                AvailableProgramId = 3,
                WaschingMachineId = 3,
                ProgramId = 3,
                Price = new decimal(9.99)
            }
        );


        modelBuilder.Entity<Customer>().HasData(
            new Customer()
            {
                CustomerId = 1,
                FirstName = "Adam",
                LastName = "Wozniak",
                PhoneNumber = "121222333",
            },
            new Customer()
            {
                CustomerId = 2,
                FirstName = "Adam",
                LastName = "Szczęsny",
                PhoneNumber = "121222333",
            },
            new Customer()
            {
                CustomerId = 3,
                FirstName = "Adam",
                LastName = "Domżalski",
                PhoneNumber = "121222555",
            }
        );

        modelBuilder.Entity<PurchaseHistory>().HasData(
            new PurchaseHistory()
            {
                AvalaibleProgramId = 1,
                CustomerId = 1,
                PurchaseDate = baseDate2,
                Rating = 1
            },
            new PurchaseHistory()
            {
                AvalaibleProgramId = 2,
                CustomerId = 2,
                PurchaseDate = baseDate2,
                Rating = 2
            },
            new PurchaseHistory()
            {
                AvalaibleProgramId = 3,
                CustomerId = 3,
                PurchaseDate = baseDate2,
                Rating = null
            }
        );

        //modelBuilder.Entity<PrescriptionMedicament>()
        //      .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

        //modelBuilder.Entity<PrescriptionMedicament>()
        //    .HasOne(pm => pm.Medicament)
        //    .WithMany(m => m.Prescription_Medicaments)
        //    .HasForeignKey(pm => pm.IdMedicament);

        //modelBuilder.Entity<PrescriptionMedicament>()
        //    .HasOne(pm => pm.Prescription)
        //    .WithMany(p => p.Prescription_Medicaments)
        //    .HasForeignKey(pm => pm.IdPrescription);

        //modelBuilder.Entity<Prescription>()
        //    .HasOne(p => p.Patient)
        //    .WithMany(pa => pa.Prescriptions)
        //    .HasForeignKey(p => p.IdPatient);

        //modelBuilder.Entity<Prescription>()
        //    .HasOne(p => p.Doctor)
        //    .WithMany(d => d.Prescriptions)
        //    .HasForeignKey(p => p.IdDoctor);

        //var baseDate = new DateTime(2025, 5, 13);

        //modelBuilder.Entity<Doctor>().HasData(
        //    new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@przychodnia.pl" },
        //    new Doctor { IdDoctor = 2, FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@przychodnia.pl" },
        //    new Doctor { IdDoctor = 3, FirstName = "Piotr", LastName = "Wiśniewski", Email = "piotr.wisniewski@przychodnia.pl" }
        //);

        //modelBuilder.Entity<Patient>().HasData(
        //    new Patient { IdPatient = 1, FirstName = "Maria", LastName = "Dąbrowska", Birthdate = new DateTime(1980, 5, 15) },
        //    new Patient { IdPatient = 2, FirstName = "Tomasz", LastName = "Lewandowski", Birthdate = new DateTime(1975, 11, 8) },
        //    new Patient { IdPatient = 3, FirstName = "Karolina", LastName = "Zielińska", Birthdate = new DateTime(1990, 3, 22) },
        //    new Patient { IdPatient = 4, FirstName = "Adam", LastName = "Kowalczyk", Birthdate = new DateTime(1985, 7, 10) }
        //);

        //modelBuilder.Entity<Medicament>().HasData(
        //    new Medicament { IdMedicament = 1, Name = "Paracetamol", Description = "Lek przeciwbólowy i przeciwgorączkowy", Type = "Tabletka" },
        //    new Medicament { IdMedicament = 2, Name = "Ibuprom", Description = "Lek przeciwbólowy, przeciwzapalny i przeciwgorączkowy", Type = "Tabletka" },
        //    new Medicament { IdMedicament = 3, Name = "Amoxicillin", Description = "Antybiotyk o szerokim spektrum działania", Type = "Kapsułka" },
        //    new Medicament { IdMedicament = 4, Name = "Loratadyna", Description = "Lek przeciwalergiczny", Type = "Tabletka" },
        //    new Medicament { IdMedicament = 5, Name = "Insulina", Description = "Hormon regulujący poziom cukru we krwi", Type = "Iniekcja" }
        //);

        //modelBuilder.Entity<Prescription>().HasData(
        //    new Prescription { IdPrescription = 1, Date = baseDate.AddDays(-5), DueDate = baseDate.AddDays(25), IdDoctor = 1, IdPatient = 1 },
        //    new Prescription { IdPrescription = 2, Date = baseDate.AddDays(-3), DueDate = baseDate.AddDays(27), IdDoctor = 2, IdPatient = 2 },
        //    new Prescription { IdPrescription = 3, Date = baseDate.AddDays(-2), DueDate = baseDate.AddDays(28), IdDoctor = 1, IdPatient = 3 },
        //    new Prescription { IdPrescription = 4, Date = baseDate.AddDays(-1), DueDate = baseDate.AddDays(29), IdDoctor = 3, IdPatient = 4 },
        //    new Prescription { IdPrescription = 5, Date = baseDate, DueDate = baseDate.AddDays(30), IdDoctor = 1, IdPatient = 2 }
        //);

        //modelBuilder.Entity<PrescriptionMedicament>().HasData(
        //    new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Dose = 1, Details = "1 tabletka 3 razy dziennie" },
        //    new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 2, Dose = 1, Details = "1 tabletka co 8 godzin" },
        //    new PrescriptionMedicament { IdPrescription = 2, IdMedicament = 3, Dose = 2, Details = "2 kapsułki co 12 godzin przez 7 dni" },
        //    new PrescriptionMedicament { IdPrescription = 3, IdMedicament = 4, Dose = 1, Details = "1 tabletka rano" },
        //    new PrescriptionMedicament { IdPrescription = 4, IdMedicament = 5, Dose = 10, Details = "10 jednostek przed posiłkiem" },
        //    new PrescriptionMedicament { IdPrescription = 5, IdMedicament = 1, Dose = 1, Details = "1 tabletka co 6 godzin w razie bólu" },
        //    new PrescriptionMedicament { IdPrescription = 5, IdMedicament = 4, Dose = 1, Details = "1 tabletka wieczorem" }
        //);
    }
}