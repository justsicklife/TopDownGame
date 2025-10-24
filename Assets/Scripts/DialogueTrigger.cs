using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    public static DialogueTrigger Instance { get; private set; }

    [Header("Ink JSON")]
    [SerializeField]
    private TextAsset inkJSON;

    [SerializeField]
    private bool autoStart = false;

    public InteractionDetector interactionDetector;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        if (autoStart)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }

    void Update()
    {
        if (autoStart) return;

        if (InputManager.GetInstance().GetInteractPressed() && interactionDetector.isInInteractRange)
        {
            if(!GameManager.Instance.IsDialoguePlaying)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
    }

    public void SetInkJSON(TextAsset pInkJson)
    {
        this.inkJSON = pInkJson; 
    }

}
