using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class ArrowManager : MonoBehaviour
{
    //public SteamVR_TrackedObject trackedObj; //old way of doing things before SteamVR 2.0
    public GameObject controller; //the contoller is now just a game object in SteamVR 2.0
    private GameObject currentArrow;
    public float arrowPowerMultiplyer;
    public GameObject arrowPrefab;
    public static ArrowManager Instance;
    public GameObject stringAttachPoint;
    public bool attach;
    public GameObject arrowStartPoint;
    private bool isAttached = false;
    public GameObject stringStartPoint;
    public bool knock;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //AttachArrowToHand();
        PullString();
    }

    private void PullString()
    {
        if (isAttached)
        {
            float dist = (stringStartPoint.transform.position - controller.transform.position).magnitude;
            stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(5f * dist, 0f, 0f); //add string default postion and the offset whe want to apply when pulled back

            if (SteamVR_Input._default.inActions.GrabPinch.GetStateUp(SteamVR_Input_Sources.RightHand))// if release fire
            {
                Fire(dist);
            }
        }
    }

    private void Fire(float strDist)
    {
        currentArrow.transform.parent = null; //detach arrow from all parants
        Rigidbody r = currentArrow.GetComponent<Rigidbody>();
        r.velocity = currentArrow.transform.forward * arrowPowerMultiplyer * strDist;
        r.useGravity = true;
        r.isKinematic = false;

        stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition; //rest string

        currentArrow.GetComponent<Arrow>().Fired();

        currentArrow = null;
        isAttached = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogError("wooow");
        AttachArrowToHand();
    }



    private void AttachArrowToHand()
    {
        if (currentArrow == null)
        {
            currentArrow = Instantiate(arrowPrefab); //create new arrow
            currentArrow.transform.parent = controller.transform; //make arrow child of contoller
            currentArrow.transform.localPosition = new Vector3(0f, 0f, .343f); //offset arrow in hand
            currentArrow.transform.localRotation = arrowPrefab.transform.localRotation;
        }
    }

    public void AttachArrowToBow()
    {
        currentArrow.transform.parent = stringAttachPoint.transform; //get arrow off controller and on to bow
        currentArrow.transform.position = arrowStartPoint.transform.position; //allign arrow to correct postion on bow
        currentArrow.transform.rotation = arrowStartPoint.transform.rotation; //allign arrow to correct roataion
        isAttached = true;
    }
}
