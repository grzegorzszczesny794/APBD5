namespace Tutorial5.DTOs
{
    public sealed record PatientResponse(int IdPatient, string FirstName, string LastName, DateTime BirthDate, List<PrescriptionResponse> Prescriptions);
    public sealed record PrescriptionResponse(int IdPrescription, DateTime Date, List<MedicamentResponse> Medicaments);
    public sealed record MedicamentResponse(int IdMedicament, string Name, int Dose, string Description);
    public sealed record DoctorResponse(int IdDoctor, string FirstName, string LastName, string Email);

}
