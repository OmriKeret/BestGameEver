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
    Scene _scene;
    Camera _camera;
    int _width, _height;
    BufferedImage _canvas;

    /**
     * Inits the renderer with scene description and sets the target canvas to
     * size (width X height). After init renderLine may be called
     *
     * @param sceneDesc Description data structure of the scene
     * @param width     Width of the canvas
     * @param height    Height of the canvas
     * @param path      File path to the location of the scene. Should be used as a
     *                  basis to load external resources (e.g. background image)
     */
    @Override
    public void init(SceneDescriptor sceneDesc, int width, int height, File path) {
        _scene = new Scene();
        _height = height;
        _width = width;
        Map<String, String> attributes;
        _canvas = new BufferedImage(width, height, BufferedImage.TYPE_INT_RGB);
        try {
            attributes = sceneDesc.getSceneAttributes();
            _scene.init(attributes);
            for (Element e : sceneDesc.getObjects()) {
                _scene.addObjectByName(e.getName(), e.getAttributes());
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        _camera = _scene.setCameraAttributes(sceneDesc.getCameraAttributes());
        _camera.setResolution(_width,_height);


    }

    /**
     * Renders the given line to the given canvas. Canvas is vcghvof the exact size
     * given to init. This method must be called only after init.
     *
     * @param canvas BufferedImage containing the partial image
     * @param line   The line of the image that should be rendered.
     */
    @Override
    public void renderLine(BufferedImage canvas, int line) {
        try {
            if (line > _height)
                throw new Exception("Line is out of bound\n" +
                        "Asked for line " + line + " but there are only " + _height + " lines in the canvas");
            for (int pixelNumber = 0; pixelNumber < _width; pixelNumber++) {
                Ray ray = _camera.constructRayThroughPixel(pixelNumber, line, _width, _height);
                _scene.findIntersection(ray);
                Color color = _scene.calcColor(ray, 1); //TODO: change the to _intensity
                canvas.setRGB(pixelNumber, line, color.getRGB());
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

    }
}
