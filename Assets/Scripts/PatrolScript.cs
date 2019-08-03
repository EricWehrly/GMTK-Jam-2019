using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    public Transform StartPosition;
    public Transform EndPosition;
    public float PatrolTime = 60;

    private Vector3 startPos;
    private Vector3 endPos;

    private float time;
    private float reverseDistance = 1;

    void Start()
    {
        if (endPos == null)
        {
            Debug.LogError("PatrolScript not configured with valid end position");
            this.enabled = false;
        }

        if (StartPosition == null) startPos = transform.position;
        else startPos = StartPosition.position;

        endPos = EndPosition.position;
    }

    void Update()
    {
        time += Time.deltaTime / PatrolTime;
        transform.position = Vector3.Lerp(startPos, endPos, time);

        if(Vector3.Distance(transform.position, endPos) < reverseDistance) 
        {
            Vector3 placeholder = startPos;
            startPos = endPos;
            endPos = placeholder;
            time = 0;

            transform.LookAt(endPos);
        }
    }
}
