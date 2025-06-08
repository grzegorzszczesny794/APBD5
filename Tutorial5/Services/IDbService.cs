using Tutorial5.DTOs;
using Tutorial5.Helpers;

namespace Tutorial5.Services;

public interface IDbService
{
    Task<Result<List<PatientResponse>>> GetPatientInfo(int IdPatient);
    Task<Result<int>> AddPrescription(PrescriptionCreateRequest request);

    Task<Result<CustomerResponse>> GetCustomerInfo(int IdCustomer);
}