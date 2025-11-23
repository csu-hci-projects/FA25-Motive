using Unity.Netcode;
using UnityEngine;

public class SimpleNetworkUI : MonoBehaviour
{
    void OnGUI()
    {
        const int width = 150;
        const int height = 30;
        int y = 10;

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUI.Button(new Rect(10, y, width, height), "Host"))
            {
                NetworkManager.Singleton.StartHost();
            }
            y += height + 5;

            if (GUI.Button(new Rect(10, y, width, height), "Client"))
            {
                NetworkManager.Singleton.StartClient();
            }
            y += height + 5;

            if (GUI.Button(new Rect(10, y, width, height), "Server"))
            {
                NetworkManager.Singleton.StartServer();
            }
        }
        else
        {
            if (GUI.Button(new Rect(10, y, width, height), "Shutdown"))
            {
                NetworkManager.Singleton.Shutdown();
            }
        }
    }
}
