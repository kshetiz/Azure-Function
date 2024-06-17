using FunctionAzure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAzure.Repository.Interface
{
    public interface IToDoItemRepository
    {
        Task AddAsync(ToDoItem item);

    }
}
