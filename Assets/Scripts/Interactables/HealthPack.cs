using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public static event Action<int> E_CollectedHealth;
    public int healAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            E_CollectedHealth?.Invoke(healAmount);
        }
    }
}
