using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private HERO _hero;

    private HeroInputAction _inputActions;

    //private void Awake()
    //{
    //    _inputActions = new HeroInputAction();
    //   _inputActions.Hero.Horizontal.performed += OnHorizontal;
    //    _inputActions.Hero.Horizontal.canceled += OnHorizontal;
    //    _inputActions.Hero.SaySomething.performed += OnSaySomething;
    //}
    //private void OnEnable()
    //{
    //    _inputActions.Enable();
    //}
    //public void OnHorizontal(InputAction.CallbackContext context)
    //{
    //    var direction = context.ReadValue<float>();
    //    _hero.SetDirection(direction);
    //}
    public void On2DMovement(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        _hero.SetDirection(direction);
    }
    public void OnSaySomething(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _hero.SaySomething();
        }
    }
}
 
