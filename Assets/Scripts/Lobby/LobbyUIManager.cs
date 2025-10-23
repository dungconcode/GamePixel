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
    [SerializeField] private GameObject namePanel;

    [Header("Weapon Player Selected")]
    public Sprite[] weaponList;
    [SerializeField] private Image weaponStart;
    [SerializeField] TextMeshProUGUI currentRole;

    [Header("Skill Player Selected")]
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private SkillRepository skillRepo;
    [SerializeField] private SkillButtonUIManager skillUI;
    private void Awake()
    {
        index = GameObject.Find("==INDEX==");
    }
    private void Start()
    {
        index.SetActive(false);
        panelIndex.SetActive(false);
        playerIndexPannel.SetActive(false);
        skillPanel.SetActive(false);
        namePanel.SetActive(false);
        ResetSelected.gameObject.SetActive(false);
        RectTransform rt = playerIndexPannel.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(-167, -12);

        RectTransform skillrt = skillPanel.GetComponent<RectTransform>();
        skillrt.anchoredPosition = new Vector2(152, -1);

        RectTransform indexrt= index.GetComponent<RectTransform>();
        indexrt.anchoredPosition = new Vector2(50, 100);

        RectTransform namert = namePanel.GetComponent<RectTransform>();
        namert.anchoredPosition = new Vector2(9, 71);

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
        skillPanel.SetActive(true);
        namePanel.SetActive(true);
        StartCoroutine(ShowpanelIndex(playerIndexPannel, new Vector2(167, -12)));
        StartCoroutine(ShowpanelIndex(skillPanel, new Vector2(-152, -1)));
        StartCoroutine(ShowpanelIndex(namePanel, new Vector2(9, -71)));

        playerName.text = characterID.player_Index.characterID;

        var skills = skillRepo.GetSkills(characterID.player_Index.characterID);
        if (skills != null) skillUI.SetSkills(skills);

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
        StartCoroutine(HidepanelIndex(playerIndexPannel, new Vector2(-167, -12)));
        StartCoroutine(HidepanelIndex(skillPanel, new Vector2(152, -1)));
        StartCoroutine(HidepanelIndex(namePanel, new Vector2(9, 90)));
        ResetSelected.gameObject.SetActive(false);
    }


    private void HidePlayerIndexOnStartGame()
    {
        panelIndex.SetActive(false);
        StartCoroutine(HidepanelIndex(playerIndexPannel, new Vector2(-167, -12)));
        StartCoroutine(HidepanelIndex(skillPanel, new Vector2(152, -1)));
        StartCoroutine(HidepanelIndex(namePanel, new Vector2(9, 90)));
        ResetSelected.gameObject.SetActive(true);
        StartCoroutine(ShowIndexPlayer());
    }


    IEnumerator ShowpanelIndex(GameObject panel, Vector2 targetPos)
    {
        RectTransform rt = panel.GetComponent<RectTransform>();
        while (Vector2.Distance(rt.anchoredPosition, targetPos) > 0.1f)
        {
            rt.anchoredPosition = Vector2.MoveTowards(
                rt.anchoredPosition,
                targetPos,
                1000 * Time.deltaTime
            );
            yield return null;
        }
        rt.anchoredPosition = targetPos;
        
    }


    IEnumerator HidepanelIndex(GameObject panel, Vector2 target)
    {
        RectTransform rt = panel.GetComponent<RectTransform>();
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
