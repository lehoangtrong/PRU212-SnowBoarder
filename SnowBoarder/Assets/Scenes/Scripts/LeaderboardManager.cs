
// 3. LeaderboardManager.cs
// Gắn script này vào một GameObject trống trong Scene, hoặc vào chính Panel "You Win".
using UnityEngine;
using System.Collections.Generic; // Để dùng List
using System.Linq; // Để dùng sắp xếp OrderByDescending

public class LeaderboardManager : MonoBehaviour
{
    [Header("UI References")]
    // Kéo Prefab của một dòng điểm vào đây
    public GameObject scoreEntryPrefab;
    // Kéo GameObject "Content" của ScrollView vào đây
    public Transform contentParent;

    void Start()
    {
        // (Tùy chọn) Thêm một vài điểm giả để kiểm tra
        // AddNewScore("Player_B", 250);
        // AddNewScore("Player_C", 100);

        // Hiển thị bảng xếp hạng khi bắt đầu
        PopulateLeaderboard();
    }

    // Hàm này được gọi từ GameManager khi người chơi thắng
    public void AddNewScore(string playerName, int newScore)
    {
        // Tải danh sách điểm hiện tại
        ScoreList scoreList = SaveSystem.LoadScores();

        // Thêm điểm mới vào danh sách
        scoreList.scores.Add(new ScoreEntry { playerName = playerName, score = newScore });

        // Lưu lại danh sách đã cập nhật
        SaveSystem.SaveScores(scoreList);
    }

    private void PopulateLeaderboard()
    {
        // Xóa tất cả các dòng cũ để tránh hiển thị trùng lặp
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Tải danh sách điểm
        ScoreList scoreList = SaveSystem.LoadScores();

        // Sắp xếp danh sách điểm từ cao đến thấp
        List<ScoreEntry> sortedScores = scoreList.scores.OrderByDescending(s => s.score).ToList();

        Debug.Log("Loaded " + sortedScores.Count + " scores. Attempting to display them.");

        // Hiển thị từng dòng điểm lên UI
        for (int i = 0; i < sortedScores.Count; i++)
        {
            // Tạo một bản sao của Prefab
            GameObject entryObject = Instantiate(scoreEntryPrefab, contentParent);

            // Lấy script UI từ bản sao đó
            ScoreEntryUI entryUI = entryObject.GetComponent<ScoreEntryUI>();

            // Gửi dữ liệu vào để hiển thị
            entryUI.Setup(i + 1, sortedScores[i].playerName, sortedScores[i].score);
        }
    }
}