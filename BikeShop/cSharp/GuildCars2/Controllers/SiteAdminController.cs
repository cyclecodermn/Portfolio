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
    public class SiteAdminController: Controller
    {
        public ActionResult Index()
        {
            string model = AuthorizeUtilities.GetUserName(this);
            if (model.IsNullOrWhiteSpace())
                model = "No Name";

            return View();

        }

        public ActionResult MngFrames()
        {

            FramesListViewModel model = new FramesListViewModel();
            
            var FrameRepo = FrameRepoFactory.GetRepo();
            model.BikeFrames = FrameRepo.GetAll();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult EditFrame(BikeFrameTable frame)
        {

            if (string.IsNullOrEmpty(frame.BikeFrame))
            {
                ModelState.AddModelError("MajorId",
                    "Please enter the name of the frame.");
            }
            if (ModelState.IsValid)
            {
                FrameRepoADO.Edit(frame);
                //MajorRepository.Edit(major);
                return RedirectToAction("MngFrames");
            }
            else
            {
                return View(frame);
            }
        }

        [HttpGet]
        public ActionResult EditFrame(int id)
        {
            var frame = FrameRepoADO.GetById(id);
            return View(frame);
        }


    }
}