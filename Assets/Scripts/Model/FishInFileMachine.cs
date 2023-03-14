using UnityEngine;

public class FishInFileMachine : MonoBehaviour
{
    [SerializeField] private float stopZPoint;
    [SerializeField] private FilePodlogka filePodlojka;
    
    private Vector3 startPos;
    private bool goFwd;
    public float _speed;

    void OnEnable()
    {
        startPos = transform.position;
        goFwd = true;
    }
    
    void Update()
    {
        if (goFwd)
        {
            var pos = transform.localPosition;
            pos += Vector3.forward * _speed * Time.deltaTime;
            transform.localPosition = pos;
            if (Mathf.Abs(pos.z) <= stopZPoint)
            {
                goFwd = false;
                filePodlojka.Go(_speed);
                GoInStartPlace();                
            }
        }
    }

    private void GoInStartPlace()
    {
        transform.position = startPos;
        gameObject.SetActive(false);
    }
}
