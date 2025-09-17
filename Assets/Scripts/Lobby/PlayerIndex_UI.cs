using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIndex_UI : MonoBehaviour
{
    [SerializeField] private List<StartUI> startUIs = new List<StartUI>();

    private void OnEnable()
    {
        CharacterEvent.OnCharacterSelected += ShowStatFromSO;
        CharacterEvent.OnStatsUpdated += UpdateStats;
    }
    private void OnDisable()
    {
        CharacterEvent.OnCharacterSelected -= ShowStatFromSO;
        CharacterEvent.OnStatsUpdated -= UpdateStats;
    }
    private void ShowStatFromSO(Character_ID characterID)
    {
        CharacterData data = SaveFileJson.LoadCharacterData(characterID.player_Index.characterID, characterID.player_Index);
        UpdateStats(data);
    }
    private void UpdateStats(CharacterData data)
    {
        foreach (StartUI ui in startUIs)
        {
            switch (ui.statName)
            {
                case "HP":
                    ui.barFill.fillAmount = data.hp / 12f;
                    ui.valueTxt.text = data.hp.ToString();
                    break;
                case "Armor":
                    ui.barFill.fillAmount = data.armor / 12f;
                    ui.valueTxt.text = data.armor.ToString();
                    break;
                case "Mana":
                    ui.barFill.fillAmount = data.mana / 300f;
                    ui.valueTxt.text = data.mana.ToString();
                    break;
                case "Damage":
                    ui.barFill.fillAmount = data.damage / 12f;
                    ui.valueTxt.text = data.damage.ToString();
                    break;
            }
        }
    }

}
