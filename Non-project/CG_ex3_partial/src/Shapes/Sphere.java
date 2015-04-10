package Shapes;

import java.util.Map;
import java.util.Vector;

import ex3.parser.StringUtils;
import math.Point3D;
import math.Ray;
import math.Vec;

public class Sphere extends Ishape{

	//sphere basic params
	public Point3D center;
	public double radius ;
	
	
	public Sphere(Point3D p,double r ){
		center = p;
		radius = r;
	}
	
	public Sphere() {
		center = new Point3D();
		radius = 0;
	}
	
	public Sphere(Map<String, String> attributes) throws Exception {
		init(attributes);
        center = StringUtils.String2Point(attributes.get("center"));
		radius = StringUtils.string2Number(attributes.get("radius"));
	}

	@Override
	public Point3D intersectWithRay(Ray ray) {
		
		//calculating Intersection  geometricly 
		Point3D p = new Point3D(center);
		Vec L = ray.p.GetVectorToPoint(p);
		double tm = L.dotProd(ray.v);
		
		double dSquered = L.lengthSquared() - (tm * tm);
		double radiusSquered = radius * radius;
		if(dSquered > radiusSquered)
			return null;
		
		double th = Math.sqrt( radiusSquered - dSquered );
		double t = Math.min(tm - th, tm + th);
		Point3D intersection = new Point3D(ray.p);
		
		//TODO: add light and capture all in a model
		return intersection.addVector(Vec.scale(t, ray.v));
	}


}
