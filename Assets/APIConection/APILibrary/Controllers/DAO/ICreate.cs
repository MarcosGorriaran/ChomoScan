using System.Collections.Generic;

namespace APILibraryDaltonismo.Controllers.DAO
{
    public interface ICreate<Model>
    {
        public void Create(Model info);
        public void Create(IEnumerable<Model> infoList);
    }
}
