using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    public static InteractionPromptUI Instance { get; private set; }

    private TMP_Text _text;

    void Awake()
    {
        Instance = this;
        _text = GetComponent<TMP_Text>();

        // Start hidden
        gameObject.SetActive(false);
    }

    public void Show(string message)
    {
        if (_text == null) return;

        _text.text = message;
        gameObject.SetActive(true);
        Debug.Log("Prompt SHOW: " + message);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Debug.Log("Prompt HIDE");
    }
}
