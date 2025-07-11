using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ToggleButton : MonoBehaviour
{
    private Image background;

    public ButtonEffectConfig buttonEffect;

    public bool isSelected = false;

    private FunctionSelector group;

    private Button myButton;
    private void Awake()
    {
        background = GetComponent<Image>();
        group = GetComponentInParent<FunctionSelector>();

        myButton = GetComponent<Button>();  
        myButton.onClick.AddListener(OnClick);
        Deselect(); 
    }

    public void OnClick()
    {
        group.Select(this);
    }

    public void Select()
    {
        isSelected = true;
        background.color = buttonEffect.pressedColor;
    }

    public void Deselect()
    {
        isSelected = false;
        background.color = buttonEffect.normalColor;
    }
}
