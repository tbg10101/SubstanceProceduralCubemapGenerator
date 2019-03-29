using UnityEngine;
using Substance.Game;
public class CubemapGenerator : MonoBehaviour {
	//this is the input material
	public Substance.Game.SubstanceGraph inputMaterial;

	//list of cubemap render sizes
	public enum Sizes { _32=32, _64=64, _128=128, _256=256, _512=512, _1024=1024, _2048=2048, _4096=4096, _8192=8192 };

	//default to 256
	public Sizes size = Sizes._256;

	//leave a reference to the last cubemap gerneated by this instance right here, in case anyone wants it
	public Cubemap lastGeneratedCubemap;

	//a quick reference to the mesh renderer so we never have to go find it
	public MeshRenderer thisMeshRenderer = null;

	//a quick reference to the camera so we never have to go find it
	public Camera thisCamera = null;

	/*
		This will generate a new cubemap based on the inputMaterial and size size properties.
	*/

	public Cubemap generate () {
		//initialize the cubemap object
		Cubemap ret = new Cubemap((int)size, TextureFormat.RGB24, false);

		//set the MeshRenderer's material to the inputMaterial
		thisMeshRenderer.material = inputMaterial.material;

		//tell the camera to render the cubemap (this will work, even when the camera component is disabled)
		thisCamera.RenderToCubemap(ret);

		//store a reference to the new cubemap
		lastGeneratedCubemap = ret;

		//return the cubemap
		return ret;
	}
}
