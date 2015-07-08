using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using CocktailizrTypes.Model.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CocktailizrTypes.Helper.Serializer
{
    public class BitmapBsonSerializer : SerializerBase<Image>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Image value)
        {
            if (value == null)
            {
                context.Writer.WriteNull();
                return;
            }
            using (var ms = new MemoryStream())
            {
                value.Save(ms, ImageFormat.Bmp);
                context.Writer.WriteBinaryData(ms.ToArray());
            }
        }

        public override Image Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            using (var ms = new MemoryStream(context.Reader.ReadBytes()))
            {
                return ms.Length > 0 ? new Bitmap(ms) : null;
            }
        }
    }
}