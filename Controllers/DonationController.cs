using BloodCareF.Data;
using BloodCareF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodCareF.Controllers
{
    public class DonationController : Controller
    {
        BloodDbContext db;
        public DonationController(BloodDbContext bloodDbContext)
        {
            db = bloodDbContext;
        }
        public IActionResult ShowDonation()
        {
            List<Donation> donations = db.donations
                .Include(p=>p.Donor)
                .Include(p=>p.Patient)
                .ToList();
            return View(donations);
        }
        public IActionResult Add()
        {
            ViewBag.Donor = db.donors.ToList();
            ViewBag.Patient = db.patients.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Donation donation)
        {
            db.donations.Add(donation);
            db.SaveChanges();
            return RedirectToAction("ShowDonation");
        }
        public IActionResult Edit(int id)
        {
            Donation donation = db.donations
                .Include(p=>p.Donor)
                .Include(p=>p.Patient)
                .FirstOrDefault(p=>p.Id==id);
            ViewBag.Donor = db.donors.ToList();
            ViewBag.Patient = db.patients.ToList();
            return View(donation);
        }
        [HttpPost]
        public IActionResult Edit(Donation donation)
        {
            var data = db.donations
                .Include(p => p.Donor)
                .Include(p => p.Patient)
                .FirstOrDefault(p=>p.Id==donation.Id);
            data.DonorId = donation.DonorId;
            data.PAtientId = donation.PAtientId;
            data.Quantity = donation.Quantity;
            data.DonationDate = donation.DonationDate;
            db.SaveChanges();
            return RedirectToAction("ShowDonation");
        }
        public IActionResult Details(int id)
        {
            Donation donation = db.donations
                .Include(p=>p.Donor)
                .Include(p=>p.Patient)
                .FirstOrDefault(p=>p.Id==id);
            return View(donation);
        }
        public IActionResult Delete(int id)
        {
            Donation donation = db.donations.Find(id);
            db.donations.Remove(donation);
            db.SaveChanges();
            return RedirectToAction("ShowDonation");
        }
    }
}
