package ex3.render.raytrace;

import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileReader;

import ex3.parser.Element;
import ex3.parser.SceneDescriptor;
import ex3.render.IRenderer;

public class RayTracer implements IRenderer {

	/**
	 * Inits the renderer with scene description and sets the target canvas to
	 * size (width X height). After init renderLine may be called
	 * 
	 * @param sceneDesc
	 *            Description data structure of the scene
	 * @param width
	 *            Width of the canvas
	 * @param height
	 *            Height of the canvas
	 * @param path
	 *            File path to the location of the scene. Should be used as a
	 *            basis to load external resources (e.g. background image)
	 */
	@Override
	public void init(SceneDescriptor sceneDesc, int width, int height, File path) {
        try {

        }
        catch (Exception e){
            System.out.println(e);
        }
		
		//for (Element e : sceneDesc.getObjects()) {
		//	scene.addObjectByName(e.getName(), e.getAttributes());
		//}
		
		//scene.setCameraAttributes(sceneDesc.getCameraAttributes());

	}

	/**
	 * Renders the given line to the given canvas. Canvas is of the exact size
	 * given to init. This method must be called only after init.
	 * 
	 * @param canvas
	 *            BufferedImage containing the partial image
	 * @param line
	 *            The line of the image that should be rendered.
	 */
	@Override
	public void renderLine(BufferedImage canvas, int line) {
		// TODO Implement this
	}

}
