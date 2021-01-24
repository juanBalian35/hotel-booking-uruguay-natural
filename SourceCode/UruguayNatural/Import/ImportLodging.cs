using System.Collections.Generic;

namespace Import
{
    public interface IImportLodging
    {
        string GetFormatName();
        ICollection<LodgingParsed> Parse(string content);
    }
}
