using Fusion;

namespace CoopHorror.CodeBase.Items
{
    public interface IItemData
    {
        public ItemData ItemData { get; }
        public NetworkObject ItemPrefab { get; }
        public NetworkObject ItemView { get; }
    }
}