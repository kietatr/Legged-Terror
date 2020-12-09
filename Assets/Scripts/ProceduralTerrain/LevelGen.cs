using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour {

	[SerializeField]
	private int mWidth, mDepth;

	[SerializeField]
	private GameObject tilePrefab;

	// public void GenerateLevel()
	void Start() 
	{
		if (tilePrefab.name.Contains("(Clone)")) return;
		Vector3 size = tilePrefab.GetComponent<MeshRenderer> ().bounds.size;
		int tWidth = (int)size.x;
		int tDepth = (int)size.z;

		for (int x = 0; x < mWidth; x++) {
			for (int z = 0; z < mDepth; z++) {
				Vector3 tilePosition = new Vector3(this.gameObject.transform.position.x + x * tWidth, this.gameObject.transform.position.y, this.gameObject.transform.position.z + z * tDepth);
				GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform.parent);
				tile.GetComponent<TileGen>().GenerateTile();
			}
		}
	}
}
