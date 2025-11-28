using UnityEngine;
using UnityEngine.InputSystem; // Required for the new Input System

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 2f;
    public Transform cameraTransform;
    public InputActionReference interactAction; // Reference to the input action for interaction

    private void OnEnable()
    {
        interactAction.action.Enable();
        interactAction.action.performed += OnInteract;
    }

    private void OnDisable()
    {
        interactAction.action.performed -= OnInteract;
        interactAction.action.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionDistance))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                hit.collider.GetComponent<Interactable>().Interact();
            }
        }
    }

    void Update()
    {
        // This part can be used for visual feedback, like showing a dot on what you are looking at.
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionDistance))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                Debug.Log("Looking at an interactable object: " + hit.collider.name);
            }
        }
    }
}
