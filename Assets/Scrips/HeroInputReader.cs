﻿using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] private HERO _hero;

    private HeroInputAction _inputActions;

    private void Awake()
    {
        _inputActions = new HeroInputAction();
       _inputActions.Hero.Horizontal.performed += OnHorizontal;
        _inputActions.Hero.Horizontal.canceled += OnHorizontal;
        _inputActions.Hero.SaySomething.performed += OnSaySomething;
    }
    private void OnEnable()
    {
        _inputActions.Enable();
    }
    private void OnHorizontal(InputAction.CallbackContext context)
    {
        var direction = context.ReadValue<float>();
        _hero.SetDirection(direction);
    }

    private void OnSaySomething(InputAction.CallbackContext context)
    {
        _hero.SaySomething();

    }
}

