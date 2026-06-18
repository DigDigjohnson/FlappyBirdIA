using UnityEngine;

public class Bird : MonoBehaviour
{
    Rigidbody2D rb;
    NeuralNetwork brain;

    float flapForce = 5f;
    float fitness = 0f;
    bool dead = false;

    Transform nearestPipeTop;
    Transform nearestPipeBottom;

    public void Init(NeuralNetwork nn)
    {
        brain = nn;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().enabled = false;
        GeneticAlgorithm.Instance.alive++;
        Invoke("EnableCollider", 0.5f);
    }

    void EnableCollider()
    {
        if (!dead)
            GetComponent<Collider2D>().enabled = true;
    }

    void FixedUpdate()
    {
        if (dead) return;

        fitness += Time.fixedDeltaTime;

        FindNearestPipe();

        if (nearestPipeTop == null) return;

        float[] inputs = new float[3];
        inputs[0] = transform.position.y / 10f;
        inputs[1] = (nearestPipeTop.position.x - transform.position.x) / 10f;
        inputs[2] = (nearestPipeTop.position.y + nearestPipeBottom.position.y) / 2f / 10f;

        float[] output = brain.FeedForward(inputs);

        if (output[0] > 0f)
            Flap();
    }

    void Flap()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, flapForce);
    }

    void FindNearestPipe()
    {
        Pipe[] pipes = FindObjectsByType<Pipe>(FindObjectsSortMode.None);
        float minDist = float.MaxValue;
        nearestPipeTop = null;
        nearestPipeBottom = null;

        foreach (Pipe p in pipes)
        {
            if (!p.IsTop) continue;
            float dist = p.transform.position.x - transform.position.x;
            if (dist > -1f && dist < minDist)
            {
                minDist = dist;
                nearestPipeTop = p.transform;
                nearestPipeBottom = p.Partner;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bird"))
            return;

        if (dead) return;
        Die();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Colisão com: " + col.gameObject.name);

        if (dead) return;
        Die();
    }

    void Die()
    {
        if (dead) return;
        dead = true;
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
        GeneticAlgorithm.Instance.BirdDied(brain, fitness);
    }
}
