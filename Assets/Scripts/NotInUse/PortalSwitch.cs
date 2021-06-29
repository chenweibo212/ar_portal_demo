using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class PortalSwitch : MonoBehaviour
{
    public Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var mat in materials)
        {
            mat.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name != "Main Camera")
        {
            return;
        }

        //outside of other world
        if (transform.position.z > other.transform.position.z)
        {
            Debug.Log("Outside");
            foreach (var mat in materials)
            {
                mat.SetInt("_StencilTest", (int)CompareFunction.Equal);
            }
        }
        //inside 
        else
        {
            Debug.Log("Inside");
            foreach (var mat in materials)
            {
                mat.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
            }
        }
    }
    void OnDestroy()
    {
        foreach (var mat in materials)
        {
            mat.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
