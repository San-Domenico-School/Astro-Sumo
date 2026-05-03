using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements; // Required for UI Toolkit
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private int countdown = 5;
    private Label _countdownLabel;
    private AudioSource audioSource;
    public bool beginGameSequence;
    private bool _hasStarted = false; // The gatekeeper
    private int fontSize;

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        var root = GetComponent<UIDocument>().rootVisualElement;
        _countdownLabel = root.Q<Label>("CountdownLabel");

        // Ensure the class is REMOVED at the start so it begins small/invisible
        _countdownLabel.RemoveFromClassList("countdown-pop");
    }

    void Update()
    {
        if(beginGameSequence && !_hasStarted) 
        {
            _hasStarted = true;
            StartCoroutine(CountdownSequence());
        }
    }

    IEnumerator CountdownSequence()
    {
       while (countdown > 0)
        {
            Debug.Log("Counting: " + countdown);
            _countdownLabel.text = countdown.ToString();

            // 1. Force the size to 0 and kill the transition for a split second
            // This "resets" the animation state instantly
            _countdownLabel.style.transitionDuration = new StyleList<TimeValue>(new List<TimeValue> { new TimeValue(0) });
            _countdownLabel.style.fontSize = 0;
            _countdownLabel.style.scale = new StyleScale(Vector2.zero);
            _countdownLabel.style.opacity = 0;

            // 2. WAIT ONE FRAME
            // This allows the UI engine to render the label at size 0
            yield return null;

            // 3. Turn the transition back on and set the target size
            // Now Unity sees: "Oh! It's 0 and needs to be 100. Time to animate!"
            _countdownLabel.style.transitionDuration = new StyleList<TimeValue>(new List<TimeValue> { new TimeValue(0.3f, TimeUnit.Second) });
            _countdownLabel.style.fontSize = 144;
            _countdownLabel.style.scale = new StyleScale(Vector2.one);
            _countdownLabel.style.opacity = 1;
            
            // 4. Wait for the second to pass before the next loop iteration
            yield return new WaitForSeconds(1.5f);
            countdown--;
        }

        fontSize = 144;
        _countdownLabel.style.fontSize = fontSize;
        _countdownLabel.text = "GO!";
        // Keep the pop class on for "GO!"
        InvokeRepeating("Transition", 0, .01f);
        yield return new WaitForSeconds(3.5f);
        BeginGame();
    }
    
    void Transition()
    {
        fontSize += 1;
        audioSource.volume -= 0.0025f;
        _countdownLabel.style.fontSize = fontSize;
    }
    
    void BeginGame()
    {
        Debug.Log("Game Started!");
        // Hide the countdown label or switch scenes here
        _countdownLabel.style.display = DisplayStyle.None;
        GlobalEvents.SendSceneIndex();
        SceneManager.LoadScene(1);
    }
}
