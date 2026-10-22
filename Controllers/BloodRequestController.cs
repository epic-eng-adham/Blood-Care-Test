using BloodCareF.Data;
using BloodCareF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodCareF.Controllers
{
    public class BloodRequestController : Controller
    {
        BloodDbContext db;
        public BloodRequestController(BloodDbContext bloodDbContext)
        {
            db= bloodDbContext;
        }
        public IActionResult ShowBloodRequest()
        {
            List<BloodRequest> bloodRequests=db.BloodRequests
                .Include(p=>p.BloodType)
                .Include(p=>p.Patient)
                .ToList();
            return View(bloodRequests);
        }
        public IActionResult Add()
        {
            ViewBag.BloodType=db.bloodTypes.ToList();
            ViewBag.Patient=db.patients.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(BloodRequest bloodRequest)
        {
            var bloodType = db.bloodTypes
                .FirstOrDefault(b => b.Id == bloodRequest.BloodTypeId);
            if (bloodType != null &&
               bloodType.AvailableQuantity >= bloodRequest.QuantityNeeded)
            {
                bloodType.AvailableQuantity -= bloodRequest.QuantityNeeded;
                db.BloodRequests.Add(bloodRequest);
                db.SaveChanges();
                return RedirectToAction("ShowBloodRequest");
            }
            ViewBag.BloodType = db.bloodTypes.ToList();
            ViewBag.Patient = db.patients.ToList();
            return View(bloodRequest);
        }
        public IActionResult Edit(int id)
        {
            BloodRequest bloodRequest=db.BloodRequests
                .Include(p=>p.BloodType)
                .Include (p=>p.Patient)
                .FirstOrDefault(p=>p.Id==id);
            ViewBag.BloodType=db.bloodTypes.ToList();
            ViewBag.Patient=db.patients.ToList();
            return View(bloodRequest);
        }
        [HttpPost]
        public IActionResult Edit(BloodRequest bloodRequest)
        {
            var data =db.BloodRequests
                .Include(p => p.BloodType)
                .Include(p => p.Patient)
                .FirstOrDefault(p=>p.Id==bloodRequest.Id);
            int oldQuantity = data.QuantityNeeded;
            int oldBloodTypeId = data.BloodTypeId;
            var oldBlood = db.bloodTypes
                .FirstOrDefault(b => b.Id == oldBloodTypeId);

            if (oldBlood != null)
            {
                oldBlood.AvailableQuantity += oldQuantity;
            }
            var newBlood = db.bloodTypes
                .FirstOrDefault(b => b.Id == bloodRequest.BloodTypeId);

            if (newBlood != null)
            {
                if (newBlood.AvailableQuantity >= bloodRequest.QuantityNeeded)
                {
                    newBlood.AvailableQuantity -= bloodRequest.QuantityNeeded;
                }
                else
                {
                    return BadRequest("Not enough blood quantity");
                }
            }
            data.PatientId=bloodRequest.PatientId;
            data.BloodTypeId=bloodRequest.BloodTypeId;
            data.QuantityNeeded=bloodRequest.QuantityNeeded;
            db.SaveChanges();
            return RedirectToAction("ShowBloodRequest");
        }
        public IActionResult Details(int id)
        {
            BloodRequest bloodRequest = db.BloodRequests
                .Include(p => p.BloodType)
                .Include(p => p.Patient)
                .FirstOrDefault(p => p.Id == id);
            return View(bloodRequest);
        }
        public IActionResult Delete(int id)
        {
            BloodRequest bloodRequest = db.BloodRequests.Find(id);
            if (bloodRequest == null)
                return NotFound();
            var bloodType = db.bloodTypes
                .FirstOrDefault(b => b.Id == bloodRequest.BloodTypeId);
            if (bloodType != null)
            {
                bloodType.AvailableQuantity += bloodRequest.QuantityNeeded;
            }
            db.BloodRequests.Remove(bloodRequest);
            db.SaveChanges();
            return RedirectToAction("ShowBloodRequest");
        }
    }
}
