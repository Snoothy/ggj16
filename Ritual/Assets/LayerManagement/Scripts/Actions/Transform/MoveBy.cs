using UnityEngine;
using System.Collections;

namespace LayerManagement.Action.Infinite {

	[System.Serializable]
	public class MoveByInfo {
		public float speed = 0.0f;
		public Vector3 direction;
		public bool useLocalSpace = false;

		public void update(MoveByInfo v) {
			this.speed = v.speed;
			this.direction = v.direction;
			this.useLocalSpace = v.useLocalSpace;
		}
		
		public MoveByInfo () {
			
		}
		
		public MoveByInfo (float speed, Vector3 direction, bool useLocalSpace) {
			this.speed = speed;
			this.direction = direction;
			this.useLocalSpace = useLocalSpace;
		}
	}

	public class MoveBy : AInfiniteAction<MoveByInfo> {

		public MoveByInfo actionInfo;

		override protected MoveByInfo getT () {
			return this.actionInfo;
		}
		
		override protected void setT (MoveByInfo v) {
			this.actionInfo = v;
		}
		
		override public void runActionWith (MoveByInfo v) {
			this.actionInfo.update(v);
			isRunning = true;
		}
		
		override protected void incrementAction (float deltaTime) {
			Vector3 upd = actionInfo.direction * (deltaTime * this.actionSpeed * this.actionInfo.speed);
			if (this.actionInfo.useLocalSpace) {
				this.transform.localPosition += upd;
			} else {
				this.transform.position += upd;
			}
		}
		
		override protected void resetAction () {

		}
	}

}