using UnityEngine;
using System.Collections;

namespace LayerManagement.Action.Finite {

	[System.Serializable]
	public class ChangeVolumeOverTimeInfo {
		public float time = 0.0f;
		public float to;
		[Range(0.0f, 1.0f)]
		public float delay = 0.0f;

		public void update(ChangeVolumeOverTimeInfo v) {
			this.time = v.time;
			this.to = v.to;
			this.delay = v.delay;
		}
		
		public ChangeVolumeOverTimeInfo () {
			
		}
		
		public ChangeVolumeOverTimeInfo (float time, float to, float delay) {
			this.time = time;
			this.to = to;
			this.delay = delay;
		}
	}

	[RequireComponent(typeof(AudioSource))]
	public class ChangeVolumeOverTime : AFiniteAction<ChangeVolumeOverTimeInfo> {

		public ChangeVolumeOverTimeInfo actionInfo;

		private AudioSource _asrc;
		protected AudioSource getAudioSource () {
			_asrc = _asrc ? _asrc : this.GetComponent<AudioSource>();
			return _asrc;
		}

		private float from;
		private float elapsedTime;

		override protected ChangeVolumeOverTimeInfo getT () {
			return this.actionInfo;
		}
		
		override protected void setT (ChangeVolumeOverTimeInfo v) {
			this.actionInfo = v;
		}
		
		override public void runActionWith (ChangeVolumeOverTimeInfo v) {
			this.actionInfo.update(v);
			isRunning = true;

			from = this.getAudioSource().volume;
			elapsedTime = 0.0f;
		}
		
		override protected void incrementAction (float deltaTime) {
			elapsedTime += deltaTime * this.actionSpeed;
			float t = AFiniteAction<ChangeVolumeOverTimeInfo>.delayTime(this.actionInfo.delay, this.elapsedTime, this.actionInfo.time);
			getAudioSource().volume = Mathf.Lerp(this.from, this.actionInfo.to, t);

			if (t >= 1.0f) {
				this.actionCompleted();
			}
		}
		
		override protected void resetAction () {

		}
	}

}
