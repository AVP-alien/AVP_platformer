using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private HERO _hero;

    private HeroInputAction _inputActions;
    
    private void Awake()
    {
        _inputActions = new HeroInputAction();
        _inputActions.Hero._2DMovement.performed += On2DMovement;
        _inputActions.Hero._2DMovement.canceled += On2DMovement;
        _inputActions.Hero.SaySomething.performed += OnSaySomething;
    }
    private void OnEnable()
    {
        _inputActions.Enable();
    }

    
    public void On2DMovement(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<Vector2>();
        _hero.SetDirection(direction);
    }
    public void OnSaySomething(InputAction.CallbackContext context)
    {
       _hero.SaySomething();
    }
}
 
