using FunctionAzure;
using FunctionAzure.Models;
using FunctionAzure.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;
using System.Text.Json;

namespace Test
{
    public class ToDoUnitTest
    {
        
        private Mock<ILogger<CreateToDo>> _loggerMock;
        private Mock<FunctionContext> _contextMock;
        private Mock<IToDoService> _toDoServiceMock;

        public  ToDoUnitTest()
        {
            _loggerMock = new Mock<ILogger<CreateToDo>>();
            _toDoServiceMock = new Mock<IToDoService>();
            _contextMock = new Mock<FunctionContext>();
        }



        [Fact]
        public async Task CreateToDo_Returns_OkResult()
        {
            // Arrange
            var function = new CreateToDo(_loggerMock.Object, _toDoServiceMock.Object);
            var todoItem = new ToDoItem { Id = "1", Name = "Test ToDo", IsComplete = false };
            var requestBody = JsonSerializer.Serialize(todoItem);
            var bodyBytes = Encoding.UTF8.GetBytes(requestBody);

            var httpRequestMock = new MockHttpRequestData(bodyBytes, _contextMock.Object);
           

            _toDoServiceMock.Setup(s => s.AddAsync(It.IsAny<ToDoItem>())).Returns(Task.CompletedTask);

            // Act
            var response = await function.RunAsync(httpRequestMock);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);

            var okResult = response as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var returnedTodoItem = okResult.Value as ToDoItem;
            Assert.NotNull(returnedTodoItem);
            Assert.Equal(todoItem.Id, returnedTodoItem.Id);
            Assert.Equal(todoItem.Name, returnedTodoItem.Name);
            Assert.Equal(todoItem.IsComplete, returnedTodoItem.IsComplete);
        }
    }


}
