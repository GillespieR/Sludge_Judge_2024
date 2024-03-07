using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;
using Cinemachine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class StoryManager : MonoBehaviour
{
    
    
    public GameObject globalDictionaryObject;

    //using this variable to hold the current position in the list
    Chapters currentChapter;
    //Scriptable object list index variable. 
    public int currentChapterIndex;

    //Initializing the list that holds the scriptable objects. 
    public List<Chapters> simChapters = new List<Chapters>();

    CameraController camController;    
    GlobalActionDictionary functionDictionary;
    AudioManager audioManager;

    public Dictionary<string, GameObject> gameObjectDictionary;

    //bool chapterComplete = false;

    private CinemachineTrackedDolly dolly;
    AudioSource audioSource;

    private void Awake()
    {        

        globalDictionaryObject = GameObject.FindWithTag("GlobalDictionaryObject");
        functionDictionary = globalDictionaryObject.GetComponent<GlobalActionDictionary>();
        gameObjectDictionary = globalDictionaryObject.GetComponent<GlobalGameObjectDictionary>().gameObjectDict;        

        PopulateDictionary();       
    }
    private void Start()
    {

        camController = gameObjectDictionary["CameraManager"].gameObject.GetComponent<CameraController>();
        audioSource = gameObjectDictionary["AudioManager"].gameObject.GetComponent<AudioSource>();
        dolly = gameObjectDictionary["Main_vcam"].gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();

        foreach (Chapters chaps in simChapters) 
        {

            chaps.chapterComplete = false;
        }

        currentChapterIndex = 0;

        currentChapter = simChapters[currentChapterIndex];

        functionDictionary.GetComponent<GlobalActionDictionary>().SubscribeChapterMethods();
        currentChapter.chapterEvent.Invoke();
        currentChapter.chapterEvent.RemoveAllListeners();
      
       

        

    }
    private void Update()
    {
        //Debug.Log("currently at Chapter: " + currentChapterIndex);
        
    }

    public void NextChapter() 
    {
        //Debug.Log("Inside StoryManager.NextChapter()");
        

            if (currentChapterIndex < simChapters.Count)
            {
                //currentChapter.chapterEvent.RemoveAllListeners();
                //go to next chapter
                currentChapterIndex++;
                currentChapter = simChapters[currentChapterIndex];
                //subscribe chapter methods
                functionDictionary.GetComponent<GlobalActionDictionary>().SubscribeChapterMethods();
                currentChapter.chapterEvent.Invoke();
                currentChapter.chapterEvent.RemoveAllListeners();
                //await Task.Yield();
            }


        //Debug.Log("Chaper Index is " + currentChapterIndex);

    }

    public void ChapterOne() 
    {
        StartCoroutine(ChapterOneCouroutine());
       
    }

    IEnumerator ChapterOneCouroutine() 
    {
        Debug.Log("Inside Chapter One");
        audioSource.Play();

        yield return null;
    }

    public void PopulateDictionary() 
    {
        
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterOne", ChapterOne);
        /*
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterTwoStory", ChapterTwoStory);
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterThreeStory", ChapterThreeStory);
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterFourStory", ChapterFourStory);
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterFiveStory", ChapterFiveStory);
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterSixStory", ChapterSixStory);
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterSevenStory", ChapterSevenStory);
        */
    }
}

