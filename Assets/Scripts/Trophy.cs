using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour,IInteractable
{
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
            GetComponent<InteractableObject>().ChangeDialogue();
        }   
    }
}
