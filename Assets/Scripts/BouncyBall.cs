using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BouncyBall : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem=null;
    [SerializeField] ParticleSystem _particleSystem2 = null;

    public float minY = -10f;

    public float maxVelocity = 15f;
    Rigidbody2D rb;
    int score=0;
    int lives = 5;
    public TextMeshProUGUI scoreTxt;
    public GameObject[] livesImage;
    public GameObject gameOverPanel;
    public GameObject youWinPanel;
    public GameObject exitPanel;
    int brickCount;
    public AudioSource audio;
    public AudioSource audio2;
    public const float moveSpeed = 10f; // Fixed speed for the ball
    private float initialVelocityMagnitude;
    private bool isPanelVisible;
    public LevelGenerator BB;
    void Start()
    {
        // exitPanel.SetActive(false);
            isPanelVisible = false;
        rb = GetComponent<Rigidbody2D>();
        brickCount = FindAnyObjectByType<LevelGenerator>().transform.childCount;
        rb.velocity = Vector2.down * moveSpeed;
        //audio = GetComponent<AudioSource>();
        initialVelocityMagnitude = rb.velocity.magnitude;
    }
    IEnumerator DelayAction()
    {
        yield return StartCoroutine(WaitForSecondsCoroutine(3f));
        youWinPanel.SetActive(true);
    }
    IEnumerator WaitForSecondsCoroutine(float delay)
    {
        float endTime = Time.time + delay;
        while (Time.time < endTime)
        {
            yield return null;
        }
    }   
    void Update()
    {
        rb.velocity = rb.velocity.normalized * moveSpeed;
        if (Input.GetKeyDown(KeyCode.Escape))
            {

            if (isPanelVisible)
            {

                ClosePanel();
                ContinueGame();

            }

            else

            {
                OpenPanel();
                StopGame();
            }




        }

        if (transform.position.y < minY)
        {
            if (lives <= 1)
            {
                GameOver();

            }
            else
            {
                transform.position = Vector3.zero;
                rb.velocity = Vector2.down * moveSpeed;
                lives--;
                livesImage[lives].SetActive(false);

            }
        }
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }


    }
    public void StopGame()
    {
        Time.timeScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            _particleSystem.Play();
            score += 10;
            scoreTxt.text=score.ToString("0000");
            brickCount--;
            if(brickCount<=0)
            {
                rb.velocity = Vector2.zero;
                audio2.Play();
                _particleSystem2.Play();
               StartCoroutine(DelayAction());

            }
            audio.Play();
        }
    }
    void GameOver()
    {
        gameOverPanel.SetActive(true);
        StopGame();
        Destroy(gameObject);
    }

    public void ExitPanel()
    {
        exitPanel.SetActive(false);
        ContinueGame();
        isPanelVisible = false;

    }
    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
    void OpenPanel()
    {
        Debug.Log("Open panel");
        exitPanel.SetActive(true);
        isPanelVisible = true;
    }

    void ClosePanel()
    {
        exitPanel.SetActive(false);
        isPanelVisible = false;
    }


}
