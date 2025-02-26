using Assets.Codebase.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T>
    where T : MonoBehaviour, IPooledInstance
{
    private IFactory<T> _factory;

    private Queue<T> _items;

    public Pool(IFactory<T> factory, int prewarmedCount = 0)
    {
        _items = new Queue<T>();
        _factory = factory;

        if (prewarmedCount > 0)
        {
            CreatePrewarmedInstances(prewarmedCount);
        }
    }

    public T Get()
    {
        _items.TryDequeue(out T freeObject);

        if (freeObject == null)
        {
            freeObject = Create();
        }

        freeObject.Enable();

        return freeObject;
    }

    private T Create()
    {
        T instance = _factory.Create();

        instance.Released += OnRelease;
        instance.Disposed += OnDispose;

        return instance;
    }

    private void OnRelease(IPooledInstance instance)
    {
        _items.Enqueue(instance as T);
    }

    private void CreatePrewarmedInstances(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _items.Enqueue(Create());
        }
    }

    private void OnDispose(IPooledInstance instance) 
    { 
        instance.Released -= OnRelease;
        instance.Disposed -= OnDispose;
    }
}
