using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField]
    private TextAsset inkJSON;

    void Update()
    {
        if(InputManager.GetInstance().GetInteractPressed())
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }
}
