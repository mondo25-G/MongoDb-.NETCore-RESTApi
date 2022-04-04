using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace StudentManagement.Models
{
    //What to do if the JSON document in MongoDB contains more fields than the properties in the corresponding C# class? 
    //We can use [BsonIgnoreExtraElements] attribute and instruct the serializer to ignore the extra elements.
    [BsonIgnoreExtraElements]
    public class Student
    {
        // attribute specifies that this is the Id field or property. 
        //Here, the property Id maps to _id field in Mongo document.
        [BsonId]
        //automatically converts Mongo data type to a .Net data type and vice-versa
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        //[BsonElement] attribute specifies the field in the Mongo document the decorated property corresponds to.
        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;

        [BsonElement("graduated")]
        public bool IsGraduated { get; set; }

        [BsonElement("courses")]
        public string[]? Courses { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; } = String.Empty;

        [BsonElement("age")]
        public int Age { get; set; }
    }
}
