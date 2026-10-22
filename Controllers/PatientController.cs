using BloodCareF.Data;
using BloodCareF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodCareF.Controllers
{
    public class PatientController : Controller
    {
        BloodDbContext db;
        public PatientController(BloodDbContext bloodDbContext)
        {
            db = bloodDbContext;
        }

        public IActionResult ShowPatient()
        {
            List<Patient> patients =db.patients
                .Include(p=>p.BloodType)
                .ToList();
            return View(patients);
        }
        public IActionResult Add()
        {
            ViewBag.BloodType=db.bloodTypes.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Patient patient)
        {
            db.patients.Add(patient);
            db.SaveChanges();
            return RedirectToAction("ShowPatient");
        }
        public IActionResult Edit(int id)
        {
            Patient patient =db.patients
                .Include(p=>p.BloodType)
                .FirstOrDefault(p => p.Id == id);
            ViewBag.BloodType = db.bloodTypes.ToList();
            return View(patient);
        }
        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            var data = db.patients.Include(p => p.BloodType).FirstOrDefault(p => p.Id==patient.Id);
            data.FullName=patient.FullName;
            data.BloodTypeID = patient.BloodTypeID;
            data.Age = patient.Age;
            data.Address = patient.Address;
            data.Gender = patient.Gender;
            data.Phone = patient.Phone;
            db.SaveChanges();
            return RedirectToAction("ShowPatient");
        }
        public IActionResult Details(int id)
        {
            Patient patient=db.patients
                .Include(p=>p.BloodType)
                .FirstOrDefault (p => p.Id == id);
            return View(patient);
        }
        public IActionResult Delete(int id) 
        {
            Patient patient = db.patients.Find(id);
            db.patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("ShowPatient");
        }
    }
}
