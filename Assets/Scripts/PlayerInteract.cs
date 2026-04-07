using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class PlayerInteract : MonoBehaviour {

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private Transform aimOrigin;

    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    [Min(1f)]
    private float hitRange = 3f;

    [SerializeField]
    private GameObject pickUpUI;

    [SerializeField]
    private Transform holdPoint;

    private GameObject inHanditem;

    private RaycastHit hit;
    private Weapon currentWeapon;

    private void Drop(InputAction.CallbackContext obj) {
        if(inHanditem == null) {
            Debug.Log("No weapon to drop");
            return;
        }

        Debug.Log("Dropping: " + inHanditem.name);

        inHanditem.transform.SetParent(null);
        // inHanditem = null;

        Rigidbody rb = inHanditem.GetComponent<Rigidbody>();
        if(rb != null) {
            rb.isKinematic = false;
        }

        Collider col = inHanditem.GetComponent<Collider>();
        if(col != null) {
            col.enabled = true;
        }

        inHanditem.layer = LayerMask.NameToLayer("Pickable");
        inHanditem = null;


    }  

    private void Interact(InputAction.CallbackContext obj) {
        Debug.Log("Interact pressed");

        if (currentWeapon == null) {
            Debug.Log("No weapon targeted");
            return;
        }

        if (currentWeapon != null) {
            Debug.Log("Targeting: " + currentWeapon.name);
            pickUpUI.SetActive(true);
        }

        if (inHanditem != null) {
            Debug.Log("Already holding: " + inHanditem.name);
            return;
        }

        if (holdPoint == null) {
            Debug.LogError("HoldPoint is not assigned!");
            return;
        }

        inHanditem = currentWeapon.gameObject;
        Debug.Log("Picking up: " + inHanditem.name);

        // Disable physics (optional)
        Rigidbody rb = inHanditem.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.isKinematic = true;
        }
        // Disable collider (optional)
        Collider col = inHanditem.GetComponent<Collider>();
        if (col != null) {
            col.enabled = false;
        }

        inHanditem.layer = LayerMask.NameToLayer("Default");

        // Parent to hand and snap
        inHanditem.transform.SetParent(holdPoint, true);
        inHanditem.transform.position = holdPoint.position;
        inHanditem.transform.rotation = holdPoint.rotation;

        // UI off
        pickUpUI.SetActive(false);
        currentWeapon = null;
    }

    private void Update() {
        pickUpUI.SetActive(false);
        currentWeapon = null;

        if (playerCamera == null || aimOrigin == null) return;

        // 1) Camera ray from screen center
        Ray camRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 targetPoint;

        // Find where the camera is aiming (or a point far away)
        if (Physics.Raycast(camRay, out RaycastHit camHit, 100f))
            targetPoint = camHit.point;
        else
            targetPoint = camRay.origin + camRay.direction * 100f;

        // 2) Ray from player chest toward that point
        Vector3 dir = (targetPoint - aimOrigin.position).normalized;
        Ray playerRay = new Ray(aimOrigin.position, dir);

        Debug.DrawRay(playerRay.origin, playerRay.direction * hitRange, Color.red);

        // 3) Only hit Pickable objects
        if (Physics.Raycast(playerRay, out hit, hitRange, pickableLayerMask))
        {
            currentWeapon = hit.collider.GetComponentInParent<Weapon>();

            if (currentWeapon != null)
                pickUpUI.SetActive(true);
        }

        if(Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame) {
            Interact(default);
        }

        if(Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame) {
            Drop(default);
        }
    }
}
