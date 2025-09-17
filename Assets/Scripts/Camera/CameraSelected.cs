using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelected : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    private Vector3 startTarget;
    private Vector3 currentTarget;
    private float zoomSize = 2f;
    private float zoomSpeed = 12f;
    private Character_ID currentCharacter;

    private CameraFollow cameraFollow;
    private void Awake()
    {
        cameraFollow = GetComponent<CameraFollow>();
        cameraFollow.enabled = false;
    }
    private void Start()
    {
        
        vcam = GetComponent<CinemachineVirtualCamera>();
        startTarget = transform.position;
    }
    private void OnEnable()
    {
        CharacterEvent.OnCharacterSelected += UpdateCamera;
        CharacterEvent.OnBackPressed += ResetCamera;
        CharacterEvent.OnStartGame += CameraAfterSelected;
    }
    private void OnDisable()
    {
        CharacterEvent.OnCharacterSelected -= UpdateCamera;
        CharacterEvent.OnBackPressed -= ResetCamera;
        CharacterEvent.OnStartGame -= CameraAfterSelected;
    }



    private void UpdateCamera(Character_ID characterID)
    {
        StopAllCoroutines();
        currentCharacter = characterID;
        cameraFollow.enabled = false;
        var sr = characterID.characterIcon.GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;
        currentTarget = characterID.characterIcon.transform.position;
        StartCoroutine(ForcusCharacter(currentTarget, zoomSize));
    }
    private void ResetCamera()
    {
        ShowPlayerIcon();
        StopAllCoroutines();
        StartCoroutine(ForcusCharacter(startTarget, 5f));
        currentCharacter = null;
    }
    private void CameraAfterSelected()
    {
        StopAllCoroutines();
        StartCoroutine(ForcusCharacter(currentTarget, 5f));
        cameraFollow.enabled = true;
    }
    private void ShowPlayerIcon()
    {
        if (currentCharacter != null)
        {
            var sr = currentCharacter.characterIcon.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = true;
        }
    }
    private IEnumerator ForcusCharacter(Vector3 target, float zoom)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f || Mathf.Abs(vcam.m_Lens.OrthographicSize - zoom) > 0.1f)
        {
            transform.position = new Vector3(target.x, target.y, transform.position.z);
            vcam.m_Lens.OrthographicSize = Mathf.MoveTowards(vcam.m_Lens.OrthographicSize, zoom, zoomSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
