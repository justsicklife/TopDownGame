using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField]
    private TextAsset inkJSON;

    [SerializeField]
    private bool autoStart = false;

    public InteractionDetector interactionDetector;

    void Start()
    {   
        if(autoStart)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }

    void Update()
    {
        if (autoStart) return;

        if (InputManager.GetInstance().GetInteractPressed() && interactionDetector.isInInteractRange)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }

    public void ChangeInkJSON(TextAsset newInkJSON)
    {
        inkJSON = newInkJSON;
    }

}
