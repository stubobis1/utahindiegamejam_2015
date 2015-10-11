using UnityEngine;
using System.Collections;


public class TalkingController_simple : MonoBehaviour
{ 
    public AudioSource source;
    public AudioClip[] audios;

    // Use this for initialization
    void Start()
    {
     
    }


    // Update is called once per frame
    int currentAudio = 0;
    void Update()
    {
        if (!source.isPlaying)
        {
            if (currentAudio < audios.Length)
            {
                source.PlayOneShot(audios[currentAudio]);
                currentAudio++;
            }
            else
            {
                Application.LoadLevel(Application.loadedLevel +1);
            }
        }
    }
}
