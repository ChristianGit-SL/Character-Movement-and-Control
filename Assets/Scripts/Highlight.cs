using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Highlight : MonoBehaviour {

    [SerializeField]
    private List<Renderer> renderers;

    [SerializeField]
    private Color color = Color.white;

    //helper list all the materials ofd this object
    private List<Material> materials;

    private void Awake() {
        materials = new List<Material>();
        foreach(var renderer in renderers) {
            // A single child-object might have multiple materials, on it
            // that is why we need to all materials with "s"
            materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    public void ToggleHighlight(bool val) {
        if(val) {
            foreach(var material in materials) {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color);
            }
        }
        else {
            foreach(var material in materials) {
                // we can just disable the EMISSION
                // if we don't use emission color anywhere else
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
