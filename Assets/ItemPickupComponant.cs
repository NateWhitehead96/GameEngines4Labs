using Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupComponant : MonoBehaviour
{

    [SerializeField] private ItemScriptable PickupItem;

    [SerializeField] private int Ammount = -1;

    [SerializeField] private MeshRenderer PropMeshRenderer;

    [SerializeField] private MeshFilter PropMeshFilter;

    private ItemScriptable ItemInstance;

    private void Instantiate()
    {
        ItemInstance = Instantiate(PickupItem);
        if (Ammount > 0) ItemInstance.SetAmount(Ammount);

        ApplyMesh();
    }

    private void ApplyMesh()
    {
        if (PropMeshFilter) PropMeshFilter.mesh = PickupItem.ItemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;

        if (PropMeshRenderer) PropMeshRenderer.materials = PickupItem.ItemPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterials;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate();
    }

    private void OnValidate()
    {
        ApplyMesh();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ItemInstance.UseItem(other.GetComponent<PlayerController>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
