using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class Hungry : ActionTask<Transform> {

        public BBParameter<GameObject> EmotionPopUp;
        public Vector3 offset = new Vector3(0, 1.5f, 0);
        private GameObject spawnedCanvas;

        public Transform seed;
        public float eatDistance;
        private NavMeshAgent hamsterAgent;
        public float eatRange;

        protected override string OnInit() {
			return null;
		}

        protected override void OnExecute()
        { //put here to execute everytime the state is ran

            spawnedCanvas = GameObject.Instantiate(EmotionPopUp.value);
            spawnedCanvas.transform.SetParent(agent, worldPositionStays: true);
            spawnedCanvas.transform.localPosition = offset;
            spawnedCanvas.SetActive(true);

            hamsterAgent = GetComponent<NavMeshAgent>();
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate() 
        {
            if (spawnedCanvas != null)
                spawnedCanvas.transform.position = agent.transform.position + offset;

            eatDistance = Vector3.Distance(hamsterAgent.position, seed.position);
            if(eatDistance < eatRange)
            {
                hamsterAgent.isStopped = true;
                Debug.Log("Seed has been eaten!");
            }

            else
            {
                hamsterAgent.isStopped = false;
                hamsterAgent.SetDestination(seed.position);
                Debug.Log("Hamster is hungry, let's look for some seed!");

            }
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