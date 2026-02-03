using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class Hungry : ActionTask<Transform> {

        public BBParameter<GameObject> EmotionPopUp;
        public Vector3 offset = new Vector3(0, 1.5f, 0);
        private GameObject spawnedCanvas;

        protected override string OnInit() {
			return null;
		}

        protected override void OnExecute()
        { //put here to execute everytime the state is ran

            spawnedCanvas = GameObject.Instantiate(EmotionPopUp.value);
            spawnedCanvas.transform.SetParent(agent, worldPositionStays: true);
            spawnedCanvas.transform.localPosition = offset;
            spawnedCanvas.SetActive(true);
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate() 
        {
            if (spawnedCanvas != null)
                spawnedCanvas.transform.position = agent.transform.position + offset;
        }

		//Called when the task is disabled.
		protected override void OnStop() 
        {
            if (spawnedCanvas != null)
                GameObject.Destroy(spawnedCanvas);
        }

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}