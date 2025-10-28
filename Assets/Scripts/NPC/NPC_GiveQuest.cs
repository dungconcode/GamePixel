using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NPC_GiveQuest : MonoBehaviour
{
    public Quest questToGive;
    public TextAsset inkJSON;
    [SerializeField] private GameObject icon;
    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        icon.SetActive(false);
    }
    private void Update()
    {
        if(playerInRange){
            icon.SetActive(true);
            if ( (Input.GetMouseButtonDown(0) || TouchClicked())&&!InkStoryController.Instance.dialogueIsPlaying)
            {
                Debug.Log("mở hội thoại");
                InkStoryController.Instance.StartDialogue(inkJSON, questToGive);
            }
        }
        else icon.SetActive(false);
    }
    private bool TouchClicked()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            return t.phase == TouchPhase.Began;
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange= true;
            Debug.Log("Player chạm NPC");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InkStoryController.Instance.dialoguePanel.SetActive(false);
            InkStoryController.Instance.dialogueIsPlaying =false;
            Debug.Log("Player thoát chạm NPC ");
        }
    }

}
