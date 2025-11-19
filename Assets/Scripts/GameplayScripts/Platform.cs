using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] private bool _horizontalMovement;
    [SerializeField] private float _targetPositionA;
    [SerializeField] private float _targetPositionB;
    [SerializeField] private float _platformSpeed;
    [SerializeField] private float _pauseDuration = 2f;
    [SerializeField] private bool _startsInRandomDirection;
    [SerializeField] private bool _startsInDirectionA;
    private float _currentTargetPosition;


    private void Start()
    {
        if (_targetPositionA > _targetPositionB) _currentTargetPosition
        = _targetPositionA;
        else _currentTargetPosition = _targetPositionB;
        DetermineMovementDirection(_startsInRandomDirection, _startsInDirectionA);
        StartCoroutine(MovementLoop());
    }
    private IEnumerator MovementLoop()
    {
        while (true)
        {
            Vector3 targetPos = _horizontalMovement
                   ? new Vector3(_currentTargetPosition, // x movement
                   transform.position.y, transform.position.z)
                   : new Vector3(transform.position.x, // y movement
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
    public void SetMovementType(bool horizontalMovement)
    {
        _horizontalMovement = horizontalMovement;
    }
    public void SetStartingDirection(bool isRandom, bool isTowardsPointA)
    {
        _startsInDirectionA = isTowardsPointA;
        _startsInRandomDirection = isRandom;
    }
    public void DetermineMovementDirection(bool isRandom, bool isTowardsPointA)
    {
        if (isRandom)
        {
            _currentTargetPosition = new System.Random().Next(2) == 0 ? _targetPositionA : _targetPositionB;
        }
        else if (isTowardsPointA)
        {
            _currentTargetPosition = _targetPositionA;
        }
        else
        {
            _currentTargetPosition = _targetPositionB;
        }
    }
    public void SetMovementPoints(float pointA, float pointB)
    {
        _targetPositionA = pointA;
        _targetPositionB = pointB;
    }
    public void SetPauseDuration(float duration)
    {
        _pauseDuration = duration;
    }
    public void SetMovementSpeed(float speed)
    {
        _platformSpeed = speed;
    }

}
