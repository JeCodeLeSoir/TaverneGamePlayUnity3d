using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[DefaultExecutionOrder(2)]
public class Player : MonoBehaviour
{
    private Collider myCollider;
    private GameManager gameManager;
    public LayerMask layerMask;
    public Transform Cursor;

    public Transform Sac;

    public float speed = 6f;

    Plane m_Plane;

    void Start()
    {
        myCollider = GetComponent<Collider>();
        gameManager = this.GetGameManager();
        m_Plane = new Plane(Vector3.up, Vector3.forward);
    }

    public Contoir contoir;
    public Craft craft;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;

        if (m_Plane.Raycast(ray, out enter))
        {
            Cursor.position = ray.GetPoint(enter);

            var lookPos = Cursor.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 6f);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.G))
        {
            if (Sac.childCount >= 1)
            {
                Transform item = Sac.GetChild(0);
                item.GetComponent<Collider>().enabled = true;
                item.SetParent(null);
                item.gameObject.AddComponent<Rigidbody>();
                item.gameObject.AddComponent<NavMeshObstacle>();
            }
        }

        ActionInput();
    }

    void ActionInput(){
        if (Input.GetKeyDown(KeyCode.E))
        {
            Armoir[] armoirs = gameManager.GetArmoir();
            for (int i = 0; i < armoirs.Length; i++)
            {
                Armoir armoir = armoirs[i];
                if (myCollider.bounds.Intersects(armoir.trigger.bounds))
                {
                    if (Sac.childCount == 0)
                    {
                        GameObject item = Instantiate(armoir.GiveItem, Sac);
                        item.GetComponent<Collider>().enabled = false;
                        return;
                    }
                }
            }
            // == Contoir
            if (myCollider.bounds.Intersects(contoir.trigger.bounds))
            {

                if (Sac.transform.childCount >= 1 && contoir.Content.childCount < 3)
                {

                    Transform item = Sac.GetChild(0);

                    item.position = Vector3.zero;
                    item.rotation = Quaternion.identity;
                    item.SetParent(null);
                    item.SetParent(contoir.Content);

                    Vector3 pos = Vector3.zero;

                    for (int b = 1; b < 4; b++)
                    {
                        var test_pos = new Vector3(b * 0.25f, 0.25f, 0);
                        if (!contoir.emplacement.Contains(test_pos))
                        {
                            pos = test_pos;
                            contoir.emplacement.Add(test_pos);
                            break;
                        }
                    }

                    item.localPosition = pos;
                    item.localRotation = Quaternion.identity;
                }
                else if (
                   Sac.childCount == 0
               && contoir.Content.childCount > 0
               )
                {

                    Transform item = contoir.Content.GetChild(0);

                    if (contoir.emplacement.Contains(item.localPosition))
                        contoir.emplacement.Remove(item.localPosition);

                    item.position = Vector3.zero;
                    item.rotation = Quaternion.identity;
                    item.SetParent(null);
                    item.SetParent(Sac);
                    item.GetComponent<Collider>().enabled = false;

                    item.localPosition = Vector3.zero;
                    item.localRotation = Quaternion.identity;

                }

                return;
            }
            // == CRAFT
            else if (myCollider.bounds.Intersects(craft.trigger.bounds))
            {
                if (Sac.childCount >= 1 && craft.Content.childCount < 3)
                {

                    Transform item = Sac.GetChild(0);

                    item.position = Vector3.zero;
                    item.rotation = Quaternion.identity;
                    item.SetParent(null);
                    item.SetParent(craft.Content);

                    Vector3 pos = Vector3.zero;

                    for (int b = 1; b < 4; b++)
                    {
                        var test_pos = new Vector3(0, 0.25f, b * 0.55f);
                        if (!craft.emplacement.Contains(test_pos))
                        {
                            pos = test_pos;
                            craft.emplacement.Add(test_pos);
                            break;
                        }
                    }

                    item.localPosition = pos;
                    item.localRotation = Quaternion.identity;
                }
                else if (
                    Sac.childCount == 0
                && craft.Content.childCount > 0
                && !craft.IsCraft())
                {

                    Transform item = craft.Content.GetChild(0);
                    if (craft.emplacement.Contains(item.localPosition))
                        craft.emplacement.Remove(item.localPosition);

                    item.position = Vector3.zero;
                    item.rotation = Quaternion.identity;

                    item.SetParent(null);
                    item.SetParent(Sac);
                    item.GetComponent<Collider>().enabled = false;
                    item.localPosition = Vector3.zero;
                    item.localRotation = Quaternion.identity;

                }
                else
                {
                    craft.GoCraft();
                }

                return;
            }
            else{

                Item[] items = gameManager.GetWorldItems();

                for (int i = 0; i < items.Length; i++)
                {
                    Item item = items[i];
                    if (myCollider.bounds.Intersects(item.trigger.bounds))
                    {
                        if (Sac.childCount == 0)
                        {
                            item.transform.position = Vector3.zero;
                            item.transform.rotation = Quaternion.identity;

                            item.transform.SetParent(null);
                            item.transform.SetParent(Sac);

                            item.transform.GetComponent<Collider>().enabled = false;

                            DestroyImmediate(item.GetComponent<Rigidbody>());

                            item.transform.localPosition = Vector3.zero;
                            item.transform.localRotation = Quaternion.identity;
                            
                            return;
                        }
                    }
                }
            }
        }
    }
}