using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerInfo")]
public class PlayerInfoSO : ScriptableObject
{

    public int WinCount;

    public int LoseCount;

    public PlayerInfoSO()
    {
        WinCount = 0;
        LoseCount = 0;
    }

}
