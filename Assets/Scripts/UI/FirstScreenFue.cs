using UnityEngine.EventSystems;
using UnityEngine;

public class FirstScreenFue : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private FueSystem fueSystem;
    public void OnPointerDown(PointerEventData eventData)
    {
        //fueSystem.touch = true;
    }

    
}
