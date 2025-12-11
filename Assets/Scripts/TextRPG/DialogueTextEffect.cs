using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTextEffect : MonoBehaviour
{

    [SerializeField]
    private float speed;

    public bool isTyping;

    private Coroutine typingCoroutine;

    public void ApplyEffect(TMP_Text text, DialogueEffectType type)
    {
        switch (type)
        {
            case DialogueEffectType.Typewriter:
                typingCoroutine = StartCoroutine(TypewriterEffect(text, speed));
                break;
        }
    }

    private IEnumerator TypewriterEffect(TMP_Text text, float speed)
    {
        isTyping = true;
        text.maxVisibleCharacters = 0;
        for (int i = 0; i < text.text.Length; i++)
        {
            text.maxVisibleCharacters++;
            yield return new WaitForSeconds(speed);
        }
        isTyping = false;
    }
    
    public void CompleteTyping(TMP_Text text)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        Debug.Log(text.text);
        text.maxVisibleCharacters = text.text.Length - 1;
        
        isTyping = false;
        
    }
}

public enum DialogueEffectType
{
    None,
    Typewriter
}