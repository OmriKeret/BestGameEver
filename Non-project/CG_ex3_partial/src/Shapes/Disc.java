package Shapes;

import math.Point3D;
import math.Ray;
import math.Vec;

public class Disc implements Ishape {

	Vec normal;
	double radius;
	Point3D center;
	
	public Disc(Point3D center, double radius, Vec normal) {
		this.normal = normal;
		this.radius = radius; 
		this.center = center;
	}
	@Override
	public Point3D intersectWithRay(Ray ray) {
		return null;
		
	}
	

}
