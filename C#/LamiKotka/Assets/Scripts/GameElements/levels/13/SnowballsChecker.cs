using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballsChecker : MonoBehaviour
{
    [SerializeField] private GameObject snowball1;
    [SerializeField] private GameObject snowball2;
    [SerializeField] private GameObject snowball3;
    [SerializeField] private GameObject wood;

    private Vector3 snowball1StartPos;
    private Vector3 snowball2StartPos;
    private Vector3 snowball3StartPos;

    private List<int> correctOrder = new List<int> { 3, 2, 1 };
    private List<int> currentOrder = new List<int>();

    private void Start()
    {
        snowball1StartPos = snowball1.transform.position;
        snowball2StartPos = snowball2.transform.position;
        snowball3StartPos = snowball3.transform.position;
        wood.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == snowball1)
        {
            currentOrder.Add(1);
        }
        else if (collision.gameObject == snowball2)
        {
            currentOrder.Add(2);
        }
        else if (collision.gameObject == snowball3)
        {
            currentOrder.Add(3);
        }

        if (currentOrder.Count == 3)
        {
            if (IsCorrectOrder())
            {
                wood.SetActive(false);
                FindObjectOfType<Level13>().MarkAsCompleted(); 
            }
            else
            {
                StartCoroutine(ResetAfterDelay());
            }      
            currentOrder.Clear();
        }
    }

    private bool IsCorrectOrder()
    {
        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        ResetSnowballs();
    }

    private void ResetSnowballs()
    {
        snowball1.transform.position = snowball1StartPos;
        snowball2.transform.position = snowball2StartPos;
        snowball3.transform.position = snowball3StartPos;
    }
}
