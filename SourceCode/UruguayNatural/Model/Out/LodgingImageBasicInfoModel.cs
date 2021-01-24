using Domain;

namespace Model.Out
{
    public class LodgingImageBasicInfoModel
    {
        public int Id { get; set; }
        public byte[] ImageData { get; set; }

        public LodgingImageBasicInfoModel(LodgingImage lodgingImage)
        {
            Id = lodgingImage.Id;
            ImageData = lodgingImage.ImageData;
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is LodgingImageBasicInfoModel))
            {
                return false;
            }

            var lodgingImage = obj as LodgingImageBasicInfoModel;
            return Id == lodgingImage.Id;
        }
    }
}
