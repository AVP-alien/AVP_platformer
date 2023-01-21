using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private HERO _hero;

    
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
 
