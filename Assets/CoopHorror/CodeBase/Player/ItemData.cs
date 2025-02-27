using Fusion;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public NetworkObject Prefab { get; private set; }
    [field: SerializeField] public Sprite View { get; private set; }
}
