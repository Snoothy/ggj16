using UnityEngine;
using System.Collections;

namespace LayerManagement.Action.Finite {

	[System.Serializable]
	public class WaitActionInfo {
		public float time = 0.0f;

		public void update(WaitActionInfo v) {
			this.time = v.time;
		}

		public WaitActionInfo () {
			
		}
		
		public WaitActionInfo (float time) {
			this.time = time;
		}
	}

	public class WaitAction : AFiniteAction<WaitActionInfo> {

		public WaitActionInfo actionInfo;

		private float remainingWaitTime;

		override protected WaitActionInfo getT () {
			return this.actionInfo;
		}
		
		override protected void setT (WaitActionInfo v) {
			this.actionInfo = v;
		}
		
		override public void runActionWith (WaitActionInfo v) {
			this.actionInfo.update(v);
			isRunning = true;
			remainingWaitTime = actionInfo.time;
		}

		override protected void incrementAction (float deltaTime) {
			remainingWaitTime -= deltaTime * this.actionSpeed;
			if (remainingWaitTime < 0) {
				this.actionCompleted();
			}
		}

		override protected void resetAction () {
			remainingWaitTime = actionInfo.time;
		}

	}

}