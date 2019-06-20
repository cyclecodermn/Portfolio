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

namespace GuildBikes.Controllers
{
    public class ModelController : Controller
    {
        //public ActionResult Index()
        //{
        //    string model = AuthorizeUtilities.GetUserName(this);
        //    if (model.IsNullOrWhiteSpace())
        //        model = "No Name";

        //    return View();

        //}

        public ActionResult MngModels()
        {

            ModelsListViewModel model = new ModelsListViewModel();

            var ModelRepo = ModelRepoFactory.GetRepo();
            model.BikeModels = ModelRepo.GetAll();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddModel(BikeModelTable model)
        {

            if (string.IsNullOrEmpty(model.BikeModelName))
            {
                ModelState.AddModelError("ModelId",
                    "Please enter the name of the model.");
            }
            if (ModelState.IsValid)
            {
                ModelRepoADO repo = new ModelRepoADO();
                repo.Insert(model);
                //MajorRepository.Edit(major);
                return RedirectToAction("MngModels");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult AddModel()
        {
            AddModelViewModel model = new AddModelViewModel();
            model.NewBikeModel = new BikeModelTable();
            var MakeRepo = MakeRepoFactory.GetRepo();
            model.BikeMakes = new SelectList(MakeRepo.GetAll(), "BikeMakeNameId", "BikeMake");


            //var model = ModelRepoADO.GetById(modelId);
            return View(model);
    }

    [HttpPost]
    public ActionResult EditModel(BikeModelTable model)
    {
        if (string.IsNullOrEmpty(model.BikeModelName))
        {
            ModelState.AddModelError("ModelId",
                "Please enter the name of the model.");
        }
        if (ModelState.IsValid)
        {
            ModelRepoADO repo = new ModelRepoADO();
            repo.Edit(model);
            return RedirectToAction("MngModels");
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public ActionResult EditModel(int modelId)
    {
        var model = ModelRepoADO.GetById(modelId);
        return View(model);
    }

    [HttpGet]
    public ActionResult DeleteModel(int modelId)
    {
        ModelDeleteViewModel ModelToDelete = new ModelDeleteViewModel();
        ModelToDelete.Model = ModelRepoADO.GetById(modelId);
        ModelToDelete.BikesWithModel = ModelRepoADO.CheckIfModelIsUsed(ModelToDelete.Model);
        //TODO: 1-The line above deletes the model before the web page appears confirming the model should be deleted.


        if (ModelToDelete.BikesWithModel.Count() > 0)
        {
            //Warn user that the model cannot be deleted since some bikes use the model.
            //This prevents an SQL err when trying to delete a FK in the bike table.
            ModelToDelete.message = "The model " + ModelToDelete.Model.BikeModelName + " is used by " +
                                    ModelToDelete.BikesWithModel.Count() + " bike(s), so it cannot be deleted.";
        }

        //BikeModelTable Model = ModelRepoADO.GetById(id);
        return View(ModelToDelete);
    }

    [HttpPost]
    public ActionResult DeleteModel(BikeModelTable model)
    {
        ModelRepoADO.Delete(model.BikeModelId);
        return RedirectToAction("MngModels");

    }
}
}