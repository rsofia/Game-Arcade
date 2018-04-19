using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace GameArcade
{
    [RequireComponent(typeof(AudioSource))]
    public class VideoManager : ParentOfAll
    {
        public VideoPlayer videoPlayer;
        public RawImage imageToDisplayVideo;
        private double speed = 0.5f;
        private bool isVideoPaused = false;
        private MenuManager menuManager;

        [Header("UI")]
        public GameObject pauseBtn;
        public GameObject playBtn;

        void Start()
        {
            videoPlayer.playOnAwake = false;

            menuManager = FindObjectOfType<MenuManager>();
        }

        //Reproduce el clip del url
        public void PrenderVideo(string _url)
        {
            videoPlayer.time = 0;
            //Puts bg in black
            imageToDisplayVideo.color = Color.black;

            videoPlayer.source = VideoSource.Url;
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.EnableAudioTrack(0, true);
            videoPlayer.SetTargetAudioSource(0, GetComponent<AudioSource>());
            videoPlayer.url = _url;
            videoPlayer.Prepare();
            AbleToPlay();
            StartCoroutine(WaitToPlayVideo());
        }

        // *** VIDEO CONTROL ***
        // Play : plays video
        public void Play()
        {
            videoPlayer.Play();
            GetComponent<AudioSource>().Play();
            StartCoroutine(WaitForUndoPause());
            AbleToPause();
        }

        // *** VIDEO CONTROL ***
        // Pause : freeze video at current frame
        public void Pause()
        {
            videoPlayer.Pause();
            isVideoPaused = true;
            AbleToPlay();
        }

        // *** VIDEO CONTROL ***
        // Stop : freeze video at frame 0
        public void Stop()
        {
            Debug.Log("Stop!");
            videoPlayer.Pause();
            videoPlayer.time = 0;
        }

        // *** VIDEO CONTROL ***
        // Forward : goes to next x frame, where x is framespeed
        public void FastForward()
        {
            videoPlayer.Pause();
            double currentTime = videoPlayer.time;
            currentTime += speed;
            videoPlayer.time = currentTime;
            isVideoPaused = true;
        }

        // *** VIDEO CONTROL ***
        // Backwards : goes x frames behind
        public void Backwards()
        {
            videoPlayer.Pause();
            double currentTime = videoPlayer.time;
            currentTime -= speed;
            if (currentTime < 0)
                currentTime = 0;
            videoPlayer.time = currentTime;
            isVideoPaused = true;
        }
        

        public void Close()
        {
            Pause();
            Debug.Log("Closing");
            menuManager.CloseSubmenu(3);
        }

        IEnumerator WaitToPlayVideo()
        {
            yield return new WaitForSeconds(1.5f);
            imageToDisplayVideo.color = Color.white;
            imageToDisplayVideo.texture = videoPlayer.texture;

            Play();
            isVideoPaused = false;
            StartCoroutine(WaitUntilVideoEnds());
        }

        IEnumerator WaitUntilVideoEnds()
        {
            yield return new WaitWhile(() => videoPlayer.isPlaying);
            if(videoPlayer.isActiveAndEnabled)
            {
                if (isVideoPaused)
                {
                    StartCoroutine(WaitUntilVideoEnds());
                }
                else
                {
                    Close();
                }
            }
           
        }

        IEnumerator WaitForUndoPause()
        {
            yield return new WaitForSeconds(0.5f);
            isVideoPaused = false;
        }

        // *** USER INTERFAE ** \\
        private void AbleToPause()
        {
            playBtn.SetActive(false);
            pauseBtn.SetActive(true);
        }

        private void AbleToPlay()
        {
            playBtn.SetActive(true);
            pauseBtn.SetActive(false);
        }
    }
}

