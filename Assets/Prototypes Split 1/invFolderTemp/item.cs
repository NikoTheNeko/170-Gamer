using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private int id;
    private string name;
    private int quantity;
    private string description;
    private int starLvl;
    private Sprite icon;
    public Item(int idIn, int starLvlIn, int quantityIn, string nameIn, string descriptionIn, string spriteName)
    {
        id = idIn;
        starLvl = starLvlIn;
        quantity = quantityIn;
        name = nameIn;
        description = descriptionIn;
        icon = Resources.Load<Sprite>("Sprites/Ingredients/" + spriteName);
    }

    public Item(Item itemIn)
    {
        id = itemIn.id;
        starLvl = itemIn.starLvl;
        quantity = itemIn.quantity;
        name = itemIn.name;
        description = itemIn.description;
        icon = itemIn.icon;
    }

    public int getID()
    {
        return this.id;
    }
    public int getStarLvl()
    {
        return this.starLvl;
    }
    public int getQuantity()
    {
        return this.quantity;
    }
    public string getName()
    {
        return this.name;
    }
    public string getDescription()
    {
        return this.description;
    }
    public Sprite getIcon()
    {
        return this.icon;
    }
    public void setID(int idIn)
    {
        this.id = idIn;
    }
    public void setStarLvl(int starIn)
    {
        this.starLvl = starIn;
    }
    public void setQuantity(int quantityIn)
    {
        this.quantity = quantityIn;
    }
    public void setName(string nameIn)
    {
        this.name = nameIn;
    }
    public void setDescription(string descIn)
    {
        this.description = descIn;
    }
    public void setIcon(Sprite iconIn)
    {
        this.icon = iconIn;
    }
}
