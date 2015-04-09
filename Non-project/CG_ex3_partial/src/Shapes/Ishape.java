package Shapes;

import math.Point3D;
import math.Ray;


public interface Ishape {
	
	/**
	 * check the nearest point of intersection. Return null if no intersection
	 * 
	 * @param ray
	 * @return
	 */
	public Point3D intersectWithRay(Ray ray);
	
	
}
