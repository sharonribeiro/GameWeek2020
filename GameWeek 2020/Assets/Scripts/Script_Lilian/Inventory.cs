using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private float m_dropDistance = 2.0f;
    [SerializeField] private int m_maxInventory = 4;
    
    [SerializeField] private List<GameObject> m_inventoryList = new List<GameObject>();
    [SerializeField] private int m_itemSelection = 0;

    public List<GameObject> GetInventory()
    {
        return m_inventoryList; 
    }
    
    private void Start()
    {
        for (int i = 0; i < m_maxInventory; ++i)
            m_inventoryList.Add(null);
        
        var ps = gameObject.AddComponent<PickUpSystem>();
        ps.Initialize(this);
    }

    public bool AddNewItem(GameObject m_item)
    {
        for (int i = 0; i < m_maxInventory; ++i)
        {
            if (!m_inventoryList[i])
            {
                m_inventoryList[i] = m_item;
                return true;
            }
        }

        Debug.Log("no place for item");
        return false;
    }

    private void Update()
    {
        SelectItem();
        
        if (Input.GetKeyDown(KeyCode.Q))
            ThrowSelectedItem(m_itemSelection);
    }

    private void SelectItem()
    {
        int newSelection = m_itemSelection + (int)(Input.GetAxis("Mouse ScrollWheel") * 10.0f) + 4;
        m_itemSelection = newSelection % m_maxInventory;
    }

    private void ThrowSelectedItem(int p_id)
    {
        if (!m_inventoryList[p_id])
            return;
        
        Vector3 dropingDistance = transform.forward * m_dropDistance;
        GameObject go = m_inventoryList[p_id];
        go.transform.position = (transform.position + dropingDistance);
        go.SetActive(true);
        m_inventoryList[p_id] = null;
    }
}
