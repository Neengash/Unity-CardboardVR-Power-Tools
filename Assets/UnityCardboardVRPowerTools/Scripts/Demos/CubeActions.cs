using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeActions : MonoBehaviour
{
    [SerializeField]
    Material[] Materials;

    private int currentMaterial;

    MeshRenderer meshRenderer;

    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        setRandomMaterial();
    }

    public void setMaterial(int i) {
        if (i < Materials.Length) {
            meshRenderer.material = Materials[i];
        }
        currentMaterial = i;
    }

    public void setRandomMaterial() {
        int i = Random.Range(0, Materials.Length-1);
        if (i == currentMaterial) { i = Materials.Length -1;}
        setMaterial(i);
    }

    public void DesroySelf() {
        Destroy(this.gameObject);
    }
}
