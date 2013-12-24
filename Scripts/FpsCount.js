var fpsCount : GUIText;
var updateInterval = 0.5;
private var lastInterval : double; // Last interval end time
private var frames = 0; // Frames over current interval
private var fps : float; // Current FPS

function Start() {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
}
function OnGUI () {
    if (fpsCount) 
		fpsCount.text = "FPS  " + fps.ToString("f0");
}
function Update() {
        ++frames;
        var timeNow = Time.realtimeSinceStartup;
    if( timeNow > lastInterval + updateInterval ) {
        fps = frames / (timeNow - lastInterval);
        frames = 0;
        lastInterval = timeNow;
    }
}