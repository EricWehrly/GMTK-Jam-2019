using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private CharacterController _controller;
    Transform target;

    public float MoveSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        // if no character controller, make one?
        target = GameObject.FindWithTag("Player").transform;
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;

        direction = direction.normalized;

        Vector3 velocity = direction * MoveSpeed;

        transform.LookAt(target);
        _controller.Move(velocity * Time.deltaTime);
    }
}
