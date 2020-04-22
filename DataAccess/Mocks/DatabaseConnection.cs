using Model.Containers;

namespace DataAccess.Mocks
{
    public interface IDatabaseConnection
    {
        IData<string> Data { get; set; }
    }

    public class DatabaseConnection : IDatabaseConnection
    {
        public IData<string> Data { get; set; } = new Data<string>() { Id = 0, Entity = "" };
    }
}
