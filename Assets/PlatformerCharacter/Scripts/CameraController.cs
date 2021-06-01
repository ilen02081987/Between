using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [Tooltip("–ассто€ние от персонажа до камеры"), SerializeField] private Vector3 _offset;
    [Tooltip("ѕлавность движени€ камеры, не выставл€ть в 0!") ,SerializeField] private float _lerpValue = .125f;
 
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _lerpValue);
    }
}
