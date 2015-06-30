using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {

	public bool useSpring = false;

	public LineRenderer dragLine;

	Rigidbody2D grabbedObject = null;
	SpringJoint2D springJoint = null;

	float velocityRatio = 4f; 	
	void Update() {
		if( Input.GetMouseButtonDown(0) ) {

			Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);

			Vector2 dir = Vector2.zero;

			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, dir);
			if(hit.collider!=null) {

				if(hit.collider.GetComponent<Rigidbody2D>() != null) {
					grabbedObject = hit.collider.GetComponent<Rigidbody2D>();

					if(useSpring) {
						springJoint = grabbedObject.gameObject.AddComponent<SpringJoint2D>();

						Vector3 localHitPoint = grabbedObject.transform.InverseTransformPoint(hit.point);
						springJoint.anchor = localHitPoint;
						springJoint.connectedAnchor = mouseWorldPos3D;
						springJoint.distance = 0.25f;
						springJoint.dampingRatio = 1;
						springJoint.frequency = 5;


						springJoint.enableCollision = true;


						springJoint.connectedBody = null;
					}
					else {
						// We're using velocity instead
						grabbedObject.gravityScale=1;
					}

					dragLine.enabled = true;
				}
			}
		}

		if( Input.GetMouseButtonUp(0) && grabbedObject!=null ) {
			if(useSpring) {
				Destroy(springJoint);
				springJoint = null;
			}
			else {
				grabbedObject.gravityScale=1;
			}
			grabbedObject = null;
			dragLine.enabled = false;
		}

	}


	void FixedUpdate () {
		if(grabbedObject != null) {
			Vector2 mouseWorldPos2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if(useSpring) {
				springJoint.connectedAnchor = mouseWorldPos2D;
			}
			else {
				grabbedObject.velocity = (mouseWorldPos2D - grabbedObject.position) * velocityRatio;
			}
		}
	}

	void LateUpdate() {
		if(grabbedObject != null) {
			if(useSpring) {
				Vector3 worldAnchor = grabbedObject.transform.TransformPoint(springJoint.anchor);
				dragLine.SetPosition(0, new Vector3(worldAnchor.x, worldAnchor.y, -1));
				dragLine.SetPosition(1, new Vector3(springJoint.connectedAnchor.x, springJoint.connectedAnchor.y, -1));
			}
			else {
				Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				dragLine.SetPosition(0, new Vector3(grabbedObject.position.x, grabbedObject.position.y, -1));
				dragLine.SetPosition(1, new Vector3(mouseWorldPos3D.x, mouseWorldPos3D.y, -1));
			}
		}
	}

}
