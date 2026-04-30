using UnityEngine;
using UnityEngine.UIElements; // Required for UI Toolkit
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private Button startButton;

    // Update is called once per frame
    void OnEnable()
    {
        // 1. Get the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        // 2. Query the button by the name you set in UI Builder
        startButton = uiDocument.rootVisualElement.Q<Button>("StartButton");

        // 3. Register the callback (the OnClick logic)
        startButton.clicked += OnButtonClicked;
    }

    void OnDisable()
    {
        // Clean up the event listener when the object is disabled
        startButton.clicked -= OnButtonClicked;
    }

    void OnButtonClicked()
    {
        GlobalEvents.SendSceneIndex();
        SceneManager.LoadScene(1);
        
    }
}
