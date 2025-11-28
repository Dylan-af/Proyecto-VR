using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("UI Components")]
    public GameObject dialoguePanel; // El Panel que contiene el fondo y los textos
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [Header("Audio")]
    public AudioSource source;

    // Guardamos referencia al NPC actual para detener su animación luego
    private NPC currentNPC;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        // Ocultar panel al inicio
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    // Método público que llama el NPC
    public void StartDialogue(NPC npcRef, string npcName, string[] dialogue, AudioClip[] audioClips)
    {
        // 1. Limpieza inicial
        StopAllCoroutines();
        currentNPC = npcRef; // Guardamos quién nos está hablando

        // 2. Activar UI
        if (dialoguePanel != null) dialoguePanel.SetActive(true);
        if (nameText != null) nameText.text = npcName;

        // 3. Iniciar Corutina
        StartCoroutine(RunDialogue(dialogue, audioClips));
    }

    IEnumerator RunDialogue(string[] dialogue, AudioClip[] audioClips)
    {
        // Validación de longitud
        if (audioClips != null && dialogue.Length != audioClips.Length)
        {
            Debug.LogWarning("Advertencia: La cantidad de textos y audios no coincide.");
        }

        for (int i = 0; i < dialogue.Length; i++)
        {
            // A. Mostrar Texto
            dialogueText.text = dialogue[i];

            // B. Manejar Audio y Espera
            if (audioClips != null && i < audioClips.Length && audioClips[i] != null)
            {
                source.clip = audioClips[i];
                source.Play();
                // Esperamos exactamente lo que dura el audio
                yield return new WaitForSeconds(audioClips[i].length);
            }
            else
            {
                // Si no hay audio, leemos por defecto 3 segundos
                yield return new WaitForSeconds(3f);
            }
        }

        // Al terminar el bucle, cerramos todo
        EndDialogue();
    }

    private void EndDialogue()
    {
        // 1. Ocultar UI
        if (dialoguePanel != null) dialoguePanel.SetActive(false);

        // 2. Detener animación del NPC
        if (currentNPC != null)
        {
            currentNPC.StopTalkingAnimation();
            currentNPC = null;
        }

        Debug.Log("Diálogo finalizado.");
    }
}