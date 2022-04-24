
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
    public class NotificationRepositories : INotification
    {
        private readonly TransactionContext _Context;
        private readonly UserManager<ApplicationUser> _userManager;


        public NotificationRepositories(TransactionContext Context)
        {
            _Context = Context;
        }

        public Notifica Create(Notifica Noti)
        {

            _Context.Notification.Add(Noti); //receive transaction and add the transaction to the in-memory transaction collection of dbcontext
            _Context.SaveChanges(); //add to the table
            return Noti;

        }

        public Notifica Delete(Notifica Noti)
        {
            _Context.Notification.Attach(Noti);
            _Context.Entry(Noti).State = EntityState.Deleted; // tell the EFCore that this entry needs to be updated
            _Context.SaveChanges();
            return Noti;
        }

        public Notifica Edit(Notifica Noti)
        {
            _Context.Notification.Attach(Noti);
            _Context.Entry(Noti).State = EntityState.Modified; // tell the EFCore that this entry needs to be updated
            _Context.SaveChanges();
            return Noti;
        }

        // Default implementation for the transaction interface
        public List<Notifica> GetItems(string SortProperty, SortTransaction sortTransaction, string Search)
        {

            List<Notifica> Trans = _Context.Notification.ToList();
            if (SortProperty.ToLower() == "location")    //Sorting by location
            {
                if (sortTransaction == SortTransaction.Ascending)
                {
                    Trans = Trans.OrderBy(n => n.NotificationRule).ToList();

                }
                else
                {
                    Trans = Trans.OrderByDescending(n => n.NotificationRule).ToList();

                }
            }
            else    //Sorting by balance type
            {
                if (sortTransaction == SortTransaction.Ascending)
                {
                    Trans = Trans.OrderBy(d => d.NotificationRule).ToList();
                }
                else
                {
                    Trans = Trans.OrderByDescending(d => d.NotificationRule).ToList();
                }
            }
            if (Search != "" && Search != null)
            {
                Trans = Trans.Where(p => p.NotificationRule.Contains(Search) == false).ToList();
                return Trans;
            }
            else
            {
                return Trans;
            }




        }

        public Notifica GetNotification(int id)
        {

            Notifica noti = _Context.Notification.Where(u => u.Id == id).FirstOrDefault(); //fetch the first transaction,
                                                                                          //if no transaction available, it will create a transaction with default value
            return noti;

        }
    }
}
