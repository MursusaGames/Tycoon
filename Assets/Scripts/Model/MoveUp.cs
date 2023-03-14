using UnityEngine;

public class MoveUp : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private GameObject parent;
    private Vector3 parentStartPos;
    private Vector3 parentPos;
    void OnEnable()
    {
        parentStartPos = parent.transform.position;
        parentPos = parent.transform.position;
    }

    void Update()
    {
        parentPos.y += speed * Time.deltaTime;
        parent.transform.position = parentPos;
    }
    private void OnDisable()
    {
        parent.transform.position = parentStartPos;
    }
}
