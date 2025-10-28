using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class InkStoryController : MonoBehaviour
{
    [Header("Ink JSON")]
    public TextAsset inkJSONAsset; 
    private Story story;

    [Header("Quest")]
    public Quest questToGive; 

    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    public static InkStoryController Instance; 
    private bool questTriggered = false; 
    public bool dialogueIsPlaying;
    private void Awake()
    {
        if (Instance == null) Instance = this;
       
    }
    private void Start()
    {
        dialoguePanel.SetActive(false); 
        dialogueIsPlaying= false;
        choicesText = new TextMeshProUGUI[choices.Length];
        int index =0;
        foreach(GameObject choice in choices){
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }
    
    public void StartDialogue(TextAsset inkAsset, Quest quest = null)
    {
        inkJSONAsset = inkAsset;
        questToGive = quest;
        questTriggered = false; 
        Debug.Log("StartDialogue được gọi!");

        story = new Story(inkJSONAsset.text);

        story.ObserveVariable("questAccepted", OnQuestAccepted);

        dialoguePanel.SetActive(true);
        dialogueIsPlaying =true;
        ContinueStory();
    }

    // Callback lắng nghe biến Ink
    private void OnQuestAccepted(string varName, object newValue)
    {
        Debug.Log($"Ink variable changed: {varName} = {newValue}");
        if (questTriggered) return;

        if (newValue is bool b && b)
        {
            questTriggered = true;
            Debug.Log("Quest triggered from Ink!");
            if (QuestManager.Instance != null && questToGive != null)
            {
                QuestManager.Instance.SetCurrentQuest(questToGive);
            }
        }
    }

    public void ContinueStory()
    {
        if (story.canContinue)
        {
            dialogueText.text = story.Continue();
            DisplayChoices();
        }
        else
        {
            EndDialogue();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = story.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("Nhiều lựa chọn hơn số button UI có sẵn: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            GameObject choiceObj = choices[index];
            choiceObj.SetActive(true);

            choicesText[index].text = choice.text;

            // Xóa listener cũ để tránh bị gọi nhiều lần
            Button btn = choiceObj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();

            int choiceIndex = index; // lưu lại index để tránh capture bug
            btn.onClick.AddListener(() =>
            {
                Debug.Log("Chọn lựa: " + choice.text);
                story.ChooseChoiceIndex(choiceIndex);
                ContinueStory();
            });

            index++;
        }

        // Tắt các button còn thừa
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }
    }



    private void EndDialogue()
    {
        dialogueText.text = "Hết hội thoại.";
        dialogueIsPlaying =false;
        dialoguePanel.SetActive(false);
    }

}