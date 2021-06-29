using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class PortalManagerTest : MonoBehaviour
{
    public GameObject InnerWorld;
    public GameObject OtherInnerWorld;
    //This materials matter needs to be optimizated!
    public Material[] materials;
    public Material[] materials_Other;
    private Vector3 camPostionInPortalSpace;
    bool wasInFront;
    bool inOtherWorld;
    bool hasCollided;
    // Start is called before the first frame update
    void Start()
    {
        SetMaterials(false);
        SetOtherMaterial(true);
    }
    void SetMaterials(bool fullRender)
    {
        var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;
        foreach (var mat in materials)
        {
            mat.SetInt("_StencilComp", (int)stencilTest);
        }
    }

    void SetOtherMaterial(bool fullRender)
    {
        var otherStencilTest = fullRender ? CompareFunction.Equal : CompareFunction.NotEqual;
        foreach (var mat in materials_Other)
        {
            mat.SetInt("_StencilComp", (int)otherStencilTest);
        }
    }
    //Set bidirectional function
    bool GetIsInFront()
    {
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 worldPos = MainCamera.transform.position + MainCamera.transform.forward * Camera.main.nearClipPlane;
        camPostionInPortalSpace = transform.InverseTransformPoint(worldPos);
        return camPostionInPortalSpace.y >= 0 ? true : false;
    }
    private void OnTriggerEnter(Collider collider)
    {
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (collider.transform != MainCamera.transform)
            return;
        wasInFront = GetIsInFront();
        hasCollided = true;
    }
    // Update is called once per frame
    void OnTriggerExit(Collider collider)
    {
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (collider.transform != MainCamera.transform)
            return;
        hasCollided = false;
    }
    void whileCameraColliding()
    {
        if (!hasCollided)
            return;
        bool isInFront = GetIsInFront();
        if ((isInFront && !wasInFront) || (wasInFront && !isInFront))
        {
            inOtherWorld = !inOtherWorld;
            SetMaterials(inOtherWorld);
            SetOtherMaterial(inOtherWorld);
        }
        wasInFront = isInFront;
    }
    private void OnDestroy()
    {
        SetMaterials(true);
    }
    private void Update()
    {
        whileCameraColliding();
    }
}