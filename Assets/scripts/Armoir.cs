using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2)]
public class Armoir : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;

    public GameObject GiveItem;

    public Collider trigger;

    void Start()
    {
        gameManager = this.GetGameManager();
        gameManager.RegisterArmoir(this);
    }

    void OnDestroy()
    {
        gameManager.UnRegisterArmoir(this);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
