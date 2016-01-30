using UnityEngine;
using System.Collections;

namespace LayerManagement.Action.Finite {

	[System.Serializable]
	public class MoveToVectorByTimeInfo {
		public float time = 0.0f;
		public Vector3 to;
		public bool useLocalSpace = false;
		[Range(0.0f, 1.0f)]
		public float delay = 0.0f;

		public void update(MoveToVectorByTimeInfo v) {
			this.time = v.time;
			this.to = v.to;
			this.useLocalSpace = v.useLocalSpace;
			this.delay = v.delay;
		}
		
		public MoveToVectorByTimeInfo () {
			
		}
		
		public MoveToVectorByTimeInfo (float time, Vector3 to, bool useLocalSpace, float delay) {
			this.time = time;
			this.to = to;
			this.useLocalSpace = useLocalSpace;
			this.delay = delay;
		}
	}

	public class MoveToVectorByTime : AFiniteAction<MoveToVectorByTimeInfo> {

		public MoveToVectorByTimeInfo actionInfo;

		private Vector3 from;
		private float elapsedTime;

		override protected MoveToVectorByTimeInfo getT () {
			return this.actionInfo;
		}
		
		override protected void setT (MoveToVectorByTimeInfo v) {
			this.actionInfo = v;
		}
		
		override public void runActionWith (MoveToVectorByTimeInfo v) {
			this.actionInfo.update(v);
			isRunning = true;

			from = this.actionInfo.useLocalSpace ? this.transform.localPosition : this.transform.position;
			elapsedTime = 0.0f;
		}
		
		override protected void incrementAction (float deltaTime) {
			elapsedTime += deltaTime * this.actionSpeed;
			float t = AFiniteAction<MoveToVectorByTimeInfo>.delayTime(this.actionInfo.delay, this.elapsedTime, this.actionInfo.time);
			Vector3 upd = Vector3.Lerp(this.from, this.actionInfo.to, t);
			if (this.actionInfo.useLocalSpace) {
				this.transform.localPosition = upd;
			} else {
				this.transform.position = upd;
			}

			if (t >= 1.0f) {
				this.actionCompleted();
			}
		}
		
		override protected void resetAction () {

		}
	}

}
