using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenColor : MonoBehaviour
{
    [SerializeField] private GameObject stopper;
    [SerializeField] private Renderer myrenderer;
    [SerializeField] private Material currentMaterial;

    [SerializeField] private List <Material> penMaterials;
    [SerializeField] private List<GameObject> brushes;


    [SerializeField] private Draw draw;

    private void Start()
    {
        if (myrenderer)
        {
            currentMaterial = myrenderer.materials[0];
            Debug.Log(currentMaterial.name);
        }
        else
            Debug.Log("no material attached to pen");

    }

    public void Red()
    {
        draw.brush = brushes[0];

        if (stopper.GetComponent<Renderer>())
        {
            Renderer rend = stopper.GetComponent<Renderer>();
            rend.material = penMaterials[0];
        }
    }

    public void Green()
    {
        draw.brush = brushes[1];
        if (stopper.GetComponent<Renderer>())
        {
            Renderer rend = stopper.GetComponent<Renderer>();
            rend.material = penMaterials[1];
        }

    }

    public void Blue()
    {
        draw.brush = brushes[2];
        if (stopper.GetComponent<Renderer>())
        {
            Renderer rend = stopper.GetComponent<Renderer>();
            rend.material = penMaterials[2];
        }

    }

    public void Black()
    {
        draw.brush = brushes[3];
        if (stopper.GetComponent<Renderer>())
        {
            Renderer rend = stopper.GetComponent<Renderer>();
            rend.material = penMaterials[3];
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
