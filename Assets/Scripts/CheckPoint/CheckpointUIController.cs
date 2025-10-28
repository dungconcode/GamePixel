using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class CheckpointUIController : MonoBehaviour
{
    public GameObject panel;
    public Button[] checkpointButtons;
    public int teleportManaCost = 50;

    public void OpenMenu()
    {
        panel.SetActive(true);
        HideAllButtons();

        List<CheckpointData> list = CheckpointManager.Instance.unlockedCheckpoints;

        for (int i = 0; i < checkpointButtons.Length; i++)
        {
            if (i >= list.Count) break;

            int index = i;
            checkpointButtons[i].gameObject.SetActive(true);
            Debug.Log("đã hiện button");
            checkpointButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = list[i].name + $" ({teleportManaCost} mana)";
            checkpointButtons[i].onClick.RemoveAllListeners();
            checkpointButtons[i].onClick.AddListener(() => TeleportTo(list[index]));
        }
    }

    public void TeleportTo(CheckpointData cp)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //if (stats.HasEnoughMana(teleportManaCost))
        //{
            //stats.ReduceMana(teleportManaCost);
            Player_Controller.Instance.transform.position = cp.location.position;
            RespawnController.Instance.respawnPoint = cp.location;
            panel.SetActive(false);
        //}
        // else
        // {
        //     UIManager.Instance.ShowNotEnoughMana();
        // }
    }

    void HideAllButtons()
    {
        foreach (Button b in checkpointButtons)
            b.gameObject.SetActive(false);
    }
    public void CloseMenu()
    {
        panel.SetActive(false);
    }
}
