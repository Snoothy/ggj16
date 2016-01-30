using UnityEngine;
using System.Collections;

namespace LayerManagement.Action.Infinite {

	[System.Serializable]
	public class RotateByInfo {
		public float speed = 0.0f;
		public Vector3 rotation;

		public void update(RotateByInfo v) {
			this.speed = v.speed;
			this.rotation = v.rotation;
		}
		
		public RotateByInfo () {
			
		}
		
		public RotateByInfo (float speed, Vector3 rotation) {
			this.speed = speed;
			this.rotation = rotation;
		}
	}

	public class RotateBy : AInfiniteAction<RotateByInfo> {

		public RotateByInfo actionInfo;
		
		override protected RotateByInfo getT () {
			return this.actionInfo;
		}
		
		override protected void setT (RotateByInfo v) {
			this.actionInfo = v;
		}
		
		override public void runActionWith (RotateByInfo v) {
			this.actionInfo.update(v);
			isRunning = true;
		}
		
		override protected void incrementAction (float deltaTime) {
			this.transform.Rotate(new Vector3(this.actionInfo.rotation.x * deltaTime * this.actionSpeed * this.actionInfo.speed,
			                                  this.actionInfo.rotation.y * deltaTime * this.actionSpeed * this.actionInfo.speed,
			                                  this.actionInfo.rotation.z * deltaTime * this.actionSpeed * this.actionInfo.speed));
		}
		
		override protected void resetAction () {

		}
	}

}