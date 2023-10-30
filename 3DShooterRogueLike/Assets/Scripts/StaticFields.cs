using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFields
{
    public static string MAIN_MENU_SCENE = "MainMenu";

    public static string COMBAT_SCENE = "CombatMenu";

    public static string SAVE_DATA = "PlayerData";

    public static Dictionary<AnamationParameters, string> ANIMATION_PARAMETERS = new Dictionary<AnamationParameters, string>() 
    {
        { AnamationParameters.Death, "Death"},
        { AnamationParameters.Speed, "Speed"},
        { AnamationParameters.Jump, "Jump"},
        { AnamationParameters.Grounded, "Grounded"},
        { AnamationParameters.FreeFall, "FreeFall"},
        { AnamationParameters.Reload, "Reload"}
    };
}
public enum AnamationParameters
{
    Death,
    Speed,
    Jump,
    Grounded,
    FreeFall,
    Reload
}

