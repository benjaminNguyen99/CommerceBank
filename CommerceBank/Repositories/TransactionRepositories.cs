using CommerceBank.Controllers;
using CommerceBank.Datas;
using CommerceBank.Interfaces;
using CommerceBank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace CommerceBank.Repositories
{
    public class TransactionRepositories : ITransaction
    {
        private readonly TransactionContext _Context;
       private readonly UserManager<ApplicationUser> _userManager;


        public TransactionRepositories(TransactionContext Context)
        {
            _Context = Context;
        }

        public Transaction Create(Transaction Trans)
        {

            _Context.Transac.Add(Trans); //receive transaction and add the transaction to the in-memory transaction collection of dbcontext
            _Context.SaveChanges(); //add to the table
            return Trans;

        }

        public Transaction Delete(Transaction Trans)
        {
            _Context.Transac.Attach(Trans);
            _Context.Entry(Trans).State = EntityState.Deleted; // tell the EFCore that this entry needs to be updated
            _Context.SaveChanges();
            return Trans;
        }

        public Transaction Edit(Transaction Trans)
        {
            _Context.Transac.Attach(Trans);
            _Context.Entry(Trans).State = EntityState.Modified; // tell the EFCore that this entry needs to be updated
            _Context.SaveChanges();
            return Trans;
        }

        // Default implementation for the transaction interface
        public List<Transaction> GetItems(string SortProperty, SortTransaction sortTransaction)
        {

            List<Transaction> Trans = _Context.Transac.ToList();
            if (SortProperty.ToLower() == "location")    //Sorting by location
            {
                if (sortTransaction == SortTransaction.Ascending)
                {
                    Trans = Trans.OrderBy(n=>n.Id).ToList();
               
                }
                else
                {
                    Trans = Trans.OrderByDescending(n => n.Location).ToList();
            
                }
            }
            else    //Sorting by balance type
            {
                if (sortTransaction == SortTransaction.Ascending)
                {
                    Trans = Trans.OrderBy(d => d.BalanceType).ToList();
                }
                else
                {
                    Trans = Trans.OrderByDescending(d => d.BalanceType).ToList();
                }
            }
            
            

            return Trans;
        }

        public Transaction GetTransaction(int id)
        {
            
                Transaction trans = _Context.Transac.Where(u => u.Id == id).FirstOrDefault(); //fetch the first transaction,
                                                                                              //if no transaction available, it will create a transaction with default value
                return trans;
            
        }
    }
}
