using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image Loading;
    public Step FirstStep;
    private Camera _camera;
    private Skybox _skybox;
    private float timer;
    private float delay = 1.5f;
    private StepTrigger currentStepTrigger;
    private int _index;
    // Start is called before the first frame update
    private void Start()
    {
        _camera = GetComponent<Camera>();
        _skybox = GetComponent<Skybox>();
        FirstStep.gameObject.SetActive(true);
        FirstStep.Activate(_skybox);
    }

    // Update is called once per frame
    private void Update()
    {
        var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 15f))
        {
            var stepTrigger = raycastHit.transform.GetComponent<StepTrigger>();
            if (stepTrigger)
                if (currentStepTrigger)
                    if (stepTrigger == currentStepTrigger)
                        timer += Time.deltaTime;
                    else
                    {
                        timer = Time.deltaTime;
                        currentStepTrigger = stepTrigger;
                    }
                else
                    currentStepTrigger = stepTrigger;
        }
        else
            timer = 0;

        if (timer >= delay)
        {
            currentStepTrigger.GotoNextStep(_skybox);
            timer = 0;
        }
            
        Loading.fillAmount = timer / delay;

    }
}
