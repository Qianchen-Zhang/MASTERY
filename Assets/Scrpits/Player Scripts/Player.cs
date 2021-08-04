using Mirror;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerSetup))]
public class Player : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead = false;

    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private float maxHealth = 100f;

    [SyncVar]
    private float currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;

    [SerializeField]
    private GameObject[] disableGameObjectsOnDeath;

    private bool[] wasEnableOnStart;

    [SerializeField]
    private GameObject deathEffect;

    public void Setup()
    {
        wasEnableOnStart = new bool[disableOnDeath.Length];
        for(int i = 0; i < disableOnDeath.Length; i++)
        {
            wasEnableOnStart[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    public void SetDefaults()
    {
        isDead = false;
        currentHealth = maxHealth;

        for(int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnableOnStart[i];
        }

        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(true);
        }


        Collider col = GetComponent<Collider>();
        if(col != null)
        {
            col.enabled = true;
        }

        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(false);
        }
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTimer);
        SetDefaults();
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

    }


    private void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(999);
        }
    }

    [ClientRpc]
    public void RpcTakeDamage(float amount)
    {
        if(isDead)
        {
            return;
        }

        currentHealth -= amount;
        Debug.Log(transform.name + " a maintenant : " + currentHealth + " points de vie.");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        for(int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(false);
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        GameObject _gfxIns = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(_gfxIns, 3f);

        if(isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);

        }

        Debug.Log(transform.name + " a été éliminé.");

        StartCoroutine(Respawn());
    }
}
