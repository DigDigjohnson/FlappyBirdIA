using UnityEngine;

public class Pipe : MonoBehaviour
{
    public bool IsTop;
    public Transform Partner;

    float speed = 3f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < -15f)
            Destroy(gameObject);
    }
}
