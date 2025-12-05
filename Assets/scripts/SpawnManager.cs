using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private CharacterManager characterManager;
    private List<ulong> connectedClients = new List<ulong>();

    private int _nextIndex = 0;
    private NetworkManager _nm;

    private void Start()
    {
        _nm = NetworkManager.Singleton;
        if (_nm == null)
        {
            Debug.LogError("SpawnManager: NetworkManager.Singleton is null in Start. " +
                           "Is there a NetworkManager in the scene?");
            return;
        }

        _nm.OnClientConnectedCallback += HandleClientConnected;
        _nm.OnServerStarted += HandleServerStarted;
    }

    private void OnDestroy()
    {
        if (_nm == null) return;

        _nm.OnClientConnectedCallback -= HandleClientConnected;
        _nm.OnServerStarted -= HandleServerStarted;
    }

    private void HandleServerStarted()
    {
        // Host player
        HandleClientConnected(_nm.LocalClientId);
    }

    private void HandleClientConnected(ulong clientId)
    {
        // Only the server/host is allowed to move/spawn players
        if (!_nm.IsServer)
            return;

        if (!_nm.ConnectedClients.TryGetValue(clientId, out var client))
            return;
        
        if (!connectedClients.Contains(clientId))
        {
            connectedClients.Add(clientId);
        }

        var playerObject = client.PlayerObject;
        if (playerObject == null)
            return;

        var spawn = GetNextSpawnPoint();
        if (spawn == null)
        {
            Debug.LogWarning("SpawnManager: No spawn points set!");
            return;
        }

        Debug.Log($"SpawnManager: Moving client {clientId} to spawn {_nextIndex - 1} at {spawn.position}");

        playerObject.transform.SetPositionAndRotation(spawn.position, spawn.rotation);
    }

    private Transform GetNextSpawnPoint()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
            return null;

        var spawn = spawnPoints[_nextIndex % spawnPoints.Length];
        _nextIndex++;
        return spawn;
    }

    public void StartGame()
    {
        if (!_nm.IsServer)
        {
            return;
        }
        if (connectedClients.Count == 0)
        {
            return;
        }

        int murdererIndex = Random.Range(0, connectedClients.Count);
        ulong murdererClientID = connectedClients[murdererIndex];

        foreach (ulong clientId in connectedClients)
        {
            var client = _nm.ConnectedClients[clientId];
            var playerObj = client.PlayerObject;
            string assignedCharacter = characterManager.AssignCharacter();

            var pc = playerObj.GetComponent<PlayerCharacter>();
            bool isMurderer = clientId == murdererClientID;
            if (pc != null)
            {
                pc.ReceiveCharacterClientRpc(assignedCharacter, isMurderer);
            }
        }
    }
}
