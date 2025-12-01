using UnityEngine;
using TMPro;

public class Clue_Pannel_Menu : MonoBehaviour
{
    public static Clue_Pannel_Menu Instance { get; private set; }

    [Header("UI References")]
    public GameObject panel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    // The clue currently being shown in the panel
    private Interactable activeClue;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    // ---- Show panel for a specific clue ----
    public void Show(Interactable clue, string title, string description)
    {
        activeClue = clue;  // remember which object this panel is for
        // Debug.Log($"[CluePanel] Show for {clue.name}");

        titleText.text = title;
        descriptionText.text = description;
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    public void OnLeavePressed()
    {
        // Debug.Log("[CluePanel] Leave pressed");

        // Turn off highlight, if any
        Interactable target = activeClue;
        if (target == null)
            target = PlayerInteraction.currentTarget;

        if (target != null && target.highlightObject != null)
        {
            target.highlightObject.SetActive(false);
        }

        // Clear references
        if (PlayerInteraction.currentTarget == target)
            PlayerInteraction.currentTarget = null;

        activeClue = null;

        // Hide UI and prompt
        Hide();
        if (InteractionPromptUI.Instance != null)
            InteractionPromptUI.Instance.Hide();
    }

    public void OnHidePressed()
    {
        // Debug.Log("[CluePanel] Hide-it pressed");

        // Prefer the activeClue, but fall back to whatever the player is targeting
        Interactable target = activeClue;
        if (target == null)
            target = PlayerInteraction.currentTarget;

        if (target != null)
        {
            // Turn off highlight
            if (target.highlightObject != null)
            {
                target.highlightObject.SetActive(false);
            }

            // Remove clue from the world
            Object.Destroy(target.gameObject);

            if (PlayerInteraction.currentTarget == target)
                PlayerInteraction.currentTarget = null;
        }

        activeClue = null;

        // Hide UI and prompt
        Hide();
        if (InteractionPromptUI.Instance != null)
            InteractionPromptUI.Instance.Hide();
    }
}
