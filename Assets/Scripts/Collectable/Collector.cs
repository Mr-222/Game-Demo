using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ICollectible coin = other.GetComponent<Coin>();
        if (coin != null)
            coin.Collect();
    }
}
