using UnityEngine;
using System.Collections;

namespace LayerManagement.Action.Finite {

	[System.Serializable]
	public class MoveToTransformBySpeedInfo {
		public float speed = 0.0f;
		public Transform transform;
		public bool useLocalSpace = false;

		public void update(MoveToTransformBySpeedInfo v) {
			this.speed = v.speed;
			this.transform = v.transform;
			this.useLocalSpace = v.useLocalSpace;
		}
		
		public MoveToTransformBySpeedInfo () {
			
		}
		
		public MoveToTransformBySpeedInfo (float speed, Transform transform, bool useLocalSpace) {
			this.speed = speed;
			this.transform = transform;
			this.useLocalSpace = useLocalSpace;
		}
	}

	public class MoveToTransformBySpeed : AFiniteAction<MoveToTransformBySpeedInfo> {

		public MoveToTransformBySpeedInfo actionInfo;
		
		override protected MoveToTransformBySpeedInfo getT () {
			return this.actionInfo;
		}
		
		override protected void setT (MoveToTransformBySpeedInfo v) {
			this.actionInfo = v;
		}
		
		override public void runActionWith (MoveToTransformBySpeedInfo v) {
			this.actionInfo.update(v);
			isRunning = true;
		}
		
		override protected void incrementAction (float deltaTime) {
			Vector3 actionPos = this.actionInfo.useLocalSpace ? this.actionInfo.transform.localPosition : this.actionInfo.transform.position;
			Vector3 upd = Vector3.MoveTowards((this.actionInfo.useLocalSpace ? this.transform.localPosition : this.transform.position),
			                                  actionPos,
			                                  deltaTime * this.actionSpeed * this.actionInfo.speed);
			if (this.actionInfo.useLocalSpace) {
				this.transform.localPosition = upd;
			} else {
				this.transform.position = upd;
			}

			if ((this.actionInfo.useLocalSpace ? this.transform.localPosition : this.transform.position).LayerManagementisEqualTo(actionPos)) {
				this.actionCompleted();
			}
		}
		
		override protected void resetAction () {

		}
	}

}
