using FunctionAzure.Models;
using FunctionAzure.Repository.Interface;
using FunctionAzure.Service.Implimentation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class ToDoCreateServiceUnitTest
    {

        private readonly Mock<IToDoItemRepository> _repositoryMock;
        private readonly ToDoService _toDoService;

        public ToDoCreateServiceUnitTest()
        {
            _repositoryMock = new Mock<IToDoItemRepository>();
            _toDoService = new ToDoService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_Should_Call_Repository_AddAsync()
        {
            // Arrange
            var todoItem = new ToDoItem { Id = 1, Name = "Test ToDo", IsComplete = false };

            // Act
            await _toDoService.AddAsync(todoItem);

            // Assert
            _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<ToDoItem>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_Should_Handle_Exception_From_Repository()
        {
            // Arrange
            var todoItem = new ToDoItem { Id = 1, Name = "Test ToDo", IsComplete = false };

            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<ToDoItem>())).ThrowsAsync(new Exception("Invalid argument"));

            try
            {
                // Act
                await _toDoService.AddAsync(todoItem);

                // Assert
                Assert.True(false, "Expected exception was not thrown");
            }
            catch (Exception ex)
            {
                Assert.Equal("Invalid argument", ex.Message);
            }

            // Verify that repository method was called once
            _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<ToDoItem>()), Times.Once);
        }
    }
}

