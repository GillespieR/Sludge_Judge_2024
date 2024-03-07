using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    StoryManager storyManager;
    AudioSource audioSource;

    public List<AudioClip> audioClips = new List<AudioClip>();


    // Start is called before the first frame update
    void Start()
    {
        storyManager = GameObject.FindWithTag("StoryManager").GetComponent<StoryManager>();
        audioSource = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying) 
        {
            SetAudio();
        }
        
    }

    public void SetAudio() 
    {
        int currentChapter = storyManager.currentChapterIndex;

        switch (currentChapter) 
        {
            case 0:
                audioSource.clip = audioClips[0];
                break;
            case 1:
                audioSource.clip = audioClips[1];
                break;
            case 2:
                audioSource.clip = audioClips[2];
                break;
            case 3:
                audioSource.clip = audioClips[3];
                break;
            case 4:
                audioSource.clip = audioClips[4];
                break;
            case 5:
                audioSource.clip = audioClips[5];
                break;


        }
    }
}
