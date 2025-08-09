using UnityEngine;
using TMPro; // Required for TextMeshPro

public class CatReturnGame : MonoBehaviour
{
    public Transform xrOrigin;               // Assign your XR Origin here
    public float returnSpeed = 15f;          // Cat return speed
    public float playerWinDistance = 1.5f;   // Distance for player to win
    public TextMeshProUGUI resultText;       // Assign your TMP text in Inspector

    private Vector3 startPosition;           // Position where cat was grabbed
    private bool isReturning = false;
    private bool gameEnded = false;

    void Update()
    {
        if (isReturning && !gameEnded)
        {
            // Move cat towards start position
            transform.position = Vector3.MoveTowards(transform.position, startPosition, returnSpeed * Time.deltaTime);

            // Cat reaches first
            if (Vector3.Distance(transform.position, startPosition) < 0.1f)
            {
                EndGame(false); // Cat wins
            }

            // Player reaches first
            if (Vector3.Distance(xrOrigin.position, startPosition) <= playerWinDistance)
            {
                EndGame(true); // Player wins
            }
        }
    }

    // Called when grabbing starts
    public void OnGrab()
    {
        startPosition = transform.position; // Save the grab position
        gameEnded = false;
        isReturning = false;

        if (resultText != null)
            resultText.text = ""; // Clear previous result
    }

    // Called when released
    public void OnRelease()
    {
        isReturning = true; // Start return immediately
    }

    void EndGame(bool playerWon)
    {
        gameEnded = true;
        isReturning = false;

        string resultMessage = playerWon ? "Player Wins!" : "Cat Wins!";

        // Show result in TextMeshPro
        if (resultText != null)
            resultText.text = resultMessage;
        else
            Debug.LogWarning("ResultText not assigned in Inspector");

        Debug.Log(resultMessage);
    }
}
