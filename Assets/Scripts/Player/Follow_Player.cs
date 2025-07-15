using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    private Transform _playerTransform;
    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        Follow();
    }
    private void Follow()
    {
        if (_playerTransform == null) return; // Ensure the player transform is assigned
        Vector3 targetPosition = _playerTransform.position;
        transform.position = targetPosition;
    }
}
