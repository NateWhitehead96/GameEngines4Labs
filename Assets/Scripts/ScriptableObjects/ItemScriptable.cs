using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemScriptable : ScriptableObject
{

    public string Name = "Item";
    public ItemCategory ItemCategory = ItemCategory.none;
    public GameObject ItemPrefab;
    public bool Stackable;
    public int MaxStack;
    public int Amount => m_Amount;
    private int m_Amount;

    public PlayerController Controller { get; private set; }

    public void Initialize(PlayerController controller)
    {
        Controller = controller;
    }

    public abstract void UseItem(PlayerController controller);

    public virtual void DeleteItem(PlayerController controller)
    {

    }

    public virtual void DropItem(PlayerController controller)
    {

    }

    public void ChangeAmount(int amount)
    {
        m_Amount += amount;
    }

    public void SetAmount(int amount)
    {
        m_Amount = amount;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
