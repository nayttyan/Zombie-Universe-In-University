using System.Collections.Generic;
using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    public int coinsAtSameTime = 2;

    private List<GameObject> coins = new List<GameObject>();
    private int collectedCount = 0;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject coin = transform.GetChild(i).gameObject;
            coins.Add(coin);
        }

        for (int i = 0; i < coins.Count; i++)
        {
            coins[i].SetActive(false);
        }

        for (int i = 0; i < coinsAtSameTime; i++)
        {
            SpawnRandomCoin();
        }
    }

    void SpawnRandomCoin()
    {
        List<GameObject> available = new List<GameObject>();

        for (int i = 0; i < coins.Count; i++)
        {
            if (coins[i] != null && coins[i].activeSelf == false)
            {
                available.Add(coins[i]);
            }
        }

        if (available.Count == 0) return;

        int randomIndex = Random.Range(0, available.Count);
        available[randomIndex].SetActive(true);
    }

    public void CoinCollected(GameObject coin)
    {
        coin.SetActive(false);

        collectedCount++;

        if (collectedCount >= coins.Count)
        {
            return;
        }

        SpawnRandomCoin();
    }
}