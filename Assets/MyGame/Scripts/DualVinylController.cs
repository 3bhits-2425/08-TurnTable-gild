using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DualVinylController : MonoBehaviour
{
    [Header("Left")]
    public Transform vinylL;
    public AudioSource audioL;
    public Slider speedSliderL;
    public Slider volumeSliderL;
    public TMP_Text playPauseTextL;
    public Button playPauseButtonL;
    public Button stopButtonL;

    [Header("Right")]
    public Transform vinylR;
    public AudioSource audioR;
    public Slider speedSliderR;
    public Slider volumeSliderR;
    public TMP_Text playPauseTextR;
    public Button playPauseButtonR;
    public Button stopButtonR;

    [Header("Shared")]
    public Slider crossfadeSlider;

    private bool isPlayingL = false;
    private bool isPlayingR = false;

    private float baseSpeed = 100f;
    private float basePitch = 1f;

    void Start()
    {
        playPauseButtonL.onClick.AddListener(() => TogglePlayPause(ref isPlayingL, audioL, playPauseTextL));
        playPauseButtonR.onClick.AddListener(() => TogglePlayPause(ref isPlayingR, audioR, playPauseTextR));

        stopButtonL.onClick.AddListener(() => StopAudio(ref isPlayingL, audioL, playPauseTextL));
        stopButtonR.onClick.AddListener(() => StopAudio(ref isPlayingR, audioR, playPauseTextR));
    }

    void Update()
    {
        // Rotation
        if (isPlayingL)
            vinylL.Rotate(Vector3.forward * baseSpeed * speedSliderL.value * Time.deltaTime);
        if (isPlayingR)
            vinylR.Rotate(Vector3.forward * baseSpeed * speedSliderR.value * Time.deltaTime);

        // Pitch & Volume
        audioL.pitch = basePitch * speedSliderL.value;
        audioR.pitch = basePitch * speedSliderR.value;

        float cf = crossfadeSlider.value;
        audioL.volume = volumeSliderL.value * (1f - cf);
        audioR.volume = volumeSliderR.value * cf;
    }

    void TogglePlayPause(ref bool isPlaying, AudioSource source, TMP_Text btnText)
    {
        isPlaying = !isPlaying;
        if (isPlaying)
        {
            if (!source.isPlaying) source.Play();
            else source.UnPause();
            btnText.text = "Pause";
        }
        else
        {
            source.Pause();
            btnText.text = "Play";
        }
    }

    void StopAudio(ref bool isPlaying, AudioSource source, TMP_Text btnText)
    {
        isPlaying = false;
        source.Stop();
        source.time = 0f;
        btnText.text = "Play";
    }
}
