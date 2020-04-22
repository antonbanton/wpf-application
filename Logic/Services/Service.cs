using DataAccess.Repositories;
using Model.Containers;

namespace Logic.Services
{
    public interface IService
    {
        IData<string> GetData();
        void UpdateDataText(string newText);
    }

    public class Service : IService
    {
        private readonly IRepository _repository;

        public Service(IRepository repository)
        {
            _repository = repository;
        }

        public IData<string> GetData()
        {
            return _repository.GetData();
        }

        public void UpdateDataText(string newText)
        {
            _repository.UpdateDataText(newText);
        }
    }
}
