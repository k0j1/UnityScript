using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    private Vector3 distance;
    public GameObject RotateCenterObj;
    public GameObject gameSystem;

    void Start()
    {
        distance = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        //transform.position = target.transform.position + distance;
        GameSystem GameSys = gameSystem.GetComponent<GameSystem>();
        transform.RotateAround(RotateCenterObj.transform.position, Vector3.up, GameSys.walkSpeed * Time.deltaTime);
    }
}
