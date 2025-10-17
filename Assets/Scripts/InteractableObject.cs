using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 대사와 관련된 코드 
public class InteractableObject : MonoBehaviour
{

    [SerializeField]
    private string dialogueKey;

    private DialogueResolver dialogueResolver;

    void Awake()
    {
        dialogueResolver = FindObjectOfType<DialogueResolver>();
        
    }

    public void ChangeDialogue()
    {
        TextAsset inkFile = dialogueResolver.GetInkFile(dialogueKey);
        DialogueTrigger.Instance.SetInkJSON(inkFile);
    }
}
