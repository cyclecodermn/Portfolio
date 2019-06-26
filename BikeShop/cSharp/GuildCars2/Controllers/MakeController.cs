using bikes.data.Interfaces.Factories;
using bikes.models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Web.UI;
using bikes.data.ADO;
using bikes.data.Interfaces;
using bikes.models.Queries;
using bikes.models.Tables;
using GuildBikes.Utilities;
using Microsoft.Ajax.Utilities;
using bikes.data.Interfaces.FactoriesFactories;

//The bike make refers to the manufacturer or company that produced the bike,
//while the bike model refers to the bike product itself and its
//registered identification name.
//Bike models are identifiable and comprised of names, initials, or numbers.
//
//BikeModelTable only contains 2 fields: Make Name & Make ID
//BikeMakeTable contains 3 fields: Make Name, Make ID, AND Make Id

namespace GuildBikes.Controllers
{
    public class MakeController : Controller
    {

        public ActionResult MngMakes()
        {

            MakesListViewModel model = new MakesListViewModel();

            var MakeRepo = MakeRepoFactory.GetRepo();
            model.BikeMakes = MakeRepo.GetAll();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddMake(BikeMakeTable model)
        {

            if (string.IsNullOrEmpty(model.BikeMakeName))
            {
                ModelState.AddModelError("MakeId",
                    "Please enter the name of the model.");
            }
            if (ModelState.IsValid)
            {
                MakeRepoADO repo = new MakeRepoADO();
                repo.Insert(model);
                //MajorRepository.Edit(major);
                return RedirectToAction("MngMakes");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult AddMake()
        {
            BikeMakeTable model = new BikeMakeTable();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditMake(BikeMakeTable model)
        {
            if (string.IsNullOrEmpty(model.BikeMakeName))
            {
                ModelState.AddModelError("MakeId",
                    "Please enter the name of the model.");
            }
            if (ModelState.IsValid)
            {
                MakeRepoADO repo = new MakeRepoADO();
                repo.Edit(model);
                return RedirectToAction("MngMakes");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult EditMake(int makeId)
        {
            var model = MakeRepoADO.GetById(makeId);
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteMake(int makeId)
        {
            MakeDeleteViewModel MakeToDelete = new MakeDeleteViewModel();
            MakeToDelete.Make = MakeRepoADO.GetById(makeId);
            MakeToDelete.BikesWithMake = MakeRepoADO.CheckIfMakeIsUsed(MakeToDelete.Make);
            //TODO: 1-The line above deletes the model before the web page appears confirming the model should be deleted.


            if (MakeToDelete.BikesWithMake.Count() > 0)
            {
                //Warn user that the model cannot be deleted since some bikes use the model.
                //This prevents an SQL err when trying to delete a FK in the bike table.
                MakeToDelete.message = "The model " + MakeToDelete.Make.BikeMakeName + " is used by " +
                                        MakeToDelete.BikesWithMake.Count() + " bike(s), so it cannot be deleted.";
            }

            //BikeMakeTable Make = MakeRepoADO.GetById(id);
            return View(MakeToDelete);
        }

        [HttpPost]
        public ActionResult DeleteMake(BikeMakeTable model)
        {
            MakeRepoADO.Delete(model.BikeMakeId);
            return RedirectToAction("MngMakes");

        }
    }
}