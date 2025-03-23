using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MyUnity.Utilities
{
    // fix to acomodate for new Dialogue system
    public class DialogueSystem : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] TextMeshProUGUI _dialogueText;
        [SerializeField] TextMeshProUGUI _characterName;
        [SerializeField] AudioManager _audioManager;
        private Queue<Dialogue> _dialogue = new();

        public int getCount { get { return _dialogue.Count; } }
        public bool isSceneEnding { get { return _dialogue.Count == 0 ? true : false; } }
        public bool isComplete;


        void Awake() => StartCoroutine(LateStart());


        // gives the audio system enough time to transition between scenes
        IEnumerator LateStart()
        {
            yield return new WaitForSeconds(1f);
            _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        public void DisplayDialogue(DialogueScript dialogueScript)
        {
            _dialogue.Clear();
            foreach (Dialogue script in dialogueScript.dialogue)
                _dialogue.Enqueue(script);

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_dialogue.Count == 0) { return; }
            if (!isComplete) { return; }

            var script = _dialogue.Dequeue();

            _characterName.text = script.characterName;
            StartCoroutine(TypeWriter(script.sentences, script.audioSFX));
        }

        IEnumerator TypeWriter(string sentence, string soundName)
        {
            if (soundName != string.Empty) _audioManager.Play(soundName);

            for (int i = 0; i < sentence.Length + 1; i++)
                _dialogueText.text = sentence.Substring(0, i);

            yield return null;
            isComplete = false;
        }
    }
}