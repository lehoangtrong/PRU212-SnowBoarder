
// 4. ScoreEntryUI.cs
// Gắn script này vào Prefab của một dòng điểm (ScoreEntryTemplate)
using UnityEngine;
using TMPro; // Để dùng TextMeshPro

public class ScoreEntryUI : MonoBehaviour
{
    // Kéo các đối tượng Text tương ứng vào đây trong Inspector của Prefab
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;

    // Hàm nhận dữ liệu và hiển thị lên UI
    public void Setup(int rank, string playerName, int score)
    {
        rankText.text = rank.ToString() + ".";
        nameText.text = playerName;
        scoreText.text = score.ToString();
    }
}