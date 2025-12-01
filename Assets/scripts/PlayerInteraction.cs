using UnityEngine;
using UnityEngine.InputSystem;   // needed for InputAction.CallbackContext

public class PlayerInteraction : MonoBehaviour
{
    // The interactable object the player is currently looking at / standing near.
    // This is set by the Interactable script, not by this one.
    public static Interactable currentTarget;

    // This is called by the Input System "Interact" action
    // (PlayerInput component -> Player map -> Interact -> PlayerInteraction.OnInteract)
    public void OnInteract(InputAction.CallbackContext context)
    {
        // Only fire on the performed phase (button actually pressed)
        if (!context.performed) return;

        if (currentTarget != null)
        {
            // Let the Interactable handle prompt + panel logic
            currentTarget.Interact();
        }
    }
}
