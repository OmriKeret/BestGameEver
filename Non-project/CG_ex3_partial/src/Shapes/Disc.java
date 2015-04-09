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
		double t = (normal.dotProd(ray.p.GetVectorToPoint(center))) / (normal.dotProd(ray.v));
		Vec temp = new Vec(ray.v);
		if(t < 0 || normal.dotProd(ray.v) == 0) {
			return null;
		}
		temp.scale(t);
		Point3D intersactionPoint = ray.p.addVector(temp);
		temp = center.GetVectorToPoint(intersactionPoint);
		if(temp.dotProd(temp) > radius * radius) {
			return null;
		}
		return intersactionPoint;
		
	}
	

}
