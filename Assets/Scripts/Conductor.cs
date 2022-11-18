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
    
    public int nextIndex;
    public int nextIndexBPM;
    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;
    private float oldSongPosition;
    //Current song position, in beats
    public float songPositionInBeats;
    public int beatsShownInAdvance;
    //How many seconds have passed since the song started
    public float dspSongTime;
    public bool started;
    public float firstBeatOffset;
    public GameObject lane_1;
    public GameObject lane_2;
    public GameObject lane_3;
    public GameObject lane_4;
    public TextAsset beatMap;
    [System.NonSerialized]
    public float[,] notes;
    public int[] bpmChanges;
    public float[] bpmChangeTimes;
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
        //add a delay
        notes = get_beat_map(beatMap);
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        oldSongPosition = songPosition;
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        if (nextIndexBPM < bpmChanges.Length && (float)bpmChangeTimes[nextIndexBPM] <= songPosition)
        {
            Debug.Log("Changed BPM");
            songBpm = bpmChanges[nextIndexBPM];
            secPerBeat = 60f / songBpm;
            nextIndexBPM++;
        }
        //determine how many beats since the song started
        songPositionInBeats += (songPosition-oldSongPosition) / secPerBeat;
        if (nextIndex < notes.GetLength(0) && notes[nextIndex,0] < songPositionInBeats + beatsShownInAdvance)
        {   
            if (notes[nextIndex,1] == 1)
            {
            NoteObject noteobject = ((GameObject) Instantiate(lane_1, new Vector3(-1.5f,10f,1f),lane_1.transform.rotation)).GetComponent<NoteObject>();
            noteobject.initialize(notes[nextIndex,0]);
            
            //initialize the fields of the music note
            }
            else if (notes[nextIndex,1] == 2)
            {
                NoteObject noteobject = ((GameObject) Instantiate(lane_1, new Vector3(-1.5f,10f,1f),lane_1.transform.rotation)).GetComponent<NoteObject>();
                noteobject.initialize(notes[nextIndex,0],true,find_nearest_three(1,nextIndex));
            }
            if (notes[nextIndex,2] == 1)
            {
            NoteObject noteobject = ((GameObject) Instantiate(lane_2, new Vector3(-0.5f,10f,1f),lane_2.transform.rotation)).GetComponent<NoteObject>();
            noteobject.initialize(notes[nextIndex,0]);

            //initialize the fields of the music note
            }
            else if (notes[nextIndex,2] == 2)
            {
                NoteObject noteobject = ((GameObject) Instantiate(lane_2, new Vector3(-0.5f,10f,1f),lane_2.transform.rotation)).GetComponent<NoteObject>();
                noteobject.initialize(notes[nextIndex,0],true,find_nearest_three(2,nextIndex));
            }
            if (notes[nextIndex,3] == 1)
            {
            NoteObject noteobject = ((GameObject) Instantiate(lane_3, new Vector3(0.5f,10f,1f),lane_3.transform.rotation)).GetComponent<NoteObject>();
            noteobject.initialize(notes[nextIndex,0]);

            //initialize the fields of the music note
            }
            else if (notes[nextIndex,3] == 2)
            {
                NoteObject noteobject = ((GameObject) Instantiate(lane_3, new Vector3(0.5f,10f,1f),lane_3.transform.rotation)).GetComponent<NoteObject>();
                noteobject.initialize(notes[nextIndex,0],true,find_nearest_three(3,nextIndex));
            }
            if (notes[nextIndex,4] == 1)
            {
            NoteObject noteobject = ((GameObject) Instantiate(lane_4, new Vector3(1.5f,10f,1f),lane_4.transform.rotation)).GetComponent<NoteObject>();
            noteobject.initialize(notes[nextIndex,0]);
            //initialize the fields of the music note
            }
            else if (notes[nextIndex,4] == 2)
            {
                NoteObject noteobject = ((GameObject) Instantiate(lane_4, new Vector3(1.5f,10f,1f),lane_4.transform.rotation)).GetComponent<NoteObject>();
                noteobject.initialize(notes[nextIndex,0],true,find_nearest_three(4,nextIndex));
            }
            nextIndex++;
        }
    }
    public float find_nearest_three(int column,int startingPosition)
    {
        int scroll = startingPosition;
        while (scroll < notes.GetLength(0))
        {
            if (notes[scroll,column] == 3)
            {
                return notes[scroll,0];
            }
            scroll++;
        }
        return -1f;
    }
    public float[,] get_beat_map(TextAsset textfile){
        var notelist = new List<List<float>>();
        var internal_list= new List<float>();
        string text = "";
        if (textfile != null)
        {
            string[] lines = (textfile.text.Split('\n'));
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    if (c != ',')
                        text += c;
                    else
                    {
                        internal_list.Add(float.Parse(text));
                        text = "";
                    }
                }
                Debug.Log(text);
                if (text.Length > 0)
                {
                    internal_list.Add(float.Parse(text));
                }
                text = "";
                var copy = new List<float>();
                foreach(float elt in internal_list)
                {
                    Debug.Log(elt);
                    copy.Add(elt);
                }
                if (copy.Count > 0)
                {
                    notelist.Add(copy);
                    internal_list = new List<float>();
                }
            }
            float[,] returnArray = new float[notelist.Count,5];
            for (int i = 0; i < notelist.Count; i++)
            {
                returnArray[i,0] = notelist[i][0];
                returnArray[i,1] = notelist[i][1];
                returnArray[i,2] = notelist[i][2];
                returnArray[i,3] = notelist[i][3];
                returnArray[i,4] = notelist[i][4];
            }
            
            return returnArray;
        }
        else
        {
            return new float[,]{{0,0,0,0,0}};
        }
        
    }
    
}
