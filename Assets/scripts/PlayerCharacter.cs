using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PlayerCharacter : NetworkBehaviour
{
    [ClientRpc]
    public void ReceiveCharacterClientRpc(string name, bool murderer)
    {
        Debug.Log($"ReceiveCharacterClientRpc on client {OwnerClientId}: " +
                  $"name='{name}', murderer={murderer}, IsOwner={IsOwner}");

        if (!IsOwner)
        {
            return;
        }

        TMP_Text uiText = GameObject.Find("CharacterAssignText")?.GetComponent<TMP_Text>();
        if (uiText != null)
        {
            uiText.text = $"Your identity is: {name}";
        }
        else
        {
            Debug.LogWarning("CharacterAssignText not found!");
        }

        TMP_Text MurderText = GameObject.Find("MurdererAssignText")?.GetComponent<TMP_Text>();
        if (MurderText != null)
        {
            MurderText.text = murderer
                ? "You ARE the murderer!"
                : "You are NOT the murderer";
        }
        else
        {
            Debug.LogWarning("MurdererAssignText not found!");
        }
    }
}
