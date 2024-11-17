using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorseCodeConverter : MonoBehaviour {
    private Dictionary<char, string> morseCodeDictionary = new Dictionary<char, string>()
    {
        {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."},
        {'F', "..-."}, {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"},
        {'K', "-.-"}, {'L', ".-.."}, {'M', "--"}, {'N', "-."}, {'O', "---"},
        {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"},
        {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"}, {'Y', "-.--"},
        {'Z', "--.."}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"},
        {'4', "....-"}, {'5', "....."}, {'6', "-...."}, {'7', "--..."},
        {'8', "---.."}, {'9', "----."}, {'0', "-----"}, {' ', "/"} // space as '/'
    };

    public Sprite dotSprite;          // Sprite for dot (.)
    public Sprite dashSprite;         // Sprite for dash (-)
    public GameObject morsePanel;     // UI Panel for displaying Morse code
    public GameObject morseSymbolPrefab; // Prefab for individual Morse symbols

    public float unitTime = 0.2f;     // 1 unit of time for Morse code (adjustable)
    public float displaySpeed = 100f; // Speed of symbol movement from right to left
    public float symbolDisplayDuration = 2f; // Duration each symbol is visible

    public Transform startPos;        // Start position for symbols

    private List<string> morseCodeList; // Store the converted Morse code
    private Coroutine currentCoroutine; // Track the running coroutine

    public void GetString(string input) {
        // Check if a coroutine is already running
        if(currentCoroutine != null) {
            Debug.LogWarning("Morse code animation is already running. Please wait until it completes.");
            return; // Exit if already running
        }

        ConvertToMorse(input);
        currentCoroutine = StartCoroutine(DisplayMorseWithSpacing());
    }

    public void OnButtonDown() {
        GetString("Hello");
    }

    private void ConvertToMorse(string input) {
        morseCodeList = new List<string>();

        foreach(char letter in input.ToUpper()) {
            if(morseCodeDictionary.ContainsKey(letter)) {
                morseCodeList.Add(morseCodeDictionary[letter]);
            }
        }
    }

    private IEnumerator DisplayMorseWithSpacing() {
        ClearMorsePanel();

        // Loop through each Morse code sequence in the list
        foreach(string morse in morseCodeList) {
            // Loop through each symbol in the current Morse letter
            foreach(char symbol in morse) {
                // Instantiate the corresponding symbol (dot or dash)
                GameObject morseSymbol = Instantiate(morseSymbolPrefab, morsePanel.transform);
                morseSymbol.transform.position = startPos.position; // Set start position

                Image morseImage = morseSymbol.GetComponent<Image>();
                RectTransform symbolRect = morseSymbol.GetComponent<RectTransform>();

                if(symbol == '.') {
                    morseImage.sprite = dotSprite;
                } else if(symbol == '-') {
                    morseImage.sprite = dashSprite;
                    // Dash size: 3 units (scale the width of the dash)
                    symbolRect.sizeDelta = new Vector2(symbolRect.sizeDelta.x * 3, symbolRect.sizeDelta.y);
                }

                // Move the symbol from right to left
                StartCoroutine(MoveSymbol(morseSymbol));

                // Wait for the appropriate time between symbols within a letter (1 unit)
                yield return new WaitForSeconds(unitTime);
            }

            // Wait for the inter-character space (3 units) between letters
            yield return new WaitForSeconds(3 * unitTime);
        }

        // Reset the current coroutine reference after completion
        currentCoroutine = null;
    }

    private IEnumerator MoveSymbol(GameObject symbol) {
        RectTransform rectTransform = symbol.GetComponent<RectTransform>();

        // Start moving the symbol immediately after instantiation
        float moveDuration = 0f; // Duration the symbol has been moving
        float totalDuration = symbolDisplayDuration; // Total time before destruction

        // Move the symbol while it exists
        while(moveDuration < totalDuration) {
            // Move the symbol from right to left
            rectTransform.anchoredPosition += Vector2.left * (displaySpeed * Time.deltaTime);
            moveDuration += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        Destroy(symbol); // Remove symbol after the display duration
    }

    private void ClearMorsePanel() {
        foreach(Transform child in morsePanel.transform) {
            Destroy(child.gameObject);
        }
    }
}
