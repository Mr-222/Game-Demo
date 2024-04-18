using System;
using UnityEngine;

public class Food : MonoBehaviour, ICollectible
{
    public static event Action OnFoodCollected;
    
    public void Collect()
    {
        OnFoodCollected?.Invoke();
        Destroy(this.gameObject);
    }
}
