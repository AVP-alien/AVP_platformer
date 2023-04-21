using AVPplatformer.Components;
using AVPplatformer.Creatures;
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

    public void Oninteract(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _hero.Interact();
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _hero.Attack();
        }
    }
    public void OnThrow(InputAction.CallbackContext context)
    {

        // if (context.started)
        // {
        //     _hero.ThrowBurst();
        // }
        //else 
        if (context.canceled)
        {
            _hero.Throw();
        }
    }
    //public void OnHeal(InputAction.CallbackContext context)
    //{
    //    if (context.canceled)
    //    {
    //        _hero.Heal();
    //    }
    //}
}
    //{
    //    // Подписываемся на события ввода, передавая имя действия и метод, который будет вызываться
    //    // при срабатывании события.
    //    GetComponent<PlayerInput>().actions["2DMovement"].canceled += On2DMovement;
    //    GetComponent<PlayerInput>().actions["Interact"].canceled += OnInteract;
    //    GetComponent<PlayerInput>().actions["Attack"].canceled += OnAttack;
    //    GetComponent<PlayerInput>().actions["Throw"].canceled += OnThrow;
    //}

    //private void OnDisable()
    //{
    //    // Отписываемся от событий ввода при отключении компонента.
    //    GetComponent<PlayerInput>().actions["2DMovement"].canceled -= On2DMovement;
    //    GetComponent<PlayerInput>().actions["Interact"].canceled -= OnInteract;
    //    GetComponent<PlayerInput>().actions["Attack"].canceled -= OnAttack;
    //    GetComponent<PlayerInput>().actions["Throw"].canceled -= OnThrow;
    //}

    //public void On2DMovement(InputAction.CallbackContext context)
    //{
    //    var direction = context.ReadValue<Vector2>();
    //    _hero.SetDirection(direction);
    //}

    //public void OnInteract(InputAction.CallbackContext context)
    //{
    //    _hero.Interact();
    //}

    //public void OnAttack(InputAction.CallbackContext context)
    //{
    //    _hero.Attack();
    //}

    //private void OnThrow(InputAction.CallbackContext context)
    //{
    //    _hero.Throw();
    //}


