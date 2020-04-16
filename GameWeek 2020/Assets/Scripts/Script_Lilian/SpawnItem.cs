using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] private GameObject m_itemSpawn = null;
    [SerializeField] private float m_spawnRadius = 2.0f;
    [SerializeField] private float m_spawningRate = 5.0f;
    
    private bool canSpawn = true;

    private void Start()
    {
        StartCoroutine(nameof(StartSpawn));
    }

    IEnumerator StartSpawn()
    {
        WaitForSecondsRealtime timeToWait = new WaitForSecondsRealtime(m_spawningRate);
        CheckIfObjectInRadius();
        if (canSpawn)
        {
            GameObject go = Instantiate(m_itemSpawn);
            go.transform.position = transform.position;
            canSpawn = false;
        }

        yield return timeToWait;
        yield return StartSpawn();
    }
    
    private void CheckIfObjectInRadius()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_spawnRadius);

        if (hitColliders.Length <= 0)
            return;
            
        foreach (var collider in hitColliders)
        {
            if (collider.GetComponent<Collectible>())
            {
                if (collider.GetComponent<Collectible>().name == m_itemSpawn.GetComponent<Collectible>().name)
                {
                    canSpawn = false;
                    return;
                }
            }
        }

        canSpawn = true;
    }
    
}
