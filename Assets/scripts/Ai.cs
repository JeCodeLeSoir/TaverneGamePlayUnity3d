using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(2)]
public class Ai : MonoBehaviour
{
    private GameManager gameManager;
    private NavMeshAgent agent;
    public GameObject neebItem;

    public Contoir contoir;
    public Transform spawn;

    private Transform target;

    public Transform Sac;

    public float damping = 2f;

    void Start()
    {
        gameManager = this.GetGameManager();
        agent = GetComponent<NavMeshAgent>();
        target = contoir.transform;
        Random();
    }

    void Random(){
        neebItem = gameManager.items.values[UnityEngine.Random.Range(0, gameManager.items.values.Length)];
    }

    void Update()
    {
        if (target != spawn)
        {
            MoveTo(target, pickUpInContoir);
        }
        else{
            MoveTo(target, removeItem);
        }
    }

    void MoveTo(Transform target, System.Action callback){
        float distance = (transform.position - target.position).magnitude;
        if (distance > 1.5f)
        {
            agent.SetDestination(target.position);
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
           
        }
        else{
            callback();
        }
    }

    void pickUpInContoir(){
         
        bool isPickUp = false;

        Item _neebItem = neebItem.GetComponent<Item>();

        for (int i = 0; i < contoir.Content.childCount; i++){
            Transform t = contoir.Content.GetChild(i);
            Item item = t.GetComponent<Item>();

            if (item.id.Equals(_neebItem.id))
            {
                if (contoir.emplacement.Contains(t.transform.localPosition))
                    contoir.emplacement.Remove(t.transform.localPosition);

                t.transform.GetComponent<Collider>().enabled = false;
                DestroyImmediate(t.GetComponent<Rigidbody>());
                
                t.SetParent(Sac);
                t.transform.localPosition = Vector3.zero;
                t.transform.localRotation = Quaternion.identity;
                isPickUp = true;
                break;
            }
        }
        if(isPickUp){
            target = spawn;
        }
    }

    void removeItem(){
        
        for (int i = 0; i < Sac.childCount; i++)
        {
            Transform t = Sac.GetChild(i);
            Destroy(t.gameObject, 0.5f);
        }
        target = contoir.transform;
        Random();
    }
}
