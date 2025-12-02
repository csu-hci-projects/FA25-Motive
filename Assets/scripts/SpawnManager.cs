using UnityEngine;
using Unity.Netcode;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

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
}
