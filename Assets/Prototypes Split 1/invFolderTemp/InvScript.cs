using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvScript : MonoBehaviour
{
    private List<Item> inventory = new List<Item>();
    void Start()
    {
        inventory = new List<Item>();
    }

    List<Item> getInventory()
    {
        return inventory;
    }
    public void AddItem(Item itemIn)
    {
        int added = 0;
        if(inventory.Count == 0)
        {
            inventory.Add(itemIn);
        }
        else
        {
            for(int i = 0; i < inventory.Count; i++)
            {
                if((inventory[i].getID() == itemIn.getID()) && added == 0)
                {
                    inventory[i].setQuantity(inventory[i].getQuantity() + itemIn.getQuantity());
                    added++;
                }
            }
        }
        if(added == 0)
        {
            inventory.Add(itemIn);
        }
    }
    public void SubItem(int idIn, int amount)
    {
        if (inventory.Count == 0)
        {
            return;
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            if ((inventory[i].getID() == idIn))
            {
                if(inventory[i].getQuantity() > amount)
                {
                    inventory[i].setQuantity(inventory[i].getQuantity() - amount);
                }
                else
                {
                    return;
                }
            }
        }
    }
    public List<Item> FilterByStar(int star)
    {
        List<Item> filterList = new List<Item>();
        if (inventory.Count == 0)
        {
            return null;
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            if ((inventory[i].getStarLvl() == star))
            {
                filterList.Add(inventory[i]);
            }
        }
        if(filterList.Count == 0)
        {
            return null;
        }
        return filterList;
    }
    public List<Item> FilterByName(string nameIn)
    {
        List<Item> filterList = new List<Item>();
        if (inventory.Count == 0)
        {
            return null;
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            if ((inventory[i].getName() == nameIn))
            {
                filterList.Add(inventory[i]);
            }
        }
        if (filterList.Count == 0)
        {
            return null;
        }
        return filterList;
    }
}
