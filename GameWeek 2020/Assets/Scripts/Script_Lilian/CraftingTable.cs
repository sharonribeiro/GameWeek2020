using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_ObjectPrefab = new List<GameObject>();
    [SerializeField] private Canvas m_blueprint = null;
    
    // Start is called before the first frame update
    void Start()
    {
        if (m_blueprint)
            m_blueprint.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ObjectPrefab.Count <= 0)
            return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if(m_blueprint)
            m_blueprint.gameObject.SetActive(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        if(m_blueprint)
            m_blueprint.gameObject.SetActive(false);
    }

    public void CrafItem(GameObject p_itemPrefab)
    {
        GameObject go = Instantiate(p_itemPrefab);
        go.transform.position = transform.position + transform.up;
    }
    
}
