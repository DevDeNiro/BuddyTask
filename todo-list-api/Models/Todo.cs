using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models
{
    public class TodoItemModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string? Name { get; set; }

        [BsonElement("DueDate")]
        public DateTime DueDate { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("Completed")]
        public bool Completed { get; set; }

        [BsonElement("categoryId")]
        public string? CategoryId { get; set; }

        [BsonIgnore]
        public CategoryItemModel? Category { get; set; }

    }

}