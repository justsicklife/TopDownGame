using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField]
    private TextMeshProUGUI displayNameText;

    [SerializeField]
    private Button ContinueButton;

    [Header("Choices UI")]
    [SerializeField]
    private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    private static DialogueManager instance;

    [SerializeField]
    private bool hasChoices;

    private const string SPEAKER_TAG = "speaker";

    private const string PORTRAIT_TAG = "portrait";

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");

        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        hasChoices = false;
        GameManager.Instance.SetDialogueState(false);
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    void Update()
    {
        if (!GameManager.Instance.IsDialoguePlaying)
        {
            return;
        }

        if (InputManager.GetInstance().GetSubmitPressed() && !hasChoices)
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        GameManager.Instance.SetDialogueState(true);
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        GameManager.Instance.SetDialogueState(false);
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string nextDialgoue = currentStory.Continue();
            Debug.Log(nextDialgoue);
            dialogueText.text = nextDialgoue;

            DisplayChoices();

            ContinueButton.gameObject.SetActive(!hasChoices);

            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(":");
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed : " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    Debug.Log(tagValue);
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    Debug.Log("portrait=" + tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently begin handled : " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        hasChoices = true;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given" +
            currentChoices.Count);
        }

        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        if (index == 0)
        {
            hasChoices = false;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    // 버튼 콜백 함수 
    public void MakeChoice(int choiceIndex)
    {
        StartCoroutine(HandleChoice(choiceIndex));
    }

    // 코루틴으로 호출 하는 이유 
    // 엔터키를 누르면 업데이트에 있는 continueStory 와 버튼이 같이 실행되서 
    // 대사를 하나 넘기게 된다.
    // 그래서 Button -> update 이렇게 실행시키기 위해 코루틴을 사용
    private IEnumerator HandleChoice(int choiceIndex)
    {
        yield return new WaitForSeconds(0.01f);
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

}
