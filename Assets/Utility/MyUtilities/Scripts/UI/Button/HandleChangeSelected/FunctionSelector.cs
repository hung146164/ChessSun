using UnityEngine;

public class FunctionSelector : MonoBehaviour
{
    public ToggleButton[] buttons;

    
    private void Start()
    {
        buttons[0].OnClick();
    }
    public void Select(ToggleButton selected)
    {
        foreach (var btn in buttons)
        {
            if (btn == selected)
                btn.Select();
            else
                btn.Deselect();
        }
    }
}
