using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
	public AnimationCurve intensityCurve = AnimationCurve.Linear(0, 1, 1, 0);
	public AnimationCurve sharpnessCurve = AnimationCurve.Linear(0, 1, 1, 0);

	// The generated ID of the last vibration played
	private string _lastVibration;

	private Camera _camera;

	// Start is called before the first frame update
    void Start()
    {
	    // Get main camera and set colors
	    _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	    _camera.backgroundColor = Color.gray;

	    // Set delegates
	    WiggleKit.Instance.onStartVibration += OnVibrationDidBegin;
	    WiggleKit.Instance.onStopVibration += OnVibrationDidFinish;
    }

    public void StartSimpleVibration()
    {
	    // Play the default vibration
	    WiggleKit.StartVibration();
    }

    public void StartSimpleCurveVibration()
    {
	    // Play the vibration with control points matching the control points of the animation curve
	    // Although the control points will be copied, the curve will not be imitated, as the in and
	    // 		out tangents of the control points are ignored. This is a cheaper calculation, but the
	    //		vibration will be less accurate.
	    WiggleKit.StartVibration(intensityCurve, sharpnessCurve, true);
    }

    public void StartInterpolatedCurveVibration()
    {
	    // Play the vibration with control points extracted from the animation curve
	    // The control points are not copied, but the curve will be more accurately imitated, as the
	    // 		control points used are defined by the actual value of the curve at each time.
	    WiggleKit.StartVibration(intensityCurve, sharpnessCurve, false, 0.05f);
    }

    // Event handler for the beginning of a vibration
    private void OnVibrationDidBegin(string id)
    {
	    // Store ID as last vibration played
	    _lastVibration = id;

	    // Set background color to cyan
	    _camera.backgroundColor = Color.cyan;
    }

    // Event handler for the end of a vibration
    private void OnVibrationDidFinish(string id)
    {
	    // If ID is the same as the ID of the last vibration, reset background color
	    if (id == _lastVibration)
	    {
		    _camera.backgroundColor = Color.gray;
	    }
    }
}
