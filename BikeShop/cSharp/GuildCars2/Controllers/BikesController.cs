using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bikes.data.ADO;
using bikes.data.Interfaces;
using bikes.data.Interfaces.Factories;
using bikes.models.Queries;
using bikes.models.Tables;
using bikes.models.VMs;
using GuildBikes.Utilities;

//The bike make refers to the manufacturer or company that produced the bike,
//while the bike model refers to the bike product itself and its
//registered identification name.
//Bike models are identifiable and comprised of names, initials, or numbers.

namespace GuildBikes.Controllers
{
    public class BikesController : Controller
    {
        // GET: Bikes
        public ActionResult Details(int id)
        {
            var repo = BikeRepoFactory.GetRepo();
            var model = repo.GetBikeDetails(id);

            return View(model);
        }

        public ActionResult Add()
        {
            BikeAddViewModel model = new BikeAddViewModel();

            model = ModelUtilities.InitBikeModel(model);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var BikeRepo = BikeRepoFactory.GetRepo();

            BikeEditViewModel model = new BikeEditViewModel();

            model = ModelUtilities.InitBikeModel(model);
            model.Bike = BikeRepo.GetById(id);


            ////TODO: When adding users, implement line below
            //if (model.Bike.UserId != AuthorizeUtilities.GetUserId(this))
            //{
            //    throw new Exception("Attempt to edit a listing you do not own! Naughty!");
            //}

            return View(model);
        }



        //[Authorize]
        [HttpPost]
        public ActionResult Add(BikeAddViewModel model)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                var repo = BikeRepoFactory.GetRepo();

                try
                {
                    //model.Bike.UserId = AuthorizeUtilities.GetUserId(this);

                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        var filePath = Path.Combine(savepath, fileName + extension);

                        int counter = 1;
                        while (System.IO.File.Exists(filePath))
                        {
                            filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                            counter++;
                        }

                        model.ImageUpload.SaveAs(filePath);
                        model.Bike.BikePictName = Path.GetFileName(filePath);
                    }

                    repo.Insert(model.Bike);

                    //return RedirectToAction("Edit", new { id = model.Listing.ListingId });

                    //model = ModelUtilities.InitBikeModel(model);
                    //Inserted the line above to fix exception error after sucessfully adding
                    //a new bike.
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {

                model = ModelUtilities.InitBikeModel(model);

                //var statesRepo = StatesRepositoryFactory.GetRepository();
                //var bathroomRepo = BathroomTypesRepositoryFactory.GetRepository();

                //model.States = new SelectList(statesRepo.GetAll(), "StateId", "StateId");
                //model.BathroomTypes = new SelectList(bathroomRepo.GetAll(), "BathroomTypeId", "BathroomTypeName");

                return View(model);
            }
            model = ModelUtilities.InitBikeModel(model);
            // I added the line above as an intermedite step to fix an err:
            // Remove the line in future revisions
            return View(model);
            //// I added the line above as an intermedite step to fix the err:
            /// Not all paths return something.
            /// >>>---> Remove the line above soon
        }

//        [Authorize]
        [HttpPost]
        public ActionResult Edit(BikeEditViewModel model)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                var repo = BikeRepoFactory.GetRepo();

                try
                {
                    model.Bike.UserId = AuthorizeUtilities.GetUserId(this);
                    var oldListing = repo.GetById(model.Bike.BikeId);

                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        var filePath = Path.Combine(savepath, fileName + extension);

                        //Add 1 to files with the same name, or 2, 3, and so on.
                        int counter = 1;
                        while (System.IO.File.Exists(filePath))
                        {
                            filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                            counter++;
                        }

                        model.ImageUpload.SaveAs(filePath);
                        model.Bike.BikePictName= Path.GetFileName(filePath);

                        // delete old file
                        var oldPath = Path.Combine(savepath, oldListing.BikePictName);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    else
                    {
                        // they did not replace the old file, so keep the old file name
                        model.Bike.BikePictName = oldListing.BikePictName;
                    }

                    repo.Update(model.Bike);

                    return RedirectToAction("Edit", new { id = model.Bike.BikeId});
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                BikeTable crntBike = new BikeTable();
                crntBike = model.Bike;
                //Set model back to its initial state

                model = ModelUtilities.InitBikeModel(model);

                model.Bike = crntBike;
                return View(model);
            }
        }

    }
}