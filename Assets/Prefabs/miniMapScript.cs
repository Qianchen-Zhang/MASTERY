using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class miniMapScript : MonoBehaviour
{

    public Transform playerPosition;

    public PlayerSetup ps;

    private void Awake()
    {
        ps = NetworkClient.connection.identity.GetComponent<PlayerSetup>();

    }



    public void LateUpdate()
    {
        Vector3 newPosition = playerPosition.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, playerPosition.eulerAngles.y, 0f);
    }

}
