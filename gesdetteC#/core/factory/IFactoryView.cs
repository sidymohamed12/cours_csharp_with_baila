
namespace gesdette.core.factory
{
    public interface IFactoryView<T>
    {
        View<T>? createView();
    }
}