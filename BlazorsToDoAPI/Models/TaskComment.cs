using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace BlazorsToDoAPI.Models
{
    public class TaskComment
    {
        [JsonPropertyName("_id")] public Guid Id { get; set; }
        [JsonPropertyName("taskId")] public Guid TaskId { get; set; }
        [JsonPropertyName("commentId")] public int CommentId { get; set; }
        [JsonPropertyName("commentText")] public string CommentText { get; set; }
        [JsonPropertyName("commentDate")] public DateTime CommentDate { get; set; }
    }
}
