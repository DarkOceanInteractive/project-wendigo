using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
	public class ToggleInventory : MonoBehaviour {
		[SerializeField] GameObject inventoryGameObject;
		[SerializeField] bool showCursor = true;

		void Start() {
			inventoryGameObject.SetActive(false);
		}

		void Update() {
			Inventory();
		}

		public void Inventory() {
			if (Singletons.Main.Input.PlayerToggledInventory) {
				if (!inventoryGameObject.activeSelf) {
					inventoryGameObject.SetActive(true);
					ShowMouseCursor();
				} else {
					inventoryGameObject.SetActive(false);
					HideMouseCursor();
				}
			}
		}

		public void ShowMouseCursor() {
			if (showCursor)
				Singletons.Main.Input.ShowCursor();
		}

		public void HideMouseCursor() {
			if (showCursor)
				Singletons.Main.Input.HideCursor();
		}
	}
}