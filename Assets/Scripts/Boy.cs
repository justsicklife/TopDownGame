using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour, IInteractable
{

    private DialogueTrigger boyDialogueTrigger;

    private bool isTalkable;

    void Start()
    {
        isTalkable = true;
    }

    public bool CanInteract()
    {
        return isTalkable;
    }

    public void Interact()
    {
        if(CanInteract())
        {
            
            TalkeToBoy();
        }   
    }
    
    private void TalkeToBoy()
    {
        TextAsset textAsset = gameObject.GetComponent<DialogueResolver>().GetInkFile("boy");
        FindObjectOfType<DialogueTrigger>().ChangeInkJSON(textAsset);
    }
}
