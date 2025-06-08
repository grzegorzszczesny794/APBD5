using Tutorial5.DTOs;
using Tutorial5.Models;

namespace Tutorial5.Services
{
    public interface IRepo
    {
        Task<Patient?> GetPatientById(int id);
        Task<Patient> AddPatient(Patient patient);
        Task<Prescription> AddPrescription(Prescription prescription);
        Task AddPrescriptionMedicaments(IEnumerable<PrescriptionMedicament> prescriptionMedicaments);
        Task<List<PatientResponse>> GetPatientInfo(int idPatient);
        Task<CustomerResponse?> GetCustomerInfo(int idCustomer);
    }
}
