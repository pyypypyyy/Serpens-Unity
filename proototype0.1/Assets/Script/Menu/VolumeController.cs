using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private AudioSource menuAudio;
    private Slider audioSlider;

    void Start()
    {
        menuAudio = GameObject.FindGameObjectWithTag("Menu").transform.GetComponent<AudioSource>();

        audioSlider = GetComponent<Slider>();

  
       
    }

    private void Update()
    {
        VolumeControll();
        CloseGameSettings();
    }

    public void VolumeControll()
    {
        
        menuAudio.volume = audioSlider.value;
    }

    public void CloseGameSettings()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameObject Menu = GameObject.FindGameObjectWithTag("Menu").transform.gameObject;
            GameObject GameSettings = GameObject.FindGameObjectWithTag("GameSettings").transform.gameObject;

            Menu.transform.GetChild(0).gameObject.SetActive(true);
            GameSettings.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

}
