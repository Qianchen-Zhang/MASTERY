using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class minionManager : NetworkBehaviour
{
    public List<GameObject> minions = new List<GameObject>();

    [SyncVar]
    public Vector3 Destination;

    private void Start()
    {
        Destination = new Vector3(Random.Range(-30, 30), 1.08f, Random.Range(-30, 30));
    }


    [Command]
    public void CmdPosition()
    {
        Destination = new Vector3(Random.Range(-30, 30), 1.08f, Random.Range(-30, 30));
    }

    

}
