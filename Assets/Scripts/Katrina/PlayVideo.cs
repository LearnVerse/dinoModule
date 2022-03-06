using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayVideo : NetworkBehaviour
{
    public GameObject videoCanvas;
    private UnityEngine.Video.VideoPlayer videoPlayer;
    public Canvas video;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = videoCanvas.GetComponentInChildren<UnityEngine.Video.VideoPlayer>();
        // video = gameObject.GetComponent<VideoPlayer>();
        // StartCoroutine(PlayThisVideoNow());

    }

    // Update is called once per frame
    public void PlayThisVideoNow()
    {
        if(isLocalPlayer) {
            Debug.Log("Playing video");
            StartCoroutine(AnimationPlayVideo());      
        }
    }

    IEnumerator AnimationPlayVideo() {
        video.enabled = true; 
        videoPlayer.Play();
        yield return new WaitForSeconds(59f);
        videoPlayer.Stop();
        video.enabled = false;
    }
}
