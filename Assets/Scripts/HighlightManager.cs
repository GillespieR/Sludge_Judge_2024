using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : MonoBehaviour
{

    // Blends between two materials
    [SerializeField]
    Material material1;
    [SerializeField]
    Material material2;

    GameObject glowGameObject;
    
    GameObject glowTarget;

    public float duration = 2.0f;
    public float intensity = 0.0f;
    MeshRenderer rend;

    Chapters currentChapter;

   
    //private bool goingUp;
    //private float a = .05f;
    //private Color matColor;
    void Start()
    {
           
        
    }

    void Update()
    {
        /*
        matColor.a = goingUp ? .03f + matColor.a : matColor.a - .03f;

        if (material1.color.a >= 1f)
            goingUp = false;
        else if (material1.color.a <= 00)
            goingUp = true;
        */

        // ping-pong between the materials over the duration



    }

    public void StartHighlightCoroutine(GameObject glowGameObject) 
    {
        StartCoroutine(StartHighlightCycle(glowGameObject));
    }

    public IEnumerator StartHighlightCycle(GameObject glowGameObject)
    {
        while (true) 
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            glowGameObject.GetComponent<MeshRenderer>().material.Lerp(material1, material2, lerp);
            yield return null;
        }
    }

    public IEnumerator MaterialFade(Material objMaterial) 
    {
        for (float f = 1f; f >= 0; f -= 0.001f)
        {
            if (f <= 0.01f)
            {
                Color col = objMaterial.color;
                col.a = 0f;
            }
            Color c = objMaterial.color;
            c.a = f;
            objMaterial.color = c;

            yield return null;
        }
        yield return null;
    }
}
