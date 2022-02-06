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


    private void LateUpdate()
    {
        Follow();
    }
    void Follow()
    {
        //CamPosition = transform.position; //position of camera                                
        // CamPosition.x = player.position.x;
        // CamPosition.y = player.position.y + 5;

        playerPosition = player.position + offSet;
        smoothFollow = Vector3.Lerp(transform.position, playerPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothFollow;

    }
}
