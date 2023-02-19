using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumWaveSource : MonoBehaviour
{
    [SerializeField] Renderer observerScreenRenderer;

    Material observerMaterial;
    // Start is called before the first frame update
    void Awake()
    {
        observerMaterial = observerScreenRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        observerMaterial.SetVector("ReferencePosition", transform.position);
    }
}
