using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material myMaterial;
    private Renderer renderPlane;

    private Color32 mainColor = new Color32(255,255,255,90);
    // Start is called before the first frame update
    void Start()
    {
        renderPlane = this.GetComponent<Renderer>();
        renderPlane.material.SetColor("_Color", mainColor);
    }

    
}
