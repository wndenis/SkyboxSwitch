﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    public Image Loading;
    public VideoPlayer VideoPlayer;

    private float _transitionDuration = 0.5f;
    private float _timer;
    private float delay = 2.25f;
    private StepTrigger _currentStepTrigger;
    
//    private Vignette _vignette;
    private ColorGrading _colorGrading;
    
    private int _index;
    private bool _transitioning;
    // Start is called before the first frame update
    private void Start()
    {
        var pp = GetComponent<PostProcessVolume>().profile;
        pp.TryGetSettings(out _colorGrading);
        //FirstStep.gameObject.SetActive(true);
        //FirstStep.Activate(this);
    }

    // Update is called once per frame
    private void Update()
    {
//        var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        var ray = new Ray(transform.position, transform.forward);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 15f))
        {
            var stepTrigger = raycastHit.transform.GetComponent<StepTrigger>();
            if (stepTrigger)
                if (_currentStepTrigger)
                    if (stepTrigger == _currentStepTrigger)
                        _timer += Time.deltaTime;
                    else
                    {
                        _timer = Time.deltaTime;
                        _currentStepTrigger = stepTrigger;
                    }
                else
                    _currentStepTrigger = stepTrigger;
        }
        else
            _timer = 0;

        if (_timer >= delay)
        {
            StartCoroutine(Transition());
            _timer = 0;
        }
            
        Loading.fillAmount = _timer / delay;
    }

    private IEnumerator Transition()
    {
        if (_transitioning)
            yield break;
        _transitioning = true;
        var ev0 = 0f;
        var ev1 = -15f;
        _currentStepTrigger.GotoNextStep(this);
        for (var t = 0f; t < _transitionDuration; t += Time.deltaTime)
        {
            _colorGrading.postExposure.value = Mathf.Lerp(ev0, ev1, t / _transitionDuration);
            if (VideoPlayer.isPrepared && t > 0.1f)
                break;
            yield return null;
        }
        _colorGrading.postExposure.value = ev1;
        yield return new WaitUntil(() => VideoPlayer.isPrepared);
        var rt = _transitionDuration * 0.65f;
        for (var t = 0f; t < rt; t += Time.deltaTime)
        {
            _colorGrading.postExposure.value = Mathf.Lerp(ev1, ev0, t / rt);
            yield return null;
        }
        _colorGrading.postExposure.value = ev0;
        _transitioning = false;
    }

    public void FixRenderTex()
    {
        var rt = RenderTexture.active;
        RenderTexture.active = GetComponent<Skybox>().material.mainTexture as RenderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
    }

    private void OnDestroy()
    {
        FixRenderTex();
    }
}
