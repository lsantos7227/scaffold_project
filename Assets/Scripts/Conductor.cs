using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    // Start is called before the first frame update
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;
    public float bpm;
    public float[] notes;
    public int nextIndex = 0;
    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;
    public int beatsShownInAdvance = 3;
    //How many seconds have passed since the song started
    public float dspSongTime;
    public bool started;
    public float firstBeatOffset = 4;
    public GameObject lane_1;
    public GameObject lane_2;
    public GameObject lane_3;
    public GameObject lane_4;
    //an AudioSource attached to this GameObject that will play the music.
public AudioSource musicSource;
    void Start()
    {
         //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
        if (nextIndex < notes.Length && notes[nextIndex] < songPositionInBeats + beatsShownInAdvance)
{
            NoteObject noteobject = ((GameObject) Instantiate(lane_1, new Vector3(-1.5f,7f,1f),lane_1.transform.rotation)).GetComponent<NoteObject>();
            noteobject.initialize(notes[nextIndex]);

            //initialize the fields of the music note

            nextIndex++;
}
    }
    
}
