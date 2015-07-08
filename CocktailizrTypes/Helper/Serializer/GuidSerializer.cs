using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CocktailizrTypes.Helper.Serializer
{
    public class GuidSerializer : SerializerBase<Guid> 
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Guid value)
        {
            context.Writer.WriteString(value.ToString());
        }

        public override Guid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new Guid(context.Reader.ReadString());
        }
    }
}