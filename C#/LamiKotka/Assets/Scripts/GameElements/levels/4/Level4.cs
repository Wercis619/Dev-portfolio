using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4 : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject dialogBoxPlayer;
    [SerializeField] private GameObject dialogBoxPimpek;

    [Header("Snails")]
    [SerializeField] private GameObject[] snails;
    [SerializeField] private Transform[] snailStartPositions;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pimpek;
    [SerializeField] private Transform playerStartPosition;
    [SerializeField] private Transform pimpekStartPosition;
    [SerializeField] private GameObject goldfish;

    [Header("Settings")]
    [SerializeField] private float dialogDuration = 3f;

    private bool hasDialogBeenShown = false;
    private bool isPimpekRescued = false;

    public void Start()
    {
        goldfish.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasDialogBeenShown)
        {
            StartCoroutine(ShowDialogAndActivateSnails());
        }
    }

    private IEnumerator ShowDialogAndActivateSnails()
    {
        dialogBoxPlayer.SetActive(true);
        hasDialogBeenShown = true;

        yield return new WaitForSeconds(dialogDuration);

        dialogBoxPlayer.SetActive(false);

        ActivateSnails();

        pimpek.GetComponent<Pimpek>().EnableDragging();
    }

    private void ActivateSnails()
    {
        for (int i = 0; i < snails.Length; i++)
        {
            snails[i].SetActive(true);
            snails[i].transform.position = snailStartPositions[i].position;
        }
    }

    public void ResetLevel()
    {
        player.transform.position = playerStartPosition.position;
        pimpek.transform.position = pimpekStartPosition.position;

        for (int i = 0; i < snails.Length; i++)
        {
            snails[i].SetActive(false);
            snails[i].transform.position = snailStartPositions[i].position;
        }

        hasDialogBeenShown = false;     
        isPimpekRescued = false;
        goldfish.SetActive(false);
        dialogBoxPimpek.SetActive(false); 
    }

    public void NotifyPimpekRescued()
    {
        if (!isPimpekRescued)
        {
            StartCoroutine(ShowRescueDialog());
            goldfish.SetActive(true);
            isPimpekRescued = true;
        }
    }

    private IEnumerator ShowRescueDialog()
    {
        dialogBoxPimpek.SetActive(true);

        yield return new WaitForSeconds(dialogDuration);

        dialogBoxPimpek.SetActive(false);
    }
}
