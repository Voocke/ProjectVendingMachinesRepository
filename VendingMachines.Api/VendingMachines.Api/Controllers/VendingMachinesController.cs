using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingMachines.Api.Data;
using VendingMachines.Api.Models;

namespace VendingMachines.Api.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class VendingMachinesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public VendingMachinesController(AppDbContext db)
        {
            _db = db;
        }



        // GET api/VendingMachines
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _db.VendingMachines
                .AsNoTracking()
                .ToList();

            return Ok(list);
        }

        // GET api/VendingMachines/5
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var vm = _db.VendingMachines
                .AsNoTracking()
                .FirstOrDefault(x => x.VendingMachineId == id); // <-- проверь имя PK в модели

            if (vm == null)
                return NotFound(new { message = "Не найдено" });

            return Ok(vm);
        }

        // POST api/VendingMachines
        [HttpPost]
        public IActionResult Create([FromBody] VendingMachine vm)
        {
            // супер-простая валидация (остальное добьёт SQL: unique, trigger, checks)
            if (string.IsNullOrWhiteSpace(vm.SerialNumber))
                return BadRequest(new { message = "SerialNumber обязателен" });

            if (string.IsNullOrWhiteSpace(vm.InventoryNumber))
                return BadRequest(new { message = "InventoryNumber обязателен" });

            try
            {
                _db.VendingMachines.Add(vm);
                _db.SaveChanges(); // <-- без async

                return CreatedAtAction(nameof(GetById),
                    new { id = vm.VendingMachineId }, vm);
            }
            catch (DbUpdateException ex)
            {
                // сюда часто попадают нарушения UNIQUE и FK
                return BadRequest(new { message = "Ошибка сохранения в БД: " + ex.InnerException?.Message ?? ex.Message });
            }
        }

        // PUT api/VendingMachines/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] VendingMachine vm)
        {
            var existing = _db.VendingMachines.FirstOrDefault(x => x.VendingMachineId == id);
            if (existing == null)
                return NotFound(new { message = "Не найдено" });

            // Самый тупой способ: “перенести значения”
            // (Можно проще через _db.Entry(existing).CurrentValues.SetValues(vm); — но оставлю максимально понятно)
            existing.SerialNumber = vm.SerialNumber;
            existing.InventoryNumber = vm.InventoryNumber;
            existing.Location = vm.Location;
            existing.ModelId = vm.ModelId;
            existing.CountryId = vm.CountryId;
            existing.StatusId = vm.StatusId;
            existing.MachineTypeId = vm.MachineTypeId;
            existing.ManufacturerCompanyId = vm.ManufacturerCompanyId;

            existing.ManufactureDate = vm.ManufactureDate;
            existing.CommissioningDate = vm.CommissioningDate;
            existing.LastVerificationDate = vm.LastVerificationDate;
            existing.VerificationIntervalMonths = vm.VerificationIntervalMonths;

            existing.ResourceHours = vm.ResourceHours;
            existing.NextMaintenanceDate = vm.NextMaintenanceDate;
            existing.ResourceHours = vm.ResourceHours;

            existing.LastVerifierUserId = vm.LastVerifierUserId;

            try
            {
                _db.SaveChanges();
                return Ok(new { message = "Обновлено" });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { message = "Ошибка обновления в БД: " + ex.InnerException?.Message ?? ex.Message });
            }
        }

        // DELETE api/VendingMachines/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var existing = _db.VendingMachines.FirstOrDefault(x => x.VendingMachineId == id);
            if (existing == null)
                return NotFound(new { message = "Не найдено" });

            try
            {
                _db.VendingMachines.Remove(existing);
                _db.SaveChanges();
                return Ok(new { message = "Удалено" });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { message = "Ошибка удаления в БД: " + ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
