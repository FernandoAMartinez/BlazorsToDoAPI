using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BlazorsToDoAPI.Models
{
    //3eb1e606-24ab-49cf-b484-4f4c00c49d9e
    public class TaskRequest
    {
        public TaskRequest() 
        { 
            Id = new Guid();
            CreationDate = DateTime.Now;
        }
        //[JsonPropertyName("_id")] public ObjectId Id { get; set; }
        [JsonIgnore][JsonPropertyName("_id")] public Guid Id { get; set; }
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
}