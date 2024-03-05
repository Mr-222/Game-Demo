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
        if (curSkill) PlaceSkillinScene(curSkill);
    }

    void PlaceSkillinScene(GameObject curSkill) {
        if (Input.GetMouseButtonDown(0)) // Check if left mouse button is clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ground")) // Check if the collider is tagged as ground
                {
                    Vector3 skillPos = hit.point; // Get the point of intersection
                    Instantiate(curSkill, skillPos, Quaternion.identity);
                }
            }
            this.curSkill = null;
        }
        // clean up the current skill after use
    }
}
