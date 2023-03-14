using UnityEngine;

public class Farsh : MonoBehaviour
{
    [SerializeField] private float stopZPoint;
    [SerializeField] private FarshPodlojka farshPodlojka;
    [SerializeField] private Animator animator;
    private Vector3 startPos;
    private bool goFwd;
    private float _speed;

    void OnEnable()
    {
        startPos = transform.position;
    }

    public void Go(float speed=-1f)
    {
        _speed = speed;  
        goFwd = true;
    }
    void Update()
    {
        if (goFwd)
        {
            var pos = transform.localPosition;
            pos += Vector3.right * _speed * Time.deltaTime;
            transform.localPosition = pos;            
            if (Mathf.Abs(pos.x) >= stopZPoint)
            {
                goFwd = false;
                animator.enabled = true;
                transform.position = startPos;
                Invoke(nameof(GoInStartPlace), 4f);
            }
        }
    }

    private void GoInStartPlace()
    {
        farshPodlojka.Go(_speed);
        animator.enabled = false;
    }
}
