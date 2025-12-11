using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public FadeEffect fadeEffect;

    [SerializeField]
    private string sceneName;

    void Start()
    {
        fadeEffect = FindObjectOfType<FadeEffect>(true);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fadeEffect.OnFade(FadeState.FadeOut,() => LoadScene(sceneName));
        }
    }

}
