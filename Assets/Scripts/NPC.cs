using UnityEngine;

public class NPC : Interactable
{
    public string npcName;
    [TextArea(2, 5)] public string[] dialogue;
    public AudioClip[] audioClips;
    public Animator animator;

    public override void Interact()
    {
        // Pasamos 'this' como primer parámetro
        DialogueManager.instance.StartDialogue(this, npcName, dialogue, audioClips);

        if (animator != null)
        {
            animator.SetTrigger("Talk");
        }
    }

    public void StopTalkingAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Idle");
        }
    }
}