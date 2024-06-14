using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform[] spawners;
    public GameObject player;
    private void Start()
    {

    }

    void SpawnPlayer()
    {
        Instantiate(player, spawners[Random.Range(0, spawners.Length + 1)].transform.position, Quaternion.identity);
    }
}
