package Tests;

import static org.junit.Assert.*;
import math.*;

import org.junit.Test;

import Shapes.Sphere;
import Shapes.Triangle;

public class ShapesTest {

	@SuppressWarnings("deprecation")
	@Test
	public void AssertIntersectionSphere() {
		Ray ray = new Ray(new Point3D(0,0,0), new Vec(5,0,3));
		ray.v.normalize();
		Sphere s = new Sphere(new Point3D(5,0,1),2);
		Point3D  interPoint = s.intersectWithRay(ray);
		Vec intersactionPointInVec = new Vec(interPoint.x,interPoint.y,interPoint.z);
		double res =  (interPoint.x - s.center.x) * (interPoint.x - s.center.x) + (interPoint.y - s.center.y) * (interPoint.y - s.center.y)
				+ (interPoint.z - s.center.z) * (interPoint.z - s.center.z);
		assertEquals(0, (int)(res - 4));
	}
	
	@SuppressWarnings("deprecation")
	@Test
	public void AssertIntersectionTriangle() {
		Ray ray = new Ray(new Point3D(0,0,0), new Vec(0,0,1));
		ray.v.normalize();
		Triangle t = new Triangle(new Point3D(5,0,1), new Point3D(-1,5,1), new Point3D(-1,-5,2));
		Point3D  interPoint = t.intersectWithRay(ray);
		Vec intersactionPointInVec = new Vec(interPoint.x,interPoint.y,interPoint.z);
		assertEquals(0, (int)(0 - 4));
	}
	
}
