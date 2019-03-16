using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepTrigger : MonoBehaviour
{
    public Step nextStep;
    // Start is called before the first frame update
    public void GotoNextStep(Skybox skybox)
    {
        nextStep.gameObject.SetActive(true);
        nextStep.Activate(skybox);
        transform.parent.gameObject.SetActive(false);
    }
}
