using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float Offset;
    [SerializeField]
    private float OffsetSmoothing;

    [SerializeField]
    private GameObject CurrentPlayerObject;
    private Vector3 playerPosition;

    [SerializeField]
    private Vector2 RoomMinBounds;

    [SerializeField]
    private Vector2 RoomMaxBounds;

    private void Update() 
    {
        playerPosition = new Vector3(CurrentPlayerObject.transform.position.x, 
                                    CurrentPlayerObject.transform.position.y, 
                                    transform.position.z);

        // NVM I dont like this shit
        // // Facing Right
        // if(CurrentPlayerObject.transform.rotation.y >= 0f)
        // {
        //     playerPosition = new Vector3(playerPosition.x + Offset, playerPosition.y, playerPosition.z);
        // }
        // // Facing Left
        // else
        // {
        //     playerPosition = new Vector3(playerPosition.x - Offset, playerPosition.y, playerPosition.z);
        // }

        Vector3 boundPosition = new Vector3(
                Mathf.Clamp(playerPosition.x, RoomMinBounds.x, RoomMaxBounds.x),
                Mathf.Clamp(playerPosition.y, RoomMinBounds.y, RoomMaxBounds.y),
                playerPosition.z);

        transform.position = Vector3.Lerp(transform.position, boundPosition, OffsetSmoothing * Time.deltaTime);
    }

    private void UpdateFollowTarget(GameObject follow)
    {
        CurrentPlayerObject = follow;
    }

    private void UpdateMinBounds(Vector2 newBounds)
    {
        RoomMinBounds = newBounds;
    }

    private void UpdateMaxBounds(Vector2 newBounds)
    {
        RoomMaxBounds = newBounds;
    }
}
