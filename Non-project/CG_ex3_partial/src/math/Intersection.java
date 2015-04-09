package math;

import Shapes.Ishape;

public class Intersection {
	Point3D point;
	Ishape shape;
	
	public Intersection(Point3D min_t ,Ishape min_primitive ) {
		point = min_t;
		shape = min_primitive;
	}
}
