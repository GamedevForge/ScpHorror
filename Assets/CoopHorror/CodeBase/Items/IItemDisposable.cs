using System;

namespace CoopHorror.CodeBase.Items
{
    public interface IItemDisposable
    {
        event Action OnDispose;
    }
}