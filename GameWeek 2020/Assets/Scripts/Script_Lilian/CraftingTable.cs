using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    [SerializeField] private Canvas m_blueprint = null;
    [SerializeField] private Inventory m_playerInventory = null;
    private GameObject _gameObject;

    // Start is called before the first frame update
    void Start()
    {
        if (m_blueprint)
            m_blueprint.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        m_playerInventory = other.GetComponent<Inventory>();
        
        if(m_blueprint)
            m_blueprint.gameObject.SetActive(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        m_playerInventory = null;
        
        if(m_blueprint)
            m_blueprint.gameObject.SetActive(false);
    }

    public void CrafItem(BluePrint p_itemPrefab)
    {
        if (!CheckInventory(p_itemPrefab))
            return;
        
        GameObject go = Instantiate(p_itemPrefab.gameObject);
        go.transform.position = transform.position + transform.up;
    }

    private bool CheckInventory(BluePrint p_itemPrefab)
    {
        //will make sure to check if player have the two item needed to construct object
        //for now we will just make sure that the player have at least two object

        if (!m_playerInventory)
            return false;
        
        //now check for the two item
        var tmpInv = m_playerInventory.GetInventory();

        int firstItemLocation = -1;
        int secondItemLocation = -1;
        
        //first check for the first item
        for (int i = 0; i < tmpInv.Count; ++i)
        {
            if (!tmpInv[i])
                continue;
                
            if (tmpInv[i].GetComponent<Collectible>().name == p_itemPrefab.m_firstItem.name)
            {
                firstItemLocation = i;
                break;
            }
        }

        //second check for the second item
        for (int i = 0; i < tmpInv.Count; ++i)
        {
            if (i == firstItemLocation)
                continue;
            
            if (!tmpInv[i])
                continue;
            
            if (tmpInv[i].GetComponent<Collectible>().name == p_itemPrefab.m_secondItem.name)
            {
                secondItemLocation = i;
                break;
            }
        }
        
        if (firstItemLocation < 0 || secondItemLocation < 0)
            return false;
        
        m_playerInventory.RemoveAt(firstItemLocation);
        m_playerInventory.RemoveAt(secondItemLocation);
        
        return true;
    }
}
