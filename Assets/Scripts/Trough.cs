using UnityEngine;

public class Trough : MonoBehaviour
{
    [SerializeField] private GameObject food;
    public bool isFull;
    public bool isFood;
    
    private void FixedUpdate()
    {
        if(isFull && !isFood)
        {
            food.SetActive(true);
            Invoke(nameof(FoodIsOver), 20f);
            isFood = true;
        }        
    }
    private void FoodIsOver()
    {
        isFull = false;
        food.SetActive(false);
        isFood = false;
    }
}
