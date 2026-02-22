using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] zombies;
    public GameObject selectedZombie;
    public Vector3 selectedSize;
    public Vector3 pushForce;
    private InputAction next, prev, jump;
    private int selectedIndex = 0;

    public TMP_Text timerText;
    private float timer;

    public float loseY = -5f;

    public GameObject startPanel;
    public GameObject gameOverPanel;

    public GameObject gamePlayingPanel;
    public TMP_Text scoreText;
    public TMP_Text finalTimeText;
    public TMP_Text finalScoreText;
    private int score;
    public int totalCoins = 15;

    enum GameState
    {
        Start,
        Playing,
        GameOver
    }

    private GameState state = GameState.Start;

    void Start()
    {
        next = InputSystem.actions.FindAction("NextZombie");
        prev = InputSystem.actions.FindAction("PrevZombie");
        jump = InputSystem.actions.FindAction("Jump");

        SelectZombie(selectedIndex);

        timer = 0f;
        timerText.text = "Time: 0.0s";

        score = 0;
        scoreText.text = score + "/" + totalCoins;

        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gamePlayingPanel.SetActive(false);

        FreezeZombies();

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayBackgroundMusic();
        }
    }

    void SelectZombie(int index)
    {
        if (selectedZombie != null)
        {
            selectedZombie.transform.localScale = Vector3.one;
        }

        selectedZombie = zombies[index];
        selectedZombie.transform.localScale = selectedSize;
        Debug.Log("selected: " + selectedZombie);
    }

    void FreezeZombies()
    {
        for (int i = 0; i < zombies.Length; i++)
        {
            Rigidbody rb = zombies[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }
    }

    void UnfreezeZombies()
    {
        for (int i = 0; i < zombies.Length; i++)
        {
            Rigidbody rb = zombies[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    void Update()
    {
        if (state == GameState.Playing)
        {
            if (next.WasPressedThisFrame())
            {
                selectedIndex++;
                if (selectedIndex >= zombies.Length) { selectedIndex = 0; }
                SelectZombie(selectedIndex);
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.PlaySwitchZombie();
                }
            }

            if (prev.WasPressedThisFrame())
            {
                selectedIndex--;
                if (selectedIndex < 0) { selectedIndex = zombies.Length - 1; }
                SelectZombie(selectedIndex);
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.PlaySwitchZombie();
                }
            }

            if (jump.WasPressedThisFrame())
            {
                Rigidbody rb = selectedZombie.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(pushForce);
                }
                if (AudioManager.instance != null)
                {
                    AudioManager.instance.PlayJump();
                }
            }

            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F1") + "s";

            for (int i = 0; i < zombies.Length; i++)
            {
                if (zombies[i].transform.position.y < loseY)
                {
                    state = GameState.GameOver;
                    gameOverPanel.SetActive(true);
                    gamePlayingPanel.SetActive(false);

                    finalTimeText.text = "Time: " + timer.ToString("F1") + "s";
                    finalScoreText.text = "Score: " + score + "/" + totalCoins;

                    FreezeZombies();

                    if (AudioManager.instance != null)
                    {
                        AudioManager.instance.PlayGameOverMusic();
                    }

                    break;
                }
            }
        }
    }

    public void StartGame()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayStart();
        }

        state = GameState.Playing;

        timer = 0f;
        timerText.text = "Time: 0.0s";

        score = 0;
        scoreText.text = score + "/" + totalCoins;

        startPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gamePlayingPanel.SetActive(true);

        UnfreezeZombies();
    }

    public void RestartGame()
    {
        Debug.Log("RESTART BUTTON CLICK");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score + "/" + totalCoins;
        AudioManager.instance.PlayCoin();
    }
}