using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cereal : MonoBehaviour
{
    public static SortedDictionary<int,GameObject> BackupLoadList = new SortedDictionary<int,GameObject>();
    public static int PlayerGold = 0;

    public void Backup()
    {
        BackupLoadList = TurnController.initiative;
        PlayerGold = GlobalVars.GoldCount;
    }
}
