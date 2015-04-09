package ex3.render.raytrace;

import java.util.Map;

import math.Vec;

/**
 * Represent a point light
 * 
 * Add methods as you wish, this can be a super class for other lights (think which)
 */
public abstract class Light implements IInitable {
	protected Vec _color;
    protected double _intensity;

	public Light() {
		_color = new Vec(1,1,1);
	}

	@Override
	public void init(Map<String, String> attributes) {
		if (attributes.containsKey("_color")){
			//TODO to uncomment this line you should inplement constructor 
			//with a string argument for Vec. You have an example in Point3D class
			
			//_color = new Vec(attributes.get("_color"));
		}
	}

    abstract public double get_intensity(double distance);

	
}
