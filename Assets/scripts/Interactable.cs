using UnityEngine;

public class Interactable : MonoBehaviour
{
    // ---- CLUE INFO ----
    [Header("Clue Info")]
    public string clueTitle = "Clue";
    [TextArea]
    public string clueDescription = "This is some clue text.";

    // ---- OUTLINE / HIGHLIGHT ----
    [Header("Highlight Settings")]
    public GameObject highlightObject;      // Outline/glow child object

    // ---- INTERNAL STATE ----
    [HideInInspector]
    public bool playerInRange = false;

    private void Start()
    {
        // Ensure outline starts hidden
        if (highlightObject != null)
            highlightObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        // Turn on highlight
        if (highlightObject != null)
            highlightObject.SetActive(true);

        // Show "Press E" prompt
        if (InteractionPromptUI.Instance != null)
            InteractionPromptUI.Instance.Show("Press E to Select");

        // Tell PlayerInteraction this is the current target
        PlayerInteraction.currentTarget = this;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;

        // Turn off highlight
        if (highlightObject != null)
            highlightObject.SetActive(false);

        // Hide prompt
        if (InteractionPromptUI.Instance != null)
            InteractionPromptUI.Instance.Hide();

        // Clear target if it's this object
        if (PlayerInteraction.currentTarget == this)
            PlayerInteraction.currentTarget = null;
    }

    // Called when the player presses E (through PlayerInteraction)
    public void Interact()
    {
        if (!playerInRange) return;

        // Hide the prompt
        if (InteractionPromptUI.Instance != null)
            InteractionPromptUI.Instance.Hide();

        // Open the clue panel with this clue's info
        if (Clue_Pannel_Menu.Instance != null)
        {
            Clue_Pannel_Menu.Instance.Show(this, clueTitle, clueDescription);
        }
    }
}
