using System.Collections.Generic;
using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    public int coinsAtSameTime = 2;

    private List<GameObject> coins = new List<GameObject>();
    private int collectedCount = 0;

    void Start()
    {
        // Собираем все монетки-дети
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject coin = transform.GetChild(i).gameObject;
            coins.Add(coin);
        }

        // Выключаем все монеты
        for (int i = 0; i < coins.Count; i++)
        {
            coins[i].SetActive(false);
        }

        // Включаем первые 2 монеты
        for (int i = 0; i < coinsAtSameTime; i++)
        {
            SpawnRandomCoin();
        }
    }

    void SpawnRandomCoin()
    {
        // Ищем все монеты, которые сейчас выключены (и ещё не собраны окончательно)
        List<GameObject> available = new List<GameObject>();

        for (int i = 0; i < coins.Count; i++)
        {
            // Если монета выключена, значит её можно включить
            if (coins[i] != null && coins[i].activeSelf == false)
            {
                available.Add(coins[i]);
            }
        }

        // Если нет доступных - нечего спавнить
        if (available.Count == 0) return;

        int randomIndex = Random.Range(0, available.Count);
        available[randomIndex].SetActive(true);
    }

    public void CoinCollected(GameObject coin)
    {
        // Монета "подобрана" -> выключаем её
        coin.SetActive(false);

        collectedCount++;

        // Если собрали все 15 - больше ничего не спавним
        if (collectedCount >= coins.Count)
        {
            return;
        }

        // Поддерживаем 2 монеты на сцене
        SpawnRandomCoin();
    }
}