using Tutorial5.DTOs;
using Tutorial5.Data;
using Microsoft.EntityFrameworkCore;
using Tutorial5.Helpers;

namespace Tutorial5.Services;

public class PrescriptionValidator(DatabaseContext _context)
{
    public async Task<Result> ValidateAsync(PrescriptionCreateRequest request)
    {
        if (request.Medicaments.Count > 10)
            return Result.Failure("Medicaments are greater than 10");

        if (request.DueDate < request.Date)
            return Result.Failure("Due date must be after or equal to date");

        if (!await _context.Doctors.AnyAsync(x => x.IdDoctor == request.IdDoctor))
            return Result.Failure("Doctor doesn't exist!");

        var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMeds = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .ToListAsync();

        if (existingMeds.Count != request.Medicaments.Count)
            return Result.Failure("Some medicaments do not exist");

        return Result.Success();
    }
}
