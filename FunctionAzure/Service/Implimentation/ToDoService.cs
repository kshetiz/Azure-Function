using FunctionAzure.Models;
using FunctionAzure.Repository.Interface;
using FunctionAzure.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grpc.Core.Metadata;

namespace FunctionAzure.Service.Implimentation
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoItemRepository _repository;

        public ToDoService(IToDoItemRepository repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(ToDoItem item)
        {
            var entity = new ToDoItem
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            };

            await _repository.AddAsync(entity);
        }
    }
}
