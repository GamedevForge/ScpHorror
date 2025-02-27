using Fusion;
using UnityEngine;

namespace CoopHorror.CodeBase.Rooms
{
    public class EnvironmentSpawner : NetworkBehaviour
    {
        [SerializeField] private NetworkObject[] _environmentPrefabs;

        public override void Spawned()
        {
            Runner.Spawn(_environmentPrefabs[Random.Range(0, _environmentPrefabs.Length)], transform.position,
                    transform.rotation);
        }
    }
}