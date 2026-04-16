using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    private InputActionReference attackInput;

    [SerializeField]
    private Collider weaponCollider;

    private Animator _animator;

    private void OnEnable() {
        if (attackInput != null) {
            attackInput.action.Enable();
            attackInput.action.performed += OnAttack;
        }
    }

    private void OnDisable() {
        if (attackInput != null) {
            attackInput.action.performed -= OnAttack;
        }
    }

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void OnAttack(InputAction.CallbackContext obj) {
        Debug.Log("Attack");
        _animator.SetTrigger("Attack");
    }

    public void EnableWeaponCollider() {
        weaponCollider.enabled = true;
        weaponCollider.GetComponent<BoxCollider>().isTrigger = true;
    }

    public void DisableWeaponCollider() {
        weaponCollider.enabled = false;
        weaponCollider.GetComponent<BoxCollider>().isTrigger = false;
    }
}
