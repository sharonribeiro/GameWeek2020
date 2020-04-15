using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private Inventory m_inventory = null;

    [SerializeField] private GameObject m_collectibleItem = null;
    
    public void Initialize(Inventory p_inventory)
    {
        m_inventory = p_inventory;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && m_collectibleItem)
        {
            if (m_inventory.AddNewItem(m_collectibleItem))
            {
                m_collectibleItem.gameObject.SetActive(false);
                m_collectibleItem = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Collectible"))
            return;

        m_collectibleItem = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Collectible"))
            return;
        
        m_collectibleItem = null;
    }
}
