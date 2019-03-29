using UnityEngine;
using Substance.Game;

public class CubemapGeneratorHelper : MonoBehaviour {
	//a reference to the singleton
	private static CubemapGeneratorHelper _instance = null;

	//the prefab used when generating cubemaps (you can also use a CubemapGenerator without the CubemapGeneratorHelper)
	public CubemapGenerator cubmapGeneratorPrefab;

	//store the instance so we don't need to re-create it upon future cubemap regenerations
	private static CubemapGenerator spawnedInstance = null;

	//store a reference to this singleton, destroy the previous one if it existed
	private void Start () {
		if (_instance) {
			Destroy(_instance);
		}

		_instance = this;
	}

	//creates an instance of a CubemapGenerator prefab if needed, sets the inputs, then tell the new generator to generate
	public Cubemap generateCubemap(SubstanceGraph mat, CubemapGenerator.Sizes size) {
		if (! spawnedInstance) {
			spawnedInstance = Instantiate(cubmapGeneratorPrefab);
		}

		spawnedInstance.size = size;
		spawnedInstance.inputMaterial = mat;

		return spawnedInstance.generate();
	}

	//getter for the singleton instance
	public static CubemapGeneratorHelper instance {
		get {
			return _instance;
		}
	}

	//getter for the singleton instance
	public static CubemapGeneratorHelper singleton {
		get {
			return _instance;
		}
	}
}
