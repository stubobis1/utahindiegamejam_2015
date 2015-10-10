using UnityEngine;
using System.Collections;

public class TalkingController : MonoBehaviour
{

    public GameObject Charater1;
    public GameObject Charater2;

    private Animator char1Anim;
    private Animator char2Anim;
    // Use this for initialization
    void Start()
    {
        char1Anim = Charater1.GetComponent<Animator>();
        char2Anim = Charater2.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        char1Anim.enabled = false;
    }
}
