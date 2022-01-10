using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerDatas;
    public UnityEvent onRecognized;
}

public class GestureDetection : MonoBehaviour
{
    public float threshold = 0.1f;
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    public List<OVRBone> fingerBones;

    [Header("Enable Debug Mode")]
    public bool debugMode = true;

    // Boolean to check if function called properly
    private bool hasStarted = false;
    private bool hasRecognize = false;
    private bool done = false;

    public UnityEvent notRecognize;
    private Gesture previousGesture;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayRoutine(2.5f, Initialize));
        previousGesture = new Gesture();
    }

    public IEnumerator DelayRoutine(float delay, Action actionToDo)
    {
        yield return new WaitForSeconds(delay);
        actionToDo.Invoke();
    }

    public void Initialize()
    {
        fingerBones = new List<OVRBone>(skeleton.Bones);
        // After initialize the skeleton set a boolean to true to confirm the initialization
        hasStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Add new gesture can only be done in debugMode
        if (debugMode && Input.GetKeyDown(KeyCode.Space))
        {
            Save();
        }

        if (hasStarted.Equals(true))
        {
            // Recognize every gesture we made
            Gesture currentGesture = Recognize();

            hasRecognize = !currentGesture.Equals(new Gesture());

            if (hasRecognize)
            {
                // we change another boolean to avoid a loop of event
                done = true;

                // after that i will invoke what put in the Event if is present
                currentGesture.onRecognized?.Invoke();
            }
            else
            {
                if (done)
                {
                    Debug.Log("Not Recognized");
                    // we set to false the boolean again, so this will not loop
                    done = false;

                    // and finally we will invoke an event when we end to make the previous gesture
                    notRecognize?.Invoke();
                }
            }

        }

        // Gesture currentGesture = Recognize();
        // bool hasRecognized = !currentGesture.Equals(new Gesture());
        // //Check if new Gesture
        // if (hasRecognized && !currentGesture.Equals(previousGesture))
        // {
        //     // New Gesture !!
        //     Debug.Log("New Gesture Found: " + currentGesture.name);
        //     previousGesture = currentGesture;
        //     currentGesture.onRecognized.Invoke();

        // }
    }


    void Save()
    {
        Gesture g = new Gesture();
        g.name = "New Gesture...";
        List<Vector3> data = new List<Vector3>();
        foreach (var Bone in fingerBones)
        {
            data.Add(skeleton.transform.InverseTransformPoint(Bone.Transform.position));
        }
        g.fingerDatas = data;
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
                float distance = Vector3.Distance(currentData, gesture.fingerDatas[i]);
                if (distance > threshold)
                {
                    isDiscarded = true;
                    break;
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
