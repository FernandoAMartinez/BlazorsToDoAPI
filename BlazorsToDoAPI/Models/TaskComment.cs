using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace BlazorsToDoAPI.Models;
//public class TaskComment
//{
//    public TaskComment()
//    {
//        Id = Guid.NewGuid();
//        CommentDate = DateTime.Now;
//    }
//    [JsonPropertyName("_id")] public Guid Id { get; set; }
//    [JsonPropertyName("taskId")] public Guid TaskId { get; set; }
//    [JsonPropertyName("commentId")] public int CommentId { get; set; }
//    [JsonPropertyName("commentText")] public string CommentText { get; set; }
//    [JsonPropertyName("commentDate")] public DateTime CommentDate { get; set; }
//}

public class CommentRequest
{
    public CommentRequest()
    {
        Id = Guid.NewGuid();
        CommentDate = DateTime.Now;
    }
    [JsonIgnore][JsonPropertyName("_id")] public Guid Id { get; set; }
    [JsonPropertyName("taskId")] public Guid TaskId { get; set; }
    [JsonPropertyName("commentId")] public int CommentId { get; set; }
    [JsonPropertyName("commentText")] public string CommentText { get; set; }
    [JsonPropertyName("commentDate")] public DateTime CommentDate { get; set; }
}

public class CommentResponse
{
    public CommentResponse()
    {
    }
    [JsonPropertyName("_id")] public Guid Id { get; set; }
    [JsonPropertyName("taskId")] public Guid TaskId { get; set; }
    [JsonPropertyName("commentId")] public int CommentId { get; set; }
    [JsonPropertyName("commentText")] public string CommentText { get; set; }
    [JsonPropertyName("commentDate")] public DateTime CommentDate { get; set; }
}
