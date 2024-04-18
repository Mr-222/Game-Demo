using UnityEngine;

public class TreasureChest : MonoBehaviour
{

    public GameObject treasure;

    public void SpawnTreasure()
    {
        GameObject treasureInstance = Instantiate(treasure, transform.position, Quaternion.identity);
        treasureInstance.transform.localScale = new Vector3(6f, 6f, 6f);
    }
}