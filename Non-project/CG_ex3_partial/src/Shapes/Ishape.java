package Shapes;

import ex3.parser.StringUtils;
import math.Point3D;
import math.Ray;
import math.Vec;

import java.awt.*;
import java.util.Map;


public abstract class Ishape {

    private Vec _diffuse;
    private Vec _specular;
    private Vec _emission;
    private Vec _ambient;

    protected void init(Map<String,String> attributes) throws Exception{
        _diffuse = StringUtils.String2Vector(attributes.get("mtl-diffuse"));
        _specular = StringUtils.String2Vector(attributes.get("mtl-specular"));
        _emission = StringUtils.String2Vector(attributes.get("mtl-emission"));
        _ambient = StringUtils.String2Vector(attributes.get("ambient-light"));
    }

	/**
	 * check the nearest point of intersection. Return null if no intersection
	 * 
	 * @param ray
	 * @return
	 */
	abstract public Point3D intersectWithRay(Ray ray);

    public Vec get_diffuse() {
        return _diffuse;
    }

    public Vec get_specular() {
        return _specular;
    }

    public Vec get_emission() {
        return _emission;
    }

    public Vec get_ambient() {
        return _ambient;
    }



	
	
}
