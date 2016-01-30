using UnityEngine;
using System.Collections;

namespace LayerManagement.Action.Finite {

	[System.Serializable]
	public class MoveToTransformByTimeInfo {
		public float time = 0.0f;
		public Transform transform;
		public bool useLocalSpace = false;
		[Range(0.0f, 1.0f)]
		public float delay = 0.0f;

		public void update(MoveToTransformByTimeInfo v) {
			this.time = v.time;
			this.transform = v.transform;
			this.useLocalSpace = v.useLocalSpace;
			this.delay = v.delay;
		}
		
		public MoveToTransformByTimeInfo () {
			
		}
		
		public MoveToTransformByTimeInfo (float time, Transform transform, bool useLocalSpace, float delay) {
			this.time = time;
			this.transform = transform;
			this.useLocalSpace = useLocalSpace;
			this.delay = delay;
		}
	}

	public class MoveToTransformByTime : AFiniteAction<MoveToTransformByTimeInfo> {

		public MoveToTransformByTimeInfo actionInfo;

		private Vector3 from;
		private float elapsedTime;

		override protected MoveToTransformByTimeInfo getT () {
			return this.actionInfo;
		}
		
		override protected void setT (MoveToTransformByTimeInfo v) {
			this.actionInfo = v;
		}
		
		override public void runActionWith (MoveToTransformByTimeInfo v) {
			this.actionInfo.update(v);
			isRunning = true;

			from = this.actionInfo.useLocalSpace ? this.transform.localPosition : this.transform.position;
			elapsedTime = 0.0f;
		}
		
		override protected void incrementAction (float deltaTime) {
			elapsedTime += deltaTime * this.actionSpeed;
			float t = AFiniteAction<MoveToTransformByTimeInfo>.delayTime(this.actionInfo.delay, this.elapsedTime, this.actionInfo.time);
			Vector3 upd = Vector3.Lerp(this.from, (this.actionInfo.useLocalSpace ? this.actionInfo.transform.localPosition : this.actionInfo.transform.position), t);
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
