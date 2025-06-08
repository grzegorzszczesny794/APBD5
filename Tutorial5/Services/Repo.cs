using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.DTOs;
using Tutorial5.Model;
using Tutorial5.Models;

namespace Tutorial5.Services
{
    public class Repo(DatabaseContext _context) : IRepo
    {
        public async Task<Patient?> GetPatientById(int id)
        => await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == id);

        public async Task<Patient> AddPatient(Patient patient)
        {
            var entry = await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<Prescription> AddPrescription(Prescription prescription)
        {
            var entry = await _context.Prescriptions.AddAsync(prescription);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task AddPrescriptionMedicaments(IEnumerable<PrescriptionMedicament> prescriptionMedicaments)
        {
            await _context.Prescription_Medicaments.AddRangeAsync(prescriptionMedicaments);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PatientResponse>> GetPatientInfo(int IdPatient)
        {
            return await _context.Patients
              .AsNoTracking()
              .Where(p => p.IdPatient == IdPatient)
              .Include(p => p.Prescriptions)
                  .ThenInclude(pr => pr.Prescription_Medicaments)
                      .ThenInclude(pm => pm.Medicament)
              .Select(p => new PatientResponse(
                  p.IdPatient,
                  p.FirstName,
                  p.LastName,
                  p.Birthdate,
                  p.Prescriptions.Select(pr => new PrescriptionResponse(
                      pr.IdPrescription,
                      pr.Date,
                      pr.Prescription_Medicaments.Select(pm => new MedicamentResponse(
                          pm.Medicament.IdMedicament,
                          pm.Medicament.Name,
                          pm.Dose,
                          pm.Details
                      )).ToList()
                  )).ToList()
              ))
              .ToListAsync();
        }

        public async Task<CustomerResponse?> GetCustomerInfo(int idCustomer)
        {
            var customer = await _context.customers
                .Include(c => c.PurchaseHistories)
                    .ThenInclude(ph => ph.AvalaibleProgram)
                    .ThenInclude(ap => ap.WachineMachine)
                .Include(c => c.PurchaseHistories)
                    .ThenInclude(ph => ph.AvalaibleProgram)
                        .ThenInclude(ap => ap.Program)
                .FirstOrDefaultAsync(c => c.CustomerId == idCustomer);


            if (customer is null)
                return null;

            var result = new CustomerResponse
            {
                firstName = customer.FirstName,
                lastName = customer.LastName,
                phoneNumber = customer.PhoneNumber,
                purchases = customer.PurchaseHistories.Select(ph => new PurchaseCustomerResponse
                {
                    date = ph.PurchaseDate,
                    rating = ph?.Rating,
                    price = ph.AvalaibleProgram?.Price,
                    washingMachine = new PurchaseCustomerWashingMachineResponse
                    {
                        serial = ph.AvalaibleProgram.WachineMachine.SerialNumber,
                        maxWeight = ph.AvalaibleProgram.WachineMachine.MaxWeight
                    },
                    program = new PurchaseCustomerProgramResponse
                    {
                        name = ph.AvalaibleProgram.Program.Name,
                        duration = ph.AvalaibleProgram.Program.DurationMinutes
                    }
                }).OrderBy(p => p.date).ToList()
            };

            return result;

        }
    }
}
