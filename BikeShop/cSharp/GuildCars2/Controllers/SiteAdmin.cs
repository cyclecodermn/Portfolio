using bikes.data.Interfaces.Factories;
using bikes.models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using bikes.data.ADO;
using bikes.data.Interfaces;
using bikes.models.Queries;
using bikes.models.Tables;

namespace GuildBikes.Controllers
{
    public class SiteAdmin: Controller
    {
        public ActionResult MngFrames()
        {

            FrameAddViewModel model = new FrameAddViewModel();

            var FrameRepo = FrameRepoFactory.GetRepo();
            model.BikeFrames = new SelectList(FrameRepo.GetAll(), "BikeFrameId", "BikeFrame");
            
            return View(model);
        }


    }
}