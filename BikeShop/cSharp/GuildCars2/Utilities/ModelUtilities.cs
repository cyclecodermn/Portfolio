using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bikes.data.Interfaces.Factories;
using bikes.data.Interfaces.FactoriesFactories;
using bikes.models.Tables;
using bikes.models.VMs;
using GuildBikes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GuildBikes.Utilities
{
    public class ModelUtilities
    {

        public static BikeAddViewModel InitBikeModel(BikeAddViewModel model)
        {
            var MakeRepo = MakeRepoFactory.GetRepo();
            model.BikeMakes = new SelectList(MakeRepo.GetAll(), "BikeMakeId", "BikeMake");

            var ModelRepo = ModelRepoFactory.GetRepo();
            model.BikeModels = new SelectList(ModelRepo.GetAll(), "BikeModelId", "BikeModel");

            var FrameRepo = FrameRepoFactory.GetRepo();
            model.BikeFrames = new SelectList(FrameRepo.GetAll(), "BikeFrameId", "BikeFrame");

            var ColorRepo = ColorRepoFactory.GetRepo();
            model.BikeColors = new SelectList(ColorRepo.GetAll(), "BikeColorId", "BikeColor");

            model.BikeYearItems = new List<SelectListItem>();
            for (int i = DateTime.Now.Year - 10; i <= DateTime.Now.Year + 1; i++)
                model.BikeYearItems.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });


            //TODO: Delete framecolor items below?
            model.FrameColorItems = new List<SelectListItem>();
            string[] theColors = { "Red", "Blue", "Yellow", "Orange", "Green", "Purple", "Black", "White" };
            for (int i = 1; i < theColors.Length; i++)
            {
                model.FrameColorItems.Add(new SelectListItem() { Value = i.ToString(), Text = theColors[i] });
            }

            model.ConditionItems = new List<SelectListItem>();
            string[] theConditions = { "1: No scratches", "2", "3", "4", "5: 5+ Scratches or nicks", "6", "7", "8", "9", "10: Barely road-worthy" };
            for (int i = 0; i < theConditions.Length; i++)
            {
                model.ConditionItems.Add(new SelectListItem() { Value = i.ToString(), Text = theConditions[i] });
            }


            model.BikeGearItems = new List<SelectListItem>();
            for (int i = 1; i <= 32; i++)
                model.BikeGearItems.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });

            model.Bike = new BikeTable();
            return model;
        }


        public static BikeEditViewModel InitBikeModel(BikeEditViewModel model)
        {
            var MakeRepo = MakeRepoFactory.GetRepo();
            model.BikeMakes = new SelectList(MakeRepo.GetAll(), "BikeMakeId", "BikeMake");

            var ModelRepo = ModelRepoFactory.GetRepo();
            model.BikeModels = new SelectList(ModelRepo.GetAll(), "BikeModelId", "BikeModel");

            var FrameRepo = FrameRepoFactory.GetRepo();
            model.BikeFrames = new SelectList(FrameRepo.GetAll(), "BikeFrameId", "BikeFrame");

            var ColorRepo = ColorRepoFactory.GetRepo();
            model.BikeColors = new SelectList(ColorRepo.GetAll(), "BikeColorId", "BikeColor");

            model.BikeYearItems = new List<SelectListItem>();
            for (int i = DateTime.Now.Year - 10; i <= DateTime.Now.Year + 1; i++)
                model.BikeYearItems.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });


            //TODO: Delete framecolor items below?
            model.FrameColorItems = new List<SelectListItem>();
            string[] theColors = { "Red", "Blue", "Yellow", "Orange", "Green", "Purple", "Black", "White" };
            for (int i = 1; i < theColors.Length; i++)
            {
                model.FrameColorItems.Add(new SelectListItem() { Value = i.ToString(), Text = theColors[i] });
            }

            model.ConditionItems = new List<SelectListItem>();
            string[] theConditions = { "1: No scratches", "2", "3", "4", "5: 5+ Scratches or nicks", "6", "7", "8", "9", "10: Barely road-worthy" };
            for (int i = 0; i < theConditions.Length; i++)
            {
                model.ConditionItems.Add(new SelectListItem() { Value = i.ToString(), Text = theConditions[i] });
            }


            model.BikeGearItems = new List<SelectListItem>();
            for (int i = 1; i <= 32; i++)
                model.BikeGearItems.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });

            model.Bike = new BikeTable();
            return model;

        }
    }
}