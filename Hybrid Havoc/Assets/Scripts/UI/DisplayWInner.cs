using Kickstarter.Variables;
using UnityEngine;
using UnityEngine.UIElements;

public class DisplayWInner : MonoBehaviour
{
    [SerializeField] private StringVariable winner;

    private UIDocument document;
    
    private void Awake()
    {
        document = GetComponent<UIDocument>();
    }

    private void Start()
    {
        document.rootVisualElement.Q<Label>().text = $"Winner: {winner.Value}";
    }
}
