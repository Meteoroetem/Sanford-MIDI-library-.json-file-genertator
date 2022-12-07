using Newtonsoft.Json;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Sanford_MIDI
{
    public partial class Form1 : Form
    {
        InputDevice midiIn;
        List<int> keysPressed = new List<int>();
        List<int[]> notesPlayed = new List<int[]>();
        public Form1()
        {
            InitializeComponent();
        }

        private void pianoControl2_PianoKeyDown(object sender, Sanford.Multimedia.Midi.UI.PianoKeyEventArgs e)
        {
            int noteID = e.NoteID;
            int timestamp = Environment.TickCount;

            notesPlayed.Add(new int[2] { noteID, timestamp });
        }
        private void pianoControl2_PianoKeyUp(object sender, Sanford.Multimedia.Midi.UI.PianoKeyEventArgs e)
        {
            int noteID = e.NoteID;
            int timestamp = Environment.TickCount;
            //Sets the length of the note. 
            notesPlayed.FindLast(note => note[0] == noteID)[1] = timestamp - notesPlayed.Find(note => note[0] == noteID)[1];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            midiIn = new InputDevice(0);
            midiIn.StartRecording();
            midiIn.ChannelMessageReceived += HandleChannelMessageRecieved;

        }

        private void HandleChannelMessageRecieved(object sender, ChannelMessageEventArgs e)
        {
            int noteID = e.Message.Data1;
            int timestamp = e.Message.Timestamp;
            if (!keysPressed.Contains(noteID))
            {
                keysPressed.Add(noteID);
                notesPlayed.Add(new int[2] { noteID, timestamp });
                toolStripProgressBar1.Value = e.Message.Data2;
            }
            else
            {
                keysPressed.Remove(noteID);
                pianoControl2.ReleasePianoKey(noteID);
                //Sets the length of the note. 
                notesPlayed.FindLast(note => note[0] == noteID)[1] = timestamp - notesPlayed.Find(note => note[0] == noteID)[1];

            }
            foreach (int key in keysPressed)
            {
                pianoControl2.PressPianoKey(key);
            }


            toolStripStatusLabel1.Text = $"{noteID}, {e.Message.Data2}";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            midiIn.StopRecording();
            notesPlayed.Clear();
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            midiIn.StopRecording();
            string path;
            if (textBox1.Text != null)
            {
                path = @"C:\Users\rotem\Desktop\Programming\Sanford MIDI\Json output\" + textBox1.Text + ".json";
            }
            else
            {
                path = @"C:\Users\rotem\Desktop\Programming\Sanford MIDI\Json output\output.json";
            }
            ArduinoSong song = new ArduinoSong(notesPlayed);

            string jsonString = JsonConvert.SerializeObject(song);

            using (FileStream fs = File.Create(path))
            {
                AddText(fs, jsonString);
            }
            notesPlayed.Clear();
            midiIn.StartRecording();
        }


    }

    class ArduinoSong
    {
        public List<int> notes = new List<int>();
        public List<int> lengths = new List<int>();

        public ArduinoSong(List<int[]> playedNotes)
        {
            List<int> notes = new List<int>();
            List<int> lengths = new List<int>();
            foreach (int[] notePlayed in playedNotes)
            {
                notes.Add(notePlayed[0]);
                lengths.Add(notePlayed[1]);
            }
            this.notes = notes;
            this.lengths = lengths;
        }
    }
}
