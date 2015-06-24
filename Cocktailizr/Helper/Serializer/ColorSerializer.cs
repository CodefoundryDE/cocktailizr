using System.Drawing;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Cocktailizr.Helper.Serializer
{
    class ColorSerializer : SerializerBase<Color>
    {
        public override Color Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Color.FromArgb(context.Reader.ReadInt32());
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Color value)
        {
            context.Writer.WriteInt32(value.ToArgb());
        }
    }
}
