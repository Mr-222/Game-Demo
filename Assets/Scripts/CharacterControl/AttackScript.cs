using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public GameObject firePos;
    public GameObject fireBallPrefab;
    public float fireballSpeed = 10f;
    private Animator anim;
    private bool canAttack;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)&&canAttack)
        {
            StartCoroutine(Attack1());
            //anim.SetTrigger("n_attack");
            //GameObject fireBall = Instantiate(fireBallPrefab, firePos.transform.position, firePos.transform.rotation);
            //fireBall.GetComponent<Rigidbody>().velocity = firePos.transform.forward * fireballSpeed;
        }
    }

    IEnumerator Attack1() {
    anim.SetTrigger("n_attack");
    canAttack = false;
     yield return new WaitForSeconds(.5f);
    GameObject fireBall = Instantiate(fireBallPrefab, firePos.transform.position, firePos.transform.rotation);
    fireBall.GetComponent<Rigidbody>().velocity = firePos.transform.forward * fireballSpeed;
        canAttack = true;
    }
}
