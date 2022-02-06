using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offSet;
    [Range(1, 10)] public float smoothFactor;

    private Vector3 smoothFollow;
    private Vector3 playerPosition;

    private Vector3 camPosition;
    public float mostLeftSide;
    public float mostRightSide;
    public float mostBottom;
    public float mostTop;

    private void Update()
    {
        camPosition = transform.position;
    }
    private void LateUpdate()
    {
        Follow();
    }
    void Follow()
    {
        //CamPosition = transform.position; //position of camera                                
        // CamPosition.x = player.position.x;
        // CamPosition.y = player.position.y + 5;
       if (camPosition.x < mostLeftSide)
        {
            camPosition.x = mostLeftSide;
        }
        if (camPosition.x > mostRightSide)
        {
            camPosition.x = mostRightSide;
        }
        if (camPosition.y < mostBottom)
        {
            camPosition.y = mostBottom;
        }
        if (camPosition.y > mostTop)
        {
            camPosition.y = mostTop;
        }


        playerPosition = player.position + offSet;
        smoothFollow = Vector3.Lerp(camPosition, playerPosition, smoothFactor * Time.fixedDeltaTime); //make camera have a slight delay when following the player
        transform.position = smoothFollow;

    }
}
