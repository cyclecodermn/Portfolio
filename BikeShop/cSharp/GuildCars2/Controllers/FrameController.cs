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
    public class FrameController : Controller
    {
        //public ActionResult Index()
        //{
        //    string model = AuthorizeUtilities.GetUserName(this);
        //    if (model.IsNullOrWhiteSpace())
        //        model = "No Name";

        //    return View();

        //}

        public ActionResult MngFrames()
        {

            FramesListViewModel model = new FramesListViewModel();

            var FrameRepo = FrameRepoFactory.GetRepo();
            model.BikeFrames = FrameRepo.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult AddFrame()
        {
            BikeFrameTable frame = new BikeFrameTable();
            return View(frame);
        }

        [HttpPost]
        public ActionResult AddFrame(BikeFrameTable frame)
        {

            if (string.IsNullOrEmpty(frame.BikeFrame))
            {
                ModelState.AddModelError("FrameId",
                    "Please enter the name of the frame.");
            }
            if (ModelState.IsValid)
            {
                FrameRepoADO repo = new FrameRepoADO();
                repo.Insert(frame);
                return RedirectToAction("MngFrames");
            }
            else
            {
                return View(frame);
            }
        }

        [HttpPost]
        public ActionResult EditFrame(BikeFrameTable frame)
        {
            if (string.IsNullOrEmpty(frame.BikeFrame))
            {
                ModelState.AddModelError("FrameId",
                    "Please enter the name of the frame.");
            }
            if (ModelState.IsValid)
            {
                FrameRepoADO repo = new FrameRepoADO();
                repo.Edit(frame);
                return RedirectToAction("MngFrames");
            }
            else
            {
                return View(frame);
            }
        }

        [HttpGet]
        public ActionResult EditFrame(int frameId)
        {
            var frame = FrameRepoADO.GetById(frameId);
            return View(frame);
        }

        [HttpGet]
        public ActionResult DeleteFrame(int frameId)
        {
            FrameDeleteViewModel FrameToDelete = new FrameDeleteViewModel();
            FrameToDelete.Frame = FrameRepoADO.GetById(frameId);
            FrameToDelete.BikesWithFrame = FrameRepoADO.CheckIfFrameIsUsed(FrameToDelete.Frame);
            //TODO: 1-The line above deletes the frame before the web page appears confirming the frame should be deleted.


            if (FrameToDelete.BikesWithFrame.Count() > 0)
            {
                //Warn user that the frame cannot be deleted since some bikes use the frame.
                //This prevents an SQL err when trying to delete a FK in the bike table.
                FrameToDelete.message = "The frame " + FrameToDelete.Frame.BikeFrame + " is used by " +
                                        FrameToDelete.BikesWithFrame.Count() + " bike(s), so it cannot be deleted.";
            }

            //BikeFrameTable Frame = FrameRepoADO.GetById(id);
            return View(FrameToDelete);
        }

        [HttpPost]
        public ActionResult DeleteFrame(BikeFrameTable frame)
        {
            FrameRepoADO.Delete(frame.BikeFrameId);
            return RedirectToAction("MngFrames");

        }
    }
}