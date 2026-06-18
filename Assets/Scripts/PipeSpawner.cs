using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public static PipeSpawner Instance;

    public GameObject pipePrefabTop;
    public GameObject pipePrefabBottom;

    public float spawnInterval = 0.1f;
    public float gapSize = 8f;
    public float minY = -2f;
    public float maxY = 2f;

    float timer = 0f;

    void Awake()
    {
        Instance = this;
        Debug.Log("PipeSpawner Awake");
    }

    void Update()
    {
        timer += Time.deltaTime;

        Debug.Log("Timer = " + timer);

        if (timer >= spawnInterval)
        {
            Debug.Log("Chamando SpawnPipes");

            timer = 0f;
            SpawnPipes();
        }
    }

    public void SpawnPipes()
    {
        Debug.Log("Spawnando canos");
        float yPos = Random.Range(minY, maxY);

        GameObject top = Instantiate(pipePrefabTop, new Vector3(12f, yPos + gapSize / 2f, 0), Quaternion.identity);
        GameObject bot = Instantiate(pipePrefabBottom, new Vector3(12f, yPos - gapSize / 2f, 0), Quaternion.identity);

        Pipe pTop = top.GetComponent<Pipe>();
        Pipe pBot = bot.GetComponent<Pipe>();

        pTop.IsTop = true;
        pTop.Partner = bot.transform;
        pBot.IsTop = false;
        pBot.Partner = top.transform;
    }

    public void ClearPipes()
    {
        foreach (var p in FindObjectsByType<Pipe>(FindObjectsSortMode.None))
            Destroy(p.gameObject);
        timer = 0f;
    }
}
