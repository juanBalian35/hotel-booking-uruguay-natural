using Domain;

namespace Model.Out
{
    public class RegionBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; } 

        public RegionBasicInfoModel(Region region)
        {
            Id = region.Id;
            Name = region.Name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RegionBasicInfoModel))
            {
                return false;
            }

            var region = obj as RegionBasicInfoModel;
            return Id == region.Id && Name == region.Name;
        }
    }
}