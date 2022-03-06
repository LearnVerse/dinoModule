using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideo : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        // video = gameObject.GetComponent<VideoPlayer>();
        StartCoroutine(PlayThisVideoNow());

    }

    // Update is called once per frame
    public IEnumerator PlayThisVideoNow()
    {
        yield return new WaitForSeconds(10f);
        videoPlayer.Play();
    }

}
