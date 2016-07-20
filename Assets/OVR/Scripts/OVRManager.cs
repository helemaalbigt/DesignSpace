/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.3 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculus.com/licenses/LICENSE-3.3

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

#if !UNITY_5 || UNITY_5_0
#error Oculus Utilities require Unity 5.1 or higher.
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using VR = UnityEngine.VR;

/// <summary>
/// Configuration data for Oculus virtual reality.
/// </summary>
public class OVRManager : MonoBehaviour
{
	public enum TrackingOrigin
	{
		EyeLevel   = OVRPlugin.TrackingOrigin.EyeLevel,
		FloorLevel = OVRPlugin.TrackingOrigin.FloorLevel,
	}

	/// <summary>
	/// Gets the singleton instance.
	/// </summary>
	public static OVRManager instance { get; private set; }
		
	/// <summary>
	/// Gets a reference to the active display.
	/// </summary>
	public static OVRDisplay display { get; private set; }

	/// <summary>
	/// Gets a reference to the active sensor.
	/// </summary>
	public static OVRTracker tracker { get; private set; }

	private static bool _profileIsCached = false;
	private static OVRProfile _profile;
	/// <summary>
	/// Gets the current profile, which contains information about the user's settings and body dimensions.
	/// </summary>
	public static OVRProfile profile
	{
		get {
			if (!_profileIsCached)
			{
				_profile = new OVRProfile();
				_profile.TriggerLoad();
				
				while (_profile.state == OVRProfile.State.LOADING)
					System.Threading.Thread.Sleep(1);
				
				if (_profile.state != OVRProfile.State.READY)
					Debug.LogWarning("Failed to load profile.");
				
				_profileIsCached = true;
			}

			return _profile;
		}
	}

	private bool _isPaused;
	private IEnumerable<Camera> disabledCameras;
	float prevTimeScale;
	private bool paused
	{
		get { return _isPaused; }
		set {
			if (value == _isPaused)
				return;

			// Sample code to handle VR Focus

//			if (value)
//			{
//				prevTimeScale = Time.timeScale;
//				Time.timeScale = 0.01f;
//				disabledCameras = GameObject.FindObjectsOfType<Camera>().Where(c => c.isActiveAndEnabled);
//				foreach (var cam in disabledCameras)
//					cam.enabled = false;
//			}
//			else
//			{
//				Time.timeScale = prevTimeScale;
//				if (disabledCameras != null) {
//					foreach (var cam in disabledCameras)
//						cam.enabled = true;
//				}
//				disabledCameras = null;
//			}

			_isPaused = value;
		}
	}

	/// <summary>
	/// Occurs when an HMD attached.
	/// </summary>
	public static event Action HMDAcquired;

	/// <summary>
	/// Occurs when an HMD detached.
	/// </summary>
	public static event Action HMDLost;

	/// <summary>
	/// Occurs when VR Focus is acquired.
	/// </summary>
	public static event Action VrFocusAcquired;

	/// <summary>
	/// Occurs when VR Focus is lost.
	/// </summary>
	public static event Action VrFocusLost;

	/// <summary>
	/// Occurs when the active Audio Out device has changed and a restart is needed.
	/// </summary>
	public static event Action AudioOutChanged;

	/// <summary>
	/// Occurs when the active Audio In device has changed and a restart is needed.
	/// </summary>
	public static event Action AudioInChanged;

	/// <summary>
	/// Occurs when the sensor gained tracking.
	/// </summary>
	public static event Action TrackingAcquired;

	/// <summary>
	/// Occurs when the sensor lost tracking.
	/// </summary>
	public static event Action TrackingLost;
	
	/// <summary>
	/// Occurs when HSW dismissed.
	/// </summary>
	public static event Action HSWDismissed;
	
	private static bool _isHmdPresentCached = false;
	private static bool _isHmdPresent = false;
	private static bool _wasHmdPresent = false;
	/// <summary>
	/// If true, a head-mounted display is connected and present.
	/// </summary>
	public static bool isHmdPresent
	{
		get {
			if (!_isHmdPresentCached)
			{
				_isHmdPresentCached = true;
				_isHmdPresent = OVRPlugin.hmdPresent;
			}

			return _isHmdPresent;
		}

		private set {
			_isHmdPresentCached = true;
			_isHmdPresent = value;
		}
	}

	private static bool _hasVrFocusCached = false;
	private static bool _hasVrFocus = false;
	private static bool _hadVrFocus = false;
	/// <summary>
	/// If true, the app has VR Focus.
	/// </summary>
	public static bool hasVrFocus
	{
		get {
			if (!_hasVrFocusCached)
			{
				_hasVrFocusCached = true;
				_hasVrFocus = OVRPlugin.hasVrFocus;
			}

			return _hasVrFocus;
		}

		private set {
			_hasVrFocusCached = true;
			_hasVrFocus = value;
		}
	}

