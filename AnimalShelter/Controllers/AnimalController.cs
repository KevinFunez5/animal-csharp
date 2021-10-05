using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {
    private readonly AnimalShelterContext _db;

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Animal animal)
    {
        _db.Animals.Add(animal);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Sort(string sortMethod)
    {
      return RedirectToAction("Index");
    }

    public AnimalsController(AnimalShelterContext db)
    {
      _db = db;
    }

    public ActionResult Index(string sortMethod)
    {
      List<Animal> model = new List<Animal> { };
      List<Animal> modelToSort = _db.Animals.ToList();
      if (sortMethod == "breed")
      {
        model = modelToSort.OrderBy(animal => animal.Breed).ToList();
      }
      else if (sortMethod == "type")
      {
        model = modelToSort.OrderBy(animal => animal.Type).ToList();
      }
      else if (sortMethod =="dateRecent")
      {
        model = modelToSort.OrderByDescending(animal => animal.DateOfAdmittance).ToList();
      }
      else if (sortMethod =="dateOldest")
      {
        model = modelToSort.OrderBy(animal => animal.DateOfAdmittance).ToList();
      }
      else
      {
        model = modelToSort;
      }
      return View(model);
    }

    public ActionResult Details(int id)
    {
    Animal thisAnimal = _db.Animals.FirstOrDefault(animal => animal.AnimalId == id);
    return View(thisAnimal);
    }
  }
}