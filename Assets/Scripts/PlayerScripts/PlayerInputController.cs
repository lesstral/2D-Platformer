using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private InputSystem_Actions _gameInput;
    private IControllable _controllable;

    private void Awake()
    {
        _gameInput = new InputSystem_Actions();

        _controllable = GetComponent<IControllable>();
        if (_controllable == null)
        {
            throw new System.Exception(message: $"There's no Icontrollable: {gameObject.name}");
        }
    }

    private void OnEnable()
    {
        _gameInput.Enable();
        _gameInput.Player.Jump.performed += OnJumpPerformed;
        _gameInput.UI.OpenMenu.performed += OpenMenu;
        //_gameInput.Player.Interact.performed += OnInteractPerformed;
        // _gameInput.Player.Sprint.started += OnSprintStarted;
        //_gameInput.Player.Sprint.canceled += OnSprintCanceled;
        //_gameInput.Player.Aim.started += OnAimStarted;
        //_gameInput.Player.Aim.canceled += OnAimCanceled;
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        _controllable.Jump();
    }

    private void OpenMenu(InputAction.CallbackContext obj)
    {
        Events.UIEvents.MenuOnKeyOpen.Publish();
    }

    private void OnDisable()
    {
        _gameInput.Disable();
        _gameInput.Player.Jump.performed -= OnJumpPerformed;
        _gameInput.UI.OpenMenu.performed -= OpenMenu;
        //_gameInput.Player.Sprint.started -= OnSprintStarted;
        // _gameInput.Player.Sprint.canceled -= OnSprintCanceled;
        // _gameInput.Player.Interact.performed -= OnInteractPerformed;


        _gameInput.Disable();
    }


    private void Update()
    {
        Vector2 moveInput = _gameInput.Player.Move.ReadValue<Vector2>();

        Vector3 move = new Vector2(moveInput.x, 0).normalized;
        _controllable.Move(move);



    }
}
