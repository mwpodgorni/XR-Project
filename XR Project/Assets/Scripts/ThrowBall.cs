using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class ThrowBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="ball")
        {
            Category.score++;
            Destroy(col.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
