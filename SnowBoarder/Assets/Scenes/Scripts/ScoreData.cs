using UnityEngine;
using System.IO; // Thêm vào để làm việc với file

[System.Serializable]
public class ScoreEntry
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class ScoreList
{
    public System.Collections.Generic.List<ScoreEntry> scores = new System.Collections.Generic.List<ScoreEntry>();
}
