using BlazorsToDoAPI.Models;
using BlazorsToDoAPI.Extensions;
using BlazorsToDoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BlazorsToDoAPI.EndpointDefinitions
{
    public class TaskEndpointDefinition : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            #region MongoDB GET Methods
            app.MapGet("api/tasks", 
            async (ITaskRepository repository) =>
            await repository.GetAll()).
            Produces<IEnumerable<TaskModel>>(StatusCodes.Status200OK)
            .WithName("Get List of Tasks")
            .WithTags("Tasks Management");

            app.MapGet("api/tasks/{guid}",
                async (Guid guid, ITaskRepository repository) =>
                await repository.GetById(guid))
                .Produces<TaskModel>(StatusCodes.Status200OK)
                .WithName("Get Task by Id")
                .WithTags("Tasks Management");

            app.MapGet("api/{guid}/tasks/",
                [Authorize] async (Guid guid, ITaskRepository repository) =>
                await repository.GetByUserId(guid))
                .Produces<IEnumerable<TaskModel>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("Get List of Tasks by User")
                .WithTags("Tasks Management")
                .RequireAuthorization();
            #endregion

            #region MongoDB POST Methods
            app.MapPost("/api/tasks",
                async ([FromBody] TaskRequest newTask,
                ITaskRepository repository) => await repository.Create(new TaskModel()
                {
                    Id = newTask.Id,
                    UserId = newTask.UserId,
                    TaskId = newTask.TaskId,
                    Title = newTask.Title,
                    Description = newTask.Description,
                    Tags = newTask.Tags,
                    CreationDate = newTask.CreationDate,
                    DueDate = newTask.DueDate,
                    ReportedUrgency = newTask.ReportedUrgency,
                    Archived = newTask.Archived,
                    Completed = newTask.Completed,
                    Comments = newTask.Comments,
                }))
                .Produces(StatusCodes.Status200OK)
                .WithName("Post Task by content")
                .WithTags("Tasks Management");

            #endregion

            #region MongoDB PUT Methods
            app.MapPut("/api/tasks",
                async ([FromBody] TaskModel updatedTask,
                ITaskRepository repository) => 
                await repository.Update(updatedTask))
                .Produces(StatusCodes.Status200OK)
                .WithName("Update Task by content")
                .WithTags("Tasks Management");

            #endregion

            #region MongoDB DELETE Methods
            app.MapDelete("/api/tasks/{guid}",
                async (Guid guid, ITaskRepository repository) => 
                await repository.Delete(guid))
                .Produces(StatusCodes.Status200OK)
                .WithName("Delete Task by content")
                .WithTags("Tasks Management");
            #endregion
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }
}
