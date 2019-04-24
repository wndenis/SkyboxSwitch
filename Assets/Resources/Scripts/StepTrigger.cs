using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StepTrigger : MonoBehaviour
{
    public Step nextStep;

    private Step currentStep;
    // Start is called before the first frame update
    public void GotoNextStep(Player player)
    {
        transform.parent.GetComponent<Step>().Deactivate(player);
        nextStep.gameObject.SetActive(true);
        nextStep.Activate(player);
    }
}
