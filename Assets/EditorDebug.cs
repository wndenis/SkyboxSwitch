#if (UNITY_EDITOR)
using UnityEngine;
using UnityEngine.Video;

[ExecuteInEditMode]
public class EditorDebug : MonoBehaviour
{
    public VideoClip VideoClip;
    public VideoPlayer VideoPlayer;
    // Update is called once per frame
    private void Update()
    {
        print("Switched to " + VideoClip.name);
        var skybox = GetComponent<Skybox>();
        VideoPlayer.clip = VideoClip;
        VideoPlayer.Play();
        RenderSettings.skybox = skybox.material;
    }
}
#endif