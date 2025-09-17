using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnBackPressed()
    {
        CharacterEvent.OnBackPressed?.Invoke();
    }
    public void OnStartGamePressed()
    {
       CharacterEvent.OnStartGame?.Invoke();
    }
    public void ResetSelected()
    {
        SceneManager.LoadScene("Lobby Scene");
    }
}
