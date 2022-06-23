using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeItem", menuName = "Data/RecipeItem")]
public class RecipeItem : ScriptableObject
{

    [SerializeField] public Item[] objectsRecip;
    [SerializeField] public Item objectResult;

    public bool IsCraft(Item[] objs)
    {
        return Craft(objs) != null;
    }

    public Item Craft(Item[] objs)
    {
        if (objs.Length == 0)
            return null;

        if (this.objectsRecip.Length == 0)
            return null;

        if (this.objectsRecip.Length != objs.Length)
            return null;

        for (int i = 0; i < this.objectsRecip.Length; i++)
        {
            if (this.objectsRecip[i].id != objs[i].id)
                return null;
        }

        return this.objectResult;
    }
}