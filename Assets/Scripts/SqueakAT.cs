using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

    public class SqueakAT : ActionTask {

        public AudioClip clip; 
        private AudioSource source;

        protected override void OnExecute()
        {
            if (clip == null)
            {
                Debug.LogWarning("No AudioClip assigned!");
                EndAction();
                return;
            }

            source = agent.GetComponent<AudioSource>();
            if (source == null)
                source = agent.gameObject.AddComponent<AudioSource>();

            source.clip = clip;
            source.Play();

        }

        protected override string OnInit() {
            return null;
        }

        protected override void OnStop()
        {
            if (source != null && source.isPlaying)
            {
                source.Stop();
            }
        }
    }
}