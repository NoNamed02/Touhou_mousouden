using UnityEngine;
using UnityEngine.Video;

public class IdleManager : MonoBehaviour
{
    public float idleTimeThreshold = 10f; // 10초 이상 입력이 없으면
    public VideoPlayer videoPlayer; // 비디오 플레이어 컴포넌트
    private float idleTimer = 0f;

    void Start()
    {
        videoPlayer.gameObject.SetActive(false); // 처음에는 비디오 비활성화
    }

    void Update()
    {
        if (Input.anyKey || Input.mousePosition != Vector3.zero)
        {
            ResetIdleTimer();
        }
        else
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleTimeThreshold)
            {
                PlayVideo();
            }
        }
    }

    void ResetIdleTimer()
    {
        idleTimer = 0f;
        if (videoPlayer.isPlaying)
        {
            StopVideo();
        }
    }

    void PlayVideo()
    {
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    void StopVideo()
    {
        videoPlayer.Stop();
        videoPlayer.gameObject.SetActive(false);
    }
}