	private static bool _isHSWDisplayedCached = false;
	private static bool _isHSWDisplayed = false;
	private static bool _wasHSWDisplayed;
	/// <summary>
	/// If true, then the Oculus health and safety warning (HSW) is currently visible.
	/// </summary>
	public static bool isHSWDisplayed
	{
		get {
			if (!isHmdPresent)
				return false;

			if (!_isHSWDisplayedCached)
			{
				_isHSWDisplayedCached = true;
				_isHSWDisplayed = OVRPlugin.hswVisible;
			}

			return _isHSWDisplayed;
		}

		private set {
			_isHSWDisplayedCached = true;
			_isHSWDisplayed = value;
		}
	}
	
	/// <summary>
	/// If the HSW has been visible for the necessary amount of time, this will make it disappear.
	/// </summary>
	public static void DismissHSWDisplay()
	{
		if (!isHmdPresent)
			return;

		OVRPlugin.DismissHSW();
	}

	/// <summary>
	/// If true, chromatic de-aberration will be applied, improving the image at the cost of texture bandwidth.
	/// </summary>
	public bool chromatic
	{
		get {
			if (!isHmdPresent)
				return false;

			return OVRPlugin.chromatic;
		}

		set {
			if (!isHmdPresent)
				return;

			OVRPlugin.chromatic = value;
		}
	}
	
	/// <summary>
	/// If true, both eyes will see the same image, rendered from the center eye pose, saving performance.
	/// </summary>
	public bool monoscopic
	{
		get {
			if (!isHmdPresent)
				return true;

			return OVRPlugin.monoscopic;
		}
		
		set {
			if (!isHmdPresent)
				return;

			OVRPlugin.monoscopic = value;
		}
	}

	/// <summary>
	/// If true, distortion rendering work is submitted a quarter-frame early to avoid pipeline stalls and increase CPU-GPU parallelism.
	/// </summary>
	public bool queueAhead = true;

	/// <summary>
	/// The number of expected display frames per rendered frame.
	/// </summary>
	public int vsyncCount
	{
		get {
			if (!isHmdPresent)
				return 1;

			return OVRPlugin.vsyncCount;
		}

		set {
			if (!isHmdPresent)
				return;

			OVRPlugin.vsyncCount = value;
		}
	}
	
	/// <summary>
	/// Gets the current battery level.
	/// </summary>
	/// <returns><c>battery level in the range [0.0,1.0]</c>
	/// <param name="batteryLevel">Battery level.</param>
	public static float batteryLevel
	{
		get {
			if (!isHmdPresent)
				return 1f;

			return OVRPlugin.batteryLevel;
		}
	}
	
	/// <summary>
	/// Gets the current battery temperature.
	/// </summary>
	/// <returns><c>battery temperature in Celsius</c>
	/// <param name="batteryTemperature">Battery temperature.</param>
	public static float batteryTemperature
	{
		get {
			if (!isHmdPresent)
				return 0f;

			return OVRPlugin.batteryTemperature;
		}
	}
	
	/// <summary>
	/// Gets the current battery status.
	/// </summary>
	/// <returns><c>battery status</c>
	/// <param name="batteryStatus">Battery status.</param>
	public static int batteryStatus
	{
		get {
			if (!isHmdPresent)
				return -1;

			return (int)OVRPlugin.batteryStatus;
		}
	}

	/// <summary>
	/// Gets the current volume level.
	/// </summary>
	/// <returns><c>volume level in the range [0,1].</c>
	public static float volumeLevel
	{
		get {
			if (!isHmdPresent)
				return 0f;

			return OVRPlugin.systemVolume;
		}
	}

	/// <summary>
	/// Gets or sets the current CPU performance level (0-2). Lower performance levels save more power.
	/// </summary>
	public static int cpuLevel
	{
		get {
			if (!isHmdPresent)
				return 2;

			return OVRPlugin.cpuLevel;
		}

		set {
			if (!isHmdPresent)
				return;

			OVRPlugin.cpuLevel = value;
		}
	}

	/// <summary>
	/// Gets or sets the current GPU performance level (0-2). Lower performance levels save more power.
	/// </summary>
	public static int gpuLevel
	{
		get {
			if (!isHmdPresent)
				return 2;

			return OVRPlugin.gpuLevel;
		}

		set {
			if (!isHmdPresent)
				return;

			OVRPlugin.gpuLevel = value;
		}
	}

