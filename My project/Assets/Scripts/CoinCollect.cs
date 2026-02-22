using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public GameManager gameManager;
    public CollectablesManager manager;

    public float rotateSpeed = 180f;
    private bool collected = false;

    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    void OnEnable()
    {
        collected = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            if (gameManager != null)
                gameManager.AddScore();

            if (manager != null)
                manager.CoinCollected(gameObject);

            gameObject.SetActive(false);
        }
    }
}