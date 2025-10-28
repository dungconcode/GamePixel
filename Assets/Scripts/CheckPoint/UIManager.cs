using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Slider manaBar;
    public CheckpointUIController checkpointUI;
    public GameObject notEnoughManaText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateManaUI(int current, int max)
    {
        manaBar.value = (float)current / max;
    }

    public void OpenTeleportMenu()
    {
        checkpointUI.OpenMenu();
    }

    public void ShowNotEnoughMana()
    {
        notEnoughManaText.SetActive(true);
        CancelInvoke("HideNotEnoughMana");
        Invoke("HideNotEnoughMana", 2f);
    }

    private void HideNotEnoughMana()
    {
        notEnoughManaText.SetActive(false);
    }
    public void CloseTeleportMenu()
    {
        checkpointUI.CloseMenu();
    }
}
