package ex3.render.raytrace;

import java.awt.Color;
import java.awt.image.BufferedImage;
import java.io.File;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import Shapes.Ishape;
import Shapes.Sphere;
import ex3.parser.StringUtils;
import math.Intersection;
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
	protected List<Ishape> shapes;
	protected List<Light> lights;
	protected Camera camera;
	
	//Scene params
	private Color _backgroundCol = new Color(0,0,0);
	private File _backgroundTex = null;
	private int _maxRecursionLevel = 10;
	private Vec _ambientLight = new Vec(0,0,0);
	
	//bonus params
	private int superSampWidth = 1;
	

	public Scene() {

		shapes = new LinkedList<Ishape>();
		lights = new LinkedList<Light>();
		camera = new Camera();
	}

	public void init(Map<String, String> attributes) {
	
		//store xml scene properties in members
        try {
            _backgroundCol = StringUtils.string2Color(attributes.get("background-col"));
            _backgroundTex = StringUtils.string2File(attributes.get("background-tex"));
            _maxRecursionLevel = (int) StringUtils.string2Number(attributes.get("max-recursion-level"));
            _ambientLight = StringUtils.String2Vector(attributes.get("ambient-light"));
        }
        catch (Exception e){
            e.printStackTrace();
        }

		
         //TODO: store xml bonus properties
	}

    public void colorBackground(BufferedImage canvas){
        for (int x=0;x<canvas.getWidth();x++){
            for (int y=0;y<canvas.getHeight();y++){
                canvas.setRGB(x,y,_backgroundCol.getRGB());
            }
        }

    }

	/**
	 * Send ray return the nearest intersection. Return null if no intersection
	 * 
	 * @param ray
	 * @return
	 */
	public Intersection findIntersection(Ray ray) {
		//TODO find ray intersection with scene, change the output type, add whatever you need
		Point3D t;
		Point3D min_t = null;
		Ishape min_primitive = null;
		for(Ishape primitive : shapes) {
			t = primitive.intersectWithRay(ray);
			if (t.distance(ray.p) < min_t.distance(ray.p)) {
				min_primitive = primitive;
				min_t = t;
			}
		}
		return new Intersection(min_t, min_primitive);
	}

		
	

	public Color calcColor(Ray ray, int level) {
        Intersection intersection = findIntersection(ray);
        if (intersection.getPoint()==null)
            return _backgroundCol;
        //TODO:get the attributes of the intersection.Ishape (and only them)
        Vec I = new Vec();
        //Ie
        I.add(intersection.getShape().get_emission());
        //KaIa
        Vec KaIa = _ambientLight;
        KaIa.scale(intersection.getShape().get_ambient());
        I.add(KaIa);
        //(Kd(NL)Il, Il is the intensity of the light
        Vec KdNLIl = intersection.getShape().get_diffuse();
        //TODO: find the normal N of the intersection, scale KdNLIl with N*L


        return null;
    }

	/**
	 * Add objects to the scene by name
	 * 
	 * @param name Object's name
	 * @param attributes Object's attributes
	 * @throws Exception 
	 */
	public void addObjectByName(String name, Map<String, String> attributes) throws Exception {
		//TODO this adds all objects to scene except the camera
		//here is some code example for adding a surface or a light. 
		//you can change everything and if you don't want this method, delete it
		
		Ishape surface = null;
		Light light = null;
	
		if ("sphere".equals(name))
			surface = new Sphere(attributes);
		
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
		if (light != null) {
			light.init(attributes);
			lights.add(light);
		}

	}

	public Camera setCameraAttributes(Map<String, String> attributes) {
		this.camera.init(attributes);
        return camera;
	}
}
