using Assets.Codebase.Interfaces;
using System.Collections.Generic;

public class Pool<T>
    where T : class, IPooledInstance
{
    private IPooledInstanceFactory _factory;

    private Queue<T> _pool;

    public Pool(IPooledInstanceFactory factory, int prewarmedCount = 0)
    {
        _pool = new Queue<T>();
        _factory = factory;

        if (prewarmedCount > 0)
        {
            CreatePrewarmedInstances(prewarmedCount);
        }
    }

    public T Get()
    {
        _pool.TryDequeue(out T freeObject);

        if (freeObject == null)
        {
            freeObject = Create();
        }

        freeObject.Enable();

        return freeObject;
    }

    private T Create()
    {
        T instance = (_factory.Create() as T);

        instance.Released += OnRelease;
        instance.Disposed += OnDispose;

        return instance;
    }

    private void OnRelease(IPooledInstance instance)
    {
        _pool.Enqueue(instance as T);
    }

    private void CreatePrewarmedInstances(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _pool.Enqueue(Create());
        }
    }

    private void OnDispose(IPooledInstance instance) 
    { 
        instance.Released -= OnRelease;
        instance.Disposed -= OnDispose;
    }
}
