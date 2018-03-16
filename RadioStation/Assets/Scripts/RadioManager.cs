using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Channels
{
    None,
    Beat,
    LibertyRock,
    ElectroChoc,
    WKTT,
    JNR,
    LCHC,
    PLR,
   
}
public class RadioManager : MonoBehaviour {
    [SerializeField]
    private RadioStation[] rS;
    public RadioStation currentRS;
    private int currentRsIndex = 0;
    private AudioClip currentSong;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject RadioUI;
    [SerializeField]
    private GameObject TutorialUI;

    public GameObject RadioChannelsSelectUI;
    public Text RadioText;
    private static RadioManager instance;
    public AudioClip clickSound;
    public static RadioManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = GetComponent<RadioManager>();
      
    }
    private void Start()
    {
        SetInitialSongs();
        audioSource = GetComponent<AudioSource>();
       
      // OnRadioOpen();
    }

    private void Update()
    {
        SetTime();
        OnFinishNextSong();


        if (Input.GetKey(KeyCode.Q))
        {
            RadioUI.SetActive(true);
            TutorialUI.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            RadioUI.SetActive(false);
            TutorialUI.SetActive(true);

        }
        if (RadioUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SetPreviousRadioStation();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                SetNextRadioStation();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                audioSource.time = audioSource.clip.length - 1;
            } 
        }

    }
    private void OnFinishNextSong()
    {
        if (currentRS!=null  )
        {

            if (currentRS.currentSongClip != null && currentRS.currentSongClip.length - 0.1f <= audioSource.time)
            {
                audioSource.time = 0;//to stop update function 
                audioSource.clip = currentRS.NextSong();
                audioSource.Stop();
                audioSource.Play(); 
            }

        }
    }
    
    private void SetNextRadioStation()
    {
        currentRsIndex = (currentRsIndex + 1) % rS.Length;
        currentRS = rS[currentRsIndex];
        SetAudio();

    }
    private void SetPreviousRadioStation()
    {
        currentRsIndex = (currentRsIndex - 1) % rS.Length;
        currentRS = rS[currentRsIndex];
        SetAudio();

    }
    public void ChangeRS(Channels channel)
    {
       
        int currentIndex=0;
        for (int i = 0; i < rS.Length; i++)
        {
            if (channel == rS[i].channelName)
            {
               
                currentIndex = i;
                break;
            }
            if(i== rS.Length - 1)
            {
                //no match
              
                channel = Channels.None;
            }
        }
        switch (channel)
        {
            case Channels.None:
                audioSource.Stop();
                RadioText.text = "None";
                break;
            default:
                currentRsIndex = currentIndex;
                currentRS = rS[currentIndex];
                RadioText.text = currentRS.channelNameText.ToString();
                SetAudio();
                break;
            
        }
        
    }
    private void SetTime()
    {
        for (int i = 0; i < rS.Length; i++)
        {
            rS[i].time = Time.time;
            rS[i].Time();

        }
    }
    private void SetInitialSongs()
    {
        for (int i = 0; i < rS.Length; i++)
        {

            float songTime = 0;
            for (int j = 0; j < rS[i].songs.Length; j++)
            {
                songTime += rS[i].songs[j].length;
            }
            rS[i].SetAllSongTime(songTime);

        }
    }
    private void  OnRadioOpen()
    {
        for (int i = 0; i < rS.Length; i++)
        {
            if (currentRS==null)
            {
                currentRS = rS[0];
                SetAudio();

                return;

            }else if (currentRS == rS[i])
            {
                SetAudio();
            }
        }
    }

    private void SetAudio()
    {
        if (currentRS.GetSongBasedOnTime()!=null)
        {
            if (currentRS.currentSongClipIndex == currentRS.songs.Length - 1 && currentRS.currentSongClip.length-2<audioSource.time)
            {
               
                currentRS.ShuffleSongs(ref currentRS.songs);
            }
            currentSong = currentRS.GetSongBasedOnTime();
            
        }
        audioSource.clip = currentSong;
        audioSource.time = currentRS.songTime;
        audioSource.Play();
    }
}
