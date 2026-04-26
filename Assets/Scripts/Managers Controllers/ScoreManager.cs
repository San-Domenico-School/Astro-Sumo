using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Singleton")]
    public static ScoreManager Instance;

    [Header("Score")]
    public int currentScore { get; private set; }
    public int highScore { get; private set; }

    [Header("Multiplier")]
    public float currentMultiplier = 1f;
    public float multiplierIncrement = 0.5f;
    public float multiplierMax = 4f;
    public float multiplierDecayTime = 5f;
    private float multiplierTimer;

    [Header("UI (optional)")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI multiplierText;

    // -------------------------------------------------------

    private void Awake()
    {
        // Singleton enforcement
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Update()
    {
        HandleMultiplierDecay();
    }

    // -------------------------------------------------------
    // Public API
    // -------------------------------------------------------

    /// <summary>Adds score using the current multiplier.</summary>
    public void AddScore(int baseValue)
    {
        int finalValue = Mathf.RoundToInt(baseValue * currentMultiplier);
        currentScore += finalValue;

        CheckHighScore();
        UpdateUI();

        Debug.Log($"[ScoreManager] +{finalValue} (base {baseValue} x{currentMultiplier}) | Total: {currentScore}");
    }

    /// <summary>Increases the multiplier, capped at multiplierMax.</summary>
    public void IncreaseMultiplier()
    {
        currentMultiplier = Mathf.Min(currentMultiplier + multiplierIncrement, multiplierMax);
        multiplierTimer = multiplierDecayTime;  // reset decay timer
        UpdateUI();
    }

    /// <summary>Resets multiplier back to 1.</summary>
    public void ResetMultiplier()
    {
        currentMultiplier = 1f;
        multiplierTimer = 0f;
        UpdateUI();
    }

    /// <summary>Resets score and multiplier (e.g. new game).</summary>
    public void ResetScore()
    {
        currentScore = 0;
        ResetMultiplier();
        UpdateUI();
    }

    // -------------------------------------------------------
    // Private helpers
    // -------------------------------------------------------

    private void HandleMultiplierDecay()
    {
        if (currentMultiplier <= 1f) return;

        multiplierTimer -= Time.deltaTime;
        if (multiplierTimer <= 0f)
        {
            ResetMultiplier();
        }
    }

    private void CheckHighScore()
    {
        if (currentScore <= highScore) return;

        highScore = currentScore;
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {currentScore}";

        if (highScoreText != null)
            highScoreText.text = $"Best: {highScore}";

        if (multiplierText != null)
            multiplierText.text = currentMultiplier > 1f ? $"x{currentMultiplier:0.0}" : "";
    }
}