using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerData;
    public UnityEvent onRecognized;
    
}
public class GestureRecognition : MonoBehaviour
{
    
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    private List<OVRBone> fingerBones;
    public float detectionThreshold = 0.1f;
    public bool isSaveNewGestures = true;
    private bool hasStarted = false;
    private Gesture previosGesture;
    void Start()
    {
        StartCoroutine(StartUpDelay(2.5f, Initialize));
        previosGesture = new Gesture();
    }
    public void Initialize()
    {
        GetSkeleton();
        if (fingerBones.Count > 0)
        {
            hasStarted = true;
        }
        else
        {
            StartCoroutine(StartUpDelay(1f, Initialize)); 
        }
    }

    public void GetSkeleton()
    {
        fingerBones = new List<OVRBone>(skeleton.Bones);
        Debug.LogError(fingerBones.Count);
       foreach (var bones in fingerBones)
       {
           Debug.LogError(bones.Transform);
       }
    }
    
    public IEnumerator StartUpDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            if (isSaveNewGestures && Input.GetKeyDown(KeyCode.Space))
            {
                SaveGesture();
            }
        
            Gesture currentGesture = Recognize();
            bool hasRecognized = !currentGesture.Equals(new Gesture());

            if (hasRecognized && !currentGesture.Equals(previosGesture))
            {
                Debug.LogWarning("New Gesture Found: " + currentGesture.name);
                previosGesture = currentGesture;
                currentGesture.onRecognized.Invoke();
            }  
        }
    }

    void SaveGesture()
    {
        Gesture g = new Gesture();
        g.name = "New Gesture";
        List<Vector3> fingerData = new List<Vector3>();
        foreach (var bone in fingerBones)
        {
            fingerData.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
            
        }

        g.fingerData = fingerData;
        gestures.Add(g);
    }

    Gesture Recognize()
    {
        Gesture currentGesture = new Gesture();
        float currentMin = Mathf.Infinity;
        foreach (var gesture in gestures)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for (int i = 0; i < fingerBones.Count; i++)
            {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerData[i]);
                if (distance > detectionThreshold)
                {
                    isDiscarded = true;
                }
                sumDistance += distance;
            }

            if (!isDiscarded && sumDistance < currentMin)
            {
                currentMin = sumDistance;
                currentGesture = gesture;
            }
        }

        return currentGesture;
    }
}
