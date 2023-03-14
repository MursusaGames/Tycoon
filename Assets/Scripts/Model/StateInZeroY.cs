using UnityEngine;

public class StateInZeroY : MonoBehaviour
{
    private void OnEnable()
    {
        var pos = transform.localPosition;
        pos.y = 0;
        transform.localPosition = pos;
    }
}
