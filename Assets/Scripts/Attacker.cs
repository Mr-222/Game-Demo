using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            transform.parent.parent.GetComponent<PlayerController>().enemy = other.gameObject;
            transform.parent.parent.GetComponent<PlayerController>().attack = true;
        }
        else
        {
            transform.parent.parent.GetComponent<PlayerController>().attack = false;
            transform.parent.parent.GetComponent<PlayerController>().enemy = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {


        transform.parent.parent.GetComponent<PlayerController>().attack = false;
        transform.parent.parent.GetComponent<PlayerController>().enemy = null;

    }
}
