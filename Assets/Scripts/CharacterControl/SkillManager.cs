using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject[] skillPrefabs;
    private GameObject curSkill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) curSkill = skillPrefabs[0];
        if (Input.GetKeyDown(KeyCode.Alpha2)) curSkill = skillPrefabs[1];
        if (Input.GetKeyDown(KeyCode.Alpha3)) curSkill = skillPrefabs[2];
        if (Input.GetKeyDown(KeyCode.Alpha4)) curSkill = skillPrefabs[3];
        if(curSkill) PlaceSkillinScene();
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
}
