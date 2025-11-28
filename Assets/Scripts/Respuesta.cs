using UnityEngine;

public class Respuesta : MonoBehaviour
{
    [Header("Referencia Obligatoria")]
    public NPC npcPrincipal; // Arrastra aquí el script NPC del mismo objeto
                             // (Necesario para que el Manager sepa a quién animar)

    [Header("Datos Respuesta A")]
    [TextArea(2, 5)]
    public string[] dialogoA;
    public AudioClip[] audiosA;

    [Header("Datos Respuesta B")]
    [TextArea(2, 5)]
    public string[] dialogoB;
    public AudioClip[] audiosB;

    // Vincula esta función al evento OnClick() de tu Botón A en el Canvas
    public void ElegirRespuestaA()
    {
        if (npcPrincipal != null)
        {
            // Enviamos los datos de la Opción A al Manager
            DialogueManager.instance.StartDialogue(
                npcPrincipal,            // Referencia para animaciones
                npcPrincipal.npcName,    // Nombre del NPC
                dialogoA,                // Texto A
                audiosA                  // Audio A
            );
        }
        else
        {
            Debug.LogError("Falta asignar el script NPC en el inspector de Respuesta.cs");
        }
    }

    // Vincula esta función al evento OnClick() de tu Botón B en el Canvas
    public void ElegirRespuestaB()
    {
        if (npcPrincipal != null)
        {
            // Enviamos los datos de la Opción B al Manager
            DialogueManager.instance.StartDialogue(
                npcPrincipal,
                npcPrincipal.npcName,
                dialogoB,
                audiosB
            );
        }
    }
}