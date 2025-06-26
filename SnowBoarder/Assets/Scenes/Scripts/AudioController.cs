using UnityEngine;
using UnityEngine.UI;
using TMPro; // Thêm dòng này để sử dụng TextMeshPro

public class AudioController : MonoBehaviour
{
    [Header("Audio")]
    // Kéo tất cả các file nhạc nền của bạn vào đây
    public AudioClip[] backgroundMusicPlaylist;
    // Component AudioSource sẽ phát nhạc, sẽ được tự động lấy
    private AudioSource backgroundMusicSource;

    [Header("Audio Source")]
    // Kéo Audio Source dành cho SFX vào đây
    [SerializeField] private AudioSource startSound;

    [Header("UI Components")]
    // Kéo Slider điều chỉnh âm lượng vào đây
    public Slider volumeSlider;
    // Kéo nút "Tới" vào đây
    public Button nextTrackButton;
    // Kéo nút "Lùi" vào đây
    public Button previousTrackButton;
    // Kéo Text hiển thị tên bài hát vào đây
    public TextMeshProUGUI songNameText;

    // Tên key để lưu giá trị trong PlayerPrefs
    private const string VOLUME_KEY = "MasterVolume";
    private const string TRACK_KEY = "SelectedTrack";

    private int currentTrackIndex = 0;

    void Awake()
    {
        // Tự động lấy component AudioSource trên cùng một GameObject
        backgroundMusicSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // -- CÀI ĐẶT ÂM LƯỢNG --
        // Tải giá trị âm lượng đã lưu, nếu không có thì mặc định là 1
        float savedVolume = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);
        backgroundMusicSource.volume = savedVolume;
        volumeSlider.value = savedVolume;
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // -- CÀI ĐẶT DANH SÁCH NHẠC --
        if (backgroundMusicPlaylist.Length > 0)
        {
            // Tải bài hát đã lưu, nếu không có thì mặc định là bài đầu tiên
            currentTrackIndex = PlayerPrefs.GetInt(TRACK_KEY, 0);

            // Đảm bảo index không vượt quá số lượng bài hát
            if (currentTrackIndex >= backgroundMusicPlaylist.Length)
            {
                currentTrackIndex = 0;
            }

            PlayTrack(currentTrackIndex);

            // Gán sự kiện cho các nút bấm
            nextTrackButton.onClick.AddListener(PlayNextTrack);
            previousTrackButton.onClick.AddListener(PlayPreviousTrack);
        }
        else
        {
            Debug.LogWarning("Chưa có bài hát nào trong playlist!");
        }
    }

    // Hàm phát một bài hát dựa trên vị trí (index) của nó trong danh sách
    void PlayTrack(int trackIndex)
    {
        if (trackIndex < 0 || trackIndex >= backgroundMusicPlaylist.Length) return;

        currentTrackIndex = trackIndex;
        backgroundMusicSource.clip = backgroundMusicPlaylist[currentTrackIndex];
        backgroundMusicSource.Play();

        // Cập nhật tên bài hát lên UI
        if (songNameText != null)
        {
            songNameText.text = backgroundMusicSource.clip.name;
        }

        // Lưu lại bài hát vừa chọn
        PlayerPrefs.SetInt(TRACK_KEY, currentTrackIndex);
    }

    // Hàm được gọi bởi nút "Tới"
    public void PlayNextTrack()
    {
        int nextTrackIndex = (currentTrackIndex + 1) % backgroundMusicPlaylist.Length;
        PlayTrack(nextTrackIndex);
    }

    // Hàm được gọi bởi nút "Lùi"
    public void PlayPreviousTrack()
    {
        int previousTrackIndex = (currentTrackIndex - 1 + backgroundMusicPlaylist.Length) % backgroundMusicPlaylist.Length;
        PlayTrack(previousTrackIndex);
    }

    // Hàm này được gọi mỗi khi người dùng thay đổi giá trị của Slider
    public void OnVolumeChanged(float value)
    {
        backgroundMusicSource.volume = value;
        PlayerPrefs.SetFloat(VOLUME_KEY, value);
    }

    private void OnDestroy()
    {
        // Dọn dẹp listener khi đối tượng bị hủy
        if (volumeSlider != null) volumeSlider.onValueChanged.RemoveAllListeners();
        if (nextTrackButton != null) nextTrackButton.onClick.RemoveAllListeners();
        if (previousTrackButton != null) previousTrackButton.onClick.RemoveAllListeners();
    }

    public void PlaySound(AudioClip clip)
    {
        // Kiểm tra xem clip và source có tồn tại không
        if (clip != null && startSound != null)
        {
            // PlayOneShot cho phép phát nhiều âm thanh chồng lên nhau
            // Rất phù hợp cho tiếng bấm nút hoặc các hiệu ứng ngắn
            startSound.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Sound clip hoặc SFX Audio Source chưa được gán!");
        }
    }
}
