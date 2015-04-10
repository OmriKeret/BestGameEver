package ex3.render.raytrace;

import java.awt.*;
import java.util.Map;

import ex3.parser.StringUtils;
import math.Point3D;
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
		_color = new Vec(0,0,0);
	}

	@Override
	public void init(Map<String, String> attributes) {
		if (attributes.containsKey("_color")){
			//TODO to uncomment this line you should inplement constructor 
			//with a string argument for Vec. You have an example in Point3D class

            try {
                _color = StringUtils.String2Vector(attributes.get("color"));
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
	}

    abstract public double get_intensity(Point3D i_point);

    abstract public Vec getDirection(Point3D i_location);

    public Vec get_color(){
        return _color;
    }

	
}
