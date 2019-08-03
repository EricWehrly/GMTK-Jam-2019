using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_teleport : MonoBehaviour
{
    private Transform PlayerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                PlayerTransform = GameObject.Find("FPSController").transform;
                PlayerTransform.position = hit.point;
            }
        }
    }
}
