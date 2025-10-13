using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueResolver : MonoBehaviour
{
    [SerializeField]
    private DialogueDicItem[] dialogueDicItems;

    private Dictionary<string, TextAsset> dialogueDict;

    void Awake()
    {
        dialogueDict = new Dictionary<string, TextAsset>();
        foreach (var item in dialogueDicItems)
        {
            if (!dialogueDict.ContainsKey(item.name))
                dialogueDict.Add(item.name, item.inkJSON);
            else
                Debug.LogWarning($"중복된 키 발견: {item.name}");
        }
    }

    public TextAsset GetInkFile(string key)
    {
        if (dialogueDict.TryGetValue(key, out TextAsset ink))
            return ink;

        Debug.LogWarning($"Ink 파일을 찾을 수 없음: {key}");
        return null;
    }
}

[Serializable]
public class DialogueDicItem
{
    [SerializeField]
    public string name;
    [SerializeField]
    public TextAsset inkJSON;
}