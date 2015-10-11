using UnityEngine;
using System.Collections;

public class talkingAudio
{
    public talkingAudio(AudioSource audio, int character )
    {
        this.audio = audio;
        this.character = character;
    }
    public AudioSource audio;
    public int character;

}
public class TalkingController : MonoBehaviour
{

    public GameObject Charater1;
    public GameObject Charater2;

    private Animator char1Anim;
    private Animator char2Anim;
    public AudioSource source;
    public AudioClip[] audios;
    public int[] talkingChar;
    // Use this for initialization
    void Start()
    {
        char1Anim = Charater1.GetComponent<Animator>();
        char2Anim = Charater2.GetComponent<Animator>();

        char1Anim.enabled = false;
        char2Anim.enabled = false;
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
                CharTalk(talkingChar[currentAudio]);
                currentAudio++;
            }
            else
            {
                CharTalk(0);

                //GO TO NEXT LEVEL
                Application.LoadLevel(Application.loadedLevel +1);
            }
        }
    }


    void CharTalk(int charNumber)
    {
        char1Anim.enabled = false;
        char2Anim.enabled = false;
        if (charNumber == 1)
        {
            char1Anim.enabled = true;
        }
        else if(charNumber == 2)
        {
            char2Anim.enabled = true;
        }
    }
}
