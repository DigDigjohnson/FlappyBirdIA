using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public GameObject birdPrefab;
    public Transform birdSpawn;

    [Header("UI")]
    public Text txtGeneration;
    public Text txtAlive;
    public Text txtBestFitness;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartNewGeneration();
    }

    public void StartNewGeneration()
    {
        Debug.Log("Nova geração iniciada");

        if (PipeSpawner.Instance != null)
        {
            PipeSpawner.Instance.ClearPipes();

            // cria um conjunto inicial de canos
            PipeSpawner.Instance.SpawnPipes();
        }

        int population = GeneticAlgorithm.Instance.populationSize;

        for (int i = 0; i < population; i++)
        {
            NeuralNetwork nn = GeneticAlgorithm.Instance.GetNext();

            if (nn == null)
                continue;

            GameObject bird =
                Instantiate(
                    birdPrefab,
                    birdSpawn.position,
                    Quaternion.identity);

            Bird birdScript = bird.GetComponent<Bird>();

            if (birdScript != null)
                birdScript.Init(nn);
        }
    }

    void Update()
    {
        if (txtGeneration != null)
        {
            string gen = "Geração: " + GeneticAlgorithm.Instance.generation;
            if (GeneticAlgorithm.Instance.finished)
                gen += " [CONCLUÍDO]";
            txtGeneration.text = gen;
        }

        if (txtAlive != null)
            txtAlive.text =
                "Vivos: " +
                GeneticAlgorithm.Instance.alive;

        if (txtBestFitness != null)
            txtBestFitness.text =
                "Melhor Fitness: " +
                GeneticAlgorithm.Instance.bestFitness.ToString("F1");
    }
}