using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2)]
public class Craft : MonoBehaviour
{
    public List<Vector3> emplacement = new List<Vector3>();
    public Collider trigger;
    public Transform Content;

    public RecipeItem[] RecipeItems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool IsCraft()
    {
        Item[] items = GetItems();
        
        bool result = false;

        for (int i = 0; i < RecipeItems.Length; i++)
        {
            if(RecipeItems[i].IsCraft(items))
            {
                result = true;
                break;
            }
        }

        return result;
    }

    private Item[] GetItems(){
        Item[] items = new Item[Content.childCount];
        if (Content.childCount > 1)
        {
            for (int b = 0; b < Content.childCount; b++)
            {
                Item _item = Content.GetChild(b).gameObject.GetComponent<Item>();
                items[b] = _item;
            }
        }
        return items;
    }

    public void GoCraft(){

        Item[] items = GetItems();

        for (int c = 0; c < RecipeItems.Length; c++)
        {
            Item craft_item = RecipeItems[c].Craft(items);

            if(craft_item !=null)
            {
                for (int v = 0; v < items.Length; v++)
                {
                    DestroyImmediate(items[v].gameObject);
                }

                GameObject obj_item = Instantiate(craft_item.gameObject, Vector3.zero, Quaternion.identity);
                
                obj_item.transform.SetParent(Content);
                obj_item.transform.localPosition = new Vector3(0, 0.5f, 1f);

                break;
            }
        }

    }

}
