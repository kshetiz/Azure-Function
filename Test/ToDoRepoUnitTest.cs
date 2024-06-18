using FunctionAzure;
using FunctionAzure.Models;
using FunctionAzure.Repository.Implimentation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class ToDoRepoUnitTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public ToDoRepoUnitTest()
        {
            // Set up in-memory DbContext options
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task AddAsync_Should_Add_Item_To_Database()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new ToDoItemRepository(context);
                var todoItem = new ToDoItem { Id = 1, Name = "Test ToDo", IsComplete = false };

                // Act
                await repository.AddAsync(todoItem);
            }

            // Assert
            using (var context = new ApplicationDbContext(_options))
            {
                var addedItem = await context.ToDoItems.FindAsync(1);
                Assert.NotNull(addedItem);
                Assert.Equal("Test ToDo", addedItem.Name);
                Assert.False(addedItem.IsComplete);
            }
        }
    }
}

