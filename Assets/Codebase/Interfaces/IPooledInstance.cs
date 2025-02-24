using System;

namespace Assets.Codebase.Interfaces
{
    public interface IPooledInstance
    {
        public event Action<IPooledInstance> Released;
        public event Action<IPooledInstance> Disposed;

        public bool IsFree { get; }

        public void Enable();
    }
}
