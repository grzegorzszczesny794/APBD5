using Microsoft.EntityFrameworkCore;
using Moq;
using Tutorial5.Data;
using Tutorial5.Models;
using Tutorial5.Services;

namespace Tests
{
    public class RepoTests
    {
        public class RepoTests
        {
            private readonly Mock<DatabaseContext> _mockContext;
            private readonly Repo _repo;

            public RepoTests()
            {
                _mockContext = new Mock<DatabaseContext>();
                _repo = new Repo(_mockContext.Object);
            }

            [Fact]
            public async Task GetPatientById_ShouldReturnPatient_WhenPatientExists()
            {
                // Arrange
                var patientId = 1;
                var patient = new Patient { IdPatient = patientId, FirstName = "Jan", LastName = "Kowalski", Birthdate = new DateTime(1990, 1, 1) };

                var patients = new List<Patient> { patient };
                _mockContext.Setup(c => c.Patients).ReturnsDbSet(patients);

                // Act
                var result = await _repo.GetPatientById(patientId);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(patientId, result.IdPatient);
                Assert.Equal("Jan", result.FirstName);
                Assert.Equal("Kowalski", result.LastName);
            }

            [Fact]
            public async Task GetPatientById_ShouldReturnNull_WhenPatientDoesNotExist()
            {
                // Arrange
                var patients = new List<Patient>();
                _mockContext.Setup(c => c.Patients).ReturnsDbSet(patients);

                // Act
                var result = await _repo.GetPatientById(1);

                // Assert
                Assert.Null(result);
            }

            [Fact]
            public async Task AddPatient_ShouldAddPatientToContext_AndReturnAddedPatient()
            {
                // Arrange
                var patient = new Patient { FirstName = "Jan", LastName = "Kowalski", Birthdate = new DateTime(1990, 1, 1) };
                var dbSetMock = new Mock<DbSet<Patient>>();

                dbSetMock.Setup(d => d.AddAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((Patient p, CancellationToken token) => new Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Patient>
                        {
                            Entity = new Patient
                            {
                                IdPatient = 1,
                                FirstName = p.FirstName,
                                LastName = p.LastName,
                                Birthdate = p.Birthdate
                            }
                        } as Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Patient>);

                _mockContext.Setup(c => c.Patients).Returns(dbSetMock.Object);
                _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

                // Act
                var result = await _repo.AddPatient(patient);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.IdPatient);
                Assert.Equal("Jan", result.FirstName);
                Assert.Equal("Kowalski", result.LastName);
                _mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            }

            [Fact]
            public async Task GetPatientInfo_ShouldReturnPatientResponse_WithPrescriptionsAndMedicaments()
            {
                // Arrange
                var patientId = 1;
                var medicament = new Medicament { IdMedicament = 1, Name = "Paracetamol" };
                var prescriptionMedicament = new PrescriptionMedicament
                {
                    IdPrescription = 1,
                    IdMedicament = 1,
                    Medicament = medicament,
                    Dose = 2,
                    Details = "Take twice a day"
                };
                var prescription = new Prescription
                {
                    IdPrescription = 1,
                    IdPatient = patientId,
                    Date = DateTime.Now,
                    Prescription_Medicaments = new List<PrescriptionMedicament> { prescriptionMedicament }
                };
                var patient = new Patient
                {
                    IdPatient = patientId,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Birthdate = new DateTime(1990, 1, 1),
                    Prescriptions = new List<Prescription> { prescription }
                };

                var patients = new List<Patient> { patient };
                var mockDbSet = patients.AsQueryable().BuildMockDbSet();

                _mockContext.Setup(c => c.Patients).Returns(mockDbSet.Object);

                // Act
                var result = await _repo.GetPatientInfo(patientId);

                // Assert
                Assert.NotNull(result);
                Assert.Single(result);
                Assert.Equal(patientId, result[0].IdPatient);
                Assert.Equal("Jan", result[0].FirstName);
                Assert.Equal("Kowalski", result[0].LastName);
                Assert.Single(result[0].Prescriptions);
                Assert.Equal(1, result[0].Prescriptions[0].IdPrescription);
                Assert.Single(result[0].Prescriptions[0].Medicaments);
                Assert.Equal(1, result[0].Prescriptions[0].Medicaments[0].IdMedicament);
                Assert.Equal("Paracetamol", result[0].Prescriptions[0].Medicaments[0].Name);
                Assert.Equal(2, result[0].Prescriptions[0].Medicaments[0].Dose);
                Assert.Equal("Take twice a day", result[0].Prescriptions[0].Medicaments[0].Details);
            }
        }
    }
}