using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager GetGameManager()
    {
        return GameManager.instance;
    }

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        GameObject GameManagerObj = new GameObject();
        GameManager.instance = GameManagerObj.AddComponent<GameManager>();
        DontDestroyOnLoad(GameManager.instance);
        GameManager.instance.Load();
 
    }

    public Items items;
    private List<Armoir> armoirs;
    private List<Item> WorldItems;
    

    void Load() {
        this.armoirs = new List<Armoir>();
        this.WorldItems = new List<Item>();
        this.items = Resources.Load<Items>("Items");
    }

    public Armoir[] GetArmoir(){
        return armoirs.ToArray();
    }

    public Item[] GetWorldItems()
    {
        return WorldItems.ToArray();
    }

    public void RegisterArmoir(Armoir armoir){
        armoirs.Add(armoir);
    }

    public void UnRegisterArmoir(Armoir armoir)
    {
       armoirs.Remove(armoir);
    }

    public void RegisterWorldItem(Item item)
    {
        WorldItems.Add(item);
    }

    public void UnRegisterWorldItem(Item item)
    {
        WorldItems.Remove(item);
    }
}

static class MonoBehaviourUltis{
    public static GameManager GetGameManager(this MonoBehaviour monoBehaviour){
        return GameManager.GetGameManager();
    }
}