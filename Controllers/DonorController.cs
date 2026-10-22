using BloodCareF.Data;
using BloodCareF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodCareF.Controllers
{
    public class DonorController : Controller
    {
        BloodDbContext db;
        public DonorController(BloodDbContext bloodDbContext)
        {
            db=bloodDbContext;
        }
        public IActionResult ShowDonor()
        {
            List<Donor> donors = db.donors.Include(p=>p.BloodType).ToList();
            return View(donors);
        }
        public IActionResult Add()
        {
            ViewBag.BloodType = db.bloodTypes.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Donor donor) 
        {
            db.donors.Add(donor);
            var bloodType = db.bloodTypes
                   .FirstOrDefault(b => b.Id == donor.BloodTypeId);
            if (bloodType != null)
            {
                bloodType.AvailableQuantity += donor.DonationQuantity;
            }
            db.SaveChanges();
            return RedirectToAction("ShowDonor");
        }
        public IActionResult Edit(int id)
        {
            Donor donor = db.donors
                .Include(p=>p.BloodType)
                .FirstOrDefault(p=>p.ID==id);
            ViewBag.BloodType = db.bloodTypes.ToList();
            return View(donor);
        }
        [HttpPost]
        public IActionResult Edit(Donor donor)
        {
            var data = db.donors.FirstOrDefault(p=>p.ID==donor.ID);
            int oldQuantity = data.DonationQuantity;
            data.FullName=donor.FullName;
            data.BloodTypeId=donor.BloodTypeId;
            data.Address=donor.Address;
            data.Age=donor.Age;
            data.Gender=donor.Gender;
            data.Address=donor.Address;
            data.DonationQuantity=donor.DonationQuantity;
            data.Phone=donor.Phone;
            var bloodType = db.bloodTypes
                    .FirstOrDefault(b => b.Id == donor.BloodTypeId);
            if (bloodType != null)
            {
                bloodType.AvailableQuantity += donor.DonationQuantity - oldQuantity;
            }
            db.SaveChanges();
            return RedirectToAction("ShowDonor");
        }
        public IActionResult Details(int id)
        {
            Donor donor=db.donors
                .Include(p=>p.BloodType)
                .FirstOrDefault(d => d.ID==id);
            return View(donor);

        }
        public IActionResult Delete(int id) 
        {
            Donor donor = db.donors.Find(id);
            var bloodType = db.bloodTypes
                 .FirstOrDefault(b => b.Id == donor.BloodTypeId);
            if (bloodType != null)
            {
                bloodType.AvailableQuantity -= donor.DonationQuantity;
            }
            db.Remove(donor);
            db.SaveChanges();
            return RedirectToAction("ShowDonor");
        }
    }
}
