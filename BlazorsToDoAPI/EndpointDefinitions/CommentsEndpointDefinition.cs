using BlazorsToDoAPI.Extensions;
using BlazorsToDoAPI.Models;
using BlazorsToDoAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorsToDoAPI.EndpointDefinitions;
public class CommentsEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        #region MongoDB GET Methods
        #region Get List of Comments
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async Task<IEnumerable<CommentResponse>> GetAllComments(ICommentRepository repository) =>
        await repository.GetAll();

        app.MapGet("api/tasks/comments", GetAllComments).
        Produces<IEnumerable<CommentResponse>>(StatusCodes.Status200OK)
        .WithName("Get List of Comments")
        .WithTags("Comments Management");

        #endregion

        #region Get Comments By Guid
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async Task<CommentResponse> GetCommentsByGuid(Guid guid, ICommentRepository repository) =>
            await repository.GetById(guid);

        app.MapGet("api/tasks/comments/{guid}", GetCommentsByGuid)
            .Produces<CommentResponse>(StatusCodes.Status200OK)
            .WithName("Get Comments by Id")
            .WithTags("Comments Management");

        #endregion

        #region Get List of Comments by Task Guid
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async Task<IEnumerable<CommentResponse>> GetCommentsByTaskGuid(Guid guid, ICommentRepository repository) =>
            await repository.GetByTaskId(guid);

        app.MapGet("api/tasks/{guid}/comments", GetCommentsByTaskGuid)
            .Produces<IEnumerable<CommentResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithName("Get List of Comments by Task")
            .WithTags("Comments Management")
            .RequireAuthorization();
        #endregion

        #endregion

        #region MongoDB POST Methods
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async Task CreateComment([FromBody] CommentRequest newComment, ICommentRepository repository) =>
        await repository.Create(new CommentResponse()
        {
            Id = new Guid(),
            TaskId = newComment.TaskId,
            CommentId = newComment.CommentId,
            CommentText = newComment.CommentText,
            CommentDate = DateTime.Now
        });

        app.MapPost("/api/tasks/comments", CreateComment)
            .Produces(StatusCodes.Status200OK)
            .WithName("Post Comment by Content")
            .WithTags("Comments Management");
        #endregion

    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<ICommentRepository, CommentRepository>();
    }
}