using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    private InputActionReference attackInput;

    // private bool _isAttacking;

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
        // if(_isAttacking) {
        //     return;
        // }

        Debug.Log("Attack");
        _animator.SetTrigger("Attack");
    }

    // private IEnumerator Hit() {
    //     _isAttacking = true;
    //     yield return new WaitForSeconds(2.5f);
    //     _isAttacking = false;
    // }
}
