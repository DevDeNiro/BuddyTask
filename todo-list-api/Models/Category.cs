using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models
{
    public class CategoryItemModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        // New empty list to prevent null todoItem  
        public List<TodoItemModel> TodoItems { get; set; } = new List<TodoItemModel>();

        // TODO: Only get ID of todoItemModel in the list : Mongo have max 16Mo per document
        // Must be a list of string, and not of TodoItemModel (object)

        // public List<string> TodoItems { get; set; } = new List<string>();
    }
}
