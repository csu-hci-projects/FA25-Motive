using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    public List<string> characters;
    private List<string> availableCharacters;

    private void Awake()
    {
        availableCharacters = new List<string>(characters);
    }
    public string AssignCharacter()
    {
        
        if (characters.Count == 0)
        {
            Debug.Log("No characters left");
            return "none";
        }
        int randomNum = Random.Range(0, characters.Count);
        string assignedCharacter = characters[randomNum];
        characters.RemoveAt(randomNum);
        return assignedCharacter;
    }
    
}
