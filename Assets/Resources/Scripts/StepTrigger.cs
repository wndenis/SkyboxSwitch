using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StepTrigger : MonoBehaviour
{
    public Step NextStep;
    private Step _currentStep;
    
    // must have parent!
    public void GotoNextStep(Player player)
    {
        var step = transform.parent.GetComponent<Step>();
        if (step != null)
            step.Deactivate(player);
        else
            transform.parent.gameObject.SetActive(false);
        NextStep.gameObject.SetActive(true);
        NextStep.Activate(player);
    }
}
