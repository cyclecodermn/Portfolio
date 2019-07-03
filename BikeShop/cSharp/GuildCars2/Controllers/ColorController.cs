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

namespace GuildBikes.Controllers
{
    public class ColorController : Controller
    {
        //public ActionResult Index()
        //{
        //    string model = AuthorizeUtilities.GetUserName(this);
        //    if (model.IsNullOrWhiteSpace())
        //        model = "No Name";

        //    return View();

        //}

        public ActionResult MngColors()
        {

            ColorsListViewModel model = new ColorsListViewModel();

            var ColorRepo = ColorRepoFactory.GetRepo();
            model.BikeColors = ColorRepo.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult AddColor()
        {
            BikeColorTable color = new BikeColorTable();
            //var color = ColorRepoADO.GetById(colorId);
            return View(color);
        }

        [HttpPost]
        public ActionResult AddColor(BikeColorTable color)
        {

            if (string.IsNullOrEmpty(color.BikeColorName))
            {
                ModelState.AddModelError("ColorId",
                    "Please enter the name of the color.");
            }
            if (ModelState.IsValid)
            {
                ColorRepoADO repo = new ColorRepoADO();
                repo.Insert(color);
                //MajorRepository.Edit(major);
                return RedirectToAction("MngColors");
            }
            else
            {
                return View(color);
            }
        }

        [HttpPost]
        public ActionResult EditColor(BikeColorTable color)
        {
            if (string.IsNullOrEmpty(color.BikeColorName))
            {
                ModelState.AddModelError("ColorId",
                    "Please enter the name of the color.");
            }
            if (ModelState.IsValid)
            {
                ColorRepoADO repo = new ColorRepoADO();
                repo.Edit(color);
                return RedirectToAction("MngColors");
            }
            else
            {
                return View(color);
            }
        }

        [HttpGet]
        public ActionResult EditColor(int colorId)
        {
            var color = ColorRepoADO.GetById(colorId);
            return View(color);
        }

        [HttpGet]
        public ActionResult DeleteColor(int colorId)
        {
            ColorDeleteViewModel ColorToDelete = new ColorDeleteViewModel();
            ColorToDelete.Color = ColorRepoADO.GetById(colorId);
            ColorToDelete.BikesWithColor = ColorRepoADO.CheckIfColorIsUsed(ColorToDelete.Color);
            //TODO: 1-The line above deletes the color before the web page appears confirming the color should be deleted.


            if (ColorToDelete.BikesWithColor.Count() > 0)
            {
                //Warn user that the color cannot be deleted since some bikes use the color.
                //This prevents an SQL err when trying to delete a FK in the bike table.
                ColorToDelete.message = "The color " + ColorToDelete.Color.BikeColorName + " is used by " +
                                        ColorToDelete.BikesWithColor.Count() + " bike(s), so it cannot be deleted.";
            }

            //BikeColorTable Color = ColorRepoADO.GetById(id);
            return View(ColorToDelete);
        }

        [HttpPost]
        public ActionResult DeleteColor(BikeColorTable color)
        {
            ColorRepoADO.Delete(color.BikeColorId);
            return RedirectToAction("MngColors");

        }
    }
}