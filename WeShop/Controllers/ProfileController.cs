using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using WeShop.Models;
using WeShop.ViewModels;

namespace WeShop.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var userId = User.Identity.GetUserId();
            var userProfile = _context.Profiles
                .Include("User")
                .Include("Addresses")
                .SingleOrDefault(u => u.User.Id == userId);

            if (userProfile == null)
                return View("ProfileForm", new ProfileFormViewModel());

            if (userProfile.Addresses != null && userProfile.Addresses.Count > 0)
            {
                var address = userProfile.Addresses.ToList()[0];
                var profileViewModel = new ProfileFormViewModel(userProfile, address);

                return View("ProfileForm", profileViewModel);
            }

            return View("ProfileForm", new ProfileFormViewModel(userProfile, new Address()));
        }

        public ActionResult Save(ProfileFormViewModel profileFormViewModel)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.Include("Profile").Single(u => u.Id == userId);

            // Check if user already has a profile and implicitly an address


            // if not create one
            var profile = user.Profile;
            if (profile == null)
            {
                var newProfile = new Profile()
                {
                    User = user,
                    FirstName = profileFormViewModel.FirstName,
                    LastName = profileFormViewModel.LastName,
                    Birthdate = profileFormViewModel.Birthdate.Value,
                    Phone = profileFormViewModel.Phone
                };

                _context.Profiles.Add(newProfile);
                _context.SaveChanges();

                var newAddress = new Address()
                {
                    Profile = newProfile,
                    County = profileFormViewModel.County,
                    City = profileFormViewModel.City,
                    StreetName = profileFormViewModel.StreetName,
                    Building = profileFormViewModel.Building,
                    Staircase = profileFormViewModel.Staircase,
                    ApartmentNr = profileFormViewModel.ApartmentNr
                };
                //newProfile.Address = newAddress;

                _context.Addresses.Add(newAddress);


                try
                {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges
                    _context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine(
                            "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }

                    throw;
                }
            }
            else
            {
                //if yes update them

                var profileInDb = _context.Profiles.Include("Addresses").Single(pd => pd.Id == user.Profile.Id);
                profileInDb.FirstName = profileFormViewModel.FirstName;
                profileInDb.LastName = profileFormViewModel.LastName;
                profileInDb.Birthdate = profileFormViewModel.Birthdate.Value;
                profileInDb.Phone = profileFormViewModel.Phone;

                var addressInDb = profileInDb.Addresses.ToList()[0];
                addressInDb.County = profileFormViewModel.County;
                addressInDb.City = profileFormViewModel.City;
                addressInDb.StreetName = profileFormViewModel.StreetName;
                addressInDb.StreetNumber = profileFormViewModel.StreetNumber;

                if (!string.IsNullOrWhiteSpace(profileFormViewModel.Building))
                    addressInDb.Building = profileFormViewModel.Building;

                if (!string.IsNullOrWhiteSpace(profileFormViewModel.Staircase))
                    addressInDb.Staircase = profileFormViewModel.Staircase;

                if (profileFormViewModel.ApartmentNr.HasValue)
                    addressInDb.ApartmentNr = profileFormViewModel.ApartmentNr;

                _context.SaveChanges();
            }


            //var address = new Address();

            //profile.User = user;
            //profile.Address = address;

            //ModelState.Clear();
            //TryValidateModel(profile);

            //if (ModelState.IsValid)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ProfileNotFound()
        {
            return View();
        }

        public ActionResult Create(ProfileFormViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = _context.Users.SingleOrDefault(u => u.Id == userId);


                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Edit");
        }
    }
}