using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour, IInteractable
{

    void Start()
    {
        
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        TalkeToBoy();
    }
    
    private void TalkeToBoy()
    {
        
    }
}
