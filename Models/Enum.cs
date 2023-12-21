using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public enum AccountType{
    [BsonRepresentation(BsonType.String)]
    Admin,
    [BsonRepresentation(BsonType.String)]
    Student,
    [BsonRepresentation(BsonType.String)]
    Instructor,
    [BsonRepresentation(BsonType.String)]
    Company
}

public enum GenderType{
    [BsonRepresentation(BsonType.String)]
    Female,
    [BsonRepresentation(BsonType.String)]
    Male
}

public enum Industry{
    [BsonRepresentation(BsonType.String)]
    SoftwareDevelopment
}

public enum JobType{
    [BsonRepresentation(BsonType.String)]
    Internship,
    [BsonRepresentation(BsonType.String)]
    PartTime,
    [BsonRepresentation(BsonType.String)]
    FullTime
}