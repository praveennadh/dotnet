using Microsoft.EntityFrameworkCore;
using fleetsystem.config;
using fleetsystem.entities;

namespace fleetsystem.repositories;

public class DriverRepository : IDriverRepository
{
    private readonly DriverDbContext _context;

    public DriverRepository(DriverDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Driver>> GetAllDriversAsync() => await _context.Drivers.ToListAsync();

    public async Task<Driver?> GetDriverByIdAsync(int id) => await _context.Drivers.FindAsync(id);

    public async Task<Driver> AddDriverAsync(Driver driver)
    {
        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();
        return driver;
    }

    public async Task<Driver?> UpdateDriverAsync(Driver driver)
    {
        var existingDriver = await _context.Drivers.FindAsync(driver.Id);
        if (existingDriver == null) return null;

        existingDriver.Name = driver.Name;
        existingDriver.License = driver.License;
        existingDriver.Details = driver.Details;

        await _context.SaveChangesAsync();
        return existingDriver;
    }

    public async Task<bool> DeleteDriverAsync(int id)
    {
        var driver = await _context.Drivers.FindAsync(id);
        if (driver == null) return false;

        _context.Drivers.Remove(driver);
        await _context.SaveChangesAsync();
        return true;
    }
}