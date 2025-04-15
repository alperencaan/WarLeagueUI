using UnityEngine;

public class Character
{
    private string _name;
    private Sprite _icon;

    public string Name 
    { 
        get => _name;
        set => _name = value;
    }

    public Sprite Icon
    {
        get => _icon;
        set => _icon = value;
    }

    public Character(string name, Sprite icon)
    {
        _name = name;
        _icon = icon;
    }
} 