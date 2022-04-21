using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScrollingText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float scrollSpeed = 10.0f;

    private TextMeshProUGUI cloneText;
    private RectTransform textRectTransform;
    private List<string> message;

    // Use this for initialization
    void Awake()
    {
        // messages db
        message = new List<string>
        {
            " Thank you mario! but our princess is in another castle!",
            " It's dangerous to go alone! take this.",
            " I like shorts! They’re comfy and easy to wear!",
            " You are in a maze of twisty passages, all alike."
        };

        text.text = message[Random.Range(0, message.Count)];
        textRectTransform = text.GetComponent<RectTransform>();

        cloneText = Instantiate(text) as TextMeshProUGUI;
        RectTransform cloneRectTransform = cloneText.GetComponent<RectTransform>();
        cloneRectTransform.SetParent(textRectTransform);
        cloneRectTransform.localPosition = new Vector3(text.preferredWidth, 0, cloneRectTransform.position.z);
        cloneRectTransform.localScale = new Vector3(1, 1, 1);
        cloneText.text = text.text;

    }

    private IEnumerator Start()
    {
        float width = text.preferredWidth;
        Vector3 startPosition = textRectTransform.localPosition;

        float scrollPosition = 0;

        while (true)
        {
            textRectTransform.localPosition = new Vector3(-scrollPosition % width, startPosition.y, startPosition.z);
            scrollPosition += scrollSpeed * 20 * Time.deltaTime;
            yield return null;
        }
    }
}
