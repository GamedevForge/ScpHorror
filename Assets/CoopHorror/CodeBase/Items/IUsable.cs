using Fusion;

namespace CoopHorror.CodeBase.Items
{
    public interface IUsable
    {
        void RpcUse(NetworkObject playerObject);
    }
}