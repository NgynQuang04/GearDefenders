using System.Collections.Generic;
using UnityEngine;

public abstract class PoolBase<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected T prefab;
    [SerializeField] protected int preloadCount = 10;

    protected readonly Stack<T> pool = new();

    protected virtual void Awake()
    {
        for (int i = 0; i < preloadCount; i++)
            CreateNew();
    }

    protected T CreateNew()
    {
        T obj = Instantiate(prefab, transform);
        obj.gameObject.SetActive(false);
        pool.Push(obj);
        return obj;
    }

    public virtual T Get()
    {
        if (pool.Count == 0)
            CreateNew();

        T obj = pool.Pop();
        obj.gameObject.SetActive(true);

        if (obj is IPoolable p)
            p.OnSpawned();

        return obj;
    }

    public virtual void Return(T obj)
    {
        if (obj is IPoolable p)
            p.OnDespawned();

        obj.gameObject.SetActive(false);
        pool.Push(obj);
    }
}
