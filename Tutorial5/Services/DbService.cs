using Tutorial5.Data;
using Tutorial5.DTOs;
using Tutorial5.Helpers;
using Tutorial5.Models;

namespace Tutorial5.Services;

public class DbService(DatabaseContext _context
                     , PrescriptionValidator _validator
                     , IRepo _repo) : IDbService
{


    public async Task<Result<List<PatientResponse>>> GetPatientInfo(int IdPatient)
    {
        if (await _repo.GetPatientById(IdPatient) is null)
            return Result.Failure<List<PatientResponse>>($"Patient with ID {IdPatient} not found.");

        return Result.Success(await _repo.GetPatientInfo(IdPatient));
    }

    public async Task<Result<int>> AddPrescription(PrescriptionCreateRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (validationResult.IsFailure)
            return Result.Failure<int>(validationResult.Error);

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var patient = await _repo.GetPatientById(request.Patient.IdPatient);

            if (patient == null)
            {
                patient = new Patient
                {
                    Birthdate = request.Patient.Birthdate,
                    FirstName = request.Patient.FirstName,
                    LastName = request.Patient.LastName,
                };
                patient = await _repo.AddPatient(patient);
            }

            var prescription = new Prescription
            {
                Date = request.Date,
                DueDate = request.DueDate,
                IdPatient = patient.IdPatient,
                IdDoctor = request.IdDoctor
            };

            prescription = await _repo.AddPrescription(prescription);

            var prescriptionMedicaments = request.Medicaments.Select(prep => new PrescriptionMedicament
            {
                IdPrescription = prescription.IdPrescription,
                IdMedicament = prep.IdMedicament,
                Dose = prep.Dose,
                Details = prep.Description
            }).ToList();

            await _repo.AddPrescriptionMedicaments(prescriptionMedicaments);

            await transaction.CommitAsync();

            return Result<int>.Success(prescription.IdPrescription);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result.Failure<int>($"An error occurred: {ex.Message}");
        }
    }

    public async Task<Result<CustomerResponse>> GetCustomerInfo(int IdCustomer)
    {

        var result = await _repo.GetCustomerInfo(IdCustomer);

        if (result == null)
            return Result.Failure<CustomerResponse>($"Customer not exists");

        return Result<CustomerResponse>.Success(result);
    }

 
}