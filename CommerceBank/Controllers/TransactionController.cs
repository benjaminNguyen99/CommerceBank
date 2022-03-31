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

namespace CommerceBank.Controllers
{
    
    [Authorize]
    public class TransactionController : Controller
    {
        float total = 0;
        private  TransactionContext _Context;
        private readonly ITransaction _TransactionRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransactionController(ITransaction transrepo, UserManager<ApplicationUser> userManager, TransactionContext Context)
        {
            _Context = Context;
            _userManager = userManager;
            _TransactionRepo = transrepo; //direct to the constructor in the file "TransactionRepositories" (Benjamin Nguyen)
        }

        

        public IActionResult Index(string sortExpression= "")
        {

            SortModels sortModel = new SortModels();
            sortModel.AddColumn("location");
            sortModel.AddColumn("balancetype");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"]=sortModel;

            List<Transaction> Trans = _TransactionRepo.GetItems(sortModel.SortedProperty, sortModel.SortedTransaction); 
            return View(Trans);
         }
        private List<StatesList> GetState()
        {
            var state = new List<StatesList>();
            state.Add(new StatesList() { id = 1, State = "MO" });

            return state;
        }




        //---------------------------- CREATE NEW TRANSACTION PART ----------------------------//
        [HttpGet]
        public IActionResult Create()
        {
            Transaction Trans = new Transaction();
            return View(Trans);
        }

        [HttpPost]
        public IActionResult Create(Transaction Trans)
        {
            int count = 0;
            ViewBag.StateSelectList = new SelectList(GetState(), "id", "state");
            try
            {
                string userId = _userManager.GetUserId(User);
                foreach (var item in _Context.Transac)
                {
                    if (item.UserKey == _userManager.GetUserId(HttpContext.User))
                    {
                        count+=1;
                    }
                }
                if (count == 0 || _userManager.Users.FirstOrDefault(u => u.Id == userId).TotalBalance == 0)
                {
                    Trans.UserKey = _userManager.GetUserId(HttpContext.User);
                    Trans.Balance = _userManager.Users.FirstOrDefault(u => u.Id == userId).TotalBalance + Trans.Amount;
                    _userManager.Users.FirstOrDefault(u => u.Id == userId).TotalBalance = Trans.Balance;


                    Trans.BalanceType = "Deposit";
           
     
                }
            

                else
                {
                    if (Trans.BalanceType == "Deposit")
                    {
                        if (Trans.Amount == 0)
                        {
                            
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            Trans.Balance = _userManager.Users.FirstOrDefault(u => u.Id == userId).TotalBalance + Trans.Amount;
                        }    
                        
                    }
                    else if (Trans.BalanceType == "Withdraw")
                    {
                        if ( _userManager.Users.FirstOrDefault(u => u.Id == userId).TotalBalance - Trans.Amount <0 || Trans.Amount == 0)
                        {
                            ViewBag.ErrorMessage = "not enough";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            Trans.Balance = _userManager.Users.FirstOrDefault(u => u.Id == userId).TotalBalance - Trans.Amount;
                        }
                        
                    }
                    //List<Transaction> Transs = _Context.Transac.ToList();
                    //Transs = _Context.Transac.OrderBy(n => n.UserKey).ToList();
                    Trans.UserKey = _userManager.GetUserId(HttpContext.User);
                    //Trans.Balance = Transs.LastOrDefault(d => d.UserKey == userId).Balance - Trans.Amount;
                    
                    _userManager.Users.FirstOrDefault(u => u.Id == userId).TotalBalance = Trans.Balance;
                }
                
                
                _TransactionRepo.Create(Trans); //direct to the method "create" in the file "TransactionRepositories" (Benjamin Nguyen)
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }


        //---------------------------- TRANSACTION DETAIL PART ----------------------------//
        public IActionResult Details(int id)
        {
            Transaction Trans = _TransactionRepo.GetTransaction(id);
            return View(Trans);

        }

        


        //---------------------------- EDIT TRANSACTION PART ----------------------------//
        public IActionResult Edit(int id)
        {
            Transaction Trans = _TransactionRepo.GetTransaction(id);
            return View(Trans);

        }
        [HttpPost]
        public IActionResult Edit(Transaction Trans)
        {
            try
            {
                _TransactionRepo.Edit(Trans);
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }


        //---------------------------- DELETE TRANSACTION PART ----------------------------//
        public IActionResult Delete(int id)
        {
            Transaction Trans = _TransactionRepo.GetTransaction(id);
            return View(Trans);

        }
        [HttpPost]
        public IActionResult Delete(Transaction Trans)
        {
            try
            {
                //_Context.Transac.Attach(Trans);
                //_Context.Entry(Trans).State = EntityState.Deleted; // tell the EFCore that this entry needs to be updated
                //_Context.SaveChanges();
                Trans = _TransactionRepo.Delete(Trans); //Step 4
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }

    }
}
