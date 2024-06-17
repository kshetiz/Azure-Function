using FunctionAzure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAzure.Service.Interface
{
    public interface IToDoService
    {
        Task AddAsync(ToDoItem item);

    }
}
