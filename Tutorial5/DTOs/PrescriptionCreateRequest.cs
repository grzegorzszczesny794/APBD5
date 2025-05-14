namespace Tutorial5.DTOs
{
    public sealed record PrescriptionCreateRequest(PrescriptionPatientRequest Patient
                                                 , int IdDoctor
                                                 , DateTime DueDate
                                                 , DateTime Date
                                                 ,  List<PrescriptionMedicamentCreateRequest> Medicaments);

    public sealed record PrescriptionMedicamentCreateRequest(int IdMedicament, int Dose, string Description);
    public sealed record PrescriptionPatientRequest(int IdPatient
                                               , string FirstName
                                               , string LastName
                                               , string Email
                                               , DateTime Birthdate);
}
