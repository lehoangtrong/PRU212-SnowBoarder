// SaveSystem.cs (Phiên bản lưu vào ổ D:)
using UnityEngine;
using System.IO; // Thêm vào để làm việc với file và thư mục

public static class SaveSystem
{
    // THAY ĐỔI: Đặt đường dẫn cố định đến ổ D:
    // Bạn có thể đổi "MyGameSaves" thành tên thư mục bạn muốn.
    private static readonly string saveDirectory = @"D:\MyGameSaves";
    private static readonly string savePath = Path.Combine(saveDirectory, "leaderboard.json");

    public static void SaveScores(ScoreList scoreList)
    {
        try
        {
            // Đảm bảo thư mục tồn tại trước khi ghi file
            Directory.CreateDirectory(saveDirectory);

            // Chuyển đối tượng C# thành một chuỗi JSON
            string json = JsonUtility.ToJson(scoreList, true);

            // Ghi chuỗi JSON vào file
            File.WriteAllText(savePath, json);
            Debug.Log("Leaderboard saved to: " + savePath);
        }
        catch (System.Exception e)
        {
            // In ra lỗi nếu không thể ghi file (ví dụ: không có quyền, không có ổ D:)
            Debug.LogError("Failed to save data to " + savePath + ". Error: " + e.Message);
        }
    }

    public static ScoreList LoadScores()
    {
        // Kiểm tra xem file có tồn tại không
        if (File.Exists(savePath))
        {
            try
            {
                // Đọc toàn bộ nội dung file JSON
                string json = File.ReadAllText(savePath);

                // Chuyển chuỗi JSON trở lại thành đối tượng C#
                ScoreList scoreList = JsonUtility.FromJson<ScoreList>(json);
                Debug.Log("Leaderboard loaded from: " + scoreList);
                return scoreList;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load data from " + savePath + ". Error: " + e.Message);
                // Trả về danh sách rỗng nếu có lỗi khi đọc file
                return new ScoreList();
            }
        }
        else
        {
            // Nếu file không tồn tại, trả về một danh sách rỗng mới
            Debug.LogWarning("Save file not found at " + savePath + ". Creating a new one.");
            return new ScoreList();
        }
    }
}
