using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class RotateSkill : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject prefab; // Prefab to instantiate
    private List<Transform> _points = new List<Transform>();
    private int count = 8;
    public bool isSpinning = false;

    [SerializeField] private GameObject particleEffect;
    private List<Transform> _effects = new List<Transform>();

    public Transform _playerTransform;
    void Start()
    {
        rotationSpeed = 150f;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        Follow();
    }
    public void ActiveSkill(float timeToSpin, float timeToBack)
    {
        if (isSpinning) return; // Prevent multiple spins at the same time
        StartCoroutine(SpinSquence(timeToSpin,timeToBack));
    }
    IEnumerator  SpinSquence(float timeToSpin, float timeToBack)
    {
        isSpinning = true;
        
        for (int i = 0; i < count; i++)
        {
            float angle = i * 360f / count;
            Vector3 offset = Quaternion.Euler(0, 0, angle) * new Vector3(1, 0, 0) * 2f;

            GameObject instance = Instantiate(prefab);
            instance.transform.SetParent(transform);
            instance.transform.localPosition = offset;
            instance.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            GameObject effect = Instantiate(particleEffect, instance.transform);
            effect.transform.localPosition = Vector3.zero; // Set effect position to the center of the object

            _effects.Add(effect.transform);
            _points.Add(instance.transform);

            StartCoroutine(MoveAndScale(instance.transform, offset, new Vector3(0.8f, 0.8f, 0.8f), new Vector3(0, 0, angle - 90), 2f));
        }
        yield return new WaitForSeconds(timeToSpin);
        foreach (Transform t in _points)
        {
            if(t != null)
            {
                StartCoroutine(MoveAndScaleBack(t, t.localPosition, Vector3.zero, new Vector3(0, 0, t.eulerAngles.z + 180), 2f));
            }
        }
        yield return new WaitForSeconds(timeToBack);
        for(int i = 0; i < _effects.Count; i++)
        {
            if (_effects[i] == null || _points[i] == null) continue;
            Destroy(_effects[i].gameObject);
            Destroy(_points[i].gameObject);
        }
        isSpinning = false;
    }
    IEnumerator MoveAndScale(Transform obj, Vector3 offset, Vector3 targetScale, Vector3 targetAngle, float duration)
    {
        Vector3 startScale = Vector3.zero;
        Vector3 startAngle = obj.eulerAngles;
        startAngle.z += 180;

        float elapsedTime = 0f;
        float angleOffset = 360f; // Tổng độ xoắn

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float spiralAngle = t * angleOffset * Mathf.Deg2Rad;

            Vector3 currentCenter = transform.position;

            float spiralRadius = offset.magnitude;
            float directionAngle = Vector3.SignedAngle(Vector3.right, offset.normalized, Vector3.forward);

            Vector3 spiralOffset = new Vector3(Mathf.Cos(spiralAngle), Mathf.Sin(spiralAngle), 0) * spiralRadius * t;
            spiralOffset = Quaternion.Euler(0, 0, directionAngle) * spiralOffset;

            obj.position = currentCenter + spiralOffset;
            obj.localScale = Vector3.Lerp(startScale, targetScale, t);
            obj.eulerAngles = Vector3.Lerp(startAngle, targetAngle, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if(obj != null)
        {
            obj.position = transform.position + offset;
            obj.localScale = targetScale;
            obj.eulerAngles = targetAngle;
        }
        
    }
    IEnumerator MoveAndScaleBack(Transform obj, Vector3 offset, Vector3 targetScale, Vector3 targetAngle, float duration)
    {
        Vector3 startScale = obj.localScale;
        Vector3 startAngle = obj.eulerAngles;
        startAngle.z += 180;

        float elapsedTime = 0f;
        float angleOffset = 360f; // Tổng độ xoắn

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float spiralAngle = t * angleOffset * Mathf.Deg2Rad;

            Vector3 currentCenter = transform.position;

            float spiralRadius = offset.magnitude;
            float directionAngle = Vector3.SignedAngle(Vector3.right, offset.normalized, Vector3.forward);

            Vector3 spiralOffset = new Vector3(Mathf.Cos(spiralAngle), Mathf.Sin(spiralAngle), 0) * spiralRadius * (1-t);
            spiralOffset = Quaternion.Euler(0, 0, directionAngle) * spiralOffset;

            obj.position = currentCenter + spiralOffset;
            obj.localScale = Vector3.Lerp(startScale, targetScale, t);
            obj.eulerAngles = Vector3.Lerp(startAngle, targetAngle, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if(obj != null)
        {
            obj.position = transform.position + offset;
            obj.localScale = targetScale;
            obj.eulerAngles = targetAngle;
        }
        
    }
    void Follow()
    {
        Vector3 targetPosition = _playerTransform.position;
        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
    }
}
