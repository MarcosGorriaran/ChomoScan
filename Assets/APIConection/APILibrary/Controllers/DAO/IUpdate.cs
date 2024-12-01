using System.Collections.Generic;

namespace APILibraryDaltonismo.Controllers.DAO
{
    public interface IUpdate<Model>
    {
        public void Update(Model info);
        public void Update(IEnumerator<Model> infoList);
    }
}
