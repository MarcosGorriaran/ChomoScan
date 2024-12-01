namespace APILibraryDaltonismo.Controllers.DAO
{
    public interface IDelete<Model>
    {
        public void Delete<IDValueType>(IDValueType id);
        public void Delete(Model infoToDelete);
    }
}
