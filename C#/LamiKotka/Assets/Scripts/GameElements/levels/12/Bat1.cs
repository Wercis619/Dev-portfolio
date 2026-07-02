using System.Collections;
using UnityEngine;

public class Bat1 : MonoBehaviour
{
    [Header("References")]
    public Level12P1 level12P1;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Animator animator;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 5f;

    private Vector3 rightPointPosition;
    private Vector3 leftPointPosition;
    private bool isMovingRight = true;
    public bool isScared = false;

    public float blowThreshold = 0.2f;
    private AudioClip microphoneInput;
    private bool isScreamEnable = false;

    private void Start()
    {
        leftPointPosition = leftPoint.position;
        rightPointPosition = rightPoint.position;

        if (Microphone.devices.Length > 0)
        {
            microphoneInput = Microphone.Start(null, true, 10, 44100);
            isScreamEnable = false;
        }
        else
        {
            Debug.LogWarning("Mikrofon niedostêpny!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
             player.TakeDamage("Bat1");
             level12P1.ResetPlayerPosition();
            
        }

    }

    private void Update()
    {
        if (isScreamEnable && Microphone.IsRecording(null))
        {
            float volume = GetMicrophoneVolume();
            if (volume > blowThreshold && !isScared)
            {
                StartCoroutine(ScareABat());
            }
        }

        if (isScared)
        {
            Move();
        }
    }

    private IEnumerator ScareABat()
    {
       
        animator.SetTrigger("isFlying");
        yield return new WaitForSeconds(1f);
        animator.Play("fly1");
        isScared = true;
    }

    private float GetMicrophoneVolume()
    {
        int sampleWindow = 128;
        float[] waveData = new float[sampleWindow];
        int microphonePosition = Microphone.GetPosition(null) - sampleWindow + 1;

        if (microphonePosition < 0) return 0;

        microphoneInput.GetData(waveData, microphonePosition);

        float sum = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            sum += waveData[i] * waveData[i];
        }

        return Mathf.Sqrt(sum / sampleWindow);
    }

    private void Move()
    {
        float moveValue = movementSpeed * Time.deltaTime;
        if (isMovingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPointPosition, moveValue);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            if (transform.position == rightPointPosition)
            {
                isMovingRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPointPosition, moveValue);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (transform.position == leftPointPosition)
            {
                isMovingRight = true;
            }
        }
    }

    public void EnableScream()
    {
        isScreamEnable = true;
    }

    public void DisableScream()
    {
        isScreamEnable = false;
    }
}
