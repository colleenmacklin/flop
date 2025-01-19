using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pad : MonoBehaviour
{
    public Material paper;
    public List<Texture> letters;

    void change_letter (Texture letter)
    {
        if ((letter != null) && (paper != null))
        {
            paper.SetTexture("_MainTex", letter);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
