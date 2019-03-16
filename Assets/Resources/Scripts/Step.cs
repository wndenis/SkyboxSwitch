using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Step : MonoBehaviour
{
    public Material skyboxMaterial;

    public void Activate(Skybox skybox)
    {
        skybox.material = skyboxMaterial;
    }
}
