using System;
using System.Collections;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    [SerializeField] private bool _xMovement;
    [SerializeField] private bool _yMovement;
    [SerializeField] private float _targetPositionA;
    [SerializeField] private float _targetPositionB;
    [SerializeField] private float _platformSpeed;
    [SerializeField] private float _pauseDuration = 2f;
    private float _currentTargetPosition;

    private void OnValidate()
    {
        if (_xMovement && _yMovement)
        {
            _yMovement = false;
        }
    }
    private void Start()
    {
        if (_targetPositionA > _targetPositionB) _currentTargetPosition
        = _targetPositionA;
        else _currentTargetPosition = _targetPositionB;

        if (!_xMovement && !_yMovement)
        {
            Debug.LogWarning("No movement toggled for platform" + this);
        }
        else
        {
            StartCoroutine(MovementLoop());
        }

    }
    private IEnumerator MovementLoop()
    {
        while (true)
        {
            Vector3 targetPos = _xMovement
                   ? new Vector3(_currentTargetPosition,
                   transform.position.y, transform.position.z)
                   : new Vector3(transform.position.x,
                   _currentTargetPosition, transform.position.z);
            while (!HasReachedTargetPosition(targetPos))
            {
                transform.position = Vector3.MoveTowards(transform.position,
                targetPos, _platformSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = targetPos;
            yield return new WaitForSeconds(_pauseDuration);
            _currentTargetPosition = _currentTargetPosition ==
            _targetPositionA ? _targetPositionB : _targetPositionA;
        }
    }
    private bool HasReachedTargetPosition(Vector3 targetPos)
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.01)
        {
            return true;
        }
        return false;
    }
    public void StopMovement()
    {
        StopAllCoroutines();
    }

    public void StartMovement()
    {
        if (_xMovement || _yMovement) StartCoroutine(MovementLoop());
    }

}
