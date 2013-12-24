#pragma strict
var camDist : float = 0.5f;
var camSpeed : float = 2f;
var fonarHeight : float = 0.5f;
var fonarDist : float = 0.5f;
var cam : Transform;
var fonar : Transform;
private var plrPos : Vector3;
private var camHeight : float;

function Start () {
        cam.parent = null;  fonar.parent = null;
}
function Update () {
    if (transform.position.y > 1f)  camHeight = 0.25f;  else  camHeight = 0.75f;
	    plrPos = new Vector3(transform.position.x, transform.position.y + camHeight, -camDist); 
	    cam.forward = Vector3.Lerp(cam.forward, transform.position - cam.position, Time.deltaTime * camSpeed);
	    cam.position = Vector3.Lerp(cam.position, plrPos, Time.deltaTime * camSpeed);
		fonar.position = new Vector3(transform.position.x, transform.position.y + fonarHeight, -fonarDist);
}