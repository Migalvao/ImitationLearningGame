using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public Material successMaterial;
    public Material failedMaterial;

    public void setSuccessMaterial()
    {
        this.GetComponent<MeshRenderer>().material = successMaterial;
    }

    public void setFailedMaterial()
    {
        this.GetComponent<MeshRenderer>().material = failedMaterial;
    }
}
