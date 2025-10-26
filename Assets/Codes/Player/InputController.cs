using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        shootAction = InputSystem.actions.FindAction("Attack");
    }

    public Vector2 GetMoveAction()
    {
        return moveAction.ReadValue<Vector2>();
    }

    public Vector2 GetMoveActionOld()
    {
        return new Vector2(Input.GetAxis("Horizontal"), 0f);
    }

    public bool GetJumpAction()
    {
        return jumpAction.WasPressedThisFrame();
    }

    public bool GetShootAction()
    {
        return shootAction.WasPressedThisFrame();
    }
}
