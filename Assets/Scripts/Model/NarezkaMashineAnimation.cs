using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarezkaMashineAnimation : MonoBehaviour
{
    [SerializeField] private GameObject stol_1;
    [SerializeField] private GameObject stol_2;
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject podlojka;
    private Vector3 startPosStol1;
    private Vector3 startPosStol2;

    private GameObject _stake;
    public bool moveDown;
    public bool moveUpStol1;
    private bool moveUpStol2;

    private void Start()
    {
        startPosStol1 = stol_1.transform.localPosition;
        startPosStol2 = stol_2.transform.localPosition;
    }
    public void StartAnimation(GameObject stake)
    {
        _stake = stake;
        StartCoroutine(nameof(Animation));
    }

    private IEnumerator Animation()
    {
        moveDown = true;
        Invoke(nameof(ResetMoveDown), 1f);
        yield return new WaitForSeconds(1.5f);
        Destroy(_stake);
        podlojka.SetActive(true);
        moveDown = false;
        moveUpStol1 = true;
        moveUpStol2 = true;
        yield return new WaitForSeconds(0.5f);
        yield break;
    }

    private void ResetMoveDown()
    {
        moveDown = false;
    }    

    private void FixedUpdate()
    {
        if (moveDown)
        {
            var pos_1 = stol_1.transform.localPosition;
            pos_1.y -= speed * Time.fixedDeltaTime;
            stol_1.transform.localPosition = pos_1;
            var pos_2 = stol_2.transform.localPosition;
            pos_2.y -= speed * Time.fixedDeltaTime;
            stol_2.transform.localPosition = pos_2;            
        }
        if (moveUpStol1)
        {
            var pos_1 = stol_1.transform.localPosition;
            pos_1.y += speed * Time.fixedDeltaTime;
            stol_1.transform.localPosition = pos_1;
            var dyst = Vector3.Distance(stol_1.transform.localPosition, startPosStol1);
            if(dyst <= 0.1f)
            {
                moveUpStol1 = false;
                stol_1.transform.localPosition = startPosStol1;
            }         
        }
        if (moveUpStol2)
        {
            var pos_2 = stol_2.transform.localPosition;
            pos_2.y += speed * Time.fixedDeltaTime;
            stol_2.transform.localPosition = pos_2;
            var dyst = Vector3.Distance(stol_2.transform.localPosition, startPosStol2);
            if (dyst <= 0.1f)
            {
                moveUpStol2 = false;
                stol_2.transform.localPosition = startPosStol2;
            }
        }
    }
}
