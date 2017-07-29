using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput {
	public class Joystick: MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
		public enum AxisOption {
			// Options for which axes to use
			Both, // Use both
			OnlyHorizontal, // Only horizontal
			OnlyVertical // Only vertical
		}

		public int MovementRange = 100;
		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

		Vector3 startPosition;
		bool useX; // Toggle for using the x axis
		bool useY; // Toggle for using the Y axis
		CrossPlatformInputManager.VirtualAxis horizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis verticalVirtualAxis; // Reference to the joystick in the cross platform input

		void OnEnable() {
			CreateVirtualAxes();
		}

		void Start() {
			startPosition = transform.position;
		}

		void UpdateVirtualAxes(Vector3 value) {
			var delta = startPosition - value;
			delta.y = -delta.y;
			delta /= MovementRange;
			if (useX) {
				horizontalVirtualAxis.Update(-delta.x);
			}

			if (useY) {
				verticalVirtualAxis.Update(delta.y);
			}
		}

		void CreateVirtualAxes() {
			// set axes to use
			useX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
			useY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

			// create new axes based on axes to use
			if (useX) {
				horizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(horizontalVirtualAxis);
			}
			if (useY) {
				verticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(verticalVirtualAxis);
			}
		}


		public void OnDrag(PointerEventData data) {
			Vector3 newPos = Vector3.zero;

			if (useX) {
				int delta = (int)(data.position.x - startPosition.x);
				delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
				newPos.x = delta;
			}

			if (useY) {
				int delta = (int)(data.position.y - startPosition.y);
				delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
				newPos.y = delta;
			}
			transform.position = new Vector3(startPosition.x + newPos.x, startPosition.y + newPos.y, startPosition.z + newPos.z);
			UpdateVirtualAxes(transform.position);
		}


		public void OnPointerUp(PointerEventData data) {
			transform.position = startPosition;
			UpdateVirtualAxes(startPosition);
		}


		public void OnPointerDown(PointerEventData data) { }

		void OnDisable() {
			// remove the joysticks from the cross platform input
			if (useX) {
				horizontalVirtualAxis.Remove();
			}
			if (useY) {
				verticalVirtualAxis.Remove();
			}
		}
	}
}