using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class PlayerInteract : MonoBehaviour {
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private GameObject pickUpUI;

    private RaycastHit hit;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    // [SerializeField]
    // private GameObject inHanditem;

    [SerializeField]
    private InputActionReference interactionInput, dropInput, useInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        interactionInput.action.performed += Interact;
        dropInput.action.performed += Drop;
        useInput.action.performed += Use;
    }

    // Update is called once per frame
    void Update() {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
        if(hit.collider != null) {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);
        }

        if(Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask)) {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickUpUI.SetActive(true);
        }
    }
}
