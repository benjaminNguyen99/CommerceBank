using System.Collections.Generic;
using CommerceBank.Controllers;
using CommerceBank.Models;


namespace CommerceBank.Interfaces
{
    public interface ITransaction // repository pattern
    {
        List<Transaction> GetItems(string SortProperty, SortTransaction sortTransaction);
        Transaction GetTransaction(int id);

        Transaction Create(Transaction Trans);

        Transaction Edit(Transaction Trans);

        Transaction Delete(Transaction Trans); // step 1
       


    }
}
