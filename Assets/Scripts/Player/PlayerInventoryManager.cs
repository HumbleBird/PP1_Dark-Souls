using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    //public Dictionary<int, Item> m_dicItem { get; } = new Dictionary<int, Item>();
    public List<Item> m_Items { get; } = new List<Item>();

    public void Add(Item item)
    {
        //m_dicItem.Add(item.Id, item);
        m_Items.Add(item);
    }

    //public Item Get(int itemID)
    //{
    //    Item item = null;
    //    m_dicItem.TryGetValue(itemID, out item);
    //    return item;
    //}

    public Item Get(Item item)
    {
        Item finditem = m_Items.Find(i => i == item);
        return finditem;
    }

    //public Item Find(Func<Item, bool> condition)
    //{
    //    foreach (Item item in m_dicItem.Values)
    //    {
    //        if (condition.Invoke(item))
    //            return item;
    //    }

    //    return null;
    //}

    public Item Find(Func<Item, bool> condition)
    {
        foreach (Item item in m_Items)
        {
            if (condition.Invoke(item))
                return item;
        }

        return null;
    }

    public List<Item> FindItems(Func<Item, bool> condition)
    {
        List<Item> items = new List<Item>();

        foreach (Item item in m_Items)
        {
            if (condition.Invoke(item))
                items.Add(item);
        }

        if (items.Count > 0)
            return items;
        else
            return null;
    }

    public void Clear()
    {
        //m_dicItem.Clear();
        m_Items.Clear();
    }
}
