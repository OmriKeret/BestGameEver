package ex3.render.raytrace;

import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileReader;
import java.util.Map;

import ex3.parser.Element;
import ex3.parser.SceneDescriptor;
import ex3.render.IRenderer;
import math.Ray;
import math.Vec;

public class RayTracer implements IRenderer {
    SceneDescriptor _sceneDec;
    Scene _scene;
    Camera _camera;
    Light _ambient;
    int _width,_height;
    File _path;
    BufferedImage _canvas;

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
        _scene = new Scene();
        _height = height;
        _width = width;
        Map<String,String> attributes;
        _canvas = new BufferedImage(width,height,BufferedImage.TYPE_INT_RGB);
        try {
            attributes = sceneDesc.getSceneAttributes();
            _scene.init(attributes);
            for (Element e : sceneDesc.getObjects()) {
                _scene.addObjectByName(e.getName(), e.getAttributes());
            }
        }
        catch (Exception e){
            e.printStackTrace();
        }

		_scene.setCameraAttributes(sceneDesc.getCameraAttributes());




	}

	/**
	 * Renders the given line to the given canvas. Canvas is vcghvof the exact size
	 * given to init. This method must be called only after init.
	 * 
	 * @param canvas
	 *            BufferedImage containing the partial image
	 * @param line
	 *            The line of the image that should be rendered.
	 */
	@Override
	public void renderLine(BufferedImage canvas, int line) {
        try {
            if (line>_height)
                throw new Exception("Line is out of bound\n" +
                        "Asked for line "+line+" but there are only "+_height+" lines in the canvas");
            for (int pixelNumber = 0; pixelNumber<_width;pixelNumber++){
                Ray ray = _camera.constructRayThroughPixel(pixelNumber,line,_width,_height);
                _scene.findIntersection(ray);
                Vec colorVector = _scene.calcColor(ray, 1); //TODO: change the to _intensity
                Color color = new Color((int)colorVector.x,(int)colorVector.y,(int)colorVector.z);
                canvas.setRGB(pixelNumber,line,color.getRGB());
            }
        }
        catch (Exception e){
            e.printStackTrace();
        }

	}

<<<<<<< HEAD
    private void setBgColor(BufferedImage i_canvas, Color i_color){
        for(int i=0;i<i_canvas.getHeight();i++){
            for (int j=0;j<_canvas.getWidth();j++){
                i_canvas.setRGB(j,i,i_color.getRGB());
            }
        }
    }

=======
>>>>>>> 520615bdd9454844f26e3668089e580b79860253
}
