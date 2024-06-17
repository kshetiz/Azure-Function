using FunctionAzure.Models;
using FunctionAzure.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FunctionAzure
{
    public class CreateToDo
    {
        private readonly ILogger<CreateToDo> _logger;
        private readonly IToDoService _service;
        public CreateToDo(ILogger<CreateToDo> logger, IToDoService toDoService)
        {
            _logger = logger;
            _service = toDoService;
        }

        [Function("CreateToDo")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "todo")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                using (StreamReader streamReader = new StreamReader(req.Body))
                {
                    string requestBody = await streamReader.ReadToEndAsync();
                    var todoItem = JsonSerializer.Deserialize<ToDoItem>(requestBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });


                    await _service.AddAsync(todoItem);

                    return new OkObjectResult(todoItem);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return new BadRequestObjectResult("An error occurred while processing the request.");
            }
        }
    }
}
