using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_DashSkill : SkillBase
{
    [SerializeField] private Player_Controller playerController;

    public float speed = 3f;
    private bool isActive = false;

    [SerializeField] private GameObject ghostPrefabs;
    private Coroutine ghostCoroutine;
    private float ghostSpawnTime = 0.05f;

    [SerializeField] private LayerMask enemyLayer;

    private List<AI_Path> affectedEnemies = new List<AI_Path>();
    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<Player_Controller>();
        playerController = Player_Controller.Instance; 
        
    }
    public override void Activate()
    {
        ActivateSkill(3f); // Call ActivateSkill with a delay of 0.5 seconds
    }
    public void ActivateSkill(float timeToDelaySkill)
    {
        if (isActive) return;
        StartCoroutine(DelayTimeSkill(timeToDelaySkill));
    }
    IEnumerator DelayTimeSkill(float timeToDelaySkill)
    {
        isActive = true;

        playerController.speed += speed;
        StartInvisible();
        StartDashEffect();
        yield return new WaitForSeconds(timeToDelaySkill);
        StopDashEffect();
        StopInvisible();
        playerController.speed -= speed;
        isActive = false;
    }
    #region DashGhost Code
    private void StopDashEffect()
    {
        if(ghostCoroutine != null)
        {
            StopCoroutine(ghostCoroutine);
            ghostCoroutine = null;
        }
    }
    private void StartDashEffect()
    {
        if(ghostCoroutine == null)
        {
            ghostCoroutine = StartCoroutine(SpawnGhostSkill());

        }
    }
    IEnumerator SpawnGhostSkill()
    {
        while(true)
        {
            if (playerController.isMoving)
            {
                GameObject ghost = Instantiate(ghostPrefabs, playerController.transform.position, Quaternion.identity);
                Sprite currentSprite = playerController.GetComponent<SpriteRenderer>().sprite;
                Destroy(ghost, 0.3f);
                yield return new WaitForSeconds(ghostSpawnTime);
            }
            else
            {
                yield return null;
            }
        }
    }
    #endregion
    private void StartInvisible()
    {
        SpriteRenderer[] sprites = playerController.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in sprites)
        {
            Color color = sr.color;
            color.a = Mathf.Lerp(color.a, 0.5f, 1f);
            sr.color = color;
        }
        affectedEnemies.Clear(); // Xoá danh sách cũ
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(playerController.transform.position, 6f, enemyLayer);
        foreach (Collider2D col in hitEnemies)
        {
            AI_Path ai = col.GetComponent<AI_Path>();
            if (ai != null)
            {
                ai.player = null; // hoặc ai.canDetectPlayer = false;
                affectedEnemies.Add(ai); // Lưu lại để phục hồi sau
            }
        }
    }
    private void StopInvisible()
    {
        SpriteRenderer[] sprites = playerController.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in sprites)
        {
            Color color = sr.color;
            color.a = Mathf.Lerp(color.a, 1f, 2f);
            sr.color = color;
        }
        foreach (AI_Path ai in affectedEnemies)
        {
            if (ai != null)
            {
                ai.player = playerController.transform; // hoặc ai.canDetectPlayer = true;
            }
        }

        affectedEnemies.Clear();
    }
}
