using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject brickPrefab;
    public Vector2Int size;
    public Vector2 offset;
   // public GameObject brickPrefab;
    public Gradient gradient;
    private SceneLoader sceneLoader;
    public BouncyBall BB;

    private void Awake()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject newBrick=Instantiate(brickPrefab,transform);
                newBrick.transform.position = transform.position + new Vector3((float)((size.x-1)*.5f-i) * offset.x, j * offset.y, 0);
                var color = gradient.Evaluate((float)j / (size.y ));
                newBrick.GetComponent<SpriteRenderer>().color = new Color (color.r, color.g, color.b, 1);
            }
        }   
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OpenFirstLevel()
    {
        SceneManager.LoadScene(0);

    }
}
