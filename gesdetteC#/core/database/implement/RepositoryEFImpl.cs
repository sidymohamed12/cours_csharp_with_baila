using gesdette.core.database;
using gesdette.entities;

public class RepositoryEFImplement<T> : IRepositoryEF<T> where T : class
{
    public int count()
    {
        using var context = new AppDbContext();
        return context.Set<T>().Count();
    }

    public void insert(T value)
    {
        using var context = new AppDbContext();
        context.Set<T>().Add(value);
        context.SaveChanges();
    }

    public List<T> selectAll()
    {
        using var context = new AppDbContext();
        return context.Set<T>().ToList();
    }

    public T selectBy(string name)
    {
        throw new NotImplementedException();
    }

    public T selectById(int id)
    {
        using var context = new AppDbContext();
        return context.Set<T>().Find(id)!;
    }

    public void update(T value)
    {
        using var context = new AppDbContext();
        context.Set<T>().Update(value);
        context.SaveChanges();
    }
}