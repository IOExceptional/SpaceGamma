using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject TriggerObject;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject == TriggerObject)
        {
            Debug.Log("Won");
        }
    }
}

