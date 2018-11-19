using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Arrow : MonoBehaviour
{
    public bool isFired = false;
    public BoxCollider Goldenbow;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFired)
        {
            transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);// make arrow lookat where it is going like an arrow
        }
    }

    public void Fired()
    {
        isFired = true;
    }

    private void OnTriggerStay(Collider col)
    {
        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand)) //if trigger pulled
        {
            ArrowManager.Instance.AttachArrowToBow(); //attach arrow to bow
        }
        //Debug.Log("blaaaaa");
    }

    void OnCollisionEnter(Collision Col)
    {
        if (isFired)
        {
            //Debug.LogError("onCollisionEnterIsFired");
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        if (Col.gameObject.CompareTag("Arrow"))//black list of items not to collide wiht not implemented
        {
            Debug.Log("arrow colided with arrow");
        }
        if (Col.gameObject.CompareTag("Bow"))
        {
            Debug.Log("arrow colided with arrow");
        }
        //Debug.LogError(Col.gameObject.tag);
            //Debug.LogError("Collided with " + Col.gameObject.name);
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

    //void StopArrow()
    //{
    //    gameObject.GetComponent<Rigidbody>().isKinematic = true;
    //}

}
