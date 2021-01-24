using Domain;

namespace Model.Out
{
    public class RegionDetailedInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
        public string VideoPath { get; set; } 
        public byte[] MapYellow { get; set; } 
        public byte[] MapTransparent { get; set; } 

        public RegionDetailedInfoModel(Region region)
        {
            Id = region.Id;
            Name = region.Name;
            Description = region.Description;
            VideoPath = region.VideoPath;
            MapYellow = region.MapYellow;
            MapTransparent = region.MapTransparent;
        }


        public override bool Equals(object obj)
        {
            if (!(obj is RegionDetailedInfoModel))
            {
                return false;
            }

            var region = obj as RegionDetailedInfoModel;
            return Id == region.Id && Name == region.Name;
        }
    }
}