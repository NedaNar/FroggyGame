using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
public class MainMenu : MonoBehaviour
{
    public GameObject SettingsPanel;
	public GameObject InstructionsPanel;
	public GameObject ShopPanel;
    public GameObject MainPanel;
 	public Slider gameVolumeSlider;
	public TMP_Dropdown resolutionDropdown;
	private Resolution[] resolutions;
    void Start()
	{
		gameVolumeSlider.value = AudioListener.volume;
		gameVolumeSlider.onValueChanged.AddListener(delegate { GameVolumeChange(); });

		resolutions = Screen.resolutions;

		resolutionDropdown.ClearOptions();

		List<string> resolutionOptions = new List<string>();
		foreach (var resolution in resolutions)
		{
			resolutionOptions.Add(resolution.width + "x" + resolution.height);
		}
		resolutionDropdown.AddOptions(resolutionOptions);
		resolutionDropdown.value = GetCurrentResolutionIndex();
	}

    public void PlayGame()
    {
		SceneManager.LoadScene("MainScene");
		Time.timeScale = 1;
	}
    
    public void ShowSettingsPanel()
	{

        SettingsPanel.SetActive(true);
        MainPanel.SetActive(false);
	}

	public void ShowInstructionsPanel()
	{
        InstructionsPanel.SetActive(true);
        MainPanel.SetActive(false);
	}

	public void ShowShopPanel()
    {
        ShopPanel.SetActive(true);
        MainPanel.SetActive(false);
    }

	public void ExitGame()
	{
        #if UNITY_EDITOR
		    UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
	}

	public void Back()
	{
		MainPanel.SetActive(true);
		if (SettingsPanel.activeSelf)
		{
			SettingsPanel.SetActive(false);
		}
		if (InstructionsPanel.activeSelf)
		{
			InstructionsPanel.SetActive(false);
		}
		if (ShopPanel.activeSelf)
		{
			ShopPanel.SetActive(false);
		}
	}

	public void GameVolumeChange()
	{
		AudioListener.volume = gameVolumeSlider.value;
	}   

	private int GetCurrentResolutionIndex()
	{
		Resolution currentResolution = Screen.currentResolution;
		for (int i = 0; i < resolutions.Length; i++)
		{
			if (resolutions[i].width == currentResolution.width &&
				resolutions[i].height == currentResolution.height)
			{
				return i;
			}
		}
		return 0;
	}

	public void OnResolutionChanged()
	{
		string selectedResolutionStr = resolutionDropdown.options[resolutionDropdown.value].text;
		string[] resolutionParts = selectedResolutionStr.Split('x');
		if (resolutionParts.Length == 2 && int.TryParse(resolutionParts[0], out int width) && int.TryParse(resolutionParts[1], out int height))
		{
			Screen.SetResolution(width, height, Screen.fullScreen);
		}
	}
}