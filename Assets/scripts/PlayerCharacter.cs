using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PlayerCharacter : NetworkBehaviour
{

    [ClientRpc]
    public void ReceiveCharacterClientRpc(string name, bool murderer)
    {
        if (!IsOwner)
        {
            return;
        }
        TMP_Text uiText = GameObject.Find("CharacterAssignText")?.GetComponent<TMP_Text>();
        if (uiText != null)
        {
            uiText.text = name;
        }
        else
        {
            Debug.Log("CharacterAssignText not found!");
        }
        TMP_Text MurderText = GameObject.Find("MurdererAssignText")?.GetComponent<TMP_Text>();
        if (murderer)
        {
            MurderText.text = "You are the Murderer!";
        }
        else
        {
            MurderText.text = "You are NOT the murderer";
        }
    }
}
