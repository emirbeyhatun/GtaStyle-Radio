using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RadioStation  {
    public Channels channelName;
    public string channelNameText;
    string currentSongName;
    public AudioClip[] songs;
    [HideInInspector]
    public float time = 0;
     float allSongTime=0;
    [HideInInspector]
    public float songTime =0;
    [HideInInspector]
    public AudioClip currentSongClip = null;
    [HideInInspector]
    public int currentSongClipIndex = 0;
   
    
    
    public void SetAllSongTime(float value)
    {
        allSongTime = value;
    }

    public void Time()
    {
       
       time= time % allSongTime;
    }

    public AudioClip GetSongBasedOnTime()
    {
        float counter = 0;
        
        for (int i = 0; i < songs.Length; i++)
        {
            if ((songs[i].length + counter)<time)
            {
                counter += songs[i].length;
               

            }
             if ((songs[i].length+counter)>=time)
            {
                currentSongClip = songs[i];
                currentSongClipIndex = i;
                songTime = time - counter;
                return currentSongClip;
            }
        }
        return currentSongClip;
    }

    public AudioClip NextSong()
    {
        if (  currentSongClipIndex == songs.Length - 1)
        {
            ShuffleSongs(ref songs);
        }
        currentSongClipIndex = (currentSongClipIndex + 1) % songs.Length;
        currentSongClip = songs[currentSongClipIndex];
       
        return currentSongClip;
    }
    public void ShuffleSongs(ref AudioClip[] array)
    {

        for (int i = array.Length; i > 1; i--)
        {
            // Pick random element to swap.
            int j = UnityEngine.Random.Range(0,i-1); // 0 <= j <= i-1
                                                     // Swap.
            AudioClip tmp = array[j];
            array[j] = array[i - 1];
            array[i - 1] = tmp;
        }
    }

}
