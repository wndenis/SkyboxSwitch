#if (UNITY_EDITOR)
using UnityEngine;
using UnityEngine.Video;

[ExecuteInEditMode]
public class EditorDebug : MonoBehaviour
{
    public VideoClip VideoClip;

    private void OnEnable()
    {
        if (Application.isPlaying)
            return;
        print("Switched to " + VideoClip.name);
        var player = GetComponent<Player>();
        var skybox = GetComponent<Skybox>();
        player.VideoPlayer.clip = VideoClip;
        player.VideoPlayer.Play();
        RenderSettings.skybox = skybox.material;
    }

    private void OnDisable()
    {
        if (Application.isPlaying)
            return;
        var player = GetComponent<Player>();
        player.VideoPlayer.Stop();
        RenderSettings.skybox = null;
        player.FixRenderTex();
    }
}
#endif