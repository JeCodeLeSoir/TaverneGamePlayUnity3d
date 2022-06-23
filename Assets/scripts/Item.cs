using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2)]
public class Item : MonoBehaviour
{
    private GameManager gameManager;
    public int id = 0;
    public Collider trigger;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = this.GetGameManager();
        gameManager.RegisterWorldItem(this);
    }

    void OnDestroy()
    {
        gameManager.UnRegisterWorldItem(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
