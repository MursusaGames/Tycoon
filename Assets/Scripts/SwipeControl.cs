using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeControl : MonoBehaviour//,IDragHandler
{
    [SerializeField] private MovementSystem _movement;
    [SerializeField] private AppData data;
    private Vector2 _offsetVelocity;    
    private Touch _touch;
    public float _speed = 1;
    public bool inMenu;

    /*public void OnDrag(PointerEventData eventData)
    {
        if (data.matchData.isFue) return;
        if (Input.touchCount <2) 
        {
            _offsetVelocity = new Vector2(eventData.delta.x * _speed*2 *  Time.deltaTime,
                eventData.delta.y * _speed*2 * Time.deltaTime);
            _movement.MoveTo(_offsetVelocity);
        }               
    }*/

    private void Update()
    {
        if (Input.touchCount == 1 && !inMenu)
        {
            if (data.matchData.isFue) return;
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                _offsetVelocity = new Vector2(_touch.deltaPosition.x * _speed * Time.deltaTime,
               _touch.deltaPosition.y * _speed * Time.deltaTime);
                _movement.MoveTo(_offsetVelocity);
            }
        }
    }

}
