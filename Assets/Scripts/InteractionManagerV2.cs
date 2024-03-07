using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManagerV2 : MonoBehaviour
{
    //interactabe object layer mask. Set in the editor. When this layer mask is on the object the mouse can interact with it. 
    public LayerMask mouseColliderLayerMask;

    StoryManager storyManager;

    Camera cam;
    //object moving
    public string target;

    GameObject wrongTarget;


    private void Awake()
    {
        storyManager = GameObject.FindWithTag("StoryManager").GetComponent<StoryManager>();
        //camera reference
        cam = Camera.main;
    
    }

    private void Start()
    {
        //cursor is confined to game window
        Cursor.lockState = CursorLockMode.Confined;

        //mouse cursor is visible
        Cursor.visible = true;

        wrongTarget = storyManager.gameObjectDictionary["WrongTarget"].gameObject;

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = GetMouseTarget(mouseColliderLayerMask);
        }
        
    }

    //function used to get the mouse target and if the target is the in the interactable layer mask to activate the MouseFollowPosition
    //coroutine
    public string GetMouseTarget(LayerMask mouseColliderLayerMask)
    {
        //shoots a ray from the mouse position, converting it to on screen coordinates. We will set the ray variable to this information
        //which we use in the raycast below
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //returns the gameobject we hit based on the information from the ray above. If a gameobject with the proper layer mask is returned,
        //sets follow to true to begin the MouseFollowPosition() coroutine, otherwise returns nothing  
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            Debug.Log("Hit an interactable target. Clicked on " + raycastHit.transform.gameObject.tag);
            return raycastHit.transform.gameObject.tag;
        }
        else
        {
            Debug.Log("Target is not interactable. Incorrect Target");
            return wrongTarget.tag;
        }

    }

}
