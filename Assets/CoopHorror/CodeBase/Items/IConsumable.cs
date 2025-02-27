namespace CoopHorror.CodeBase.Items
{
    public interface IConsumable : IBeingDestroyed
    {
        void RpcUseUp();
    }
}