using Microsoft.AspNetCore.Mvc;
using CommerceBank.Datas;
using CommerceBank.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CommerceBank.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using System;

namespace CommerceBank.Controllers
{

    [Authorize]
    public class NotificationController : Controller
    {
        float total = 0;
        private TransactionContext _Context;
        private readonly INotification _Repo;
        private readonly ITransaction _TransRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationController(ITransaction repoT, INotification repo, UserManager<ApplicationUser> userManager, TransactionContext Context)
        {
            _Context = Context;
            _userManager = userManager;
            _Repo = repo; //direct to the constructor in the file "TransactionRepositories" (Benjamin Nguyen)
            _TransRepo = repoT; 
        }



        public IActionResult Index(string sortExpression = "", string Search = "")
        {

            dynamic dy = new ExpandoObject();
            dy.notilist = GetNoti(sortExpression, Search);
            dy.translist = GetTrans(sortExpression, Search);
            return View(dy);
        }
    
        public List<Transaction> GetTrans(string sortExpression, string Search)
        {
            
            SortModels sortModel = new SortModels();
            sortModel.AddColumn("location");
            sortModel.AddColumn("balancetype");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            List<Transaction> trans = _TransRepo.GetItems(sortModel.SortedProperty, sortModel.SortedTransaction);
            return trans;
        }
        public List<Notifica> GetNoti(string sortExpression, string Search)
        {
            SortModels sortModel = new SortModels();
            sortModel.AddColumn("location");
            sortModel.AddColumn("balancetype");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            List<Notifica> noti = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedTransaction, Search);
            return noti;
        }


        //---------------------------- CREATE NEW TRANSACTION PART ----------------------------//
        [HttpGet]
        public IActionResult Create()
        {
            Notifica noti = new Notifica();
            return View(noti);
        }

        [HttpPost]
        public IActionResult Create(Notifica noti)
        {
            int count = 0;
            
            try
            {
                
                 //List<Transaction> Transs = _Context.Transac.ToList();
                //Transs = _Context.Transac.OrderBy(n => n.UserKey).ToList();
                noti.UserKey = _userManager.GetUserId(HttpContext.User);
                noti.CreatedDate = DateTime.Now;
                //Trans.Balance = Transs.LastOrDefault(d => d.UserKey == userId).Balance - Trans.Amount;





                _Repo.Create(noti); //direct to the method "create" in the file "TransactionRepositories" (Benjamin Nguyen)
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }


        //---------------------------- TRANSACTION DETAIL PART ----------------------------//
        public IActionResult Details(int id)
        {
            Transaction noti = _TransRepo.GetTransaction(id);
            return View(noti);

        }




        //---------------------------- EDIT TRANSACTION PART ----------------------------//
        public IActionResult Edit(int id)
        {
            Notifica noti = _Repo.GetNotification(id);
            return View(noti);

        }
        [HttpPost]
        public IActionResult Edit(Notifica noti)
        {
            try
            {
                _Repo.Edit(noti);
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }


        //---------------------------- DELETE TRANSACTION PART ----------------------------//
        public IActionResult Delete(int id)
        {
            Notifica Trans = _Repo.GetNotification(id);
            return View(Trans);

        }
        [HttpPost]
        public IActionResult Delete(Notifica noti)
        {
            try
            {
                //_Context.Transac.Attach(Trans);
                //_Context.Entry(Trans).State = EntityState.Deleted; // tell the EFCore that this entry needs to be updated
                //_Context.SaveChanges();
                noti = _Repo.Delete(noti); //Step 4
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }

    }
}
