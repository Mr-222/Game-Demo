using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AttackScript : MonoBehaviour
{
    public GameObject[] skillPrefabs;
    private GameObject curSkill;
    public GameObject firePos;
    public GameObject fireBallPrefab;
    public float fireballSpeed = 10f;
    private Animator anim;
    private bool canAttack;
    
    void Start()
    {
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canAttack&& curSkill == null)
        {
            StartCoroutine(Attack1());
        }
    }
    
    public void SetCurrSkill(CardType cardType)
    {
        int skillIndex = (int)cardType;
        curSkill = skillPrefabs[skillIndex];
        PlaceSkillinScene();
    }
    
    void PlaceSkillinScene() {
        if (Input.GetMouseButtonDown(0)) // Check if left mouse button is clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 skillPos = new Vector3(0, 0, 0);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ground")) // Check if the collider is tagged as ground
                {
                    skillPos = hit.point; // Get the point of intersection
                }
                else {
                    if (Physics.Raycast(hit.collider.transform.position, Vector3.down, out RaycastHit groundHit, Mathf.Infinity))
                    {
                        if (groundHit.collider.CompareTag("Ground"))  skillPos = groundHit.point;
                    }
                }
            }
            Instantiate(curSkill, skillPos, Quaternion.identity);
            this.curSkill = null;
        }
    }

    IEnumerator Attack1()
    {
        canAttack = false;
        yield return new WaitForSeconds(.5f);
        Vector3 temp = Input.mousePosition;
        temp.z = Camera.main.transform.position.y;
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(temp);
        mPosition.y = firePos.transform.position.y;
        GameObject fireBall = Instantiate(fireBallPrefab, firePos.transform.position, firePos.transform.rotation);
        fireBall.GetComponent<Rigidbody>().velocity = (mPosition-firePos.transform.position).normalized * fireballSpeed;
        canAttack = true;
    }
}
