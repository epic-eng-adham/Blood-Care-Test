using BloodCareF.Data;
using BloodCareF.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodCareF.Controllers
{
    public class BloodTypeController : Controller
    {
        BloodDbContext db;
        public BloodTypeController(BloodDbContext bloodDbContext)
        {
            db = bloodDbContext;
        }
        public IActionResult ShowBloodTypes()
        {
            List<BloodType> bloodTypes=db.bloodTypes.ToList();
            return View(bloodTypes);
        }
        public IActionResult Edit(int id)
        {
            BloodType bloodType = db.bloodTypes.FirstOrDefault(p=>p.Id==id);
            return View(bloodType); 
        }
        [HttpPost]
        public IActionResult Edit(BloodType bloodType)
        {
            var data = db.bloodTypes.FirstOrDefault(p=>p.Id==bloodType.Id);
            data.AvailableQuantity= bloodType.AvailableQuantity;
            db.SaveChanges();
            return RedirectToAction("ShowBloodTypes");
        }

    }
}
