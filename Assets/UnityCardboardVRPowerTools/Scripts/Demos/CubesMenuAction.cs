using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesMenuAction : MonoBehaviour
{
    [SerializeField] Material[] materials;

    [SerializeField] GameObject[] cubes;

    private int currentMaterial = 0;

    private void Start() {
        SetCubesMaterial();
    }

    private void SetCubesMaterial() {
        foreach (GameObject cube in cubes) {
            MeshRenderer mesh = cube.GetComponent<MeshRenderer>();
            mesh.material = materials[currentMaterial];
        }
    }

    public void SetNextMaterial() {
        currentMaterial++;
        currentMaterial %= materials.Length;
        SetCubesMaterial();
    }
}
