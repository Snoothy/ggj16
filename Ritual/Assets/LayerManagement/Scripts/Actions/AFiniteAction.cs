using UnityEngine;
using System.Collections;

namespace LayerManagement.Action.Finite {

	public abstract class AFiniteAction<T> : AAction<T> where T : new () {

		public delegate void ActionCompletedDelegate (AFiniteAction<T> action);
		public ActionCompletedDelegate delegates;

		public bool resetOnFinish = false;

		protected void actionCompleted () {
			isRunning = false;
			if (delegates != null) {
				delegates(this);
			}
			if (resetOnFinish) {
				this.resetAction();
			}
		}

		static protected float delayTime(float delay, float elapsedTime, float totalTime) {
			float r;
			return delayTime(delay, elapsedTime, totalTime, out r);
		}

		static protected float delayTime(float delay, float elapsedTime, float totalTime, out float r) {
			if (totalTime > 0.0f) {
				float t = elapsedTime / totalTime;
				float m = delay >= 1.0f ? 0.0f : (delay >= t ? 0.0f : ((elapsedTime - (totalTime*delay)) / (totalTime - (totalTime*delay))));
				r = m > 1.0f ? m - 1.0f : (m < 0.0f ? m : 0.0f );
				return Mathf.Clamp01(m);
			} else {
				r = elapsedTime;
				return 1.0f;
			}
		}

	}

}