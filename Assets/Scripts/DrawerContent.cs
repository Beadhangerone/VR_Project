using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawerContent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject theOther = other.gameObject;
        
        if (theOther.GetComponent<Grabbable>() != null)
        {
            Vector3 oldLocalScale = theOther.transform.localScale;
            Vector3 lossyScale = theOther.transform.lossyScale;
            //theOther.transform.localScale = Vector3.one;
            theOther.transform.parent = gameObject.transform;
            theOther.transform.rotation = Quaternion.identity;
            theOther.transform.localScale = gameObject.transform.InverseTransformVector(lossyScale);
        }

    }

    public void OnTriggerExit(Collider other)
    {
        GameObject theOther = other.gameObject;
        
        if (theOther.GetComponent<Grabbable>() != null)
        {
            Vector3 oldLocalScale = theOther.transform.localScale;
            Vector3 lossyScale = theOther.transform.lossyScale;
            theOther.transform.parent = null;
            //theOther.transform.localScale = Vector3.one;
            theOther.transform.localScale = gameObject.transform.TransformVector(oldLocalScale);
        }
    }
}

