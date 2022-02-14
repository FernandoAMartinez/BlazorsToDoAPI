using BlazorsToDoAPI.Models;
using BlazorsToDoAPI.Extensions;
using BlazorsToDoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BlazorsToDoAPI.EndpointDefinitions;
public class TaskEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        #region MongoDB GET Methods
        #region Get List of Taks
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async Task<IEnumerable<TaskResponse>> GetAllTasks(ITaskRepository repository) =>
        await repository.GetAll();

        app.MapGet("api/tasks", GetAllTasks).
        Produces<IEnumerable<TaskResponse>>(StatusCodes.Status200OK)
        .WithName("Get List of Tasks")
        .WithTags("Tasks Management");

        //app.MapGet("api/tasks", async (ITaskRepository repository) =>
        //await repository.GetAll()).
        //Produces<IEnumerable<TaskResponse>>(StatusCodes.Status200OK)
        //.WithName("Get List of Tasks")
        //.WithTags("Tasks Management");
        #endregion

        #region Get Task By Guid
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async Task<TaskResponse> GetTaskByGuid(Guid guid, ITaskRepository repository) =>
        await repository.GetById(guid);

        app.MapGet("api/tasks/{guid}", GetTaskByGuid)
            .Produces<TaskResponse>(StatusCodes.Status200OK)
            .WithName("Get Task by Id")
            .WithTags("Tasks Management");

        //app.MapGet("api/tasks/{guid}", async (Guid guid, ITaskRepository repository) =>
        //    await repository.GetById(guid))
        //    .Produces<TaskResponse>(StatusCodes.Status200OK)
        //    .WithName("Get Task by Id")
        //    .WithTags("Tasks Management");
        #endregion

        #region Get List of Tasks by User Guid

        //app.MapGet("api/{guid}/tasks/", [Authorize] async (Guid guid, ITaskRepository repository) =>
        ////app.MapGet("api/{guid}/tasks/", async (Guid guid, ITaskRepository repository) =>
        //    await repository.GetByUserId(guid))
        //    .Produces<IEnumerable<TaskResponse>>(StatusCodes.Status200OK)
        //    .Produces(StatusCodes.Status401Unauthorized)
        //    .WithName("Get List of Tasks by User")
        //    .WithTags("Tasks Management")
        //    .RequireAuthorization();

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async Task<IEnumerable<TaskResponse>> GetTasksByUserGuid(Guid guid, ITaskRepository repository) => 
        await repository.GetByUserId(guid);
        
        app.MapGet("api/{guid}/tasks/", GetTasksByUserGuid)
            .Produces<IEnumerable<TaskResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithName("Get List of Tasks by User")
            .WithTags("Tasks Management")
            .RequireAuthorization();
        #endregion

        #endregion

        #region MongoDB POST Methods
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async Task CreateTask([FromBody]TaskRequest newTask, ITaskRepository repository) =>
        await repository.Create(new TaskResponse()
            {
                Id = new Guid(),
                UserId = newTask.UserId,
                TaskId = newTask.TaskId,
                Title = newTask.Title,
                Description = newTask.Description,
                Tags = newTask.Tags,
                CreationDate = DateTime.Now,
                DueDate = newTask.DueDate,
                ReportedUrgency = newTask.ReportedUrgency,
                Archived = newTask.Archived,
                Completed = newTask.Completed,
                Comments = newTask.Comments
            });

        app.MapPost("/api/tasks", CreateTask)
            .Produces(StatusCodes.Status200OK)
            .WithName("Post Task by content")
            .WithTags("Tasks Management");

        //app.MapPost("/api/tasks", async ([FromBody] TaskRequest newTask, ITaskRepository repository) => 
        //    await repository.Create(new TaskResponse()
        //    {
        //        Id = newTask.Id,
        //        UserId = newTask.UserId,
        //        TaskId = newTask.TaskId,
        //        Title = newTask.Title,
        //        Description = newTask.Description,
        //        Tags = newTask.Tags,
        //        CreationDate = newTask.CreationDate,
        //        DueDate = newTask.DueDate,
        //        ReportedUrgency = newTask.ReportedUrgency,
        //        Archived = newTask.Archived,
        //        Completed = newTask.Completed,
        //        Comments = newTask.Comments
        //    }))
        //    .Produces(StatusCodes.Status200OK)
        //    .WithName("Post Task by content")
        //    .WithTags("Tasks Management");
        #endregion

        #region MongoDB PUT Methods
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async Task UpdateTask([FromBody] TaskResponse updatedTask, ITaskRepository repository) =>
        await repository.Update(updatedTask);

        app.MapPut("/api/tasks", UpdateTask)
            .Produces(StatusCodes.Status200OK)
            .WithName("Update Task by content")
            .WithTags("Tasks Management");

        //app.MapPut("/api/tasks", async ([FromBody] TaskResponse updatedTask, ITaskRepository repository) =>
        //     await repository.Update(updatedTask))
        //    .Produces(StatusCodes.Status200OK)
        //    .WithName("Update Task by content")
        //    .WithTags("Tasks Management");
        #endregion

        #region MongoDB DELETE Methods
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async Task DeleteTask(Guid guid, ITaskRepository repository) =>
        await repository.Delete(guid);

        app.MapDelete("/api/tasks/{guid}", DeleteTask)
            .Produces(StatusCodes.Status200OK)
            .WithName("Delete Task by content")
            .WithTags("Tasks Management"); 
        //app.MapDelete("/api/tasks/{guid}",
         //async (Guid guid, ITaskRepository repository) =>
         //await repository.Delete(guid))
         //.Produces(StatusCodes.Status200OK)
         //.WithName("Delete Task by content")
         //.WithTags("Tasks Management");
        #endregion
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
    }
}