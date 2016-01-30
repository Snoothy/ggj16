using UnityEngine;
using System.Collections;

namespace LayerManagement.Action.Finite {

	[System.Serializable]
	public class MoveToVectorBySpeedInfo {
		public float speed = 0.0f;
		public Vector3 position;
		public bool useLocalSpace = false;

		public void update(MoveToVectorBySpeedInfo v) {
			this.speed = v.speed;
			this.position = v.position;
			this.useLocalSpace = v.useLocalSpace;
		}
		
		public MoveToVectorBySpeedInfo () {
			
		}
		
		public MoveToVectorBySpeedInfo (float speed, Vector3 position, bool useLocalSpace) {
			this.speed = speed;
			this.position = position;
			this.useLocalSpace = useLocalSpace;
		}
	}

	public class MoveToVectorBySpeed : AFiniteAction<MoveToVectorBySpeedInfo> {

		public MoveToVectorBySpeedInfo actionInfo;
		
		override protected MoveToVectorBySpeedInfo getT () {
			return this.actionInfo;
		}
		
		override protected void setT (MoveToVectorBySpeedInfo v) {
			this.actionInfo = v;
		}
		
		override public void runActionWith (MoveToVectorBySpeedInfo v) {
			this.actionInfo.update(v);
			isRunning = true;
		}
		
		override protected void incrementAction (float deltaTime) {
			Vector3 upd = Vector3.MoveTowards((this.actionInfo.useLocalSpace ? this.transform.localPosition : this.transform.position),
			                                  this.actionInfo.position,
			                                  deltaTime * this.actionSpeed * this.actionInfo.speed);
			if (this.actionInfo.useLocalSpace) {
				this.transform.localPosition = upd;
			} else {
				this.transform.position = upd;
			}

			if ((this.actionInfo.useLocalSpace ? this.transform.localPosition : this.transform.position).LayerManagementisEqualTo(this.actionInfo.position)) {
				this.actionCompleted();
			}
		}
		
		override protected void resetAction () {

		}
	}

}
