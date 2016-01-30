using UnityEngine;
using System.Collections;

namespace LayerManagement.Action {

	public abstract class AAction<T> : MonoBehaviour where T : new () {

		public bool isRunning = false;
		public float actionSpeed = 1.0f;

		public bool runActionOnStart = false;
		public bool resetOnDisable = false;

		protected AAction () {
			this.setT(new T());
		}

		protected virtual float deltaTime () {
			return Time.deltaTime;
		}

		public void runAction () {
			this.runActionWith(this.getT());
		}

		public abstract void runActionWith (T v);
		protected abstract void incrementAction (float deltaTime);
		protected abstract void resetAction ();
		protected abstract T getT ();
		protected abstract void setT (T v);
		protected virtual void ActionStart () {}
		protected virtual void ActionEarlyUpdate () {}
		protected virtual void ActionLateUpdate () {}
		protected virtual void ActionOnDisable () {}

		void Start () {
			this.ActionStart();
			if (this.runActionOnStart) {
				this.runAction();
			}
		}

		// Update is called once per frame
		void Update () {
			this.ActionEarlyUpdate();
			if (isRunning) {
				this.incrementAction(this.deltaTime());
			}
			this.ActionLateUpdate();
		}

		void OnDisable () {
			if (isRunning && resetOnDisable) {
				this.resetAction();
			}
			this.ActionOnDisable();
		}
	}

}