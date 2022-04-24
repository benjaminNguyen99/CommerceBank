
using System.Collections.Generic;
using CommerceBank.Controllers;
using CommerceBank.Models;


namespace CommerceBank.Interfaces
{
    public interface INotification // repository pattern
    {
        List<Notifica> GetItems(string SortProperty, SortTransaction sortTransaction, string Search);
        Notifica GetNotification(int id);

        Notifica Create(Notifica Trans);

        Notifica Edit(Notifica Trans);

        Notifica Delete(Notifica Trans); // step 1



    }
}
