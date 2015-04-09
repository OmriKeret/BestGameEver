package ex3.render.raytrace;

import java.awt.Color;
import java.io.File;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Scanner;

import ex3.parser.StringUtils;
import math.Point3D;
import math.Ray;
import math.Vec;
/**
 * A Scene class containing all the scene objects including camera, lights and
 * surfaces. Some suggestions for code are in comment
 * If you uncomment these lines you'll need to implement some new types like Surface
 * 
 * You can change all methods here this is only a suggestion! This is your world, 
 * add members methods as you wish
 */
public class Scene implements IInitable {

//TODO add members

	protected List<Light> lights;
	protected Camera camera;
	
	//Scene params
	private Color backgroundCol = new Color(0,0,0);
	private File backgroundTex = null;
	private int maxRecursionLevel = 10;
	private Color ambientLight = new Color(0,0,0);
	
	//bonus params
	private int superSampWidth = 1;
	

	public Scene() {


		lights = new LinkedList<Light>();
		camera = new Camera();
	}

	public void init(Map<String, String> attributes) {
	
		//store xml scene properties in members
        try {
            backgroundCol = StringUtils.string2Color(attributes.get("background-col"));
            backgroundTex = StringUtils.string2File(attributes.get("background-tex"));
            maxRecursionLevel = StringUtils.string2Number(attributes.get("max-recursion-level"));
            ambientLight = StringUtils.string2Color(attributes.get("ambient-light"));
        }
        catch (Exception e){
            e.printStackTrace();
        }
		
         //TODO: store xml bonus properties
	}

	/**
	 * Send ray return the nearest intersection. Return null if no intersection
	 * 
	 * @param ray
	 * @return
	 */
	public void findIntersection(Ray ray) {
		//TODO find ray intersection with scene, change the output type, add whatever you need
		
		
		
	}

	public Vec calcColor(Ray ray, int level) {
		//TODO implement ray tracing recursion here, add whatever you need
		return null;
	}

	/**
	 * Add objects to the scene by name
	 * 
	 * @param name Object's name
	 * @param attributes Object's attributes
	 */
	public void addObjectByName(String name, Map<String, String> attributes) {
		//TODO this adds all objects to scene except the camera
		//here is some code example for adding a surface or a light. 
		//you can change everything and if you don't want this method, delete it
		
//		Surface surface = null;
//		Light light = null;
//	
//		if ("sphere".equals(name))
//			surface = new Sphere();
//		
//		
//		if ("omni-light".equals(name))
//			light = new OmniLight();
//
//		//adds a surface to the list of surfaces
//		if (surface != null) {
//			surface.init(attributes);
//			surfaces.add(surface);
//		}
//		
		//adds a light to the list of lights
//		if (light != null) {
//			light.init(attributes);
//			lights.add(light);
//		}

	}

	public void setCameraAttributes(Map<String, String> attributes) {
		//TODO uncomment after implementing camera interface if you like
		//this.camera.init(attributes);
	}
}
