using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BlazorsToDoAPI.Models;
public class TaskResponse
{
    public TaskResponse() { Id = new Guid(); }
    [JsonPropertyName("_id")] public Guid Id { get; set; }
    [JsonPropertyName("taskId")] public int TaskId { get; set; }
    [JsonPropertyName("userId")] public Guid UserId { get; set; }
    [JsonPropertyName("title")] [Required] public string Title { get; set; }
    [JsonPropertyName("description")] [Required] public string Description { get; set; }
    [JsonPropertyName("creationDate")] public DateTime CreationDate { get; set; }
    [JsonPropertyName("dueDate")] [Required] public DateTime DueDate { get; set; } 
    [JsonPropertyName("urgency")] public Urgency ReportedUrgency { get; set; }
    [JsonPropertyName("tags")] public string[] Tags { get; set; }
    [JsonPropertyName("isCompleted")] public bool Completed { get; set; }
    [JsonPropertyName("isArchived")] public bool Archived { get; set; }
    //[JsonPropertyName("comments")] public List<TaskComment> Comments { get; set; }
    [JsonPropertyName("comments")] public List<Guid> Comments { get; set; }
}