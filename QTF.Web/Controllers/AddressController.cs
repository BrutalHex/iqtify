using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QTF.Dtos.UserBundle;
using QTF.ServiceContract;

namespace QTF.Web.Controllers
{

    [AllowAnonymous]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly IUserService _userService;
        public AddressController(IAddressService addressService, IUserService userService)
        {
            _userService = userService;
            _addressService = addressService;
        }
        // GET: Address
        public ActionResult Index()
        {

            return View(_addressService.GetAll());
        }



        // GET: Address/Create
        public ActionResult Create()
        {
            SetupViewBag();
            return View(new AddressDto());
        }

        private void SetupViewBag()
        {
            var users = _userService.GetAll().ToList().Select(a => new SelectListItem()
            {
                Text = a.UserName,
                Value = a.Id

            }).ToList();



            ViewBag.Users = users;
        }

        // POST: Address/Create
        [HttpPost]
        public ActionResult Create([Bind("PostalCode","Path", "UserKey", "Key")]AddressDto item)
        {
            if(item.Key != 0)
            {
                return Edit(item.Key, item);
            }
            var result = _addressService.Add(item);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                SetupViewBag();
                return View(item);
            }

        }

        // GET: Address/Edit/5
        public ActionResult Edit(int id)
        {
            SetupViewBag();
            return View("create", _addressService.Get(id));
        }

        // POST: Address/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, [FromBody]AddressDto item)
        {

            var result = _addressService.Edit(item);
            if (result.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                SetupViewBag();
                ModelState.AddModelError("", result.Message);

                return View("create", item);
            }

        }

        // GET: Address/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_addressService.Get(id));
        }

        // POST: Address/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind("PostalCode", "Path", "UserKey", "Key")]AddressDto item)
        {
            _addressService.Remove(item);

            return RedirectToAction(nameof(Index));

        }
    }
}