	[SerializeField]
	private OVRManager.TrackingOrigin _trackingOriginType = OVRManager.TrackingOrigin.EyeLevel;
	/// <summary>
	/// Defines the current tracking origin type.
	/// </summary>
	public OVRManager.TrackingOrigin trackingOriginType
	{
		get {
			if (!isHmdPresent)
				return _trackingOriginType;

			return (OVRManager.TrackingOrigin)OVRPlugin.GetTrackingOriginType();
		}
		
		set {
			if (!isHmdPresent)
				return;

			if (OVRPlugin.SetTrackingOriginType((OVRPlugin.TrackingOrigin)value))
			{
				// Keep the field exposed in the Unity Editor synchronized with any changes.
				_trackingOriginType = value;
			}
		}
	}

	/// <summary>
	/// If true, head tracking will affect the orientation of each OVRCameraRig's cameras.
	/// </summary>
	public bool usePositionTracking = true;

	/// <summary>
	/// If true, each scene load will cause the head pose to reset.
	/// </summary>
	public bool resetTrackerOnLoad = false;

	/// <summary>
	/// True if the current platform supports virtual reality.
	/// </summary>
    public bool isSupportedPlatform { get; private set; }

	/// <summary>
	/// True if the user is currently wearing the display.
	/// </summary>
	public bool isUserPresent { get { return OVRPlugin.userPresent; } }

	private static bool prevAudioOutIdIsCached = false;
	private static bool prevAudioInIdIsCached = false;
	private static string prevAudioOutId = string.Empty;
	private static string prevAudioInId = string.Empty;
	private static bool wasPositionTracked = false;
	
	[SerializeField]
	[HideInInspector]
	internal static bool runInBackground = false;

	[NonSerialized]
	private static OVRVolumeControl volumeController = null;
	[NonSerialized]
	private Transform volumeControllerTransform = null;

#region Unity Messages

