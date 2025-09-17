using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCharacterManager : MonoBehaviour
{
    public Character_ID[] characters;
    private void Start()
    {
        RefreshCharacters();
    }
    private void OnEnable()
    {
        CharacterEvent.OnStartGame += LockClickedAfterStart;
    }
    private void OnDisable()
    {
        CharacterEvent.OnStartGame -= LockClickedAfterStart;
    }
    private void LockClickedAfterStart()
    {
        foreach (Character_ID c in characters)
        {
            if(c != null)
            {
                SpriteRenderer sr = c.characterIcon.GetComponent<SpriteRenderer>();
                Animator anim = c.characterIcon.GetComponent<Animator>();
                Collider2D col = c.characterIcon.GetComponent<Collider2D>();
                sr.color = Color.white;
                anim.enabled = true;
                col.enabled = false;
            }
        }
    }
    public void RefreshCharacters()
    {
        foreach (Character_ID c in characters)
        {
            SpriteRenderer sr = c.characterIcon.GetComponent<SpriteRenderer>();
            Animator anim = c.characterIcon.GetComponent<Animator>();

            if (c.isUnlocked)
            {
                sr.color = Color.white;
                anim.enabled = true;
            }
            else
            {
                sr.color = Color.gray;
                anim.enabled = false;
            }
        }
    }
}
