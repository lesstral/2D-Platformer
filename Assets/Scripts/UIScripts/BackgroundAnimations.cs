using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
public class BackgroundAnimations : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _canvasRect;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _leftEdgeOffset = 0;
    [SerializeField] private float _rightEdgeOffset = 0;
    [SerializeField] private bool _startOffScreen;
    float _leftEdge;
    float _rightEdge;
    Vector3 _startingPos;
    private void OnEnable()
    {
        Events.UIEvents.onResolutionChange.Subscribe(CalculateEdges);
    }
    private void OnDisable()
    {
        Events.UIEvents.onResolutionChange.Unsubscribe(CalculateEdges);
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();

        if (_canvasRect == null)
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null)
                _canvasRect = canvas.GetComponent<RectTransform>();
        }

    }
#endif
    private void Start()
    {

        CalculateEdges();

        if (_startOffScreen)
        {
            _startingPos = new Vector3(_leftEdge, transform.position.y, 0f);
        }
        else
        {
            _startingPos = transform.position;
        }
        transform.position = _startingPos;
    }
    private void Update()
    {
        transform.position += Vector3.right * _speed * Time.unscaledDeltaTime;
        if (transform.position.x > _rightEdge)
        {
            transform.position = new Vector3(_leftEdge, transform.position.y, 0f);
        }

    }
    private void CalculateEdges()
    {
        _leftEdge = -(_canvasRect.rect.width * 0.5f * _canvasRect.localScale.x) - (_rectTransform.rect.width * 0.5f * _canvasRect.localScale.x);
        _rightEdge = (_canvasRect.rect.width * 0.5f * _canvasRect.localScale.x) + (_rectTransform.rect.width * 0.5f * _canvasRect.localScale.x);
        _leftEdge += _leftEdgeOffset * _canvasRect.localScale.x;
        _rightEdge += _rightEdgeOffset * _canvasRect.localScale.x;
    }
}