	private void Awake()
	{
		// Only allow one instance at runtime.
		if (instance != null)
		{
			enabled = false;
			DestroyImmediate(this);
			return;
		}

		instance = this;

		Debug.Log("Unity v" + Application.unityVersion + ", " +
		          "Oculus Utilities v" + OVRPlugin.wrapperVersion + ", " +
		          "OVRPlugin v" + OVRPlugin.version + ", " +
		          "SDK v" + OVRPlugin.nativeSDKVersion + ".");

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
		if (SystemInfo.graphicsDeviceType != UnityEngine.Rendering.GraphicsDeviceType.Direct3D11)
			Debug.LogWarning("VR rendering requires Direct3D11. Your graphics device: " + SystemInfo.graphicsDeviceType);
#endif

        // Detect whether this platform is a supported platform
        RuntimePlatform currPlatform = Application.platform;
        isSupportedPlatform |= currPlatform == RuntimePlatform.Android;
        //isSupportedPlatform |= currPlatform == RuntimePlatform.LinuxPlayer;
        isSupportedPlatform |= currPlatform == RuntimePlatform.OSXEditor;
        isSupportedPlatform |= currPlatform == RuntimePlatform.OSXPlayer;
        isSupportedPlatform |= currPlatform == RuntimePlatform.WindowsEditor;
        isSupportedPlatform |= currPlatform == RuntimePlatform.WindowsPlayer;
        if (!isSupportedPlatform)
        {
            Debug.LogWarning("This platform is unsupported");
            return;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
		// We want to set up our touchpad messaging system
		OVRTouchpad.Create();

        // Turn off chromatic aberration by default to save texture bandwidth.
        chromatic = false;
#endif

        InitVolumeController();

		if (display == null)
			display = new OVRDisplay();
		if (tracker == null)
			tracker = new OVRTracker();

		if (resetTrackerOnLoad)
			display.RecenterPose();
		
		// Disable the occlusion mesh by default until open issues with the preview window are resolved.
		OVRPlugin.occlusionMesh = false;

		OVRPlugin.ignoreVrFocus = runInBackground;
	}

	private void OnEnable()
	{
		if (volumeController != null)
		{
			volumeController.UpdatePosition(volumeControllerTransform);
		}
    }

	private void Update()
	{
#if !UNITY_EDITOR
		paused = !OVRPlugin.hasVrFocus;
#endif

		if (OVRPlugin.shouldQuit)
			Application.Quit();

		if (OVRPlugin.shouldRecenter)
			OVRManager.display.RecenterPose();

		if (trackingOriginType != _trackingOriginType)
			trackingOriginType = _trackingOriginType;

		tracker.isEnabled = usePositionTracking;

		// Dispatch HMD events.

		isHmdPresent = OVRPlugin.hmdPresent;

		if (isHmdPresent)
		{
			OVRPlugin.queueAheadFraction = (queueAhead) ? 0.25f : 0f;
		}

		if (_wasHmdPresent && !isHmdPresent)
		{
			try
			{
				if (HMDLost != null)
					HMDLost();
			}
			catch (Exception e)
			{
				Debug.LogError("Caught Exception: " + e);
			}
		}

        if (!_wasHmdPresent && isHmdPresent)
		{
			try
			{
				if (HMDAcquired != null)
					HMDAcquired();
			}
			catch (Exception e)
			{
				Debug.LogError("Caught Exception: " + e);
			}
		}

		_wasHmdPresent = isHmdPresent;

		// Dispatch VR Focus events.

		hasVrFocus = OVRPlugin.hasVrFocus;

		if (_hadVrFocus && !hasVrFocus)
		{
			try
			{
				if (VrFocusLost != null)
					VrFocusLost();
			}
			catch (Exception e)
			{
				Debug.LogError("Caught Exception: " + e);
			}
		}

        if (!_hadVrFocus && hasVrFocus)
		{
			try
			{
				if (VrFocusAcquired != null)
					VrFocusAcquired();
			}
			catch (Exception e)
			{
				Debug.LogError("Caught Exception: " + e);
			}
		}

		_hadVrFocus = hasVrFocus;

		// Dispatch Audio Device events.

		string audioOutId = OVRPlugin.audioOutId;
		if (!prevAudioOutIdIsCached)
		{
			prevAudioOutId = audioOutId;
			prevAudioOutIdIsCached = true;
		}
		else if (audioOutId != prevAudioOutId)
		{
			try
			{
				if (AudioOutChanged != null)
					AudioOutChanged();
			}
			catch (Exception e)
			{
				Debug.LogError("Caught Exception: " + e);
			}

			prevAudioOutId = audioOutId;
		}

		string audioInId = OVRPlugin.audioInId;
		if (!prevAudioInIdIsCached)
		{
			prevAudioInId = audioInId;
			prevAudioInIdIsCached = true;
		}
		else if (audioInId != prevAudioInId)
		{
			try
			{
				if (AudioInChanged != null)
					AudioInChanged();
			}
			catch (Exception e)
			{
				Debug.LogError("Caught Exception: " + e);
			}

			prevAudioInId = audioInId;
		}

		// Dispatch tracking events.

		if (wasPositionTracked && !tracker.isPositionTracked)
		{
			try
			{
				if (TrackingLost != null)
					TrackingLost();
			}
			catch (Exception e)
			{
				Debug.LogError("Caught Exception: " + e);
			}
		}

		if (!wasPositionTracked && tracker.isPositionTracked)
		{
			try
			{
				if (TrackingAcquired != null)
					TrackingAcquired();
			}
			catch (Exception e)
			{
				Debug.LogError("Caught Exception: " + e);
			}
		}

		wasPositionTracked = tracker.isPositionTracked;

		// Dispatch HSW events.

		isHSWDisplayed = OVRPlugin.hswVisible;

		if (isHSWDisplayed && Input.anyKeyDown)
			DismissHSWDisplay();

		if (!isHSWDisplayed && _wasHSWDisplayed)
		{
			try
			{
				if (HSWDismissed != null)
					HSWDismissed();
			}
			catch (Exception e)
			{
				Debug.LogError("Caught Exception: " + e);
			}
		}
		
		_wasHSWDisplayed = isHSWDisplayed;

		display.Update();
		OVRInput.Update();
		
		if (volumeController != null)
		{
			if (volumeControllerTransform == null)
			{
				if (gameObject.GetComponent<OVRCameraRig>() != null)
				{
					volumeControllerTransform = gameObject.GetComponent<OVRCameraRig>().centerEyeAnchor;
				}
			}
			volumeController.UpdatePosition(volumeControllerTransform);
		}
    }

	/// <summary>
	/// Creates a popup dialog that shows when volume changes.
	/// </summary>
	private static void InitVolumeController()
	{
		if (volumeController == null)
		{
			Debug.Log("Creating volume controller...");
			// Create the volume control popup
			GameObject go = GameObject.Instantiate(Resources.Load("OVRVolumeController")) as GameObject;
			if (go != null)
			{
				volumeController = go.GetComponent<OVRVolumeControl>();
			}
			else
			{
				Debug.LogError("Unable to instantiate volume controller");
			}
		}
	}

	/// <summary>
	/// Leaves the application/game and returns to the launcher/dashboard
	/// </summary>
	public void ReturnToLauncher()
	{
		// show the platform UI quit prompt
		OVRManager.PlatformUIConfirmQuit();
	}

#endregion

    public static void PlatformUIConfirmQuit()
	{
		if (!isHmdPresent)
			return;

		OVRPlugin.ShowUI(OVRPlugin.PlatformUI.ConfirmQuit);
    }

    public static void PlatformUIGlobalMenu()
	{
		if (!isHmdPresent)
			return;

		OVRPlugin.ShowUI(OVRPlugin.PlatformUI.GlobalMenu);
    }
}
