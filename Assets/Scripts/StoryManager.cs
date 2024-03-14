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
    public int currentChapterIndex = 0;

    //Initializing the list that holds the scriptable objects. 
    public List<Chapters> simChapters = new List<Chapters>();

    CameraController camController;    
    GlobalActionDictionary functionDictionary;
    AudioManager audioManager;
    InteractionManagerV2 interactionManager;
    HighlightManager highlightManager;

    public Dictionary<string, GameObject> gameObjectDictionary;
    Outline outline;

    //bool chapterComplete = false;

    private CinemachineTrackedDolly dolly;
    AudioSource audioSource;

    Animator SJAnim;
    Animator ClipboardAnim;
    GameObject SJ;
    GameObject blockRaycast;
    GameObject SJSludge;
    GameObject sludgeRandomized;

    public float userReadingInputValue;
    public float sludgeValue;
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
        audioManager = gameObjectDictionary["AudioManager"].gameObject.GetComponent<AudioManager>();
        interactionManager = gameObjectDictionary["InteractionManager"].gameObject.GetComponent<InteractionManagerV2>();
        highlightManager = gameObjectDictionary["HighlightManager"].gameObject.GetComponent<HighlightManager>();

        dolly = gameObjectDictionary["Main_vcam"].gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
        SJAnim = gameObjectDictionary["SJ_Water_animated"].gameObject.GetComponent<Animator>();
        ClipboardAnim = gameObjectDictionary["clipBoard"].gameObject.GetComponent<Animator>();
        SJ = gameObjectDictionary["Sludge_judge"].gameObject;
        SJSludge = gameObjectDictionary["sludge"];
        sludgeRandomized = gameObjectDictionary["sludge_randomized"];
        blockRaycast = gameObjectDictionary["BlockRaycast"].gameObject;
        outline = SJ.GetComponent<Outline>();
        audioSource = audioManager.GetComponent<AudioSource>();

        foreach (Chapters chaps in simChapters) 
        {

            chaps.chapterComplete = false;
        }

        // currentChapterIndex = 0;        

        currentChapter = simChapters[currentChapterIndex];

        functionDictionary.GetComponent<GlobalActionDictionary>().SubscribeChapterMethods();
        currentChapter.chapterEvent.Invoke();
        currentChapter.chapterEvent.RemoveAllListeners();

        highlightManager.StartHighlightCoroutine();
    }
    private void Update()
    {
        //Debug.Log("currently at Chapter: " + currentChapterIndex);
        

    }

    public void NextChapter() 
    {
        //Debug.Log("Inside StoryManager.NextChapter()");
        

        interactionManager.target = "No Tag";
        highlightManager.stopHighlight = true;
        

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
        
        SJ.GetComponent<Collider>().enabled = false;
        StartCoroutine(ChapterOneCouroutine());
       
    }

    public void ChapterTwo()
    {
        StartCoroutine(ChapterTwoCouroutine());

    }

    public void ChapterThree()
    {
        StartCoroutine(ChapterThreeCouroutine());

    }
    public void ChapterFour()
    {
        StartCoroutine(ChapterFourCouroutine());

    }
    public void ChapterFive()
    {
        StartCoroutine(ChapterFiveCouroutine());

    }
    public void ChapterSix()
    {
        StartCoroutine(ChapterSixCouroutine());

    }
    IEnumerator ChapterOneCouroutine() 
    {


        Debug.Log("Inside Chapter One");
        Debug.Log("Current chapter interact Object tag is " + currentChapter.interactObjectTag.ToString());
        yield return new WaitForSeconds(1f);
        audioManager.PlayAudio();
        SJ.GetComponent<Collider>().enabled = true;
        

        while (true) 
        {
            if(interactionManager.target == currentChapter.interactObjectTag) 
            {
                
                yield return new WaitForSeconds(.5f);                
                SJAnim.Play("SJ_ToDipSite");
                yield return new WaitForSeconds(3f);
                break;
                
            }
            yield return null;
        }
        
        NextChapter();

        yield return null;
    }

    IEnumerator ChapterTwoCouroutine()
    {


        Debug.Log("Inside Chapter Two");
        Debug.Log("Current chapter interact Object tag is " + currentChapter.interactObjectTag.ToString());
        yield return new WaitForSeconds(1f);
        audioManager.PlayAudio();
        
        

        while (true)
        {
            if (interactionManager.target == currentChapter.interactObjectTag)
            {

                yield return new WaitForSeconds(.5f);
                SJAnim.Play("SJ_Dip");
                yield return new WaitForSeconds(3f);
                break;


            }
            yield return null;
        }

        NextChapter();

        yield return null;
    }

    IEnumerator ChapterThreeCouroutine()
    {


        Debug.Log("Inside Chapter Three");
        Debug.Log("Current chapter interact Object tag is " + currentChapter.interactObjectTag.ToString());
        yield return new WaitForSeconds(1f);
        audioManager.PlayAudio();

        while (true)
        {
            if (interactionManager.target == currentChapter.interactObjectTag)
            {

                yield return new WaitForSeconds(.5f);
                SJAnim.Play("SJ_WithdrawSample");                
                yield return new WaitForSeconds(4f);
                break;

            }
            yield return null;
        }
        //SJSludge.transform.localScale = new Vector3(0.0518035777f, 0f, 0.0518035777f);        
        NextChapter();
        yield return null;
    }

    IEnumerator ChapterFourCouroutine()
    {        
        Debug.Log("Inside Chapter Four");
        Debug.Log("Current chapter interact Object tag is " + currentChapter.interactObjectTag.ToString());
        SJSludge.GetComponent<MeshRenderer>().enabled = false;
        sludgeRandomized.GetComponent<MeshRenderer>().enabled = true;

        sludgeRandomized.transform.localScale = new Vector3(0.0518035777f, Random.Range(.1f, 2.5f), 0.0518035777f);
        

        yield return new WaitForSeconds(1f);
        audioManager.PlayAudio();
        SJAnim.Play("SJ_ReadSample");
        yield return new WaitUntil(() => !audioSource.isPlaying );
        yield return new WaitForSeconds(1f);
        SJAnim.Play("SJ_ReadSample_Reversed");

        Debug.Log("value of sludgeValue is " + sludgeValue);
        Debug.Log("Value of userReadingInputValue is " + userReadingInputValue);



        while (true)
        {
            if (interactionManager.target == currentChapter.interactObjectTag)
            {

                yield return new WaitForSeconds(.5f);
                
                ClipboardAnim.Play("Clipboard_RecordSample");

                if (Mathf.Round(sludgeValue) == Mathf.Round(userReadingInputValue))
                {
                    break;
                }
                /*
                yield return new WaitForSeconds(3f);
                
                break;
                */


            }
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        ClipboardAnim.Play("Clipboard_RecordSample_Reversed");
        NextChapter();
        
        yield return null;
    }

    IEnumerator ChapterFiveCouroutine()
    {


        Debug.Log("Inside Chapter Five");
        Debug.Log("Current chapter interact Object tag is " + currentChapter.interactObjectTag.ToString());
        SJSludge.GetComponent<MeshRenderer>().enabled = false;
        sludgeRandomized.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(1f);
        audioManager.PlayAudio();

        while (true)
        {
            if (interactionManager.target == currentChapter.interactObjectTag)
            {

                yield return new WaitForSeconds(.5f);
                SJAnim.Play("SJ_ReleaseSample");
                yield return new WaitForSeconds(3f);
                break;

            }
            yield return null;
        }


        NextChapter();
        yield return null;
    }

    IEnumerator ChapterSixCouroutine()
    {


        Debug.Log("Inside Chapter Six");
        yield return new WaitForSeconds(1f);
        audioManager.PlayAudio();


        yield return null;
    }



    public void PopulateDictionary() 
    {
        
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterOne", ChapterOne);
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterTwo", ChapterTwo);
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterThree", ChapterThree);
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterFour", ChapterFour);
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterFive", ChapterFive);
        functionDictionary.GetComponent<GlobalActionDictionary>().ActionDict.Add("ChapterSix", ChapterSix);
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

