using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class XmasTreeChecker : MonoBehaviour
{
    [SerializeField] private GameObject denmarkBall;
    [SerializeField] private GameObject swedenBall;
    [SerializeField] private GameObject norwayBall;
    [SerializeField] private GameObject finlandBall;
    [SerializeField] private GameObject icelandBall;
    [SerializeField] private GameObject faroeIslandBall;
    [SerializeField] private GameObject goldfish;
    [SerializeField] private GameObject gift;
    [SerializeField] private GameObject canvas2;
    [SerializeField] private GameObject player;

    private Vector3 denmarkBallStartPos;
    private Vector3 swedenBallStartPos;
    private Vector3 norwayBallStartPos;
    private Vector3 finlandBallStartPos;
    private Vector3 icelandBallStartPos;
    private Vector3 faroeIslandBallStartPos;

   private int ballscounter = 0;
    private int correctcounter = 0;

    private void Start()
    {
        denmarkBallStartPos = denmarkBall.transform.position;
        swedenBallStartPos = swedenBall.transform.position;
        norwayBallStartPos = norwayBall.transform.position;
        finlandBallStartPos = finlandBall.transform.position;
        icelandBallStartPos = icelandBall.transform.position;
        faroeIslandBallStartPos = faroeIslandBall.transform.position;
        goldfish.SetActive(false);
        gift.SetActive(false);
    }

      private void OnTriggerEnter2D(Collider2D collision)
      {
          if (collision.CompareTag("Xmasball"))
          {
              if (collision.gameObject == denmarkBall)
              {
                  Debug.Log("Kolizja Dani z drzewem");
                  ballscounter++;
                  correctcounter++;
              }
              else if (collision.gameObject == swedenBall)
              {
                  Debug.Log("Kolizja Szwecji z drzewem");
                  ballscounter++;
                  correctcounter++;
              }
              else if (collision.gameObject == norwayBall)
              {
                  Debug.Log("Kolizja Norwegi z drzewem");
                  ballscounter++;
                  correctcounter++;
              }
              else if (collision.gameObject == finlandBall)
              {
                  Debug.Log("Kolizja Finlandii z drzewem");
                  ballscounter++;
              }
              else if (collision.gameObject == icelandBall)
              {
                  Debug.Log("Kolizja Islandi z drzewem");
                  ballscounter++;
              }
              else if (collision.gameObject == faroeIslandBall)
              {
                  Debug.Log("Kolizja Wyspy owcze z drzewem");
                  ballscounter++;
              }
              // Logika do aktywacji nagród
              if (ballscounter == 3)
              {
                  if (correctcounter == 3)
                  {
                      gift.SetActive(true);
                      goldfish.SetActive(true);
                      FindObjectOfType<Level14>().MarkAsCompleted();
                        finlandBall.SetActive(false);
                        icelandBall.SetActive(false);
                        faroeIslandBall.SetActive(false);
                         StartCoroutine(ShowDialog());
                }
                  else
                  {
                      StartCoroutine(ResetAfterDelay());
                      ballscounter = 0;
                      correctcounter = 0;
                  }
              }
          }
      }

    private IEnumerator ShowDialog()
    {
        canvas2.SetActive(true);
        yield return new WaitForSeconds(3f);
        canvas2.SetActive(false);
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        ResetXmasballs();
    }

    private void ResetXmasballs()
    {
        
        denmarkBall.transform.position = denmarkBallStartPos;
        swedenBall.transform.position = swedenBallStartPos;
        norwayBall.transform.position = norwayBallStartPos;
        finlandBall.transform.position = finlandBallStartPos;
        icelandBall.transform.position = icelandBallStartPos;
        faroeIslandBall.transform.position = faroeIslandBallStartPos;

        // Resetowanie flagi isOnTree dla ka¿dej bombki
        denmarkBall.GetComponent<DragXmasballs>().isntOnTree();
        swedenBall.GetComponent<DragXmasballs>().isntOnTree();
        norwayBall.GetComponent<DragXmasballs>().isntOnTree();
        finlandBall.GetComponent<DragXmasballs>().isntOnTree();
        icelandBall.GetComponent<DragXmasballs>().isntOnTree();
        faroeIslandBall.GetComponent<DragXmasballs>().isntOnTree();

    }
}