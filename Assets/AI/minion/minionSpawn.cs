using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class minionSpawn : NetworkBehaviour
{
    public int spawnTime = 5;
    public int numberSpawns;
    public GameObject minionPrefab;
    private IEnumerator coroutine;

   
    public minionManager mManager;

    //public List<GameObject> minions = new List<GameObject>();

    



    private void Start()
    {
       
        if (isServer)
        {
            coroutine = SpawnObject();
            StartCoroutine(coroutine);
            PlayerPrefs.SetInt("numberOfMinions", numberSpawns);
            mManager = GetComponent<minionManager>();
        }
    }

    private void Update()
    {
        
        if (mManager.minions.Count <= numberSpawns && mManager.minions.Count > 0)
        {
            
            NetworkClient.RegisterPrefab(mManager.minions[mManager.minions.Count - 1]);
            NetworkServer.Spawn(mManager.minions[mManager.minions.Count - 1]);
        }
    }



    IEnumerator SpawnObject()
    {
        print("Start Spawing minions");
        for(int i = 0; i < numberSpawns; ++i)
        {
            yield return StartCoroutine("Wait");
            GameObject minionInstance = Instantiate(minionPrefab, 
                SpawnRandomPosition(this.transform.position),Quaternion.identity);
            //NetworkServer.Spawn(minionInstance);
            mManager.minions.Add(minionInstance);
            print("Spawning minions " + i);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(spawnTime);
    }

    private Vector3 SpawnRandomPosition(Vector3 v)
    {
        float x = v.x + Random.Range(10f, 20f);
        float z = v.z + Random.Range(10f, 20f);
        float y = v.y;

        v = new Vector3(x, y, z);
        return v;
    }

}
