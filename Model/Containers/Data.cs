
namespace Model.Containers
{
    public interface IData<TData>
    {
        int Id { get; set; }
        TData Entity { get; set; }
    }

    public class Data<TData> : IData<TData>
    {
        public int Id { get; set; }
        public TData Entity { get; set; }
    }
}
