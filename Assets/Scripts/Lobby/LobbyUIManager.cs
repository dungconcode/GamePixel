using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private GameObject panelIndex;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerHealth;
    [SerializeField] private Sprite[] playerImage;
    [SerializeField] private Image playerIcon;

    [SerializeField] private GameObject playerIndexPannel;

    
    public Button ResetSelected;

    [Header("Player Index Bar")]
    public GameObject index;

    [Header("Weapon Player Selected")]
    public Sprite[] weaponList;
    [SerializeField] private Image weaponStart;
    [SerializeField] TextMeshProUGUI currentRole;
    private void Awake()
    {
        index = GameObject.Find("==INDEX==");
    }
    private void Start()
    {
        index.SetActive(false);
        panelIndex.SetActive(false);
        playerIndexPannel.SetActive(false);
        ResetSelected.gameObject.SetActive(false);
        RectTransform rt = playerIndexPannel.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(-167, -12);


        RectTransform indexrt= index.GetComponent<RectTransform>();
        indexrt.anchoredPosition = new Vector2(50, 100);
        IndexBar idB = index.GetComponent<IndexBar>();
        idB.enabled = false;
    }

    private void OnEnable()
    {
        CharacterEvent.OnCharacterSelected += UpdatePlayerIndex;
        CharacterEvent.OnBackPressed += HidePlayerIndex;
        CharacterEvent.OnStartGame += HidePlayerIndexOnStartGame;
    }
    private void OnDisable()
    {
        CharacterEvent.OnCharacterSelected -= UpdatePlayerIndex;
        CharacterEvent.OnBackPressed -= HidePlayerIndex;
        CharacterEvent.OnStartGame -= HidePlayerIndexOnStartGame;
    }
    private void UpdatePlayerIndex(Character_ID characterID)
    {
        //Update Icon Player Selected
        GameData.selectedCharacter = characterID;
        playerIcon.sprite = playerImage[characterID.characterID];
        playerIcon.SetNativeSize();

        //Update Panel Index
        panelIndex.SetActive(true);
        playerIndexPannel.SetActive(true);
        StartCoroutine(ShowpanelIndex());
        playerName.text = characterID.player_Index.characterID;
        

        UpdateRolePlayer(characterID);

        ResetSelected.gameObject.SetActive(false);
    }

    private void UpdateRolePlayer(Character_ID characterID)
    {
        currentRole.text = characterID.player_Index.Role;
        weaponStart.sprite = weaponList[characterID.characterID];
    }
    private void HidePlayerIndex()
    {
        panelIndex.SetActive(false);
        StartCoroutine(HidepanelIndex());
        ResetSelected.gameObject.SetActive(false);
    }


    private void HidePlayerIndexOnStartGame()
    {
        panelIndex.SetActive(false);
        StartCoroutine(HidepanelIndex());
        ResetSelected.gameObject.SetActive(true);
        StartCoroutine(ShowIndexPlayer());
    }


    IEnumerator ShowpanelIndex()
    {
        RectTransform rt = playerIndexPannel.GetComponent<RectTransform>();
        Vector2 target = new Vector2(167, -12);
        while (Vector2.Distance(rt.anchoredPosition, target) > 0.1f)
        {
            rt.anchoredPosition = Vector2.MoveTowards(
                rt.anchoredPosition,
                target,
                1000 * Time.deltaTime
            );
            yield return null;
        }
        rt.anchoredPosition = target;
        
    }


    IEnumerator HidepanelIndex()
    {
        RectTransform rt = playerIndexPannel.GetComponent<RectTransform>();
        Vector2 target = new Vector2(-167, -12);
        while (Vector2.Distance(rt.anchoredPosition, target) > 0.1f)
        {
            rt.anchoredPosition = Vector2.MoveTowards(
                rt.anchoredPosition,
                target,
                5000 * Time.deltaTime
            );
            yield return null;
        }
        rt.anchoredPosition = target;
        playerIndexPannel.SetActive(false);
    }

    IEnumerator ShowIndexPlayer()
    {
        index.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        IndexBar idB = index.GetComponent<IndexBar>();
        idB.enabled = true;
        RectTransform indexrt = index.GetComponent<RectTransform>();
        while(Vector2.Distance(indexrt.anchoredPosition, new Vector2(50, -50)) > 0.1f)
        {
            indexrt.anchoredPosition = Vector2.MoveTowards(
                indexrt.anchoredPosition,
                new Vector2(50, -50),
                1000 * Time.deltaTime
            );
            yield return null;
        }
        indexrt.anchoredPosition = new Vector2(50, -50);
    }
}
