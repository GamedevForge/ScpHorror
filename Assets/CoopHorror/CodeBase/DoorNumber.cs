using CoopHorror.CodeBase.Rooms;
using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CoopHorror.CodeBase
{
    public class DoorNumber : NetworkBehaviour, INetworkRunnerCallbacks
    {
        [Networked] private int _currentNumber { get; set; }
        
        [SerializeField] private TextMeshProUGUI _text;

        private RoomCount _roomCount;

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RpcInit(RoomCount roomCount)
        {
            _roomCount = roomCount;
            _roomCount.CountChanged += RpcChangeNumber;
            _roomCount.OnUnsubscribe += UnsubscribeOnEvent;
        }

        private void UnsubscribeOnEvent()
        {
            _roomCount.CountChanged -= RpcChangeNumber;
            _roomCount.OnUnsubscribe -= UnsubscribeOnEvent;
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RpcChangeNumber()
        {
            _currentNumber = _roomCount.Count;
            _text.text = _currentNumber.ToString();
            _roomCount.CountChanged -= RpcChangeNumber;
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RpcDrawNumber()
        {
            Debug.Log(_currentNumber);
            _text.text = _currentNumber.ToString();
        }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {

        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {

        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            RpcDrawNumber();
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {

        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        { 

        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {

        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {

        }

        public void OnConnectedToServer(NetworkRunner runner)
        {

        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {

        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {

        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {

        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {

        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {

        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {

        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {

        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
        {

        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {

        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {

        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {

        }
    }
}