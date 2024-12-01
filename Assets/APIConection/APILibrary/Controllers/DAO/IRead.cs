using System.Collections.Generic;

namespace APILibraryDaltonismo.Controllers.DAO
{
    public interface IRead<Model>
    {
        public Model  Get<IDValueType>(IDValueType id);
        public IEnumerable<Model> Get();
    }
}
